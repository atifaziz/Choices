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
        public static Choice<T1, T2> If<T1, T2>(bool flag, Func<T1> t, Func<T2> f) =>
            flag ? Choice<T1, T2>.Choice1(t())
                 : Choice<T1, T2>.Choice2(f());

        public static Choice<T1, T2, T3> If<T1, T2, T3>(bool flag, Func<T1> t, Func<Choice<T2, T3>> f) =>
            flag ? Choice<T1, T2, T3>.Choice1(t())
                 : f().Match(Choice<T1, T2, T3>.Choice2,
                             Choice<T1, T2, T3>.Choice3);

        public static Choice<T1, T2, T3, T4> If<T1, T2, T3, T4>(bool flag, Func<T1> t, Func<Choice<T2, T3, T4>> f) =>
            flag ? Choice<T1, T2, T3, T4>.Choice1(t())
                 : f().Match(Choice<T1, T2, T3, T4>.Choice2,
                             Choice<T1, T2, T3, T4>.Choice3,
                             Choice<T1, T2, T3, T4>.Choice4);

        public static Func<Choice<T>, TResult> When1<T, TResult>(Func<T, TResult> selector) =>
            choice => choice.Match(selector);

        public static Func<Choice<T1, T2>, TResult> When2<T1, T2, TResult>(this Func<Choice<T1>, TResult> otherwise, Func<T2, TResult> selector) =>
            choice => choice.Match(first => otherwise(Choice<T1>.Choice1(first)), selector);

        public static Func<Choice<T1, T2, T3>, TResult> When3<T1, T2, T3, TResult>(this Func<Choice<T1, T2>, TResult> otherwise, Func<T3, TResult> selector) =>
            choice => choice.Match(first  => otherwise(Choice<T1, T2>.Choice1(first)),
                                   second => otherwise(Choice<T1, T2>.Choice2(second)),
                                   selector);

        public static Func<Choice<T1, T2, T3, T4>, TResult> When4<T1, T2, T3, T4, TResult>(this Func<Choice<T1, T2, T3>, TResult> otherwise, Func<T4, TResult> selector) =>
            choice => choice.Match(first  => otherwise(Choice<T1, T2, T3>.Choice1(first)),
                                   second => otherwise(Choice<T1, T2, T3>.Choice2(second)),
                                   third  => otherwise(Choice<T1, T2, T3>.Choice3(third)),
                                   selector);

        public static Choice<TResult> Map<T, TResult>(this Choice<T> choice, Func<T, TResult> selector) =>
            choice.Match(x => Choice<TResult>.Choice1(selector(x)));

        public static Choice<TResult, T2> Map1<T1, T2, TResult>(this Choice<T1, T2> choice, Func<T1, TResult> selector) =>
            choice.Match(x => Choice<TResult, T2>.Choice1(selector(x)),
                         Choice<TResult, T2>.Choice2);

        public static Choice<T1, TResult> Map2<T1, T2, TResult>(this Choice<T1, T2> choice, Func<T2, TResult> selector) =>
            choice.Match(Choice<T1, TResult>.Choice1,
                         x => Choice<T1, TResult>.Choice2(selector(x)));

        public static Choice<TResult, T2, T3> Map1<T1, T2, T3, TResult>(this Choice<T1, T2, T3> choice, Func<T1, TResult> selector) =>
            choice.Match(x => Choice<TResult, T2, T3>.Choice1(selector(x)),
                         Choice<TResult, T2, T3>.Choice2,
                         Choice<TResult, T2, T3>.Choice3);

        public static Choice<T1, TResult, T3> Map2<T1, T2, T3, TResult>(this Choice<T1, T2, T3> choice, Func<T2, TResult> selector) =>
            choice.Match(Choice<T1, TResult, T3>.Choice1,
                         x => Choice<T1, TResult, T3>.Choice2(selector(x)),
                         Choice<T1, TResult, T3>.Choice3);

        public static Choice<T1, T2, TResult> Map3<T1, T2, T3, TResult>(this Choice<T1, T2, T3> choice, Func<T3, TResult> selector) =>
            choice.Match(Choice<T1, T2, TResult>.Choice1,
                         Choice<T1, T2, TResult>.Choice2,
                         x => Choice<T1, T2, TResult>.Choice3(selector(x)));

        public static Choice<TResult, T2, T3, T4> Map1<T1, T2, T3, T4, TResult>(this Choice<T1, T2, T3, T4> choice, Func<T1, TResult> selector) =>
            choice.Match(x => Choice<TResult, T2, T3, T4>.Choice1(selector(x)),
                         Choice<TResult, T2, T3, T4>.Choice2,
                         Choice<TResult, T2, T3, T4>.Choice3,
                         Choice<TResult, T2, T3, T4>.Choice4);

        public static Choice<T1, TResult, T3, T4> Map2<T1, T2, T3, T4, TResult>(this Choice<T1, T2, T3, T4> choice, Func<T2, TResult> selector) =>
            choice.Match(Choice<T1, TResult, T3, T4>.Choice1,
                         x => Choice<T1, TResult, T3, T4>.Choice2(selector(x)),
                         Choice<T1, TResult, T3, T4>.Choice3,
                         Choice<T1, TResult, T3, T4>.Choice4);

        public static Choice<T1, T2, TResult, T4> Map3<T1, T2, T3, T4, TResult>(this Choice<T1, T2, T3, T4> choice, Func<T3, TResult> selector) =>
            choice.Match(Choice<T1, T2, TResult, T4>.Choice1,
                         Choice<T1, T2, TResult, T4>.Choice2,
                         x => Choice<T1, T2, TResult, T4>.Choice3(selector(x)),
                         Choice<T1, T2, TResult, T4>.Choice4);

        public static Choice<T1, T2, T3, TResult> Map4<T1, T2, T3, T4, TResult>(this Choice<T1, T2, T3, T4> choice, Func<T4, TResult> selector) =>
            choice.Match(Choice<T1, T2, T3, TResult>.Choice1,
                         Choice<T1, T2, T3, TResult>.Choice2,
                         Choice<T1, T2, T3, TResult>.Choice3,
                         x => Choice<T1, T2, T3, TResult>.Choice4(selector(x)));

        internal static string ToString<T>(T value) =>
            value?.ToString() ?? string.Empty;
    }

    abstract partial class Choice<T>
    {
        public static Choice<T> Choice1(T value) => new FirstChoice(value);

        public abstract TResult Match<TResult>(Func<T, TResult> first);
        public abstract override string ToString();

        sealed class FirstChoice : Choice<T>
        {
            readonly T _value;

            public FirstChoice(T value) => _value = value;

            public override TResult Match<TResult>(Func<T, TResult> first) =>
                first(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }
    }

    abstract partial class Choice<T1, T2>
    {
        public static Choice<T1, T2> Choice1(T1 value) => new FirstChoice(value);
        public static Choice<T1, T2> Choice2(T2 value) => new SecondChoice(value);

        public abstract TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second);
        public abstract override string ToString();

        sealed class FirstChoice : Choice<T1, T2>
        {
            readonly T1 _value;

            public FirstChoice(T1 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second) =>
                first(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }

        sealed class SecondChoice : Choice<T1, T2>
        {
            readonly T2 _value;

            public SecondChoice(T2 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second) =>
                second(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }
    }

    abstract partial class Choice<T1, T2, T3>
    {
        public static Choice<T1, T2, T3> Choice1(T1 value) => new FirstChoice(value);
        public static Choice<T1, T2, T3> Choice2(T2 value) => new SecondChoice(value);
        public static Choice<T1, T2, T3> Choice3(T3 value) => new ThirdChoice(value);

        public abstract TResult Match<TResult>(
            Func<T1, TResult> first,
            Func<T2, TResult> second,
            Func<T3, TResult> third);

        public abstract override string ToString();

        sealed class FirstChoice : Choice<T1, T2, T3>
        {
            readonly T1 _value;

            public FirstChoice(T1 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third) =>
                first(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }

        sealed class SecondChoice : Choice<T1, T2, T3>
        {
            readonly T2 _value;

            public SecondChoice(T2 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third) =>
                second(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }

        sealed class ThirdChoice : Choice<T1, T2, T3>
        {
            readonly T3 _value;

            public ThirdChoice(T3 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third) =>
                third(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }
    }

    abstract partial class Choice<T1, T2, T3, T4>
    {
        public static Choice<T1, T2, T3, T4> Choice1(T1 value) => new FirstChoice(value);
        public static Choice<T1, T2, T3, T4> Choice2(T2 value) => new SecondChoice(value);
        public static Choice<T1, T2, T3, T4> Choice3(T3 value) => new ThirdChoice(value);
        public static Choice<T1, T2, T3, T4> Choice4(T4 value) => new FourthChoice(value);

        public abstract TResult Match<TResult>(
            Func<T1, TResult> first,
            Func<T2, TResult> second,
            Func<T3, TResult> third,
            Func<T4, TResult> fourth);

        public abstract override string ToString();

        sealed class FirstChoice : Choice<T1, T2, T3, T4>
        {
            readonly T1 _value;

            public FirstChoice(T1 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                first(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }

        sealed class SecondChoice : Choice<T1, T2, T3, T4>
        {
            readonly T2 _value;

            public SecondChoice(T2 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                second(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }

        sealed class ThirdChoice : Choice<T1, T2, T3, T4>
        {
            readonly T3 _value;

            public ThirdChoice(T3 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                third(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }

        sealed class FourthChoice : Choice<T1, T2, T3, T4>
        {
            readonly T4 _value;

            public FourthChoice(T4 value) => _value = value;

            public override TResult Match<TResult>(Func<T1, TResult> first, Func<T2, TResult> second, Func<T3, TResult> third, Func<T4, TResult> fourth) =>
                fourth(_value);

            public override string ToString() =>
                Choice.ToString(_value);
        }
    }
}
