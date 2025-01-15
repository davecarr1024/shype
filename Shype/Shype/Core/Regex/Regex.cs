
using System.Collections;

namespace Shype.Core.Regex;

public abstract record Regex : Errors.Errorable<Regex>
{
    public (State state, Result result) Apply(string input, Chars.Position? position = null)
        => Apply(new(input, position));

    public abstract (State state, Result result) Apply(State state);

    private abstract record UnaryRegex(Regex Child) : Regex
    {
        protected (State state, Result result) ApplyChild(State state) => Try(() => Child.Apply(state));
    }

    private record ZeroOrMoreRegex(Regex Child) : UnaryRegex(Child)
    {
        public override string ToString() => $"{Child}*";

        public override (State state, Result result) Apply(State state)
        {
            Result result = new();
            while (true)
            {
                try
                {
                    (state, Result child_result) = ApplyChild(state);
                    result += child_result;
                }
                catch (Errors.Error)
                {
                    return (state, result);
                }
            }
        }
    }

    public Regex ZeroOrMore() => new ZeroOrMoreRegex(this);

    private record ZeroOrOneRegex(Regex Child) : UnaryRegex(Child)
    {
        public override string ToString() => $"{Child}?";

        public override (State state, Result result) Apply(State state)
        {
            try
            {
                return ApplyChild(state);
            }
            catch (Errors.Error)
            {
                return (state, new());
            }
        }
    }

    public Regex ZeroOrOne() => new ZeroOrOneRegex(this);

    private record OneOrMoreRegex(Regex Child) : UnaryRegex(Child)
    {
        public override string ToString() => $"{Child}+";

        public override (State state, Result result) Apply(State state)
        {
            Result result = new();
            (state, Result child_result) = ApplyChild(state);
            result += child_result;
            while (true)
            {
                try
                {
                    (state, child_result) = ApplyChild(state);
                    result += child_result;
                }
                catch (Errors.Error)
                {
                    return (state, result);
                }
            }
        }
    }

    public Regex OneOrMore() => new OneOrMoreRegex(this);

    private abstract record HeadRegex : Regex
    {
        public abstract Chars.Char Apply(Chars.Char head);

        protected Chars.Char Head(State state) => Try(state.Head, "empty state");

        public override (State state, Result result) Apply(State state)
        {
            Chars.Char head = Head(state);
            return (state.Tail(), new(Try(() => Apply(head), $"error while processing head {head}")));
        }

        public override Regex Not() => new NotRegex(this);
    }

    private record LiteralRegex(char Value) : HeadRegex
    {
        public override string ToString() => Value.ToString();

        public override Chars.Char Apply(Chars.Char head)
        {
            if (head.Value != Value)
            {
                throw CreateError($"expected literal {Value}");
            }
            return head;
        }
    }

    public static Regex Literal(char value) => new LiteralRegex(value);

    private record AnyRegex : HeadRegex
    {
        public override string ToString() => ".";

        public override Chars.Char Apply(Chars.Char head) => head;
    }

    public static Regex Any() => new AnyRegex();

    private record RangeRegex(char Min, char Max) : HeadRegex
    {
        public override string ToString() => $"[{Min}-{Max}]";

        public override Chars.Char Apply(Chars.Char head)
        {
            if (head.Value < Min || head.Value > Max)
            {
                throw CreateError($"expected head in range {Min}-{Max} got {head.Value}");
            }
            return head;
        }
    }

    public static Regex Range(char min, char max) => new RangeRegex(min, max);

    private record ClassRegex(IImmutableSet<char> Values, string? Display) : HeadRegex
    {
        public override string ToString() => Display ?? $"({string.Concat(Values)})";

        public override Chars.Char Apply(Chars.Char head)
            => Values.Contains(head.Value) ? head : throw CreateError($"expected head in set {Values} got {head.Value}");
    }

    public static Regex Class(IImmutableSet<char> values, string? display = null) => new ClassRegex(values, display);

    public static Regex Whitespace() => Class(" \t\n".ToImmutableHashSet(), "\\w");

    public static Regex Digits() => Class("0123456789".ToImmutableHashSet(), "\\d");

    private record NotRegex(HeadRegex Child) : HeadRegex
    {
        public override string ToString() => $"^{Child}";

        public override (State state, Result result) Apply(State state)
        {
            Chars.Char head = Head(state), child_result;
            try
            {
                child_result = Child.Apply(head);
            }
            catch (Errors.Error)
            {
                return (state.Tail(), new(head));
            }
            throw CreateError($"child {Child} expected to not match but matched {child_result}");
        }

        public override Chars.Char Apply(Chars.Char head) => throw new NotImplementedException();
    }

    public virtual Regex Not() => throw new NotImplementedException($"non-head regex {this} doesn't support not");

    private abstract record NaryRegex(IImmutableList<Regex> Children) : Regex, IEnumerable<Regex>
    {
        public IEnumerator<Regex> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Children).GetEnumerator();

        protected (State state, Result result) ApplyChild(Regex child, State state)
        {
            if (!Children.Contains(child))
            {
                throw CreateError($"unknown child {child}");
            }
            return Try(() => child.Apply(state), $"error while applying to child {child}");
        }
    }

    private record AndRegex(IImmutableList<Regex> Children) : NaryRegex(Children)
    {
        public override string ToString() => $"({string.Concat(Children.Select(child => child.ToString()))})";

        public override (State state, Result result) Apply(State state)
        {
            Result result = new();
            foreach (Regex child in this)
            {
                (state, Result child_result) = ApplyChild(child, state);
                result += child_result;
            }
            return (state, result);
        }
    }

    public static Regex And(params Regex[] children) => new AndRegex(children.ToImmutableList());

    public static Regex operator &(Regex lhs, Regex rhs) => And(lhs, rhs);

    private record OrRegex(IImmutableList<Regex> Children) : NaryRegex(Children)
    {
        public override string ToString() => $"({string.Join("|", this.Select(child => child.ToString()))})";

        public override (State state, Result result) Apply(State state)
        {
            List<Errors.Error> errors = [];
            foreach (Regex child in this)
            {
                try
                {
                    return child.Apply(state);
                }
                catch (Errors.Error error)
                {
                    errors.Add(error);
                }
            }
            throw CreateError("", [.. errors]);
        }
    }

    public static Regex Or(params Regex[] children) => new OrRegex(children.ToImmutableList());

    public static Regex operator |(Regex lhs, Regex rhs) => Or(lhs, rhs);
}