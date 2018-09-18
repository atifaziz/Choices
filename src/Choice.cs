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

namespace Choices
{
    using System;

    static partial class Choice
    {
        public static ChoiceOf2<T1, T2> If<T1, T2>(bool flag, Func<T1> t, Func<T2> f) =>
            flag ? ChoiceOf2<T1, T2>.Choice1(t())
                 : ChoiceOf2<T1, T2>.Choice2(f());

        public static ChoiceOf3<T1, T2, T3> If<T1, T2, T3>(bool flag, Func<T1> t, Func<ChoiceOf2<T2, T3>> f) =>
            flag ? ChoiceOf3<T1, T2, T3>.Choice1(t())
                 : f().Match(ChoiceOf3<T1, T2, T3>.Choice2,
                             ChoiceOf3<T1, T2, T3>.Choice3);

        public static ChoiceOf4<T1, T2, T3, T4> If<T1, T2, T3, T4>(bool flag, Func<T1> t, Func<ChoiceOf3<T2, T3, T4>> f) =>
            flag ? ChoiceOf4<T1, T2, T3, T4>.Choice1(t())
                 : f().Match(ChoiceOf4<T1, T2, T3, T4>.Choice2,
                             ChoiceOf4<T1, T2, T3, T4>.Choice3,
                             ChoiceOf4<T1, T2, T3, T4>.Choice4);

        public static Func<ChoiceOf1<T>, TResult> When1<T, TResult>(Func<T, TResult> selector) =>
            choice => choice.Match(selector);

        public static Func<ChoiceOf2<T1, T2>, TResult> When2<T1, T2, TResult>(this Func<ChoiceOf1<T1>, TResult> otherwise, Func<T2, TResult> selector) =>
            choice => choice.Match(first => otherwise(ChoiceOf1<T1>.Choice1(first)), selector);

        public static Func<ChoiceOf3<T1, T2, T3>, TResult> When3<T1, T2, T3, TResult>(this Func<ChoiceOf2<T1, T2>, TResult> otherwise, Func<T3, TResult> selector) =>
            choice => choice.Match(first  => otherwise(ChoiceOf2<T1, T2>.Choice1(first)),
                                   second => otherwise(ChoiceOf2<T1, T2>.Choice2(second)),
                                   selector);

        public static Func<ChoiceOf4<T1, T2, T3, T4>, TResult> When4<T1, T2, T3, T4, TResult>(this Func<ChoiceOf3<T1, T2, T3>, TResult> otherwise, Func<T4, TResult> selector) =>
            choice => choice.Match(first  => otherwise(ChoiceOf3<T1, T2, T3>.Choice1(first)),
                                   second => otherwise(ChoiceOf3<T1, T2, T3>.Choice2(second)),
                                   third  => otherwise(ChoiceOf3<T1, T2, T3>.Choice3(third)),
                                   selector);

        public static ChoiceOf1<TResult> Map<T, TResult>(this ChoiceOf1<T> choice, Func<T, TResult> selector) =>
            choice.Match(x => ChoiceOf1<TResult>.Choice1(selector(x)));

        public static ChoiceOf2<TResult, T2> Map1<T1, T2, TResult>(this ChoiceOf2<T1, T2> choice, Func<T1, TResult> selector) =>
            choice.Match(x => ChoiceOf2<TResult, T2>.Choice1(selector(x)),
                         ChoiceOf2<TResult, T2>.Choice2);

        public static ChoiceOf2<T1, TResult> Map2<T1, T2, TResult>(this ChoiceOf2<T1, T2> choice, Func<T2, TResult> selector) =>
            choice.Match(ChoiceOf2<T1, TResult>.Choice1,
                         x => ChoiceOf2<T1, TResult>.Choice2(selector(x)));

        public static ChoiceOf3<TResult, T2, T3> Map1<T1, T2, T3, TResult>(this ChoiceOf3<T1, T2, T3> choice, Func<T1, TResult> selector) =>
            choice.Match(x => ChoiceOf3<TResult, T2, T3>.Choice1(selector(x)),
                         ChoiceOf3<TResult, T2, T3>.Choice2,
                         ChoiceOf3<TResult, T2, T3>.Choice3);

        public static ChoiceOf3<T1, TResult, T3> Map2<T1, T2, T3, TResult>(this ChoiceOf3<T1, T2, T3> choice, Func<T2, TResult> selector) =>
            choice.Match(ChoiceOf3<T1, TResult, T3>.Choice1,
                         x => ChoiceOf3<T1, TResult, T3>.Choice2(selector(x)),
                         ChoiceOf3<T1, TResult, T3>.Choice3);

        public static ChoiceOf3<T1, T2, TResult> Map3<T1, T2, T3, TResult>(this ChoiceOf3<T1, T2, T3> choice, Func<T3, TResult> selector) =>
            choice.Match(ChoiceOf3<T1, T2, TResult>.Choice1,
                         ChoiceOf3<T1, T2, TResult>.Choice2,
                         x => ChoiceOf3<T1, T2, TResult>.Choice3(selector(x)));

        public static ChoiceOf4<TResult, T2, T3, T4> Map1<T1, T2, T3, T4, TResult>(this ChoiceOf4<T1, T2, T3, T4> choice, Func<T1, TResult> selector) =>
            choice.Match(x => ChoiceOf4<TResult, T2, T3, T4>.Choice1(selector(x)),
                         ChoiceOf4<TResult, T2, T3, T4>.Choice2,
                         ChoiceOf4<TResult, T2, T3, T4>.Choice3,
                         ChoiceOf4<TResult, T2, T3, T4>.Choice4);

        public static ChoiceOf4<T1, TResult, T3, T4> Map2<T1, T2, T3, T4, TResult>(this ChoiceOf4<T1, T2, T3, T4> choice, Func<T2, TResult> selector) =>
            choice.Match(ChoiceOf4<T1, TResult, T3, T4>.Choice1,
                         x => ChoiceOf4<T1, TResult, T3, T4>.Choice2(selector(x)),
                         ChoiceOf4<T1, TResult, T3, T4>.Choice3,
                         ChoiceOf4<T1, TResult, T3, T4>.Choice4);

        public static ChoiceOf4<T1, T2, TResult, T4> Map3<T1, T2, T3, T4, TResult>(this ChoiceOf4<T1, T2, T3, T4> choice, Func<T3, TResult> selector) =>
            choice.Match(ChoiceOf4<T1, T2, TResult, T4>.Choice1,
                         ChoiceOf4<T1, T2, TResult, T4>.Choice2,
                         x => ChoiceOf4<T1, T2, TResult, T4>.Choice3(selector(x)),
                         ChoiceOf4<T1, T2, TResult, T4>.Choice4);

        public static ChoiceOf4<T1, T2, T3, TResult> Map4<T1, T2, T3, T4, TResult>(this ChoiceOf4<T1, T2, T3, T4> choice, Func<T4, TResult> selector) =>
            choice.Match(ChoiceOf4<T1, T2, T3, TResult>.Choice1,
                         ChoiceOf4<T1, T2, T3, TResult>.Choice2,
                         ChoiceOf4<T1, T2, T3, TResult>.Choice3,
                         x => ChoiceOf4<T1, T2, T3, TResult>.Choice4(selector(x)));
    }

    abstract partial class ChoiceOf1<T>
    {
        public static ChoiceOf1<T> Choice1(T value) => new Choice1Of1(value);

        public abstract TResult Match<TResult>(Func<T, TResult> first);

        sealed class Choice1Of1 : ChoiceOf1<T>
        {
            readonly T _value;
            public Choice1Of1(T value) => _value = value;
            public override TResult Match<TResult>(Func<T, TResult> first) =>
                first(_value);
        }
    }

    abstract partial class ChoiceOf2<T1, T2>
    {
        public static ChoiceOf2<T1, T2> Choice1(T1 value) => new Choice1Of2(value);
        public static ChoiceOf2<T1, T2> Choice2(T2 value) => new Choice2Of2(value);

        public abstract TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second);

        sealed class Choice1Of2 : ChoiceOf2<T1, T2>
        {
            readonly T1 _value;
            public Choice1Of2(T1 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second) =>
                first(_value);
        }

        sealed class Choice2Of2 : ChoiceOf2<T1, T2>
        {
            readonly T2 _value;
            public Choice2Of2(T2 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second) =>
                second(_value);
        }
    }

    abstract partial class ChoiceOf3<T1, T2, T3>
    {
        public static ChoiceOf3<T1, T2, T3> Choice1(T1 value) => new Choice1Of3(value);
        public static ChoiceOf3<T1, T2, T3> Choice2(T2 value) => new Choice2Of3(value);
        public static ChoiceOf3<T1, T2, T3> Choice3(T3 value) => new Choice3Of3(value);

        public abstract TResult Match<TResult>(
            Func<T1, TResult> first,
            Func<T2, TResult> second,
            Func<T3, TResult> third);

        sealed class Choice1Of3 : ChoiceOf3<T1, T2, T3>
        {
            readonly T1 _value;
            public Choice1Of3(T1 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third) =>
                first(_value);
        }

        sealed class Choice2Of3 : ChoiceOf3<T1, T2, T3>
        {
            readonly T2 _value;
            public Choice2Of3(T2 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third) =>
                second(_value);
        }

        sealed class Choice3Of3 : ChoiceOf3<T1, T2, T3>
        {
            readonly T3 _value;
            public Choice3Of3(T3 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third) =>
                third(_value);
        }
    }

    abstract partial class ChoiceOf4<T1, T2, T3, T4>
    {
        public static ChoiceOf4<T1, T2, T3, T4> Choice1(T1 value) => new Choice1Of4(value);
        public static ChoiceOf4<T1, T2, T3, T4> Choice2(T2 value) => new Choice2Of4(value);
        public static ChoiceOf4<T1, T2, T3, T4> Choice3(T3 value) => new Choice3Of4(value);
        public static ChoiceOf4<T1, T2, T3, T4> Choice4(T4 value) => new Choice4Of4(value);

        public abstract TResult Match<TResult>(
            Func<T1, TResult> first,
            Func<T2, TResult> second,
            Func<T3, TResult> third,
            Func<T4, TResult> fourth);

        sealed class Choice1Of4 : ChoiceOf4<T1, T2, T3, T4>
        {
            readonly T1 _value;
            public Choice1Of4(T1 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                first(_value);
        }

        sealed class Choice2Of4 : ChoiceOf4<T1, T2, T3, T4>
        {
            readonly T2 _value;
            public Choice2Of4(T2 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                second(_value);
        }

        sealed class Choice3Of4 : ChoiceOf4<T1, T2, T3, T4>
        {
            readonly T3 _value;
            public Choice3Of4(T3 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                third(_value);
        }

        sealed class Choice4Of4 : ChoiceOf4<T1, T2, T3, T4>
        {
            readonly T4 _value;
            public Choice4Of4(T4 value) => _value = value;
            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                fourth(_value);
        }
    }
}
