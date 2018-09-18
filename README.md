# Choices

Choices is a .NET Standard library that provides _choice types_, otherwise
known as generic or general-purpose discriminated unions.


## Usage

Create a choice of 2 types:

```c#
var nc = Choice<int, string>.Choice1(42);
var sc = Choice<int, string>.Choice2("foobar");
```

Match either of the choices:

```c#
var r1 = nc.Match(n => n * 2, s => _.Length); // = 82
var r2 = sc.Match(n => n * 2, s => _.Length); // = 6
```

Map a choice:

```c#
var rc = nc.Map1(n => n.ToString()); // = "82"
           .Map2(s => _.Length);     // = 6
```

Note that `rc` now has a type of `ChoiceOf2<string, int>`.

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
    Choice.If(n <= 2, () => "1-2", () => // else...
    Choice.If(n <= 4, () => "3-4", () => // else...
    Choice.If(n <= 6, () => "5-6", () => // else...
                            "7-10")))
```

Note that type of `r3` will be `Choice<string, string, string, string>`.

`Choice.When1`, `Choice.When2`, `Choice.When3` and so on, are useful for
setting up a function that maps choices to results.

```c#
var map =
    Choice.When1((int x) => x * 2)
          .When2((string s) => s.Length)
          .When3((DateTime d) => d.Year);

var c = Choice<int, string, DateTime>.Choice1(21);
Console.WriteLine(map(c)); // 42

c = Choice<int, string, DateTime>.Choice2("foobar");
Console.WriteLine(map(c)); // 6

c = Choice<int, string, DateTime>.Choice3(new DateTime(2002, 2, 13));
Console.WriteLine(map(c)); // 2002
```


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
    ? Choice<FormatException, int>.Choice2(n)
    : Choice<FormatException, int>.Choice1(new FormatException($"\"{s}\" is not a valid signed integer."));
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
