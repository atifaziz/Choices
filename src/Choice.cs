#region Copyright (c) 2015 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

// ReSharper disable PartialTypeWithSinglePart

namespace Choices
{
    using System;
    using static Choice.New;

    partial interface IChoice<out T>
    {
        TResult Match<TResult>(Func<T, TResult> first);
    }

    partial interface IChoice<out T1, out T2>
    {
        TResult Match<TResult>(Func<T1, TResult> first,
                               Func<T2, TResult> second);
    }

    partial interface IChoice<out T1, out T2, out T3>
    {
        TResult Match<TResult>(Func<T1, TResult> first,
                               Func<T2, TResult> second,
                               Func<T3, TResult> third);
    }

    partial interface IChoice<out T1, out T2, out T3, out T4>
    {
        TResult Match<TResult>(Func<T1, TResult> first,
                               Func<T2, TResult> second,
                               Func<T3, TResult> third,
                               Func<T4, TResult> fourth);
    }

    static partial class Choice
    {
        static partial class New
        {
            public static IChoice<T>              Choice1<T>             (T  value) => new Choice1Of1<T>             (value);
            public static IChoice<T1, T2>         Choice1<T1, T2>        (T1 value) => new Choice1Of2<T1, T2>        (value);
            public static IChoice<T1, T2>         Choice2<T1, T2>        (T2 value) => new Choice2Of2<T1, T2>        (value);
            public static IChoice<T1, T2, T3>     Choice1<T1, T2, T3>    (T1 value) => new Choice1Of3<T1, T2, T3>    (value);
            public static IChoice<T1, T2, T3>     Choice2<T1, T2, T3>    (T2 value) => new Choice2Of3<T1, T2, T3>    (value);
            public static IChoice<T1, T2, T3>     Choice3<T1, T2, T3>    (T3 value) => new Choice3Of3<T1, T2, T3>    (value);
            public static IChoice<T1, T2, T3, T4> Choice1<T1, T2, T3, T4>(T1 value) => new Choice1Of4<T1, T2, T3, T4>(value);
            public static IChoice<T1, T2, T3, T4> Choice2<T1, T2, T3, T4>(T2 value) => new Choice2Of4<T1, T2, T3, T4>(value);
            public static IChoice<T1, T2, T3, T4> Choice3<T1, T2, T3, T4>(T3 value) => new Choice3Of4<T1, T2, T3, T4>(value);
            public static IChoice<T1, T2, T3, T4> Choice4<T1, T2, T3, T4>(T4 value) => new Choice4Of4<T1, T2, T3, T4>(value);
        }

        public static IChoice<T1, T2> If<T1, T2>(bool flag, Func<T1> t, Func<T2> f)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (f == null) throw new ArgumentNullException(nameof(f));

            return flag ? Choice1<T1, T2>(t())
                        : Choice2<T1, T2>(f());
        }

        public static IChoice<T1, T2, T3> If<T1, T2, T3>(bool flag, Func<T1> t, Func<IChoice<T2, T3>> f)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (f == null) throw new ArgumentNullException(nameof(f));

            return flag
                 ? Choice1<T1, T2, T3>(t())
                 : f().Match(Choice2<T1, T2, T3>,
                             Choice3<T1, T2, T3>);
        }

        public static IChoice<T1, T2, T3, T4> If<T1, T2, T3, T4>(bool flag, Func<T1> t, Func<IChoice<T2, T3, T4>> f)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            if (f == null) throw new ArgumentNullException(nameof(f));

            return flag
                 ? Choice1<T1, T2, T3, T4>(t())
                 : f().Match(Choice2<T1, T2, T3, T4>,
                             Choice3<T1, T2, T3, T4>,
                             Choice4<T1, T2, T3, T4>);
        }

        public static Func<IChoice<T>, TResult> When1<T, TResult>(Func<T, TResult> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return choice => choice.Match(selector);
        }

        public static Func<IChoice<T1, T2>, TResult> When2<T1, T2, TResult>(this Func<IChoice<T1>, TResult> otherwise, Func<T2, TResult> selector)
        {
            if (otherwise == null) throw new ArgumentNullException(nameof(otherwise));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice => choice.Match(first => otherwise(Choice1(first)), selector);
        }

        public static Func<IChoice<T1, T2, T3>, TResult> When3<T1, T2, T3, TResult>(this Func<IChoice<T1, T2>, TResult> otherwise, Func<T3, TResult> selector)
        {
            if (otherwise == null) throw new ArgumentNullException(nameof(otherwise));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice =>
                choice.Match(first  => otherwise(Choice1<T1, T2>(first)),
                             second => otherwise(Choice2<T1, T2>(second)),
                             selector);
        }

        public static Func<IChoice<T1, T2, T3, T4>, TResult> When4<T1, T2, T3, T4, TResult>(this Func<IChoice<T1, T2, T3>, TResult> otherwise, Func<T4, TResult> selector)
        {
            if (otherwise == null) throw new ArgumentNullException(nameof(otherwise));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice =>
                choice.Match(first  => otherwise(Choice1<T1, T2, T3>(first)),
                             second => otherwise(Choice2<T1, T2, T3>(second)),
                             third  => otherwise(Choice3<T1, T2, T3>(third)),
                             selector);
        }

        public static IChoice<TResult> Map<T, TResult>(this IChoice<T> choice, Func<T, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(x => Choice1(selector(x)));
        }

        public static IChoice<TResult, T2> Map1<T1, T2, TResult>(this IChoice<T1, T2> choice, Func<T1, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(x => Choice1<TResult, T2>(selector(x)),
                                Choice2<TResult, T2>);
        }

        public static IChoice<T1, TResult> Map2<T1, T2, TResult>(this IChoice<T1, T2> choice, Func<T2, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(Choice1<T1, TResult>,
                                x => Choice2<T1, TResult>(selector(x)));
        }

        public static IChoice<TResult, T2, T3> Map1<T1, T2, T3, TResult>(this IChoice<T1, T2, T3> choice, Func<T1, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(x => Choice1<TResult, T2, T3>(selector(x)),
                                Choice2<TResult, T2, T3>,
                                Choice3<TResult, T2, T3>);
        }

        public static IChoice<T1, TResult, T3> Map2<T1, T2, T3, TResult>(this IChoice<T1, T2, T3> choice, Func<T2, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(Choice1<T1, TResult, T3>,
                                x => Choice2<T1, TResult, T3>(selector(x)),
                                Choice3<T1, TResult, T3>);
        }

        public static IChoice<T1, T2, TResult> Map3<T1, T2, T3, TResult>(this IChoice<T1, T2, T3> choice, Func<T3, TResult> selector) =>
            choice.Match(Choice1<T1, T2, TResult>,
                         Choice2<T1, T2, TResult>,
                         x => Choice3<T1, T2, TResult>(selector(x)));

        public static IChoice<TResult, T2, T3, T4> Map1<T1, T2, T3, T4, TResult>(this IChoice<T1, T2, T3, T4> choice, Func<T1, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(x => Choice1<TResult, T2, T3, T4>(selector(x)),
                                Choice2<TResult, T2, T3, T4>,
                                Choice3<TResult, T2, T3, T4>,
                                Choice4<TResult, T2, T3, T4>);
        }

        public static IChoice<T1, TResult, T3, T4> Map2<T1, T2, T3, T4, TResult>(this IChoice<T1, T2, T3, T4> choice, Func<T2, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(Choice1<T1, TResult, T3, T4>,
                                x => Choice2<T1, TResult, T3, T4>(selector(x)),
                                Choice3<T1, TResult, T3, T4>,
                                Choice4<T1, TResult, T3, T4>);
        }

        public static IChoice<T1, T2, TResult, T4> Map3<T1, T2, T3, T4, TResult>(this IChoice<T1, T2, T3, T4> choice, Func<T3, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(Choice1<T1, T2, TResult, T4>,
                                Choice2<T1, T2, TResult, T4>,
                                x => Choice3<T1, T2, TResult, T4>(selector(x)),
                                Choice4<T1, T2, TResult, T4>);
        }

        public static IChoice<T1, T2, T3, TResult> Map4<T1, T2, T3, T4, TResult>(this IChoice<T1, T2, T3, T4> choice, Func<T4, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Match(Choice1<T1, T2, T3, TResult>,
                                Choice2<T1, T2, T3, TResult>,
                                Choice3<T1, T2, T3, TResult>,
                                x => Choice4<T1, T2, T3, TResult>(selector(x)));
        }

        internal static string ToString<T>(T value) =>
            value?.ToString() ?? string.Empty;
    }

    abstract partial class Choice<T> : IChoice<T>
    {
        public abstract TResult Match<TResult>(Func<T, TResult> first);
        public abstract override string ToString();
    }

    sealed partial class Choice1Of1<T> : Choice<T>
    {
        readonly T _value;

        public Choice1Of1(T value) => _value = value;

        public override TResult Match<TResult>(Func<T, TResult> first)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            return first(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    abstract partial class Choice<T1, T2> : IChoice<T1, T2>
    {
        public abstract TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second);
        public abstract override string ToString();
    }

    sealed partial class Choice1Of2<T1, T2> : Choice<T1, T2>
    {
        readonly T1 _value;

        public Choice1Of2(T1 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            return first(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    sealed partial class Choice2Of2<T1, T2> : Choice<T1, T2>
    {
        readonly T2 _value;

        public Choice2Of2(T2 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            return second(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    abstract partial class Choice<T1, T2, T3> : IChoice<T1, T2, T3>
    {
        public abstract TResult Match<TResult>(
            Func<T1, TResult> first,
            Func<T2, TResult> second,
            Func<T3, TResult> third);

        public abstract override string ToString();
    }

    sealed partial class Choice1Of3<T1, T2, T3> : Choice<T1, T2, T3>
    {
        readonly T1 _value;

        public Choice1Of3(T1 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));

            return first(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    sealed partial class Choice2Of3<T1, T2, T3> : Choice<T1, T2, T3>
    {
        readonly T2 _value;

        public Choice2Of3(T2 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));

            return second(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    sealed partial class Choice3Of3<T1, T2, T3> : Choice<T1, T2, T3>
    {
        readonly T3 _value;

        public Choice3Of3(T3 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));

            return third(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    abstract partial class Choice<T1, T2, T3, T4> : IChoice<T1, T2, T3, T4>
    {
        public abstract TResult Match<TResult>(
            Func<T1, TResult> first,
            Func<T2, TResult> second,
            Func<T3, TResult> third,
            Func<T4, TResult> fourth);

        public abstract override string ToString();
    }

    sealed partial class Choice1Of4<T1, T2, T3, T4> : Choice<T1, T2, T3, T4>
    {
        readonly T1 _value;

        public Choice1Of4(T1 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));

            return first(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    sealed partial class Choice2Of4<T1, T2, T3, T4> : Choice<T1, T2, T3, T4>
    {
        readonly T2 _value;

        public Choice2Of4(T2 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));

            return second(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    sealed partial class Choice3Of4<T1, T2, T3, T4> : Choice<T1, T2, T3, T4>
    {
        readonly T3 _value;

        public Choice3Of4(T3 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));

            return third(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }

    sealed partial class Choice4Of4<T1, T2, T3, T4> : Choice<T1, T2, T3, T4>
    {
        readonly T4 _value;

        public Choice4Of4(T4 value) => _value = value;

        public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));

            return fourth(_value);
        }

        public override string ToString() =>
            Choice.ToString(_value);
    }
}
