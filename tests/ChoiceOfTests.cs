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
    using System.Globalization;
    using NUnit.Framework;
    using static Choice.New;

    [TestFixture]
    public class ChoiceOfTests
    {
        public class ChoiceOf1
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<int>(42);
                Assert.That(c, Is.Not.Null);
                Assert.That(c.Match(x => x * 2), Is.EqualTo(84));
            }

            [Test]
            public void Map()
            {
                var c = Choice1<int>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map(x => (char) x).Match(ch => ch);
                Assert.That(result, Is.EqualTo('*'));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void ToString(string input, string expected)
            {
                var actual = Choice1<string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            public class Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<string>("foobar");
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<string>("foobar");
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<string>("foobar");
                    var y = Choice1<string>("foobar");
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<string>("foobar");
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void OtherValue()
                {
                    var foo = Choice1<string>("foo");
                    var bar = Choice1<string>("bar");
                    Assert.False(foo.Equals((object) bar));
                    Assert.False(foo.Equals(bar));
                }
            }
        }

        public class ChoiceOf2
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<int, string>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(84));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<int, string>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<int, string>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<int, string>(42);
                    var y = Choice1<int, string>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<int, string>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice2()
                {
                    var choice1 = Choice1<int, int>(42);
                    var choice2 = Choice2<int, int>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<int, string>(123);
                    var y = Choice1<int, string>(456);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<int, string>("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     s => s.ToUpperInvariant());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<int, string>("foobar");
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<int, string>("foobar");
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<int, string>("foobar");
                    var y = Choice2<int, string>("foobar");
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<int, string>("foobar");
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice1()
                {
                    var choice2 = Choice2<string, string>("foobar");
                    var choice1 = Choice1<string, string>("foobar");
                    Assert.False(choice2.Equals(choice1));
                }

                [Test]
                public void OtherValue()
                {
                    var foo = Choice2<int, string>("foo");
                    var bar = Choice2<int, string>("bar");
                    Assert.False(foo.Equals((object) bar));
                    Assert.False(foo.Equals(bar));
                }
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<int, string>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => (char) x)
                              .Match(ch => ch,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo('*'));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<int, string>("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(s => s.Length)
                              .Match(_ => throw BadChoice(),
                                     x => x);
                Assert.That(result, Is.EqualTo(6));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice1ToString(string input, string expected)
            {
                var actual = Choice1<string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice2ToString(string input, string expected)
            {
                var actual = Choice2<string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        public class ChoiceOf3
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<int, string, DateTime>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(84));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<int, string, DateTime>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<int, string, DateTime>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<int, string, DateTime>(42);
                    var y = Choice1<int, string, DateTime>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<int, string, DateTime>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice2()
                {
                    var choice1 = Choice1<int, int, int>(42);
                    var choice2 = Choice2<int, int, int>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void Choice3()
                {
                    var choice1 = Choice1<int, int, int>(42);
                    var choice3 = Choice3<int, int, int>(42);
                    Assert.False(choice1.Equals(choice3));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<int, string, DateTime>(123);
                    var y = Choice1<int, string, DateTime>(456);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<int, string, DateTime>("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     s => s.ToUpperInvariant(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<int, string, DateTime>("foobar");
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<int, string, DateTime>("foobar");
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<int, string, DateTime>("foobar");
                    var y = Choice2<int, string, DateTime>("foobar");
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<int, string, DateTime>("foobar");
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice1()
                {
                    var choice2 = Choice2<int, int, int>(42);
                    var choice1 = Choice1<int, int, int>(42);
                    Assert.False(choice2.Equals(choice1));
                }

                [Test]
                public void Choice3()
                {
                    var choice2 = Choice2<int, int, int>(42);
                    var choice3 = Choice1<int, int, int>(42);
                    Assert.False(choice2.Equals(choice3));
                }

                [Test]
                public void OtherValue()
                {
                    var foo = Choice2<int, string, DateTime>("foo");
                    var bar = Choice2<int, string, DateTime>("bar");
                    Assert.False(foo.Equals((object) bar));
                    Assert.False(foo.Equals(bar));
                }
            }

            [Test]
            public void Choice3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = Choice3<int, string, DateTime>(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     d => d.Ticks);
                Assert.That(result, Is.EqualTo(date.Ticks));
            }

            public class Choice3Equality
            {
                [Test]
                public void Null()
                {
                    var date = new DateTime(1970, 1, 1);
                    var choice = Choice3<int, string, DateTime>(date);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var date = new DateTime(1970, 1, 1);
                    var choice = Choice3<int, string, DateTime>(date);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var date = new DateTime(1970, 1, 1);
                    var x = Choice3<int, string, DateTime>(date);
                    var y = Choice3<int, string, DateTime>(date);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice3Inequality
            {
                [Test]
                public void OtherType()
                {
                    var date = new DateTime(1970, 1, 1);
                    var choice = Choice3<int, string, DateTime>(date);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice1()
                {
                    var choice3 = Choice3<int, int, int>(42);
                    var choice1 = Choice1<int, int, int>(42);
                    Assert.False(choice3.Equals(choice1));
                }

                [Test]
                public void Choice2()
                {
                    var choice3 = Choice3<int, int, int>(42);
                    var choice2 = Choice1<int, int, int>(42);
                    Assert.False(choice3.Equals(choice2));
                }

                [Test]
                public void OtherValue()
                {
                    var date = new DateTime(1970, 1, 1);
                    var a = Choice3<int, string, DateTime>(date);
                    var b = Choice3<int, string, DateTime>(date.AddYears((1)));
                    Assert.False(a.Equals((object) b));
                    Assert.False(a.Equals(b));
                }
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<int, string, DateTime>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => (char) x)
                              .Match(ch => ch,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo('*'));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<int, string, DateTime>("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(s => s.Length)
                              .Match(_ => throw BadChoice(),
                                     x => x,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(6));
            }

            [Test]
            public void Map3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = Choice3<int, string, DateTime>(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Map3(d => d.Year)
                              .Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     x => x);
                Assert.That(result, Is.EqualTo(1970));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice1ToString(string input, string expected)
            {
                var actual = Choice1<string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice2ToString(string input, string expected)
            {
                var actual = Choice2<string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice3ToString(string input, string expected)
            {
                var actual = Choice3<string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        public class ChoiceOf4
        {
            [Test]
            public void Choice1()
            {
                var c = Choice1<int, string, DateTime, char>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(84));
            }

            public class Choice1Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice1<int, string, DateTime, char>(42);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice1<int, string, DateTime, char>(42);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice1<int, string, DateTime, char>(42);
                    var y = Choice1<int, string, DateTime, char>(42);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice1Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice1<int, string, DateTime, char>(42);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice2()
                {
                    var choice1 = Choice1<int, int, int, int>(42);
                    var choice2 = Choice2<int, int, int, int>(42);
                    Assert.False(choice1.Equals(choice2));
                }

                [Test]
                public void Choice3()
                {
                    var choice1 = Choice1<int, int, int, int>(42);
                    var choice3 = Choice3<int, int, int, int>(42);
                    Assert.False(choice1.Equals(choice3));
                }

                [Test]
                public void Choice4()
                {
                    var choice1 = Choice1<int, int, int, int>(42);
                    var choice4 = Choice4<int, int, int, int>(42);
                    Assert.False(choice1.Equals(choice4));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice1<int, string, DateTime, char>(123);
                    var y = Choice1<int, string, DateTime, char>(456);
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice2()
            {
                var c = Choice2<int, string, DateTime, char>("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     s => s.ToUpperInvariant(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            public class Choice2Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice2<int, string, DateTime, char>("foobar");
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice2<int, string, DateTime, char>("foobar");
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice2<int, string, DateTime, char>("foobar");
                    var y = Choice2<int, string, DateTime, char>("foobar");
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice2Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice2<int, string, DateTime, char>("foobar");
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice1()
                {
                    var choice2 = Choice2<int, int, int, int>(42);
                    var choice1 = Choice1<int, int, int, int>(42);
                    Assert.False(choice2.Equals(choice1));
                }

                [Test]
                public void Choice3()
                {
                    var choice2 = Choice2<int, int, int, int>(42);
                    var choice3 = Choice3<int, int, int, int>(42);
                    Assert.False(choice2.Equals(choice3));
                }

                [Test]
                public void Choice4()
                {
                    var choice2 = Choice2<int, int, int, int>(42);
                    var choice4 = Choice4<int, int, int, int>(42);
                    Assert.False(choice2.Equals(choice4));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice2<int, string, DateTime, char>("foo");
                    var y = Choice2<int, string, DateTime, char>("bar");
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = Choice3<int, string, DateTime, char>(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     d => d.Ticks,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(date.Ticks));
            }

            public class Choice3Equality
            {
                [Test]
                public void Null()
                {
                    var date = new DateTime(1970, 1, 1);
                    var choice = Choice3<int, string, DateTime, char>(date);
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var date = new DateTime(1970, 1, 1);
                    var choice = Choice3<int, string, DateTime, char>(date);
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var date = new DateTime(1970, 1, 1);
                    var x = Choice3<int, string, DateTime, char>(date);
                    var y = Choice3<int, string, DateTime, char>(date);
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice3Inequality
            {
                [Test]
                public void OtherType()
                {
                    var date = new DateTime(1970, 1, 1);
                    var choice = Choice3<int, string, DateTime, char>(date);
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice1()
                {
                    var choice3 = Choice3<int, int, int, int>(42);
                    var choice1 = Choice1<int, int, int, int>(42);
                    Assert.False(choice3.Equals(choice1));
                }

                [Test]
                public void Choice2()
                {
                    var choice3 = Choice3<int, int, int, int>(42);
                    var choice2 = Choice2<int, int, int, int>(42);
                    Assert.False(choice3.Equals(choice2));
                }

                [Test]
                public void Choice4()
                {
                    var choice3 = Choice3<int, int, int, int>(42);
                    var choice4 = Choice4<int, int, int, int>(42);
                    Assert.False(choice3.Equals(choice4));
                }

                [Test]
                public void OtherValue()
                {
                    var date = new DateTime(1970, 1, 1);
                    var x = Choice3<int, string, DateTime, char>(date);
                    var y = Choice3<int, string, DateTime, char>(date.AddYears(1));
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Choice4()
            {
                var c = Choice4<int, string, DateTime, char>('4');
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     char.GetUnicodeCategory);
                Assert.That(result, Is.EqualTo(UnicodeCategory.DecimalDigitNumber));
            }

            public class Choice4Equality
            {
                [Test]
                public void Null()
                {
                    var choice = Choice4<int, string, DateTime, char>('*');
                    Assert.False(choice.Equals((object) null));
                    Assert.False(choice.Equals(null));
                }

                [Test]
                public void Self()
                {
                    var choice = Choice4<int, string, DateTime, char>('*');
                    Assert.True(choice.Equals((object) choice));
                    Assert.True(choice.Equals(choice));
                }

                [Test]
                public void Value()
                {
                    var x = Choice4<int, string, DateTime, char>('*');
                    var y = Choice4<int, string, DateTime, char>('*');
                    Assert.True(x.Equals((object) y));
                    Assert.True(x.Equals(y));
                }
            }

            public class Choice4Inequality
            {
                [Test]
                public void OtherType()
                {
                    var choice = Choice4<int, string, DateTime, char>('*');
                    Assert.False(choice.Equals(new object()));
                }

                [Test]
                public void Choice1()
                {
                    var choice4 = Choice4<int, int, int, int>(42);
                    var choice1 = Choice1<int, int, int, int>(42);
                    Assert.False(choice4.Equals(choice1));
                }

                [Test]
                public void Choice2()
                {
                    var choice4 = Choice4<int, int, int, int>(42);
                    var choice2 = Choice2<int, int, int, int>(42);
                    Assert.False(choice4.Equals(choice2));
                }

                [Test]
                public void Choice3()
                {
                    var choice4 = Choice4<int, int, int, int>(42);
                    var choice3 = Choice3<int, int, int, int>(42);
                    Assert.False(choice4.Equals(choice3));
                }

                [Test]
                public void OtherValue()
                {
                    var x = Choice4<int, string, DateTime, char>('*');
                    var y = Choice4<int, string, DateTime, char>('!');
                    Assert.False(x.Equals((object) y));
                    Assert.False(x.Equals(y));
                }
            }

            [Test]
            public void Map1()
            {
                var c = Choice1<int, string, DateTime, char>(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => (char) x)
                              .Match(ch => ch,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo('*'));
            }

            [Test]
            public void Map2()
            {
                var c = Choice2<int, string, DateTime, char>("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Map2(s => s.Length)
                              .Match(_ => throw BadChoice(),
                                     x => x,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(6));
            }

            [Test]
            public void Map3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = Choice3<int, string, DateTime, char>(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Map3(d => d.Ticks)
                              .Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     x => x,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(date.Ticks));
            }

            [Test]
            public void Map4()
            {
                var c = Choice4<int, string, DateTime, char>('4');
                Assert.That(c, Is.Not.Null);
                var result = c.Map4(char.GetUnicodeCategory)
                              .Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     u => u);
                Assert.That(result, Is.EqualTo(UnicodeCategory.DecimalDigitNumber));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice1ToString(string input, string expected)
            {
                var actual = Choice1<string, string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice2ToString(string input, string expected)
            {
                var actual = Choice2<string, string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice3ToString(string input, string expected)
            {
                var actual = Choice3<string, string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice4ToString(string input, string expected)
            {
                var actual = Choice4<string, string, string, string>(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        static Exception BadChoice() => throw new Exception("Invalid choice.");
    }
}
