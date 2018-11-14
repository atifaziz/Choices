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
    using NUnit.Framework;
    using static Choice.New;
    using Int1 = Box1<int>;
    using Int2 = Box2<int>;
    using Int3 = Box3<int>;
    using Int4 = Box4<int>;

    [TestFixture]
    public partial class ChoiceOfTests
    {
        static Exception BadChoice() => ChoiceTests.BadChoice();

        static T AssertNotCalled<T>()
        {
            Assert.Fail("Unexpected call.");
            return default;
        }

        static void TestLeftRight<TExpected, TChoice, TL, TR>(
            TExpected expected,
            Func<TExpected, TChoice> cf,
            Func<TChoice, Choice<TL, TR>> lrf,
            Func<TL, TExpected> l,
            Func<TR, TExpected> r)
        {
            Assert.That(lrf(cf(expected)).Match(l, r), Is.EqualTo(expected));
        }

        static void TestLeft<TExpected, TChoice, TR>(
            TExpected expected,
            Func<TExpected, TChoice> cf,
            Func<TChoice, Choice<TExpected, TR>> ef) =>
            TestLeft(expected, cf, ef, x => x);

        static void TestLeft<TExpected, TChoice, TL, TR>(
            TExpected expected,
            Func<TExpected, TChoice> cf,
            Func<TChoice, Choice<TL, TR>> ef,
            Func<TL, TExpected> lf) =>
            TestLeftRight(expected, cf, ef, lf, _ => AssertNotCalled<TExpected>());

        static void TestRight<TExpected, TChoice, TL>(
            TExpected expected,
            Func<TExpected, TChoice> cf,
            Func<TChoice, Choice<TL, TExpected>> ef) =>
            TestRight(expected, cf, ef, x => x);

        static void TestRight<TExpected, TChoice, TL, TR>(
            TExpected expected,
            Func<TExpected, TChoice> cf,
            Func<TChoice, Choice<TL, TR>> ef,
            Func<TR, TExpected> rf) =>
            TestLeftRight(expected, cf, ef, _ => AssertNotCalled<TExpected>(), rf);

        #region LeftN/RightN for ChoiceOf3

        [Test]
        public void Left1WithChoice1Of3()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3>, c => c.Left1());
        }

        [Test]
        public void Left1WithChoice2Of3()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3>,
                c => c.Left1(),
                r => r.Match(x => x, _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Left1WithChoice3Of3()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3>,
                c => c.Left1(),
                r => r.Match(_ => AssertNotCalled<Int3>(), x => x));
        }

        [Test]
        public void Left2WithChoice1Of3()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3>,
                c => c.Left2(),
                r => r.Match(x => x, _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Left2WithChoice2Of3()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3>, c => c.Left2());
        }

        [Test]
        public void Left2WithChoice3Of3()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3>,
                c => c.Left2(),
                r => r.Match(_ => AssertNotCalled<Int3>(), x => x));
        }

        [Test]
        public void Left3WithChoice1Of3()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3>,
                c => c.Left3(),
                r => r.Match(x => x, _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Left3WithChoice2Of3()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3>,
                c => c.Left3(),
                r => r.Match(_ => AssertNotCalled<Int2>(), x => x));
        }

        [Test]
        public void Left3WithChoice3Of3()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3>, c => c.Left3());
        }

        [Test]
        public void Right1WithChoice1Of3()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3>, c => c.Right1());
        }

        [Test]
        public void Right1WithChoice2Of3()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3>,
                c => c.Right1(),
                r => r.Match(x => x, _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Right1WithChoice3Of3()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3>,
                c => c.Right1(),
                r => r.Match(_ => AssertNotCalled<Int3>(), x => x));
        }

        [Test]
        public void Right2WithChoice1Of3()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3>,
                c => c.Right2(),
                r => r.Match(x => x, _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Right2WithChoice2Of3()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3>, c => c.Right2());
        }

        [Test]
        public void Right2WithChoice3Of3()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3>,
                c => c.Right2(),
                r => r.Match(_ => AssertNotCalled<Int3>(), x => x));
        }

        [Test]
        public void Right3WithChoice1Of3()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3>,
                c => c.Right3(),
                r => r.Match(x => x, _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Right3WithChoice2Of3()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3>,
                c => c.Right3(),
                r => r.Match(_ => AssertNotCalled<Int2>(), x => x));
        }

        [Test]
        public void Right3WithChoice3Of3()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3>, c => c.Right3());
        }

        #endregion

        #region LeftN/RightN for ChoiceOf4

        [Test]
        public void Left1WithChoice1Of4()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3, Int4>, c => c.Left1());
        }

        [Test]
        public void Left1WithChoice2Of4()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                      c => c.Left1(),
                      r => r.Match(x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Left1WithChoice3Of4()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                      c => c.Left1(),
                      r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
        }

        [Test]
        public void Left1WithChoice4Of4()
        {
            TestRight(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                      c => c.Left1(),
                      r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
        }

        [Test]
        public void Left2WithChoice1Of4()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                      c => c.Left2(),
                      r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Left2WithChoice2Of4()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3, Int4>, c => c.Left2());
        }

        [Test]
        public void Left2WithChoice3Of4()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                      c => c.Left2(),
                      r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
        }

        [Test]
        public void Left2WithChoice4Of4()
        {
            TestRight(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                      c => c.Left2(),
                      r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
        }

        [Test]
        public void Left3WithChoice1Of4()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                      c => c.Left3(),
                      r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Left3WithChoice2Of4()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                      c => c.Left3(),
                      r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Left3WithChoice3Of4()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3, Int4>, c => c.Left3());
        }

        [Test]
        public void Left3WithChoice4Of4()
        {
            TestRight(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                      c => c.Left3(),
                      r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
        }

        [Test]
        public void Left4WithChoice1Of4()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                      c => c.Left4(),
                      r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Left4WithChoice2Of4()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                      c => c.Left4(),
                      r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Left4WithChoice3Of4()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                      c => c.Left4(),
                      r => r.Match(_ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>(), x => x));
        }

        [Test]
        public void Left4WithChoice4Of4()
        {
            TestLeft(new Int4(42), Choice4<Int1, Int2, Int3, Int4>, c => c.Left4());
        }

        [Test]
        public void Right1WithChoice1Of4()
        {
            TestRight(new Int1(42), Choice1<Int1, Int2, Int3, Int4>, c => c.Right1());
        }

        [Test]
        public void Right1WithChoice2Of4()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                     c => c.Right1(),
                     r => r.Match(x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Right1WithChoice3Of4()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                     c => c.Right1(),
                     r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
        }

        [Test]
        public void Right1WithChoice4Of4()
        {
            TestLeft(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                     c => c.Right1(),
                     r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
        }

        [Test]
        public void Right2WithChoice1Of4()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                     c => c.Right2(),
                     r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Right2WithChoice2Of4()
        {
            TestRight(new Int2(42), Choice2<Int1, Int2, Int3, Int4>, c => c.Right2());
        }

        [Test]
        public void Right2WithChoice3Of4()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                     c => c.Right2(),
                     r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
        }

        [Test]
        public void Right2WithChoice4Of4()
        {
            TestLeft(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                     c => c.Right2(),
                     r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
        }

        [Test]
        public void Right3WithChoice1Of4()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                     c => c.Right3(),
                     r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Right3WithChoice2Of4()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                     c => c.Right3(),
                     r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Right3WithChoice3Of4()
        {
            TestRight(new Int3(42), Choice3<Int1, Int2, Int3, Int4>, c => c.Right3());
        }

        [Test]
        public void Right3WithChoice4Of4()
        {
            TestLeft(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                     c => c.Right3(),
                     r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
        }

        [Test]
        public void Right4WithChoice1Of4()
        {
            TestLeft(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                     c => c.Right4(),
                     r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
        }

        [Test]
        public void Right4WithChoice2Of4()
        {
            TestLeft(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                     c => c.Right4(),
                     r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>()));
        }

        [Test]
        public void Right4WithChoice3Of4()
        {
            TestLeft(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                     c => c.Right4(),
                     r => r.Match(_ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>(), x => x));
        }

        [Test]
        public void Right4WithChoice4Of4()
        {
            TestRight(new Int4(42), Choice4<Int1, Int2, Int3, Int4>, c => c.Right4());
        }

        #endregion
    }
}
