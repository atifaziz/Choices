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
    using Int5 = Box5<int>;

    [TestFixture]
    public class ChoiceTests
    {
        internal static Exception BadChoice() => new Exception("Invalid choice.");

        [Test]
        public void IfTrue()
        {
            var choice =
                Choice.If<Int1, Int2>(
                    true,
                    () => 42,
                    () => throw new NotImplementedException());
            Assert.That(choice, Is.Not.Null);
            var result = choice.Match(n => (object) n, s => s);
            Assert.That(result, Is.EqualTo(new Int1(42)));
        }

        [Test]
        public void IfFalse()
        {
            var choice =
                Choice.If<Int1, Int2>(
                    false,
                    () => throw new NotImplementedException(),
                    () => 42);
            Assert.That(choice, Is.Not.Null);
            var result = choice.Match(a => (object) a, b => b);
            Assert.That(result, Is.EqualTo(new Int2(42)));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void If2(int i)
        {
            Assert.That(i, Is.GreaterThanOrEqualTo(1));
            Assert.That(i, Is.LessThanOrEqualTo(3));

            var x = new Int1(42);
            var y = new Int2(42);
            var z = new Int3(42);

            var choices = new object[] { x, y, z };

            var choice =
                Choice.If(
                    i == 1,
                    () => x,
                    () =>
                        Choice.If(
                            i == 2,
                            () => y,
                            () => z));

            Assert.That(choice, Is.Not.Null);
            var actual = choice.Match(a => (object) a, b => b, c => c);
            Assert.That(actual, Is.EqualTo(choices[i - 1]));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void If3(int i)
        {
            Assert.That(i, Is.GreaterThanOrEqualTo(1));
            Assert.That(i, Is.LessThanOrEqualTo(4));

            var w = new Int1(42);
            var x = new Int2(42);
            var y = new Int3(42);
            var z = new Int4(42);

            var choices = new object[] { w, x, y, z };

            var choice =
                Choice.If(
                    i == 1,
                    () => w,
                    () =>
                        Choice.If(
                            i == 2,
                            () => x,
                            () =>
                                Choice.If(
                                    i == 3,
                                    () => y,
                                    () => z)));

            Assert.That(choice, Is.Not.Null);
            var actual = choice.Match(a => (object) a, b => b, c => c, d => d);
            Assert.That(actual, Is.EqualTo(choices[i - 1]));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void If4(int i)
        {
            Assert.That(i, Is.GreaterThanOrEqualTo(1));
            Assert.That(i, Is.LessThanOrEqualTo(5));

            var v = new Int1(42);
            var w = new Int2(42);
            var x = new Int3(42);
            var y = new Int4(42);
            var z = new Int5(42);

            var choices = new object[] { v, w, x, y, z };

            var choice =
                Choice.If(
                    i == 1,
                    () => v,
                    () =>
                        Choice.If(
                            i == 2,
                            () => w,
                            () =>
                                Choice.If(
                                    i == 3,
                                    () => x,
                                    () =>
                                        Choice.If(
                                            i == 4,
                                            () => y,
                                            () => z))));

            Assert.That(choice, Is.Not.Null);
            var actual = choice.Match(a => (object) a, b => b, c => c, d => d, e => e);
            Assert.That(actual, Is.EqualTo(choices[i - 1]));
        }

        [Test]
        public void When1()
        {
            var f = new Func<int, int>(x => x * 2);
            var map = Choice.When1(f);
            Assert.That(map, Is.Not.Null);
        }

        [Test]
        public void When2()
        {
            var map = Choice.When1((int x) => x * 2)
                            .When2((string s) => s.Length);
            Assert.That(map, Is.Not.Null);

            var r1 = map(Choice1<int, string>(42));
            Assert.That(r1, Is.EqualTo(84));

            var r2 = map(Choice2<int, string>("foobar"));
            Assert.That(r2, Is.EqualTo(6));
        }

        [Test]
        public void When3()
        {
            var map = Choice.When1((int x) => x * 2L)
                            .When2((string s) => s.Length)
                            .When3((DateTime d) => d.Ticks);
            Assert.That(map, Is.Not.Null);

            var r1 = map(Choice1<int, string, DateTime>(42));
            Assert.That(r1, Is.EqualTo(84));

            var r2 = map(Choice2<int, string, DateTime>("foobar"));
            Assert.That(r2, Is.EqualTo(6));

            var r3 = map(Choice3<int, string, DateTime>(new DateTime(1970, 1, 1)));
            Assert.That(r3, Is.EqualTo(621355968000000000L));
        }

        [Test]
        public void When4()
        {
            var map = Choice.When1((int x) => x * 2L)
                            .When2((string s) => s.Length)
                            .When3((DateTime d) => d.Ticks)
                            .When4((char c) => '*');
            Assert.That(map, Is.Not.Null);

            var r1 = map(Choice1<int, string, DateTime, char>(42));
            Assert.That(r1, Is.EqualTo(84));

            var r2 = map(Choice2<int, string, DateTime, char>("foobar"));
            Assert.That(r2, Is.EqualTo(6));

            var r3 = map(Choice3<int, string, DateTime, char>(new DateTime(1970, 1, 1)));
            Assert.That(r3, Is.EqualTo(621355968000000000L));

            var r4 = map(Choice4<int, string, DateTime, char>('*'));
            Assert.That(r4, Is.EqualTo(42));
        }

        [Test]
        public void When5()
        {
            var map = Choice.When1((int x) => "1:" + x.ToString("x"))
                            .When2((int x) => "2:" + x.ToString("x"))
                            .When3((int x) => "3:" + x.ToString("x"))
                            .When4((int x) => "4:" + x.ToString("x"))
                            .When5((int x) => "5:" + x.ToString("x"));
            Assert.That(map, Is.Not.Null);

            var r1 = map(Choice1<int, int, int, int, int>(42));
            Assert.That(r1, Is.EqualTo("1:2a"));

            var r2 = map(Choice2<int, int, int, int, int>(42));
            Assert.That(r2, Is.EqualTo("2:2a"));

            var r3 = map(Choice3<int, int, int, int, int>(42));
            Assert.That(r3, Is.EqualTo("3:2a"));

            var r4 = map(Choice4<int, int, int, int, int>(42));
            Assert.That(r4, Is.EqualTo("4:2a"));

            var r5 = map(Choice5<int, int, int, int, int>(42));
            Assert.That(r5, Is.EqualTo("5:2a"));
        }

        [Test]
        public void SwapFirst()
        {
            var result =
                Choice1<Int1, Int2>(42)
                    .Swap()
                    .Match(_ => throw BadChoice(), x => x);
            Assert.That(result, Is.EqualTo(new Int1(42)));
        }

        [Test]
        public void SwapSecond()
        {
            var result =
                Choice2<Int1, Int2>(42)
                    .Swap()
                    .Match(x => x, _ => throw BadChoice());
            Assert.That(result, Is.EqualTo(new Int2(42)));
        }

        static T AssertNotCalled<T>()
        {
            Assert.Fail("Unexpected call.");
            return default;
        }

        static void Test<TExpected, TChoice, T1, T2>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2>> mapper,
            Func<T1, TExpected> selector) =>
            Test(expected, factory, mapper, selector, _ => AssertNotCalled<TExpected>());

        static void Test<TExpected, TChoice, T1, T2>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2>> mapper,
            Func<T2, TExpected> selector) =>
            Test(expected, factory, mapper, _ => AssertNotCalled<TExpected>(), selector);

        static void Test<TExpected, TChoice, T>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<TExpected, T>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T, TExpected>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2>> mapper,
            Func<T1, TExpected> selector1,
            Func<T2, TExpected> selector2)
        {
            Assert.That(mapper(factory(expected)).Match(selector1, selector2), Is.EqualTo(expected));
        }

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3>> mapper,
            Func<T1, TExpected> selector) =>
            Test(expected, factory, mapper, selector, _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>());

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3>> mapper,
            Func<T2, TExpected> selector) =>
            Test(expected, factory, mapper, _ => AssertNotCalled<TExpected>(), selector, _ => AssertNotCalled<TExpected>());

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3>> mapper,
            Func<T3, TExpected> selector) =>
            Test(expected, factory, mapper, _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>(), selector);

        static void Test<TExpected, TChoice, T1, T2>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<TExpected, T1, T2>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, TExpected, T2>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, TExpected>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3>> mapper,
            Func<T1, TExpected> selector1,
            Func<T2, TExpected> selector2,
            Func<T3, TExpected> selector3)
        {
            Assert.That(mapper(factory(expected)).Match(selector1, selector2, selector3), Is.EqualTo(expected));
        }

        static void Test<TExpected, TChoice, T1, T2, T3, T4>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3, T4>> mapper,
            Func<T1, TExpected> selector) =>
            Test(expected, factory, mapper, selector, _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>());

        static void Test<TExpected, TChoice, T1, T2, T3, T4>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3, T4>> mapper,
            Func<T2, TExpected> selector) =>
            Test(expected, factory, mapper, _ => AssertNotCalled<TExpected>(), selector, _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>());

        static void Test<TExpected, TChoice, T1, T2, T3, T4>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3, T4>> mapper,
            Func<T3, TExpected> selector) =>
            Test(expected, factory, mapper, _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>(), selector, _ => AssertNotCalled<TExpected>());

        static void Test<TExpected, TChoice, T1, T2, T3, T4>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3, T4>> mapper,
            Func<T4, TExpected> selector) =>
            Test(expected, factory, mapper, _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>(), _ => AssertNotCalled<TExpected>(), selector);

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<TExpected, T1, T2, T3>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, TExpected, T2, T3>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, TExpected, T3>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2, T3>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3, TExpected>> selector) =>
            Test(expected, factory, selector, x => x);

        static void Test<TExpected, TChoice, T1, T2, T3, T4>(
            TExpected expected,
            Func<TExpected, TChoice> factory,
            Func<TChoice, Choice<T1, T2, T3, T4>> mapper,
            Func<T1, TExpected> selector1,
            Func<T2, TExpected> selector2,
            Func<T3, TExpected> selector3,
            Func<T4, TExpected> selector4)
        {
            Assert.That(mapper(factory(expected)).Match(selector1, selector2, selector3, selector4), Is.EqualTo(expected));
        }

        [TestFixture]
        public class ChoiceOf3
        {
            [TestFixture]
            public class Right1
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3>, c => c.Right1());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3>,
                             c => c.Right1(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3>,
                             c => c.Right1(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), x => x));
                }
            }

            [TestFixture]
            public class Right2
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3>,
                             c => c.Right2(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3>, c => c.Right2());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3>,
                             c => c.Right2(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), x => x));
                }
            }

            [TestFixture]
            public class Right3
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3>,
                             c => c.Right3(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3>,
                             c => c.Right3(),
                             r => r.Match(_ => AssertNotCalled<Int2>(), x => x));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3>, c => c.Right3());
                }
            }

            [TestFixture]
            public class Forbid1
            {
                [Test]
                public void Choice1()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice1<Int1, Int2, Int3>(42).Forbid1());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3>, c => c.Forbid1());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3>, c => c.Forbid1());
                }
            }

            [TestFixture]
            public class Forbid2
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3>, c => c.Forbid2());
                }

                [Test]
                public void Choice2()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice2<Int1, Int2, Int3>(42).Forbid2());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3>, c => c.Forbid2());
                }
            }

            [TestFixture]
            public class Forbid3
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3>, c => c.Forbid3());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3>, c => c.Forbid3());
                }

                [Test]
                public void Choice3()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice3<Int1, Int2, Int3>(42).Forbid3());
                }
            }
        }

        [TestFixture]
        public class ChoiceOf4
        {
            [TestFixture]
            public class Right1
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>, c => c.Right1());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                             c => c.Right1(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                             c => c.Right1(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                             c => c.Right1(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
                }
            }

            [TestFixture]
            public class Right2
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                             c => c.Right2(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>, c => c.Right2());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                             c => c.Right2(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                             c => c.Right2(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
                }
            }

            [TestFixture]
            public class Right3
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                             c => c.Right3(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                             c => c.Right3(),
                             r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>, c => c.Right3());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>,
                             c => c.Right3(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
                }
            }

            [TestFixture]
            public class Right4
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>,
                             c => c.Right4(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>,
                             c => c.Right4(),
                             r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>,
                             c => c.Right4(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>(), x => x));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>, c => c.Right4());
                }

            }

            [TestFixture]
            public class Forbid1
            {
                [Test]
                public void Choice1()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice1<Int1, Int2, Int3, Int4>(42).Forbid1());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>, c => c.Forbid1());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>, c => c.Forbid1());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>, c => c.Forbid1());
                }
            }

            [TestFixture]
            public class Forbid2
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>, c => c.Forbid2());
                }

                [Test]
                public void Choice2()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice2<Int1, Int2, Int3, Int4>(42).Forbid2());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>, c => c.Forbid2());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>, c => c.Forbid2());
                }
            }

            [TestFixture]
            public class Forbid3
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>, c => c.Forbid3());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>, c => c.Forbid3());
                }

                [Test]
                public void Choice3()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice3<Int1, Int2, Int3, Int4>(42).Forbid3());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4>, c => c.Forbid3());
                }
            }

            [TestFixture]
            public class Forbid4
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4>, c => c.Forbid4());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4>, c => c.Forbid4());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4>, c => c.Forbid4());
                }

                [Test]
                public void Choice4()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice4<Int1, Int2, Int3, Int4>(42).Forbid4());
                }
            }
        }

        [TestFixture]
        public class ChoiceOf5
        {
            [TestFixture]
            public class Right1
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>, c => c.Right1());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right1(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right1(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>()));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right1(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x, _ => AssertNotCalled<Int4>()));
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right1(),
                             r => r.Match(_ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), x => x));
                }
            }

            [TestFixture]
            public class Right2
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right2(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>, c => c.Right2());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right2(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>()));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right2(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x, _ => AssertNotCalled<Int4>()));
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right2(),
                             r => r.Match(_ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), x => x));
                }
            }

            [TestFixture]
            public class Right3
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right3(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right3(),
                             r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>, c => c.Right3());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right3(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x, _ => AssertNotCalled<Int4>()));
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right3(),
                             r => r.Match(_ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), x => x));
                }
            }

            [TestFixture]
            public class Right4
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right4(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right4(),
                             r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right4(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>, c => c.Right4());
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right4(),
                             r => r.Match(_ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), _ => AssertNotCalled<Int5>(), x => x));
                }
            }

            [TestFixture]
            public class Right5
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right5(),
                             r => r.Match(x => x, _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>(), _ => AssertNotCalled<Int1>()));
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right5(),
                             r => r.Match(_ => AssertNotCalled<Int2>(), x => x, _ => AssertNotCalled<Int2>(), _ => AssertNotCalled<Int2>()));
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right5(),
                             r => r.Match(_ => AssertNotCalled<Int3>(), _ => AssertNotCalled<Int3>(), x => x, _ => AssertNotCalled<Int3>()));
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>,
                             c => c.Right5(),
                             r => r.Match(_ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), _ => AssertNotCalled<Int4>(), x => x));
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>, c => c.Right5());
                }
            }

            [TestFixture]
            public class Forbid1
            {
                [Test]
                public void Choice1()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice1<Int1, Int2, Int3, Int4, Int5>(42).Forbid1());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid1());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid1());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid1());
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid1());
                }
            }

            [TestFixture]
            public class Forbid2
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid2());
                }

                [Test]
                public void Choice2()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice2<Int1, Int2, Int3, Int4, Int5>(42).Forbid2());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid2());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid2());
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid2());
                }
            }

            [TestFixture]
            public class Forbid3
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid3());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid3());
                }

                [Test]
                public void Choice3()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice3<Int1, Int2, Int3, Int4, Int5>(42).Forbid3());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid3());
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid3());
                }
            }

            [TestFixture]
            public class Forbid4
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid4());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid4());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid4());
                }

                [Test]
                public void Choice4()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice4<Int1, Int2, Int3, Int4, Int5>(42).Forbid4());
                }

                [Test]
                public void Choice5()
                {
                    Test(new Int5(42), Choice5<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid4());
                }
            }

            [TestFixture]
            public class Forbid5
            {
                [Test]
                public void Choice1()
                {
                    Test(new Int1(42), Choice1<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid5());
                }

                [Test]
                public void Choice2()
                {
                    Test(new Int2(42), Choice2<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid5());
                }

                [Test]
                public void Choice3()
                {
                    Test(new Int3(42), Choice3<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid5());
                }

                [Test]
                public void Choice4()
                {
                    Test(new Int4(42), Choice4<Int1, Int2, Int3, Int4, Int5>, c => c.Forbid5());
                }

                [Test]
                public void Choice5()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        Choice5<Int1, Int2, Int3, Int4, Int5>(42).Forbid5());
                }
            }
        }
    }
}
