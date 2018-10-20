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
    using NUnit.Framework;
    using static Choice.New;
    using Int1 = Box1<int>;
    using Int2 = Box2<int>;
    using Int3 = Box3<int>;
    using Int4 = Box4<int>;

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
        }
    }
}
