# Ext.NET Core draft

## Introduction

This document will guide you through the steps necessary to run the Ext.NET Core draft project on OS X.

## System Requirements

- Mac OS X Mojave 10.14.2
- [ASPNet Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) - SDK only, the runtime will be installed with the SDK.

## Getting the project

```
git clone https://github.com/extnet/Temp
cd Temp/encore
```

## Building the Library project

Currently, we called the library project as `enlib`, to be short and as we still may decide on the name to give it.

```
cd enlib
dotnet build
```

## Building the Client project

### Predefined basic project

Currently, the Library project outputs a NuGet package, but it does not perform specific steps to set up a clean/empy project down to all requirements an ASPNet page would require to run.

Basically, a page with Razor and TagHelpers is required, and a bare client project is set up in the repository for convenience. It is being used as a base to make a minimal set up that will determine the steps the NuGet package should do while setting up a fresh Ext.NET Core project.

### Setup NuGet Package's local repository

It is necessary to set up the output folder of the Ext.NET Core library (`enlib`) project as a *local NuGet packages repository* -- or copy the NuGet package to a known local NuGet packages. It is currently output to `enlib/bin/Debug`.

To add a local NuGet repository so that the `dotnet` command is able to find it, edit the `${HOME}/.nuget/NuGet/NuGet.config` file, and add a line like this within the `<packageSources>` block:

```
<add key="local" value="/users/myself/github/extnet/Temp/encore/enlib/bin/Debug" />
```

The above assumes your username is `myself` and you have the *Temp* repo on your home's `github/extnet/temp` directory.

With this, `dotnet` should be able to find the `enlib` NuGet package when you build the project.

*Note:* `dotnet restore` should work, but it is not necessary; every time you run `dotnet build`, it does call `dotnet restore`: so if it does not restore a NuGet package during build, calling `restore` directly is likely not to help (but useful for troubleshooting package restore issues).

### Building the project

Simply move to the project directory and build it:

```
cd ../ent
dotnet build
```

## Running the project

If the `ent` client project was built successfully, you can test it by running:

```
dotnet run
```

A webserver will be started in the address informed on the terminal. Currently, it seems the default installation of .NET Core 2.2 does not allow `dotnet run` to open a browser to the URL as it starts the server, so just open a chrome or safari session to the informed URL and you should see the test page.

The line of interest (to get the url) would read something like:

```
Now listening on: http://localhost:5000/
```

Press `ctrl+c` (*not* `cmd+c`) in the terminal to stop the embedded `dotnet` webserver.

## Checking the result

In the open page, you'll be able to see four buttons, two per `<h2>` headings:

### HtmlComponents Approach

This one uses code similar to the current Ext.NET MVC Razor syntax. `@Html.Button()` and `@Html.Button("text")` as a simple overload to fill the button's `value` attribute.

### TagHelpers Approach

This is a new concept introduced by ASPNet Core (since early version 1), and this is closer to the legacy WebForms, as it allows us to use a xml-driven syntax to build the components. It has some limitations that should be assessed before deciding whether it is a feasible feature -- for instance, it does not participate in the request (no form data/post is accessible at the time TagHelpers are expanded), but this is possibly not a problem for a range of situations.

### Code-behing "ish" Approach

This section repeats the two *HtmlComponents* and *TagHelpers* blocks above, but drawing the code from code-behind. It currently is a "hackism" to get the output string off the components and write them to a passthru-string (`HtmlString` class) model variable at page load time (`OnGet()` model call).

It implements both parameterless and parameter (content) usage of the buttons.

### Index.cshtml

Currently, the `ent` client project uses the `Pages/` folder ASPNet core concept to lay the pages across the website. It is a mid-term between the `aspx` and `MVC5` concept, as for this, each page is a pair of files (*.cshtml* view and *.cshtml.cs* model), without an explicit controller file.

It still supports the old `Views/` folder, but it is needed further analysis; the MVC template project does not come with the `Controllers/` folder (it has the `Views/` folder), this might be now similar to the `Pages/` folder structure.

## Troubleshooting

This section will point a few pitfalls you may find during the project setup + run. You'd likely to fall in one of those in particular if you make changes to the `enlib` project.

### NuGet package refreshing

If you venture rebuilding the `enlib` project and try to see the changes reflecting in the `ent` project, you should notice how frustrating it may become. Basically, it is necessary to wipe the NuGet package folder from the local cache. Sometimes it may also be necessary to do a full force-rebuild of the client `ent` project. Usually, if under Visual Studio (windows), the force-rebuild (righ-click project name, click *rebuild*) may be necessary. Usually `dotnet build` refreshes nicely, so it should work well on mac if relying on `dotnet build`.

To wipe out the NuGet cache off the `enlib` package, on OS X, this should do:

```
rm -rf ~/.nuget/packages/enlib/1.0.0
```

Where *1.0.0* is the current version of the library. You can remove the whole *enlib* folder, but this would ensure older versions (if any) are retained in the cache).

*Note:* The first character in the path is a tilde (~), it can be replaced by either your home directory (/Users/myself) or the bash environment variable `${HOME}`.

## Missing features

### The NuGet package is useless on new projects

Currently the `enlib` NuGet package does not set up anything like namespaces for razor or taghelpers. I should do this next.

### No Html.X()

Wrapping up the `Html.X().Button()` concept should take a little work, it was not so trivial as just going ahead with `Html.Button()`, so that's what we have for now. At least understanding how Ext.NET currently implements this will take a little more efforts as there are lots of code about that should not be required for that specific aspect.

### Browser doesn't open

The `dotnet run` command does not seem to open the browser on OS X or Windows, at least by default, I didn't look up why, but as soon as it says `Now listening on`, it works like a charm no matter where we do it.

### dotnet run - Cant bind to address: already in use

If `dotnet run` complains about not being able to run the webserver you may have another copy of it already running, or just the port is not available on the system. For `dotnet` call, it uses port 5000; from Visual Studio, it will try to bind to, 8474.

If you want to change the port edit the `ent/Properties/launchSettings.json` file. It will have an entry for VisualStudio's *iisexpress*, and another below (under `profiles` block) that the `dotnet` client uses.

I don't see a reason why the port is different from IISExpress and CLI; that's proabably just because VS does not care what's the other setting and binds to a random port on its own first time it is setup.

Refrain from committing the `launchSettings.json` file as your change would break somebory else's run. I removed it from .gitignore but should have just force-committed it instead.

### build - unable to replace DLL, file in use

If you run the server thru `dotnet run` (at least under windows), the built DLL files by the web project will be stuck, even if the project is not running in debug mode. So, if the project is run via the CLI, you must stop the server before attemnpting to rebuild it.

If the project has been run in Visual Studio -- at least under windows, its IISExpress version won't lock up the DLLs, and as the project is rebuilt the output is updated next page refresh. Running from VS4Mac was not tested, so I can't say whether it locks up or not the DLLs.

## Tested environments

- Windows 10, VS2017, v15.9.4
- Windows 10, Dotnet CLI, 2.2.101
- OS X Mojave 10.14.2, Dotnet CLI, 2.2.101
(add yours here if you try with different versions or VS4Mac).

## Conclusion

With this, you should be able to give the project a first look, and whether it works on your system.
