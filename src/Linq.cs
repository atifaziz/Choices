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
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, choice express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Choices.Linq.Right
{
    using System;
    using static Choice.New;

    static partial class RightResult
    {
        public static IChoice<T, TResult> Return<T, TResult>(TResult value) =>
            Choice2<T, TResult>(value);

        public static IChoice<T1, TResult>
            Bind<T1, T2, TResult>(
                this IChoice<T1, T2> choice,
                Func<T2, IChoice<T1, TResult>> fun) =>
            choice.Match(Choice1<T1, TResult>, fun);

        public static IChoice<T1, TResult>
            Select<T1, T2, TResult>(
                this IChoice<T1, T2> choice,
                Func<T2, TResult> selector) =>
            choice.Bind(b => Choice2<T1, TResult>(selector(b)));

        public static IChoice<T1, TResult>
            SelectMany<T1, T2, TResult>(
                this IChoice<T1, T2> first,
                Func<T2, IChoice<T1, TResult>> secondSelector) =>
            first.Bind(secondSelector);

        public static IChoice<T1, TResult>
            SelectMany<T1, T2, T3, TResult>(
                this IChoice<T1, T2> first,
                Func<T2, IChoice<T1, T3>> secondSelector,
                Func<T2, T3, TResult> resultSelector) =>
            first.Bind(b1 => secondSelector(b1).Select(b2 => resultSelector(b1, b2)));
    }
}

namespace Choices.Linq.Left
{
    using System;
    using static Choice.New;

    static partial class LeftResult
    {
        public static IChoice<TResult, T> Return<TResult, T>(TResult value) =>
            Choice1<TResult, T>(value);

        public static IChoice<TResult, T2>
            Bind<T1, T2, TResult>(
                this IChoice<T1, T2> choice,
                Func<T1, IChoice<TResult, T2>> fun) =>
            choice.Match(fun, Choice2<TResult, T2>);

        public static IChoice<TResult, T2>
            Select<T1, T2, TResult>(
                this IChoice<T1, T2> choice,
                Func<T1, TResult> selector) =>
            choice.Bind(a => Choice1<TResult, T2>(selector(a)));

        public static IChoice<TResult, T2>
            SelectMany<T1, T2, TResult>(
                this IChoice<T1, T2> first,
                Func<T1, IChoice<TResult, T2>> secondSelector) =>
            first.Bind(secondSelector);

        public static IChoice<TResult, T2>
            SelectMany<T1, T2, T3, TResult>(
                this IChoice<T1, T2> first,
                Func<T1, IChoice<T3, T2>> secondSelector,
                Func<T1, T3, TResult> resultSelector) =>
            first.Bind(a1 => secondSelector(a1).Select(a2 => resultSelector(a1, a2)));
    }
}
