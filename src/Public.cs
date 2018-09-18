namespace Choices
{
    public partial class Choice
    {
        public partial class New {}
    }

    public partial interface IChoice<out T> {}
    public partial interface IChoice<out T1, out T2> {}
    public partial interface IChoice<out T1, out T2, out T3> {}
    public partial interface IChoice<out T1, out T2, out T3, out T4> {}

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
