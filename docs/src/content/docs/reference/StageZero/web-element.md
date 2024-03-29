---
title: IElementWeb
description: References and documentation on StageZero IElementWeb instance(s).
---

`IElementWeb` is a representation of the browsers HTML element.

## Usage

```csharp
// Your driver reference
var driver = DriverBuilder.Create(new WebDriverOptions());

// Access an IElementWeb by calling GetElement
var element = await driver.GetElement("#element-selector");
```

## Properties

### Text

**Description:** The inner text of the current element

**Type:** `string`

### IsDisplayed

**Description:** Whether the current element is displayed on the page

**Type:** `bool`

### ClassName

**Description:** The elements class name

**Type:** `string`

### Id

**Description:** The elements id

**Type:** `string`

### Tag

**Description:** The elements tag name

**Type:** `string`

## Methods

### Type

**Description:** Type the provided text into the current element

**Arguments:** `string`

**Returns:** `Task`

```csharp
await element.Type("Typing some text");
```

### PressKeys

**Description:** Mimic a user "press" of the provided keys.

**Arguments:** `Keys`

**Returns:** `Task`

```csharp
await element.PressKeys(Keys.S);
```

### Click

**Description:** Invoke a click event

**Returns:** `Task`

```csharp
await element.Click();
```

### RightClick

**Description:** Invoke a right-click event

**Returns:** `Task`

```csharp
await element.RightClick();
```

### DoubleClick

**Description:** Invoke a double-click event

**Returns:** `Task`

```csharp
await element.DoubleClick();
```

### ClickAndHold

**Description:** Click and hold the current element

**Arguments:** `TimeSpan`

**Returns:** `Task`

```csharp
await element.ClickAndHold(TimeSpan.FromSeconds(2));
```

### GetAttributeValue

**Description:** Get the value of a HTML attribute from its specified name

**Arguments:** `TimeSpan`

**Returns:** `Task<string>`

```csharp
var elementValue = await element.GetAttributeValue("value");
```

### ScrollTo

**Description:** Scroll to and get the specified element

**Arguments:** `string`

**Returns:** `Task<IElementWeb>`

```csharp
var scrolledToElement = await element.ScrollTo("#scrolled-to-element-selector");
```
