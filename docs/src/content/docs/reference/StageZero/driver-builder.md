---
title: DriverBuilder
description: References and documentation on the StageZero DriverBuilder instance.
---

The `DriverBuilder` is responsible for the registration of a `IDriverBuilder` instance and the creation of `IDriver` instances.

## Usage

### Register

**Description:** Registers a `IDriverBuilder` instance used to instantiate new `IDriver` instances of the same target integration (e.g. Selenium / Playwright).

**Returns:** `void`

```csharp
DriverBuilder.Register<TDriverBuilder>();
```

### Create

**Description:** Creates a new `IDriver` instance of the registered integration type (e.g. Selenium / Playwright).

**Arguments:** `DriverOptions` | `WebDriverOptions`

**Returns:** `IDriver`

```csharp
DriverBuilder.Create(new WebDriverOptions());
```
