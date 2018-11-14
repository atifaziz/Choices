# Choices

[![Build Status][build-badge]][builds]
[![NuGet][nuget-badge]][nuget-pkg]
[![MyGet][myget-badge]][edge-pkgs]

Choices is a .NET Standard library that provides _choice types_, otherwise
known as generic or general-purpose discriminated unions.


## Usage

Create a choice of 2 types:

```c#
var nc = Choice.New.Choice1<int, string>(42);
var sc = Choice.New.Choice2<int, string>("foobar");
```

If you are using C# 6 or later (which remaining examples assume), it is
recommended you [statically import][using-static] `Choices.Choice.New`
as follows:

```c#
using static Choices.Choice.New;
```

  [using-static]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-static

This makes creation of choices terser:

```c#
var nc = Choice1<int, string>(42);
var sc = Choice2<int, string>("foobar");
```

Match either of the choices:

```c#
var r1 = nc.Match(n => n * 2, s => s.Length); // = 82
var r2 = sc.Match(n => n * 2, s => s.Length); // = 6
```

Map a choice:

```c#
var rc = nc.Map1(n => n.ToString()); // = "82"
           .Map2(s => s.Length);     // = 6
```

Note that `rc` now has a type of `Choice<string, int>`. Here is another way
to achieve the same but in a single `Map` invocation:

```c#
var rc = nc.Map(n => n.ToString(),  // = "82"
                s => s.Length);     // = 6
```

Swap a _couple_ of choices, which will swap values and thus the types:

```c#
Choice<string, int> swapped = Choice1<int, string>(42).Swap();
```

Use `Choice.If` to base the construction of choice based on some condition:

```c#
var rand = new Random();
var r3 =
    Choice.If(rand.Next(10) + 1 > 5,
              /* then */ () => 42,
              /* else */ () => "foobar")
```

`Choice.If` can be nested up to three levels:

```c#
var rn = new Random().Next(10) + 1;
var r3 =
    Choice.If(rn <= 2, () => "1-2", () => // else...
    Choice.If(rn <= 4, () => "3-4", () => // else...
    Choice.If(rn <= 6, () => "5-6", () => // else...
                             "7-10")));
```

Note that type of `r3` will be `Choice<string, string, string, string>`.

`Choice.When1`, `Choice.When2`, `Choice.When3` and so on, are useful for
setting up a function that maps choices to results.

```c#
var map =
    Choice.When1((int x) => x * 2)
          .When2((string s) => s.Length)
          .When3((DateTime d) => d.Year);

var c = Choice1<int, string, DateTime>(21);
Console.WriteLine(map(c)); // 42

c = Choice2<int, string, DateTime>("foobar");
Console.WriteLine(map(c)); // 6

c = Choice3<int, string, DateTime>(new DateTime(2002, 2, 13));
Console.WriteLine(map(c)); // 2002
```

Convert tuples to choices:

```c#
var choiceTypes =
    from choice in (42, "foobar", Math.PI).ToChoices()
    select choice.Match(a => a.GetType(),
                        b => b.GetType(),
                        c => c.GetType())
    into type
    select type.Name;

Console.WriteLine(string.Join(", ", choiceTypes)); // Int32, String, Double
```

Forbid a choice:

```c#
var choices = (42, "foobar", Math.PI).ToChoices();
var last = choices.Select(c => c.Forbid2())
                  .Last();
Console.WriteLine(last);
```

Since the second choice is forbidden, an `InvalidOperationException` is
thrown at run-time if such a choice appears in the data. Also, note that while
the type of `choices` will be some list of `Choice<int, string, double>`, the
type of `last` will be `Choice<int, double>`.


### LINQ

A choice of two can also be used with LINQ by either importing the
`Choices.Linq.Right` or `Choices.Linq.Left` namespace.

When the `Choices.Linq.Right` namespace is imported then LINQ queries bind on
the second (right) choice only. Likewise, when the `Choices.Linq.Left`
namespace is imported then LINQ queries bind on the first (left) choice only.

Suppose the following function that attempts to parse a `string` into an
`int`:

```c#
static Choice<FormatException, int> TryParseInt32(string s) =>
    int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var n)
    ? Choice2<FormatException, int>(n)
    : Choice1<FormatException, int>(new FormatException($"\"{s}\" is not a valid signed integer."));
```

If the parsing fails, then it returns a choice where a `FormatException` is
chosen as the first choice. Otherwise the integer parsed is chosen as the
second choice. Assuming then that `Choices.Linq.Right` has been imported, the
following example shows how choices work with LINQ:

```c#
// using Choices.Linq.Right;

var r =
    from x in TryParseInt32("40")
    from y in TryParseInt32("2")
    select x + y;

var output =
    r.Match(e => e.Message, n => n.ToString());

Console.WriteLine(output); // 42
```

In the example below, `"forty"` is an invalid integer so all subsequent parts
of a LINQ query expression never evaluate:

```c#
// using Choices.Linq.Right;

var r =
    from x in TryParseInt32("forty")
    from y in TryParseInt32("2")    // this won't be evaluated
    select x + y;                   // this won't be evaluated either

var output =
    r.Match(e => e.Message, n => n.ToString());

Console.WriteLine(output); // "forty" is not a valid signed integer.
```


## Building

To build just the binaries on Windows, run:

    .\build.cmd

On Linux or macOS, run instead:

    ./build.sh

To build the binaries and the NuGet packages on Windows, run:

    .\pack.cmd

On Linux or macOS, run instead:

    ./pack.sh


## Testing

To exercise the unit tests on Windows, run:

    .\test.cmd

On Linux or macOS, run:

    ./test.sh

This will also build the binaries if necessary.


[build-badge]: https://img.shields.io/appveyor/ci/raboof/choices/master.svg
[builds]: https://ci.appveyor.com/project/raboof/choices
[myget-badge]: https://img.shields.io/myget/raboof/vpre/Choices.svg?label=myget
[edge-pkgs]: https://www.myget.org/feed/raboof/package/nuget/Choices
[nuget-badge]: https://img.shields.io/nuget/v/Choices.svg
[nuget-pkg]: https://www.nuget.org/packages/Choices
