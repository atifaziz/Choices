namespace Choices
{
    public partial class Choice {}
    public partial class Choice<T1, T2> {}
    public partial class Choice<T1, T2, T3> {}
    public partial class Choice<T1, T2, T3, T4> {}

    public partial class WhenPartial<T, TResult> {}

    namespace Linq
    {
        namespace Left
        {
            public partial class LeftResult {}
        }

        namespace Right
        {
            public partial class RightResult {}
        }
    }
}
