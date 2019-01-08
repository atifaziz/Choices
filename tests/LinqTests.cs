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
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Choices.Linq.Right.Tests
{
    using System;
    using System.Globalization;
    using NUnit.Framework;
    using static Choice.New;

    [TestFixture]
    public class LinqTests
    {
        static Choice<FormatException, int> TryParseInt32(string s) =>
            int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var n)
            ? Choice2<FormatException, int>(n)
            : Choice1<FormatException, int>(new FormatException($"\"{s}\" is not a valid signed integer."));

        static T AssertNotCalled<T>()
        {
            Assert.Fail("Unexpected call.");
            return default;
        }

        [Test]
        public void Return()
        {
            var result =
                RightResult
                    .Return<Exception, int>(42)
                    .Match(_ => AssertNotCalled<int>(),
                           x => x);

            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void BindLeft()
        {
            var error =
                TryParseInt32("forty-two")
                    .Bind(_ => AssertNotCalled<Choice<FormatException, object>>())
                    .Match(e => e, _ => AssertNotCalled<FormatException>());

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo("\"forty-two\" is not a valid signed integer."));
        }

        [Test]
        public void BindRight()
        {
            var n =
                TryParseInt32("1970")
                    .Bind(y => Choice2<FormatException, DateTime>(new DateTime(y, 1, 1)))
                    .Match(_ => AssertNotCalled<DateTime>(), d => d);

            Assert.That(n, Is.EqualTo(new DateTime(1970, 1, 1)));
        }

        [Test]
        public void SelectLeft()
        {
            var result =
                from y in TryParseInt32("forty-two")
                select new DateTime(y, 1, 1);

            var error =
                result.Match(e => e, _ => AssertNotCalled<FormatException>());

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo("\"forty-two\" is not a valid signed integer."));
        }

        [Test]
        public void SelectRight()
        {
            var result =
                from y in TryParseInt32("1970")
                select new DateTime(y, 1, 1);

            var n = result.Match(_ => AssertNotCalled<DateTime>(), d => d);

            Assert.That(n, Is.EqualTo(new DateTime(1970, 1, 1)));
        }

        [TestCase("forty", "two")]
        [TestCase("forty", "2")]
        public void SelectManyLeft(string first, string second)
        {
            var result =
                from x in TryParseInt32(first)
                from y in TryParseInt32(second)
                select AssertNotCalled<int>();

            var error = result.Match(e => e, _ => AssertNotCalled<FormatException>());

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo($"\"{first}\" is not a valid signed integer."));
        }

        [Test]
        public void SelectManyRightLeft()
        {
            var result =
                from x in TryParseInt32("40")
                from y in TryParseInt32("two")
                select AssertNotCalled<int>();

            var error = result.Match(e => e, _ => AssertNotCalled<FormatException>());

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo("\"two\" is not a valid signed integer."));
        }

        [Test]
        public void SelectManyRightRight()
        {
            var result =
                from x in TryParseInt32("40")
                from y in TryParseInt32("2")
                select x + y;

            var n = result.Match(_ => AssertNotCalled<int>(), r => r);

            Assert.That(n, Is.EqualTo(42));
        }
    }
}

namespace Choices.Linq.Left.Tests
{
    using System;
    using System.Globalization;
    using NUnit.Framework;
    using static Choice.New;

    [TestFixture]
    public class LinqTests
    {
        static Choice<int, FormatException> TryParseInt32(string s) =>
            int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var n)
            ? Choice1<int, FormatException>(n)
            : Choice2<int, FormatException>(new FormatException($"\"{s}\" is not a valid signed integer."));

        static T AssertNotCalled<T>()
        {
            Assert.Fail("Unexpected call.");
            return default;
        }

        [Test]
        public void Return()
        {
            var result =
                LeftResult
                    .Return<int, Exception>(42)
                    .Match(x => x,
                           _ => AssertNotCalled<int>());

            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void BindLeft()
        {
            var error =
                TryParseInt32("forty-two")
                    .Bind(_ => AssertNotCalled<Choice<object, FormatException>>())
                    .Match(_ => AssertNotCalled<FormatException>(), e => e);

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo("\"forty-two\" is not a valid signed integer."));
        }

        [Test]
        public void BindRight()
        {
            var n =
                TryParseInt32("1970")
                    .Bind(y => Choice1<DateTime, FormatException>(new DateTime(y, 1, 1)))
                    .Match(d => d, _ => AssertNotCalled<DateTime>());

            Assert.That(n, Is.EqualTo(new DateTime(1970, 1, 1)));
        }

        [Test]
        public void SelectLeft()
        {
            var result =
                from y in TryParseInt32("forty-two")
                select new DateTime(y, 1, 1);

            var error =
                result.Match(_ => AssertNotCalled<FormatException>(), e => e);

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo("\"forty-two\" is not a valid signed integer."));
        }

        [Test]
        public void SelectRight()
        {
            var result =
                from y in TryParseInt32("1970")
                select new DateTime(y, 1, 1);

            var n = result.Match(d => d, _ => AssertNotCalled<DateTime>());

            Assert.That(n, Is.EqualTo(new DateTime(1970, 1, 1)));
        }

        [TestCase("forty", "two")]
        [TestCase("forty", "2")]
        public void SelectManyLeft(string first, string second)
        {
            var result =
                from x in TryParseInt32(first)
                from y in TryParseInt32(second)
                select AssertNotCalled<int>();

            var error = result.Match(_ => AssertNotCalled<FormatException>(), e => e);

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo($"\"{first}\" is not a valid signed integer."));
        }

        [Test]
        public void SelectManyRightLeft()
        {
            var result =
                from x in TryParseInt32("40")
                from y in TryParseInt32("two")
                select AssertNotCalled<int>();

            var error = result.Match(_ => AssertNotCalled<FormatException>(), e => e);

            Assert.That(error, Is.InstanceOf(typeof(FormatException)));
            Assert.That(error.Message, Is.EqualTo("\"two\" is not a valid signed integer."));
        }

        [Test]
        public void SelectManyRightRight()
        {
            var result =
                from x in TryParseInt32("40")
                from y in TryParseInt32("2")
                select x + y;

            var n = result.Match(r => r, _ => AssertNotCalled<int>());

            Assert.That(n, Is.EqualTo(42));
        }
    }
}
