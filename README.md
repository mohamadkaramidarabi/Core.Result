# Core.Result

A lightweight .NET library that implements the **Result pattern** with a fluent builder API. Return explicit success or failure outcomes from your domain services instead of relying on exceptions for expected error paths.

## Features

- Generic `Result<T, TSuccessStatus, TFailureStatus>` with `Success` and `Failure` variants
- Domain-defined status enums — each bounded context defines its own business vocabulary
- Fluent builder API for configuring results with data, messages, and errors
- `Unit` type for operations that succeed without a meaningful payload
- Zero third-party dependencies

## Installation

```bash
dotnet add package Core.Result
```

Or via Package Manager:

```powershell
Install-Package Core.Result
```

## Domain Layer Setup

Define success and failure status enums in your domain project using business language:

```csharp
public enum OrderSuccessStatus { Placed, Cancelled, Shipped }

public enum OrderFailureStatus
{
    NotFound,
    AlreadyShipped,
    InvalidQuantity,
    InsufficientStock
}
```

Map domain statuses to HTTP, events, or UI in the **application layer** — not inside domain logic.

## Quick Start

### Success with data

```csharp
using Core.Result;

var result = Result<Order, OrderSuccessStatus, OrderFailureStatus>
    .InitSuccess(OrderSuccessStatus.Placed)
    .WithData(order)
    .WithMessage("Order placed successfully")
    .Build();

if (result is Success<Order, OrderSuccessStatus, OrderFailureStatus> success)
{
    Console.WriteLine(success.Status);   // OrderSuccessStatus.Placed
    Console.WriteLine(success.Data!.Id);
    Console.WriteLine(success.Message);
}
```

### Success without data

Use `Unit` when an operation succeeds but has no return value:

```csharp
var result = Result<Unit, OrderSuccessStatus, OrderFailureStatus>
    .InitSuccess(OrderSuccessStatus.Cancelled)
    .WithMessage("Order cancelled")
    .Build();
```

### Failure with errors

```csharp
var result = Result<Order, OrderSuccessStatus, OrderFailureStatus>
    .InitFailure(OrderFailureStatus.InvalidQuantity)
    .WithMessage("Order validation failed")
    .AppendErrors(["Quantity must be greater than zero"])
    .Build();

if (result is Failure<Order, OrderSuccessStatus, OrderFailureStatus> failure)
{
    Console.WriteLine(failure.Status);   // OrderFailureStatus.InvalidQuantity
    Console.WriteLine(failure.Message);
    foreach (var error in failure.Errors)
    {
        Console.WriteLine(error);
    }
}
```

## API Overview

### `Result<T, TSuccessStatus, TFailureStatus>`

The base type for all operation outcomes. Both status type parameters must be enums.

| Member | Description |
|--------|-------------|
| `Message` | Optional message describing the outcome |
| `InitSuccess(TSuccessStatus status)` | Starts building a success result with the required status |
| `InitFailure(TFailureStatus status)` | Starts building a failure result with the required status |

### `Success<T, TSuccessStatus, TFailureStatus>`

Represents a successful operation.

| Member | Description |
|--------|-------------|
| `Status` | The domain-defined success status |
| `Data` | The success payload |
| `Init(TSuccessStatus status)` | Alternative entry point for the success builder |

### `Failure<T, TSuccessStatus, TFailureStatus>`

Represents a failed operation.

| Member | Description |
|--------|-------------|
| `Status` | The domain-defined failure status |
| `Errors` | List of error messages |
| `AppendError(errors)` | Appends errors to the failure |
| `Init(TFailureStatus status)` | Alternative entry point for the failure builder |

### Builder methods

**Success builder** (`IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus>`):

| Method | Description |
|--------|-------------|
| `WithData(T? data)` | Sets the success payload |
| `WithMessage(string? message)` | Sets an optional message |
| `Build()` | Returns the configured `Success<T, TSuccessStatus, TFailureStatus>` |

**Failure builder** (`IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus>`):

| Method | Description |
|--------|-------------|
| `WithMessage(string message)` | Sets the failure message |
| `AppendErrors(List<string> errors)` | Adds one or more error messages |
| `Build()` | Returns the configured `Failure<T, TSuccessStatus, TFailureStatus>` |

### `Unit`

A singleton value type for void-like success results. All `Unit` instances are equal.

```csharp
var unit = Unit.Value;
var completed = await Unit.Task; // pre-completed Task<Unit>
```

## Usage in Domain Services

```csharp
public Success<Order, OrderSuccessStatus, OrderFailureStatus> PlaceOrder(PlaceOrderCommand cmd)
{
    if (cmd.Quantity <= 0)
    {
        return Result<Order, OrderSuccessStatus, OrderFailureStatus>
            .InitFailure(OrderFailureStatus.InvalidQuantity)
            .WithMessage("Validation failed")
            .AppendErrors(["Quantity must be greater than zero"])
            .Build();
    }

    var order = Order.Place(cmd);

    return Result<Order, OrderSuccessStatus, OrderFailureStatus>
        .InitSuccess(OrderSuccessStatus.Placed)
        .WithData(order)
        .WithMessage("Order placed")
        .Build();
}
```

## Mapping at the Application Layer

```csharp
if (result is Failure<Order, OrderSuccessStatus, OrderFailureStatus> failure)
    return failure.Status switch
    {
        OrderFailureStatus.NotFound => Results.NotFound(failure.Message),
        OrderFailureStatus.InvalidQuantity => Results.BadRequest(failure.Errors),
        OrderFailureStatus.InsufficientStock => Results.UnprocessableEntity(failure.Message),
        _ => Results.BadRequest(failure.Message)
    };
```

## Domain Usage Checklist

1. Define `XxxSuccessStatus` and `XxxFailureStatus` enums in the domain project
2. Return `Success<T, XxxSuccessStatus, XxxFailureStatus>` or `Failure<...>` from domain services
3. Use `InitSuccess(status)` / `InitFailure(status)` — status is always explicit
4. Use `Message` and `Errors` for human-readable detail; use `Status` for programmatic branching
5. Map domain status to HTTP, problem details, or integration events in application handlers

## Requirements

- .NET 10.0

## Publishing

For maintainers releasing a new version to GitHub and NuGet, see **[PUBLISHING.md](PUBLISHING.md)**.

Quick reference:

```powershell
# 1. Test
dotnet test Core.Result.Test\Core.Result.Test.csproj -c Release

# 2. Bump <Version> in Core.Result.csproj, then pack
dotnet pack Core.Result.csproj -c Release

# 3. Push to GitHub
git push origin main

# 4. Push to NuGet
dotnet nuget push "bin\Release\Core.Result.1.1.0.nupkg" `
  --api-key YOUR_NUGET_API_KEY `
  --source https://api.nuget.org/v3/index.json
```

## License

MIT
