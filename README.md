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
