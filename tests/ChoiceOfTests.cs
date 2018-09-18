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

    [TestFixture]
    public class ChoiceOfTests
    {
        public class ChoiceOf1
        {
            [Test]
            public void Choice1()
            {
                var c = Choice<int>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                Assert.That(c.Match(x => x * 2), Is.EqualTo(84));
            }

            [Test]
            public void Map()
            {
                var c = Choice<int>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map(x => (char) x).Match(ch => ch);
                Assert.That(result, Is.EqualTo('*'));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void ToString(string input, string expected)
            {
                var actual = Choice<string>.Choice1(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        public class ChoiceOf2
        {
            [Test]
            public void Choice1()
            {
                var c = Choice<int, string>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(84));
            }

            [Test]
            public void Choice2()
            {
                var c = Choice<int, string>.Choice2("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     s => s.ToUpperInvariant());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            [Test]
            public void Map1()
            {
                var c = Choice<int, string>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Map1(x => (char) x)
                              .Match(ch => ch,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo('*'));
            }

            [Test]
            public void Map2()
            {
                var c = Choice<int, string>.Choice2("foobar");
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
                var actual = Choice<string, string>.Choice1(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice2ToString(string input, string expected)
            {
                var actual = Choice<string, string>.Choice2(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        public class ChoiceOf3
        {
            [Test]
            public void Choice1()
            {
                var c = Choice<int, string, DateTime>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(84));
            }

            [Test]
            public void Choice2()
            {
                var c = Choice<int, string, DateTime>.Choice2("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     s => s.ToUpperInvariant(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            [Test]
            public void Choice3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = Choice<int, string, DateTime>.Choice3(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     d => d.Ticks);
                Assert.That(result, Is.EqualTo(date.Ticks));
            }

            [Test]
            public void Map1()
            {
                var c = Choice<int, string, DateTime>.Choice1(42);
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
                var c = Choice<int, string, DateTime>.Choice2("foobar");
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
                var c = Choice<int, string, DateTime>.Choice3(date);
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
                var actual = Choice<string, string, string>.Choice1(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice2ToString(string input, string expected)
            {
                var actual = Choice<string, string, string>.Choice2(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice3ToString(string input, string expected)
            {
                var actual = Choice<string, string, string>.Choice3(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        public class ChoiceOf4
        {
            [Test]
            public void Choice1()
            {
                var c = Choice<int, string, DateTime, char>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(84));
            }

            [Test]
            public void Choice2()
            {
                var c = Choice<int, string, DateTime, char>.Choice2("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     s => s.ToUpperInvariant(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            [Test]
            public void Choice3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = Choice<int, string, DateTime, char>.Choice3(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     d => d.Ticks,
                                     _ => throw BadChoice());
                Assert.That(result, Is.EqualTo(date.Ticks));
            }

            [Test]
            public void Choice4()
            {
                var c = Choice<int, string, DateTime, char>.Choice4('4');
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     _ => throw BadChoice(),
                                     char.GetUnicodeCategory);
                Assert.That(result, Is.EqualTo(UnicodeCategory.DecimalDigitNumber));
            }

            [Test]
            public void Map1()
            {
                var c = Choice<int, string, DateTime, char>.Choice1(42);
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
                var c = Choice<int, string, DateTime, char>.Choice2("foobar");
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
                var c = Choice<int, string, DateTime, char>.Choice3(date);
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
                var c = Choice<int, string, DateTime, char>.Choice4('4');
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
                var actual = Choice<string, string, string, string>.Choice1(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice2ToString(string input, string expected)
            {
                var actual = Choice<string, string, string, string>.Choice2(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice3ToString(string input, string expected)
            {
                var actual = Choice<string, string, string, string>.Choice3(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }

            [TestCase("foobar", "foobar")]
            [TestCase(null, "")]
            public void Choice4ToString(string input, string expected)
            {
                var actual = Choice<string, string, string, string>.Choice4(input).ToString();
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        static Exception BadChoice() => throw new Exception("Invalid choice.");
    }
}
