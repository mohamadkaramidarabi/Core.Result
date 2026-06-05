# Core.Result

A lightweight .NET library that implements the **Result pattern** with a fluent builder API. Return explicit success or failure outcomes from your services instead of relying on exceptions for expected error paths.

## Features

- Generic `Result<T>` with `Success<T>` and `Failure<T>` variants
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

## Quick Start

### Success with data

```csharp
using Core.Result;

var result = Result<User>.InitSuccess()
    .WithData(user)
    .WithMessage("User created successfully")
    .Build();

if (result is Success<User> success)
{
    Console.WriteLine(success.Data!.Name);
    Console.WriteLine(success.Message);
}
```

### Success without data

Use `Unit` when an operation succeeds but has no return value:

```csharp
var result = Result<Unit>.InitSuccess()
    .WithMessage("Operation completed")
    .Build();
```

### Failure with errors

```csharp
var result = Result<Order>.InitFailure()
    .WithMessage("Order validation failed")
    .AppendErrors(["Invalid quantity", "Product not found"])
    .Build();

if (result is Failure<Order> failure)
{
    Console.WriteLine(failure.Message);
    foreach (var error in failure.Errors)
    {
        Console.WriteLine(error);
    }
}
```

## API Overview

### `Result<T>`

The base type for all operation outcomes.

| Member | Description |
|--------|-------------|
| `Message` | Optional message describing the outcome |
| `InitSuccess()` | Starts building a success result |
| `InitFailure()` | Starts building a failure result |

### `Success<T>`

Represents a successful operation.

| Member | Description |
|--------|-------------|
| `Data` | The success payload |
| `Init()` | Alternative entry point for the success builder |

### `Failure<T>`

Represents a failed operation.

| Member | Description |
|--------|-------------|
| `Errors` | List of error messages |
| `AppendError(errors)` | Appends errors to the failure |
| `Init()` | Alternative entry point for the failure builder |

### Builder methods

**Success builder** (`IConfigureSuccessResultBuilder<T>`):

| Method | Description |
|--------|-------------|
| `WithData(T? data)` | Sets the success payload |
| `WithMessage(string? message)` | Sets an optional message |
| `Build()` | Returns the configured `Success<T>` |

**Failure builder** (`IConfigureFailureResultBuilder<T>`):

| Method | Description |
|--------|-------------|
| `WithMessage(string message)` | Sets the failure message |
| `AppendErrors(List<string> errors)` | Adds one or more error messages |
| `Build()` | Returns the configured `Failure<T>` |

### `Unit`

A singleton value type for void-like success results. All `Unit` instances are equal.

```csharp
var unit = Unit.Value;
var completed = await Unit.Task; // pre-completed Task<Unit>
```

## Usage in Services

```csharp
public Success<Customer> CreateCustomer(CreateCustomerRequest request)
{
    if (string.IsNullOrWhiteSpace(request.Name))
    {
        return Result<Customer>.InitFailure()
            .WithMessage("Validation failed")
            .AppendErrors(["Name is required"])
            .Build();
    }

    var customer = new Customer { Name = request.Name };

    return Result<Customer>.InitSuccess()
        .WithData(customer)
        .WithMessage("Customer created")
        .Build();
}
```

## Requirements

- .NET 10.0

## License

MIT
