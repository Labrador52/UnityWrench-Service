# UnityWrench Service

This is a lightweight service locator solution for Unity projects, created by the author for personal use.

## Purpose

To provide a simple and efficient way to manage and access services within a Unity project, reducing dependencies and improving code maintainability.

## Features

*   Lightweight and easy to integrate
*   Simple API for registering and resolving services
*   Reduces dependencies between components
*   Improves code testability

## Usage

```csharp
// Register a service
Service.Register<IService>(this);
Service.Register<IInputGetter>(this, true);

// Resolve a service
Service.Unregister<IService>();

//Use the service
service.Get<IGameplayControl>().Restart();
```

## License

MIT License