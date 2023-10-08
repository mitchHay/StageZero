---
title: WebDriverOptions
description: References and documentation on the StageZero WebDriverOptions instance.
---

`WebDriverOptions` is responsible for configuring the `IDriverWeb` instance (e.g. Selenium's `WebDriver`).

## Usage

```csharp
var config = new WebDriverOptions
{
    Headless = false
};
```

## Properties

### Headless

**Description:** Whether the driver should run in a headless state or not.

**Type:** `bool`

### Browser

**Description:** The browser to run a test against

**Type:** `Browser`

#### Supported Browsers

| Browser | API Reference     |
|---------|-------------------|
| Chrome  | `Browser.Chrome`  |
| Edge    | `Browser.Edge`    |
| Firefox | `Browser.Firefox` |
| Safari  | `Browser.Safari`  |

### EmulatedDeviceName

**Description:** The target emulated device (e.g. iPhone 12 Pro). 

> **Note:** Mobile emulation is only supported in `Chrome` and `Edge`

**Type:** `string`

> You can specify an `EmulatedDeviceName` either via a plain ole string you write yourself, or via a list of supported devices in our `Device` class. Your `EmulatedDeviceName` **must** match 1:1 with the `title` of the device in Chrome/Edge's preferences.

#### Supported Devices via the `Device` class

| Device                   | API Reference                  |
|--------------------------|--------------------------------|
| iPhone 12 Pro            | `Device.IPhone12Pro`           |
| iPhone SE                | `Device.IPhoneSE`              |
| iPad Air                 | `Device.IPadAir`               |
| iPad Mini                | `Device.IPadMini`              |
| Nest Hub                 | `Device.NestHub`               |
| Nest Hub Max             | `Device.NestHubMax`            |
| Samsung Galaxy S8+       | `Device.SamsungGalaxyS8Plus`   |
| Samsung Galaxy S20 Ultra | `Device.SamsungGalaxyS20Ultra` |
| Samsung Galaxy Fold      | `Device.SamsungGalaxyFold`     |