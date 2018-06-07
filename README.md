# Omicron
Fluent REST API testing.

```csharp
Omicron.Get("api.example.com/widgets")
	.Is.OK();
```

```csharp
Omicron.Get("api.example.com/widgets/1")
	.Is.NotFound();
```

```csharp
var widget = new
{
	Name = "Super Widget"
};

Omicron.Put("api.example.com/widgets/1")
	.With.Json(widget)
	.Has.Status(201);
```

```csharp
Omicron.Delete("api.example.com/widgets/1")
	.Is.Unauthorized();
```

```csharp
Omicron.Delete("api.example.com/widgets/1")
	.With.Authorization("Bearer", "xWd3jyM7")
	.Is.Not.Unauthorized();
```
