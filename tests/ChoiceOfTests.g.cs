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
    using Str1 = Box1<string>;
    using Str2 = Box2<string>;
    using Str3 = Box3<string>;
    using Str4 = Box4<string>;
    using Str5 = Box5<string>;

    [TestFixture]
    partial class ChoiceOfTests
    {
        public class ChoiceOf2
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<Int1, Int2>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x.Value, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<Int1, Int2>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => x.Value.ToString("x"))
                              .Match(x => x, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice1ToString()
            {
                var actual = Choice1<Int1, Int2>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice1Map()
            {
                var c = Choice1<Int1, Int2>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Str1, Str2>(x => new Str1(x.Value.ToString("x")), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice1<Str1, Str2>(new Str1("2a"))));
            }

            [Test]
            public void Choice1GetHashCode()
            {
                var choice1 = Choice1<Int1, Int2>(42);
                var choice2 = Choice2<Int1, Int2>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<Int1, Int2>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<Int1, Int2>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<Int1, Int2>(42);
                    var y = Choice1<Int1, Int2>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<Int1, Int2>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice1<Int1, Int2>(42);
                    var choice2 = Choice2<Int1, Int2>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<Int1, Int2>(4);
                    var y = Choice1<Int1, Int2>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<Int1, Int2>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), x => x.Value);
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<Int1, Int2>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), x => x);
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice2ToString()
            {
                var actual = Choice2<Int1, Int2>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice2Map()
            {
                var c = Choice2<Int1, Int2>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Str1, Str2>(_ => throw BadChoice(), x => new Str2(x.Value.ToString("x")));
                Assert.That(result, Is.EqualTo(Choice2<Str1, Str2>(new Str2("2a"))));
            }

            [Test]
            public void Choice2GetHashCode()
            {
                var choice1 = Choice2<Int1, Int2>(42);
                var choice2 = Choice1<Int1, Int2>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<Int1, Int2>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<Int1, Int2>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<Int1, Int2>(42);
                    var y = Choice2<Int1, Int2>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<Int1, Int2>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice2<Int1, Int2>(42);
                    var choice2 = Choice1<Int1, Int2>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice2<Int1, Int2>(4);
                    var y = Choice2<Int1, Int2>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void TuplesToChoices()
            {
                var choices = Tuple.Create<Int1, Int2>(1, 2).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2>(1),
                    Choice2<Int1, Int2>(2),
                }));
            }

            [Test]
            public void ValueTuplesToChoices()
            {
                var choices = ((Int1) 1, (Int2) 2).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2>(1),
                    Choice2<Int1, Int2>(2),
                }));
            }
        }

        public class ChoiceOf3
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x.Value, _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => x.Value.ToString("x"))
                              .Match(x => x, _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice1ToString()
            {
                var actual = Choice1<Int1, Int2, Int3>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice1Map()
            {
                var c = Choice1<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Str1, Str2, Str3>(x => new Str1(x.Value.ToString("x")), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice1<Str1, Str2, Str3>(new Str1("2a"))));
            }

            [Test]
            public void Choice1GetHashCode()
            {
                var choice1 = Choice1<Int1, Int2, Int3>(42);
                var choice2 = Choice2<Int1, Int2, Int3>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<Int1, Int2, Int3>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<Int1, Int2, Int3>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<Int1, Int2, Int3>(42);
                    var y = Choice1<Int1, Int2, Int3>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<Int1, Int2, Int3>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice1<Int1, Int2, Int3>(42);
                    var choice2 = Choice2<Int1, Int2, Int3>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<Int1, Int2, Int3>(4);
                    var y = Choice1<Int1, Int2, Int3>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), x => x.Value, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), x => x, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice2ToString()
            {
                var actual = Choice2<Int1, Int2, Int3>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice2Map()
            {
                var c = Choice2<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Str1, Str2, Str3>(_ => throw BadChoice(), x => new Str2(x.Value.ToString("x")), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice2<Str1, Str2, Str3>(new Str2("2a"))));
            }

            [Test]
            public void Choice2GetHashCode()
            {
                var choice1 = Choice2<Int1, Int2, Int3>(42);
                var choice2 = Choice1<Int1, Int2, Int3>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<Int1, Int2, Int3>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<Int1, Int2, Int3>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<Int1, Int2, Int3>(42);
                    var y = Choice2<Int1, Int2, Int3>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<Int1, Int2, Int3>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice2<Int1, Int2, Int3>(42);
                    var choice2 = Choice1<Int1, Int2, Int3>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice2<Int1, Int2, Int3>(4);
                    var y = Choice2<Int1, Int2, Int3>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice3()
            {
                var c = Choice3<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), _ => throw BadChoice(), x => x.Value);
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map3()
            {
                var c = Choice3<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map3(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), _ => throw BadChoice(), x => x);
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice3ToString()
            {
                var actual = Choice3<Int1, Int2, Int3>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice3Map()
            {
                var c = Choice3<Int1, Int2, Int3>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Str1, Str2, Str3>(_ => throw BadChoice(), _ => throw BadChoice(), x => new Str3(x.Value.ToString("x")));
                Assert.That(result, Is.EqualTo(Choice3<Str1, Str2, Str3>(new Str3("2a"))));
            }

            [Test]
            public void Choice3GetHashCode()
            {
                var choice1 = Choice3<Int1, Int2, Int3>(42);
                var choice2 = Choice1<Int1, Int2, Int3>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice3Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice3<Int1, Int2, Int3>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice3<Int1, Int2, Int3>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice3<Int1, Int2, Int3>(42);
                    var y = Choice3<Int1, Int2, Int3>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice3Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice3<Int1, Int2, Int3>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice3<Int1, Int2, Int3>(42);
                    var choice2 = Choice1<Int1, Int2, Int3>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice3<Int1, Int2, Int3>(4);
                    var y = Choice3<Int1, Int2, Int3>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void TuplesToChoices()
            {
                var choices = Tuple.Create<Int1, Int2, Int3>(1, 2, 3).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2, Int3>(1),
                    Choice2<Int1, Int2, Int3>(2),
                    Choice3<Int1, Int2, Int3>(3),
                }));
            }

            [Test]
            public void ValueTuplesToChoices()
            {
                var choices = ((Int1) 1, (Int2) 2, (Int3) 3).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2, Int3>(1),
                    Choice2<Int1, Int2, Int3>(2),
                    Choice3<Int1, Int2, Int3>(3),
                }));
            }
        }

        public class ChoiceOf4
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x.Value, _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => x.Value.ToString("x"))
                              .Match(x => x, _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice1ToString()
            {
                var actual = Choice1<Int1, Int2, Int3, Int4>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice1Map()
            {
                var c = Choice1<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Str1, Str2, Str3, Str4>(x => new Str1(x.Value.ToString("x")), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice1<Str1, Str2, Str3, Str4>(new Str1("2a"))));
            }

            [Test]
            public void Choice1GetHashCode()
            {
                var choice1 = Choice1<Int1, Int2, Int3, Int4>(42);
                var choice2 = Choice2<Int1, Int2, Int3, Int4>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<Int1, Int2, Int3, Int4>(42);
                    var y = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice1<Int1, Int2, Int3, Int4>(42);
                    var choice2 = Choice2<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<Int1, Int2, Int3, Int4>(4);
                    var y = Choice1<Int1, Int2, Int3, Int4>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), x => x.Value, _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), x => x, _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice2ToString()
            {
                var actual = Choice2<Int1, Int2, Int3, Int4>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice2Map()
            {
                var c = Choice2<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Str1, Str2, Str3, Str4>(_ => throw BadChoice(), x => new Str2(x.Value.ToString("x")), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice2<Str1, Str2, Str3, Str4>(new Str2("2a"))));
            }

            [Test]
            public void Choice2GetHashCode()
            {
                var choice1 = Choice2<Int1, Int2, Int3, Int4>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<Int1, Int2, Int3, Int4>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<Int1, Int2, Int3, Int4>(42);
                    var y = Choice2<Int1, Int2, Int3, Int4>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice2<Int1, Int2, Int3, Int4>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice2<Int1, Int2, Int3, Int4>(4);
                    var y = Choice2<Int1, Int2, Int3, Int4>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice3()
            {
                var c = Choice3<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), _ => throw BadChoice(), x => x.Value, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map3()
            {
                var c = Choice3<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map3(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), _ => throw BadChoice(), x => x, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice3ToString()
            {
                var actual = Choice3<Int1, Int2, Int3, Int4>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice3Map()
            {
                var c = Choice3<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Str1, Str2, Str3, Str4>(_ => throw BadChoice(), _ => throw BadChoice(), x => new Str3(x.Value.ToString("x")), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice3<Str1, Str2, Str3, Str4>(new Str3("2a"))));
            }

            [Test]
            public void Choice3GetHashCode()
            {
                var choice1 = Choice3<Int1, Int2, Int3, Int4>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice3Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice3<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice3<Int1, Int2, Int3, Int4>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice3<Int1, Int2, Int3, Int4>(42);
                    var y = Choice3<Int1, Int2, Int3, Int4>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice3Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice3<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice3<Int1, Int2, Int3, Int4>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice3<Int1, Int2, Int3, Int4>(4);
                    var y = Choice3<Int1, Int2, Int3, Int4>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice4()
            {
                var c = Choice4<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => x.Value);
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map4()
            {
                var c = Choice4<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map4(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => x);
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice4ToString()
            {
                var actual = Choice4<Int1, Int2, Int3, Int4>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice4Map()
            {
                var c = Choice4<Int1, Int2, Int3, Int4>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Str1, Str2, Str3, Str4>(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => new Str4(x.Value.ToString("x")));
                Assert.That(result, Is.EqualTo(Choice4<Str1, Str2, Str3, Str4>(new Str4("2a"))));
            }

            [Test]
            public void Choice4GetHashCode()
            {
                var choice1 = Choice4<Int1, Int2, Int3, Int4>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice4Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice4<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice4<Int1, Int2, Int3, Int4>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice4<Int1, Int2, Int3, Int4>(42);
                    var y = Choice4<Int1, Int2, Int3, Int4>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice4Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice4<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice4<Int1, Int2, Int3, Int4>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice4<Int1, Int2, Int3, Int4>(4);
                    var y = Choice4<Int1, Int2, Int3, Int4>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void TuplesToChoices()
            {
                var choices = Tuple.Create<Int1, Int2, Int3, Int4>(1, 2, 3, 4).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2, Int3, Int4>(1),
                    Choice2<Int1, Int2, Int3, Int4>(2),
                    Choice3<Int1, Int2, Int3, Int4>(3),
                    Choice4<Int1, Int2, Int3, Int4>(4),
                }));
            }

            [Test]
            public void ValueTuplesToChoices()
            {
                var choices = ((Int1) 1, (Int2) 2, (Int3) 3, (Int4) 4).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2, Int3, Int4>(1),
                    Choice2<Int1, Int2, Int3, Int4>(2),
                    Choice3<Int1, Int2, Int3, Int4>(3),
                    Choice4<Int1, Int2, Int3, Int4>(4),
                }));
            }
        }

        public class ChoiceOf5
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x.Value, _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => x.Value.ToString("x"))
                              .Match(x => x, _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice1ToString()
            {
                var actual = Choice1<Int1, Int2, Int3, Int4, Int5>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice1Map()
            {
                var c = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Int5, Str1, Str2, Str3, Str4, Str5>(x => new Str1(x.Value.ToString("x")), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice1<Str1, Str2, Str3, Str4, Str5>(new Str1("2a"))));
            }

            [Test]
            public void Choice1GetHashCode()
            {
                var choice1 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                var choice2 = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    var y = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    var choice2 = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<Int1, Int2, Int3, Int4, Int5>(4);
                    var y = Choice1<Int1, Int2, Int3, Int4, Int5>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), x => x.Value, _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), x => x, _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice2ToString()
            {
                var actual = Choice2<Int1, Int2, Int3, Int4, Int5>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice2Map()
            {
                var c = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Int5, Str1, Str2, Str3, Str4, Str5>(_ => throw BadChoice(), x => new Str2(x.Value.ToString("x")), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice2<Str1, Str2, Str3, Str4, Str5>(new Str2("2a"))));
            }

            [Test]
            public void Choice2GetHashCode()
            {
                var choice1 = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    var y = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice2<Int1, Int2, Int3, Int4, Int5>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice2<Int1, Int2, Int3, Int4, Int5>(4);
                    var y = Choice2<Int1, Int2, Int3, Int4, Int5>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice3()
            {
                var c = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), _ => throw BadChoice(), x => x.Value, _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map3()
            {
                var c = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map3(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), _ => throw BadChoice(), x => x, _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice3ToString()
            {
                var actual = Choice3<Int1, Int2, Int3, Int4, Int5>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice3Map()
            {
                var c = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Int5, Str1, Str2, Str3, Str4, Str5>(_ => throw BadChoice(), _ => throw BadChoice(), x => new Str3(x.Value.ToString("x")), _ => throw BadChoice(), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice3<Str1, Str2, Str3, Str4, Str5>(new Str3("2a"))));
            }

            [Test]
            public void Choice3GetHashCode()
            {
                var choice1 = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice3Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                    var y = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice3Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice3<Int1, Int2, Int3, Int4, Int5>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice3<Int1, Int2, Int3, Int4, Int5>(4);
                    var y = Choice3<Int1, Int2, Int3, Int4, Int5>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice4()
            {
                var c = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => x.Value, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map4()
            {
                var c = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map4(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => x, _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice4ToString()
            {
                var actual = Choice4<Int1, Int2, Int3, Int4, Int5>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice4Map()
            {
                var c = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Int5, Str1, Str2, Str3, Str4, Str5>(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => new Str4(x.Value.ToString("x")), _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(Choice4<Str1, Str2, Str3, Str4, Str5>(new Str4("2a"))));
            }

            [Test]
            public void Choice4GetHashCode()
            {
                var choice1 = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice4Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                    var y = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice4Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice4<Int1, Int2, Int3, Int4, Int5>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice4<Int1, Int2, Int3, Int4, Int5>(4);
                    var y = Choice4<Int1, Int2, Int3, Int4, Int5>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice5()
            {
                var c = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => x.Value);
                Assert.That(result, Is.EqualTo(42));
            }

            [Test]
            public void Map5()
            {
                var c = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map5(x => x.Value.ToString("x"))
                              .Match(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => x);
                Assert.That(result, Is.EqualTo("2a"));
            }

            [Test]
            public void Choice5ToString()
            {
                var actual = Choice5<Int1, Int2, Int3, Int4, Int5>(42).ToString();
                Assert.That(actual, Is.EqualTo("42"));
            }

            [Test]
            public void Choice5Map()
            {
                var c = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map<Int1, Int2, Int3, Int4, Int5, Str1, Str2, Str3, Str4, Str5>(_ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), _ => throw BadChoice(), x => new Str5(x.Value.ToString("x")));
                Assert.That(result, Is.EqualTo(Choice5<Str1, Str2, Str3, Str4, Str5>(new Str5("2a"))));
            }

            [Test]
            public void Choice5GetHashCode()
            {
                var choice1 = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                Assert.That(choice1.GetHashCode(), Is.Not.EqualTo(choice2.GetHashCode()));
            }

            public class Choice5Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                    var y = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice5Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherChoice()
                {
                    var choice1 = Choice5<Int1, Int2, Int3, Int4, Int5>(42);
                    var choice2 = Choice1<Int1, Int2, Int3, Int4, Int5>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice5<Int1, Int2, Int3, Int4, Int5>(4);
                    var y = Choice5<Int1, Int2, Int3, Int4, Int5>(2);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void TuplesToChoices()
            {
                var choices = Tuple.Create<Int1, Int2, Int3, Int4, Int5>(1, 2, 3, 4, 5).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2, Int3, Int4, Int5>(1),
                    Choice2<Int1, Int2, Int3, Int4, Int5>(2),
                    Choice3<Int1, Int2, Int3, Int4, Int5>(3),
                    Choice4<Int1, Int2, Int3, Int4, Int5>(4),
                    Choice5<Int1, Int2, Int3, Int4, Int5>(5),
                }));
            }

            [Test]
            public void ValueTuplesToChoices()
            {
                var choices = ((Int1) 1, (Int2) 2, (Int3) 3, (Int4) 4, (Int5) 5).ToChoices();

                Assert.That(choices, Is.EqualTo(new[]
                {
                    Choice1<Int1, Int2, Int3, Int4, Int5>(1),
                    Choice2<Int1, Int2, Int3, Int4, Int5>(2),
                    Choice3<Int1, Int2, Int3, Int4, Int5>(3),
                    Choice4<Int1, Int2, Int3, Int4, Int5>(4),
                    Choice5<Int1, Int2, Int3, Int4, Int5>(5),
                }));
            }
        }
    }
}
