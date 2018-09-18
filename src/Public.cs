namespace Choices
{
    public partial class Choice {}
    public partial class ChoiceOf1<T> {}
    public partial class ChoiceOf2<T1, T2> {}
    public partial class ChoiceOf3<T1, T2, T3> {}
    public partial class ChoiceOf4<T1, T2, T3, T4> {}

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
