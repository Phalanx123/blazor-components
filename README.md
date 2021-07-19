<img align="left" width="140" height="140" src="https://github.com/majorimi/blazor-components/blob/master/.github/Images/blazor.components.png" />

Majorsoft Blazor Components
============

[![Build Status](https://dev.azure.com/major-soft/GitHub/_apis/build/status/blazor-components/blazor-components-build-check)](https://dev.azure.com/major-soft/GitHub/_build/latest?definitionId=6)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/majorimi/blazor-components/blob/master/LICENSE)
![GitHub last commit](https://img.shields.io/github/last-commit/majorimi/blazor-components)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/majorimi/blazor-components)
![GitHub Release Date](https://img.shields.io/github/release-date/majorimi/blazor-components)
![GitHub Repo stars](https://img.shields.io/github/stars/majorimi/blazor-components)
[![Build Status](https://dev.azure.com/major-soft/GitHub/_apis/build/status/blazor-components/blazor-components-Nuget?branchName=master)](https://dev.azure.com/major-soft/GitHub/_build/latest?definitionId=7&branchName=master)

Majorsoft Blazor Components is a set of UI Components and other useful Extensions for [Blazor](https://blazor.net) applications.
All components are available on [NuGet](https://www.nuget.org/profiles/Blazor.Components). 

You can try out all components and extensions by launching the [demo app](https://blazorextensions.z6.web.core.windows.net/). **Note: this app is hosted on _Azure Static website_ feature. Which uses aggressive caching you might have to use HARD reload (CTRL + F5 or CMD + SHIFT + R), or clear browser cache.**

## About the project
**Majorsoft Blazor Components** is one of the newest but the most modern Blazor library. Using the fastest ever .NET 5 Framework leveraging CSS and JS isolation. 
The main goal of this project is to provide an easy to use, feature reach set of customizable components with other 
useful extensions. Which can boost Blazor App developments by:

- Providing reusable components which are the main building blocks of Blazor.
- Hiding CSS details but allowing component customizations as well (no dependency on CSS libraries).
- Hiding JS implementations but exposing many reusable JS functionality and events via new C# APIs (no dependency on JS libraries).
- All running on the fastest ever .NET framework: **.NET 5**. Fully leveraging CSS and JS isolation, JS object reference and modul exports, etc.
- Modular project each package has "single responsibility" install only what you need, reduce download size.
- As simple as possible setup (custom JS referencing not required) all documented with usage examples and demo app.
- All components work on Blazor Server and Clint side.
- Components are extensible, provided extensions and services can be used in other components.
- All components and extensions written in C# (with some required JS) and unit tested with [bUnit](https://github.com/egil/bUnit).

## Releases
![GitHub release (latest by date)](https://img.shields.io/github/v/release/majorimi/blazor-components)
![GitHub Release Date](https://img.shields.io/github/release-date/majorimi/blazor-components)

:warning: For the full **release history with detailed change description and _breaking change announcements_** please see: [release notes](https://github.com/majorimi/blazor-components/releases).  

## Prerequisites
- .NET 5
- Visual Studio 2019/Visual Studio Code.

## Majorsoft Blazor Components and Extensions

Detailed descriptions and usage code samples are available on separated docs files. 
Please follow the link provided on each bullet points. Also you can try out all components and extensions by launching the [demo app](https://blazorextensions.z6.web.core.windows.net/).

Check out our planned components and extensions on the project [Wiki page](https://github.com/majorimi/blazor-components/wiki). If you have any prefered component to have priority or new component ideas please submit it.

### **Majorsoft Blazor Extensions**

**Majorsoft Blazor Extensions are providing useful features to develop Balazor applications:**

- **Majorsoft.Blazor.Server.Logging.Console**: Enables [Browser console logging](https://github.com/majorimi/blazor-components/blob/master/.github/docs/ServerHostedLogging.md) for Blazor applications using **Server Hosted model**.
- **Majorsoft.Blazor.WebAssembly.Logging.Console**: Enables [Browser console logging](https://github.com/majorimi/blazor-components/blob/master/.github/docs/WebAssemblyHostedLogging.md) for Blazor applications using **WebAssembly Hosting model**.
- **Majorsoft.Blazor.Extensions.BrowserStorage**: Enables [Browser Local and Session storages and Cookies store](https://github.com/majorimi/blazor-components/blob/master/.github/docs/BrowserStorage.md) access for Blazor applications.
- **Majorsoft.Blazor.Extensions.Analytics**: Enables [Analytics services usage](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Analytics.md) for Blazor applications e.g. Google Analytics, etc.

### **Majorsoft Blazor Components**

**Majorsoft Blazor Components are providing custom UI components to develop Balazor applications:**

- **Majorsoft.Blazor.Components.Common.JsInterop**: [Js Interop components, injectable services and extensions](https://github.com/majorimi/blazor-components/blob/master/.github/docs/JsInterop.md) that provides useful functionality and event notifications which can be achieved only with JS Interop e.g. scroll, clipboard, focus, resize, language detection, Geolocation, HTML Head (title, meta, SEO), etc..
- **Majorsoft.Blazor.Components.Debounce**: [Debounce components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/DebounceInputs.md) that renders an Input, InputText, Textarea or InputTextarea, etc. element with debounced `onChange` event.
- **Majorsoft.Blazor.Components.Typeahead**: [Typeahead components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Typeahead.md) that renders an HTML Input or InputText with Typeahead panel.
- **Majorsoft.Blazor.Components.Inputs**: [Inputs components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Inputs.md) renders an Input, InputText, Textarea or InputTextarea, etc. element with `maxlength` set and counter to show remaining characters.
- **Majorsoft.Blazor.Components.Loading**: [Loading components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Loading.md) that renders Overlay for page load. HTML Button with customizable content for showing async operation in progress/loading...
- **Majorsoft.Blazor.Components.Timer**: [Timer component](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Timer.md) that can be used for scheduled and periodically repeated tasks to call custom code.
- **Majorsoft.Blazor.Components.CssEvents**: [CSS Transition and Animation events](https://github.com/majorimi/blazor-components/blob/master/.github/docs/CssEvents.md) injectable Services and wrapper Components to notify on CSS Transition and Animation events.
- **Majorsoft.Blazor.Components.Modal**: [Modal dialog component](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Modal.md) that can be used to render Modal dialog window with customizable content and parameterized Overlay, etc.
- **Majorsoft.Blazor.Components.PermaLink**: [PermaLink component and extension](https://github.com/majorimi/blazor-components/blob/master/.github/docs/PermaLink.md) that can be used to create navigation element inside Blazor pages (#permalink).
- **Majorsoft.Blazor.Components.Toggle**: [Toggle components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Toggle.md) that can be used to render customizable Toggle switch and Toggle button components.
- **Majorsoft.Blazor.Components.Tabs**: [Tabs components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Tabs.md) that renders customizable Tabs panel with many tabs and custom content.
- **Majorsoft.Blazor.Components.Collapse**: [Collapse components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Collapse.md) that renders customizable Collapsable/Expandable panel and Accordion with many but only one active panel also custom content and header.
- **Majorsoft.Blazor.Components.Maps**: [Google/Bing Maps components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Maps.md) that renders **Google/Bing maps** wrapped into Blazor components allowing to control and mange maps with .Net code.
- **Majorsoft.Blazor.Components.GdprConsent**: [GDPR Consent components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/GdprConsent.md) injectable service and components that renders a customizable GDPR consent Banner or Popup witch Accept/Reject for cookie settings chosen value is persisted to Browser storage.

## Other info
- [Contributing](CONTRIBUTING.md)
- [Docs](.github/docs)
- [License](LICENSE)
