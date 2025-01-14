
using System.Collections;

namespace Shype.Core.Regex;

public abstract record Regex : Errors.Errorable<Regex>
{
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
                catch (Error)
                {
                    return (state, result);
                }
            }
        }
    }

    public Regex ZeroOrMore() => new ZeroOrMoreRegex(this);

    private abstract record HeadRegex : Regex
    {
        protected abstract Chars.Char Apply(Chars.Char head);

        public override (State state, Result result) Apply(State state)
            => (
                state.Tail,
                new Result(
                    ImmutableList.Create(
                        Try(
                            () => Apply(state.Head),
                            $"error while processing head {state.Head}"
                        )
                    )
                )
            );
    }

    private record LiteralRegex(char Value) : HeadRegex
    {
        public override string ToString() => Value.ToString();

        protected override Chars.Char Apply(Chars.Char head)
        {
            if (head.Value != Value)
            {
                throw CreateError($"expected literal {Value}");
            }
            return head;
        }
    }

    public static Regex Literal(char value) => new LiteralRegex(value);

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
        public override string ToString() => $"({string.Join(" & ", Children.Select(child => child.ToString()))})";

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
}