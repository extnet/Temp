# Manually setting up Ext.NET Core

## Introduction

Thanks for the interest in **Ext.NET Core**! If you're reading this document, it
means you're interested in setting up an empty web project from scratch and get
it running with **Ext.NET Core**.

In order to start using the components in your project you must add references
to the HtmlHelpers and/or TagHelpers from **Ext.NET Core**.

Follow the instructions below for whichever you want to enable.

## HtmlHelpers

This enables access to the Ext.NET Core components from view pages using Razor
Markup.

To enable HtmlHelpers, add this line to **Pages\_ViewImports.cshtml** or
**Views\_ViewImports.cshtml**:

```
@using enlib.HtmlComponents
```

The line above can also be included at view level in the beginning of the
**.cshtml** view file. In **_ViewImports.cshtml** makes the Ext.NET components
available to all views in that folder and subfolders.

## TagHelpers

This enables the XML-style tags, similar to WebForms syntax in ASPNET Core.

To enable TagHelpers, add this line to **Pages\_ViewImports.cshtml** or
**Views\_ViewImports.cshtml**:

```
@addTagHelper *, enlib
```

## Enable MVC support

In order to use either of the options above, it is required that the project
has MVC enabled in **Startup.cs**. At least these two lines would be required:

Within `ConfigureServices()`:

```
services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
```

Within `Configure()`:

```
app.UseMvc();
```

## More info

The lines above can also be included at view level in the beginning of the
**.cshtml** view file. The **_ViewImports.cshtml** file makes the Ext.NET
components available to all views in that folder and subfolders.