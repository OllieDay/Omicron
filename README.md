# Omicron

Fluent REST API testing.

```csharp
O.Get("api.example.com/widgets")
    .Is.OK();
```

```csharp
O.Get("api.example.com/widgets/1")
    .Is.NotFound();
```

```csharp
var widget = new
{
    Name = "Super Widget"
};

O.Put("api.example.com/widgets/1")
    .With.Json(widget)
    .Has.Status(201);
```

```csharp
O.Delete("api.example.com/widgets/1")
    .Is.Unauthorized();
```

```csharp
O.Delete("api.example.com/widgets/1")
    .With.Authorization("Bearer", "xWd3jyM7")
    .Is.Not.Unauthorized();
```

## Getting started

You can use any unit testing framework including xUnit.net, NUnit, and MSTest. This example uses xUnit.net.

1. Create a new test project.

```shell
dotnet new xunit
```

2. Install the NuGet package into your application.

```shell
dotnet add package Omicron
```

3. Add the following method to the generated `UnitTest1.cs`, substituting `<some-url>` for your REST API endpoint.

```csharp
[Fact]
public void GetSomeUrlReturns200OK()
{
    O.Get("<some-url>")
        .Is.OK();
}
```

4. Run the test with

```shell
dotnet test
```

If `<some-url>` returns status code 200 for a GET request then the test will succeed, otherwise it will fail with an
appropriate error message.
