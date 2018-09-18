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

    [TestFixture]
    public class ChoiceTests
    {
        [Test]
        public void IfTrue()
        {
            var choice =
                Choice.If<int, string>(
                    true,
                    () => 42,
                    () => throw new NotImplementedException());
            Assert.That(choice, Is.Not.Null);
            var result = choice.Match(n => (object) n, s => s);
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void IfFalse()
        {
            var choice =
                Choice.If<int, string>(
                    false,
                    () => throw new NotImplementedException(),
                    () => "foobar");
            Assert.That(choice, Is.Not.Null);
            var result = choice.Match(n => (object) n, s => s);
            Assert.That(result, Is.EqualTo("foobar"));
        }

        [TestCase(1, 42)]
        [TestCase(2, "foo")]
        [TestCase(3, 4.2)]
        public void If2(int i, object expected)
        {
            Assert.That(i, Is.GreaterThanOrEqualTo(1));
            Assert.That(i, Is.LessThanOrEqualTo(3));

            var choice =
                Choice.If(
                    i == 1,
                    () => 42,
                    () =>
                        Choice.If(
                            i == 2,
                            () => "foo",
                            () => 4.2));

            Assert.That(choice, Is.Not.Null);
            var actual = choice.Match(n => (object) n, s => s, d => d);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1, 42)]
        [TestCase(2, "foo")]
        [TestCase(3, 4.2)]
        [TestCase(4, '*')]
        public void If3(int i, object expected)
        {
            Assert.That(i, Is.GreaterThanOrEqualTo(1));
            Assert.That(i, Is.LessThanOrEqualTo(4));

            var choice =
                Choice.If(
                    i == 1,
                    () => 42,
                    () =>
                        Choice.If(
                            i == 2,
                            () => "foo",
                            () =>
                                Choice.If(
                                    i == 3,
                                    () => 4.2,
                                    () => '*')));

            Assert.That(choice, Is.Not.Null);
            var actual = choice.Match(n => (object) n, s => s, d => d, ch => ch);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void When1()
        {
            var map = Choice.When1((int x) => x * 2);
            Assert.That(map, Is.Not.Null);
            var result = map(Choice<int>.Choice1(42));
            Assert.That(result, Is.EqualTo(84));
        }

        [Test]
        public void When2()
        {
            var map = Choice.When1((int x) => x * 2)
                            .When2((string s) => s.Length);
            Assert.That(map, Is.Not.Null);

            var r1 = map(Choice<int, string>.Choice1(42));
            Assert.That(r1, Is.EqualTo(84));

            var r2 = map(Choice<int, string>.Choice2("foobar"));
            Assert.That(r2, Is.EqualTo(6));
        }

        [Test]
        public void When3()
        {
            var map = Choice.When1((int x) => x * 2L)
                            .When2((string s) => s.Length)
                            .When3((DateTime d) => d.Ticks);
            Assert.That(map, Is.Not.Null);

            var r1 = map(Choice<int, string, DateTime>.Choice1(42));
            Assert.That(r1, Is.EqualTo(84));

            var r2 = map(Choice<int, string, DateTime>.Choice2("foobar"));
            Assert.That(r2, Is.EqualTo(6));

            var r3 = map(Choice<int, string, DateTime>.Choice3(new DateTime(1970, 1, 1)));
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

            var r1 = map(Choice<int, string, DateTime, char>.Choice1(42));
            Assert.That(r1, Is.EqualTo(84));

            var r2 = map(Choice<int, string, DateTime, char>.Choice2("foobar"));
            Assert.That(r2, Is.EqualTo(6));

            var r3 = map(Choice<int, string, DateTime, char>.Choice3(new DateTime(1970, 1, 1)));
            Assert.That(r3, Is.EqualTo(621355968000000000L));

            var r4 = map(Choice<int, string, DateTime, char>.Choice4('*'));
            Assert.That(r4, Is.EqualTo(42));
        }
    }
}
