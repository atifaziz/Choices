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
        public static Choice<T, TResult> Return<T, TResult>(TResult value) =>
            Choice2<T, TResult>(value);

        public static Choice<T1, TResult>
            Bind<T1, T2, TResult>(
                this Choice<T1, T2> choice,
                Func<T2, Choice<T1, TResult>> fun)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (fun == null) throw new ArgumentNullException(nameof(fun));

            return choice.Match(Choice1<T1, TResult>, fun);
        }

        public static Choice<T1, TResult>
            Select<T1, T2, TResult>(
                this Choice<T1, T2> choice,
                Func<T2, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Bind(b => Choice2<T1, TResult>(selector(b)));
        }

        public static Choice<T1, TResult>
            SelectMany<T1, T2, TResult>(
                this Choice<T1, T2> choice,
                Func<T2, Choice<T1, TResult>> resultSelector) =>
            choice.Bind(resultSelector);

        public static Choice<T1, TResult>
            SelectMany<T1, T2, T3, TResult>(
                this Choice<T1, T2> first,
                Func<T2, Choice<T1, T3>> secondSelector,
                Func<T2, T3, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (secondSelector == null) throw new ArgumentNullException(nameof(secondSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return first.Bind(b1 => secondSelector(b1).Select(b2 => resultSelector(b1, b2)));
        }
    }
}

namespace Choices.Linq.Left
{
    using System;
    using static Choice.New;

    static partial class LeftResult
    {
        public static Choice<TResult, T> Return<TResult, T>(TResult value) =>
            Choice1<TResult, T>(value);

        public static Choice<TResult, T2>
            Bind<T1, T2, TResult>(
                this Choice<T1, T2> choice,
                Func<T1, Choice<TResult, T2>> fun)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (fun == null) throw new ArgumentNullException(nameof(fun));

            return choice.Match(fun, Choice2<TResult, T2>);
        }

        public static Choice<TResult, T2>
            Select<T1, T2, TResult>(
                this Choice<T1, T2> choice,
                Func<T1, TResult> selector)
        {
            if (choice == null) throw new ArgumentNullException(nameof(choice));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return choice.Bind(a => Choice1<TResult, T2>(selector(a)));
        }

        public static Choice<TResult, T2>
            SelectMany<T1, T2, TResult>(
                this Choice<T1, T2> choice,
                Func<T1, Choice<TResult, T2>> resultSelector) =>
            choice.Bind(resultSelector);

        public static Choice<TResult, T2>
            SelectMany<T1, T2, T3, TResult>(
                this Choice<T1, T2> first,
                Func<T1, Choice<T3, T2>> secondSelector,
                Func<T1, T3, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (secondSelector == null) throw new ArgumentNullException(nameof(secondSelector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return first.Bind(a1 => secondSelector(a1).Select(a2 => resultSelector(a1, a2)));
        }
    }
}
