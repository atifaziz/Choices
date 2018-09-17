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
                var c = ChoiceOf1<int>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                Assert.That(c.Match(x => x * 2), Is.EqualTo(84));
            }
        }

        public class ChoiceOf2
        {
            [Test]
            public void Choice1()
            {
                var c = ChoiceOf2<int, string>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw new NotImplementedException());
                Assert.That(result, Is.EqualTo(84));
            }

            [Test]
            public void Choice2()
            {
                var c = ChoiceOf2<int, string>.Choice2("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw new NotImplementedException(),
                                     s => s.ToUpperInvariant());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }
        }

        public class ChoiceOf3
        {
            [Test]
            public void Choice1()
            {
                var c = ChoiceOf3<int, string, DateTime>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException());
                Assert.That(result, Is.EqualTo(84));
            }

            [Test]
            public void Choice2()
            {
                var c = ChoiceOf3<int, string, DateTime>.Choice2("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw new NotImplementedException(),
                                     s => s.ToUpperInvariant(),
                                     _ => throw new NotImplementedException());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            [Test]
            public void Choice3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = ChoiceOf3<int, string, DateTime>.Choice3(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException(),
                                     d => d.Ticks);
                Assert.That(result, Is.EqualTo(date.Ticks));
            }
        }

        public class ChoiceOf4
        {
            [Test]
            public void Choice1()
            {
                var c = ChoiceOf4<int, string, DateTime, char>.Choice1(42);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(x => x * 2,
                                     _ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException());
                Assert.That(result, Is.EqualTo(84));
            }

            [Test]
            public void Choice2()
            {
                var c = ChoiceOf4<int, string, DateTime, char>.Choice2("foobar");
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw new NotImplementedException(),
                                     s => s.ToUpperInvariant(),
                                     _ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException());
                Assert.That(result, Is.EqualTo("FOOBAR"));
            }

            [Test]
            public void Choice3()
            {
                var date = new DateTime(1970, 1, 1);
                var c = ChoiceOf4<int, string, DateTime, char>.Choice3(date);
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException(),
                                     d => d.Ticks,
                                     _ => throw new NotImplementedException());
                Assert.That(result, Is.EqualTo(date.Ticks));
            }

            [Test]
            public void Choice4()
            {
                var c = ChoiceOf4<int, string, DateTime, char>.Choice4('4');
                Assert.That(c, Is.Not.Null);
                var result = c.Match(_ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException(),
                                     _ => throw new NotImplementedException(),
                                     char.GetUnicodeCategory);
                Assert.That(result, Is.EqualTo(UnicodeCategory.DecimalDigitNumber));
            }
        }
    }
}
