#region Copyright (c) 2018 Atif Aziz. All rights reserved.
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

namespace Choices.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(Value) + "}")]
    sealed class Box1<T>
    {
        public readonly T Value;
        public Box1(T value) => Value = value;
        public static implicit operator Box1<T>(T x) => new Box1<T>(x);
        public override bool Equals(object obj)
            => obj is Box1<T> other
            && EqualityComparer<T>.Default.Equals(Value, other.Value);
        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);
        public override string ToString() => FormattableString.Invariant($"{Value}");
    }

    [DebuggerDisplay("{" + nameof(Value) + "}")]
    sealed class Box2<T>
    {
        public readonly T Value;
        public Box2(T value) => Value = value;
        public static implicit operator Box2<T>(T x) => new Box2<T>(x);
        public override bool Equals(object obj)
            => obj is Box2<T> other
            && EqualityComparer<T>.Default.Equals(Value, other.Value);
        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);
        public override string ToString() => FormattableString.Invariant($"{Value}");
    }

    [DebuggerDisplay("{" + nameof(Value) + "}")]
    sealed class Box3<T>
    {
        public readonly T Value;
        public Box3(T value) => Value = value;
        public static implicit operator Box3<T>(T x) => new Box3<T>(x);
        public override bool Equals(object obj)
            => obj is Box3<T> other
            && EqualityComparer<T>.Default.Equals(Value, other.Value);
        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);
        public override string ToString() => FormattableString.Invariant($"{Value}");
    }

    [DebuggerDisplay("{" + nameof(Value) + "}")]
    sealed class Box4<T>
    {
        public readonly T Value;
        public Box4(T value) => Value = value;
        public static implicit operator Box4<T>(T x) => new Box4<T>(x);
        public override bool Equals(object obj)
            => obj is Box4<T> other
            && EqualityComparer<T>.Default.Equals(Value, other.Value);
        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);
        public override string ToString() => FormattableString.Invariant($"{Value}");
    }
}
