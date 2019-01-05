# Ext.NET Core draft

## 1. Introduction

This document will guide you through the steps necessary to run the Ext.NET Core draft project on OS X.

### 1.1. Quick install+run guide

If you're in a hurry and don't want to read this document, you can try this sequence to fully build the project and create a new template project from a new installation.

If problems/questions arise, you may need to read the rest of the documentation.

```
git clone https://github.com/extnet/Temp
cd Temp/encore
dotnet build


# add Temp/encore/nuget_repository to your ~/.nuget/NuGet/NuGet.config 
# as a local repo with your favourite text editor
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="local" value="<path>/Temp/encore/nuget_repository" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>


# Run the 'ent' project website:
#     http://localhost:5000
cd ent
dotnet run


#ctrl+c (to shutdown webserver and return to the shell)


dotnet new --install enlib.tpl.Empty::0.0.1-draft0

cd ~
mkdir myProject
cd myProject
dotnet new extnet

# http://localhost:5000
dotnet run 

#ctrl+c (to shutdown webserver and return to the shell)
```

## 2. System Requirements

- Mac OS X Mojave 10.14.2
- [ASPNet Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) - SDK only, the runtime will be installed with the SDK.

## 3. Getting the project

```
git clone https://github.com/extnet/Temp
cd Temp/encore
```

## 4. Building the project

The project is set up so that it can be easily built from Visual Studio 2017, Visual Studio Code, and the `dotnet` CLI.

### 4.1. General building information

The project includes the library itself (**enlib**), a test project (**ent**) linked directly to the library, and also an empty template project (**Templates/empty**) and the corresponding template's package building project (**Templates/Package/enlib.tpl.Empty**).

The projects will be built in this order: **enlib**, **ent**, and **enlib.tpl.Empty**. From the full build, two NuGet packages will be produced and output to the **Temp/encore/nuget_repository** folder. This nuget folder should be set up as a local NuGet repository in order to actually use the built project and install both the `dotnet` CLI project template -and- set up the new project. Instructions in section 5.

For every NuGet package the project builds, a check is performed against the user's NuGet packages cache; if a package with the same version is present in the cache, that specific version will be removed. This will help ensure fresh packages are retrieved the next time the template or library package is installed.

**Note:** the installed template becomes saved even if the NuGet package cache is reset. To update the package it is necessary to manually run `dotnet new --uninstall enlib.tpl.Empty`.

### 4.2. Building with the dotnet CLI

The `dotnet` CLI supports reading **.sln** files, so one should be safe to just call the build command off

```
dotnet build
```

### 4.3. Building with Visual Studio Code

Just open the project folder (**Temp/encore**) and issue the build task (**crtl+shift+b**, **⌘+shift+b**). In the end the (updated) NuGet packages should be laid in the expected **encore/nuget_repository/** folder.

### 4.4. Building with Visual Studio

Similarly to the other two building options, to build with Visual Studio, just open the solution and trigger the build command.

## 5. Running the built code

### 5.1. ent: Builtin client project

The builtin client project (**ent**) is useful to immediately debug changed code in the library project (**enlib**). If the solution is "run", a browser (or listening server) will be set up with the **ent** project. Debugging can be enabled in the IDE and code from the library project can be stepped in.

#### 5.1.1. dotnet CLI

Simply issue in the commandline:
```
dotnet run
```

Then a webserver will be started in the address informed on the terminal. As explained in **section 8.3**, the `dotnet` CLI does not open a browser window to the website location by itself.

The line of interest in the console output would be something similar to:

```
Now listening on: http://localhost:5000/
```

To cancel the `dotnet run` server, press `ctrl+c` in the terminal.

##### 5.1.2. Stuck dotnet processes

After the server finishes, it may be that some **dotnet** processes are left running. Check the system monitor for stuck `dotnet` processes, in particular if after a successful run, a new `dotnet run` complains about "address already in use" or can't start by some reason. On Windows, stuck **dotnet** processes will also block the project from building (they lock the DLLs).

This is especially true when running it from a cygwin terminal in windows (it does not pass ctrl+c to the underlying process), and also using the debugger feature in Visual Studio Code (Windows too).

#### 5.1.2. Visual Studio Code

Running the project with debugging from Visual Studio Code **is currently only supported under Windows**. On Windows, use the **ctrl+shift+d** shortcut to open the debugger, and launch the **ent** project (_Launch test project_).

Beware **dotnet** processes will become stuck after the debugger is stopped. These must be killed manually and there's no known fix for that currently.

#### 5.1.3. Visual Studio 2017

Under Visual Studio, just choose the _Start Debugging_ or _Start without debugging_ from the _Debug_ menu. Visual Studio uses its own iisexpress to serve the requests and supports rebuilding and restarting/debugging the website without issues.

**Note:** Visual Studio for mac is not tested.

### 5.2. Installing and Setting Up the project template

To effectively test the library and templates, it is necessary to install the template, create a project using that template, build that project and then run it.

#### 5.2.1. Before you begin: Setup NuGet repository

The steps above will include downloading NuGet packages produced by the project build process, so it is necessary to set up the system so that the NuGet client may locate the packages. One way to do so is pointing the **encore/nuget_repository** folder as a *Local NuGet packages repository* on your system.

The fastest way to set up the local repository is by editing the `${HOME}/.nuget/NuGet/NuGet.config` file, and add a line like this within the `<packageSources>` block:

```
<add key="local" value="/Users/myself/github/extnet/Temp/encore/nuget_repository" />
```

The above assumes your username is `myself` and you have the *Temp* repo on your home's `github/extnet/temp` directory. 

With this, `dotnet` CLI should be able to find the NuGet packages built from the project.

**Note:** Microsoft [recommends using `nuget config`](https://docs.microsoft.com/en-us/nuget/tools/cli-ref-config) to safely update the NuGet config file, but this would require extra tools (like a mono installation on OS X/Linux). At the time of the writing, `dotnet nuget` didn't offer a subcommand equivalent to `nuget config`.

**Note:** The NuGet config file location depends on the platform; the above should be correct for OS X and linux. More information about the file location is available at [the nuget config reference documentation](https://docs.microsoft.com/en-us/nuget/tools/cli-ref-config).

### 5.2.2. Install the template

To install the `dotnet` CLI template, issue the command:

```
dotnet new --install enlib.tpl.Empty::0.0.1-draft0
```

The `dotnet` CLI should show the list of installed templates, and last the **extnet** template.

**Note:** If you are updating the template with a fresh rebuild, it is necessary to uninstall the old and re-install. The template is saved once installed, so it won't pull the Nuget package again. If uninstalled then reinstalled, then it will pull the NuGet package afresh. The build process will ensure the old version is pruned off the NuGet packages cache.

**Note:** The **::0.0.1-draft0** bit in the install command is only required to install an older/specific version or a **prerelease** version (which is the case, currently).

### 5.2.3. Create a project using the template

With the package installed, create the project with:

```
mkdir myProject
cd myProject
dotnet new extnet
```

Similarly, you could specify the project name from the `dotnet` command:

```
dotnet new extnet --name myProject
cd myProject
```

### 5.2.4. Building and running the new project

Just go ahead and issue:

```
dotnet run
```

By default, the templates set up a server at `http://localhost:5000`, this can be changed in the **Properties/launchSettings.json** file.

The `dotnet run` command above includes calling `dotnet build` which, in turn, calls `dotnet restore`, so all should be updated.

### 5.2.5. Refreshing the project

#### 5.2.5.1. the **enlib** project

If you rebuild the **enlib** project, just rebuilding the template end-project should suffice to get the fresh code. Beware of properly stopping the `dotnet run` server if running, or the updated assemblies won't reflect on the running page.

As noted in 7.1 below, the build process already wipes the local Nuget cache if the package is rebuilt under the same version number, so no extra steps should be necessary.

#### 5.2.5.2. the template-driven projects

For a project created off an Ext.NET Core template (5.2.3), just rebuilding it would pull the fresh assemblies, assuming the automatic cache purging covered in 7.1 is working.

In case the project version changed (say, from **0.0.1-draft0** to **0.0.1-draft1**), upgrading the NuGet package in the project will be necessary. All it would take is a `dotnet add enlib --version 0.0.1-draft1`.

**Note:** During development it is not a good idea to keep updating the NuGet package version number, the NuGet package cache (and repository folder) will clobber on old versions!

**Note:** The `--version` argument is required to add/upgrade NuGet packages marked as `prerelease`. If the version wanted is not a prerelease (for example **0.0.2**), the flag is not necessary, as long as that's the latest version.

### 5.2.6. Refreshing the template

To updating the template with a fresh rebuild, it is necessary to uninstall the old one and re-install it. So, after rebuilding the project with changes to the empty template project (**enlib.tpl.Empty**), this would update the template:

```
dotnet new --uninstall enlib.tpl.Empty
dotnet new --install enlib.tpl.Empty::0.0.1-draft0
```

As noted in 7.1 below, the build process will ensure the old NuGet package is pruned off the local packages cache.

**Note:** updating the template won't upgrade the existing projects. You can either create a new project or `dotnet new extnet --force` to replace the project files in-place. Notice any changes on the project files will be overwritten by conflicting files.

**Note:** As mentioned in 5.2.2, the **::0.0.1-draft0** bit in the install command is only required to install an older/specific version or a **prerelease** version (which is the case, currently).

## 6. Checking the result from the **ent** project

The **ent** project will usually feature what's supported from the library project, whereas the currently available template is just an empty project where the components could be explored, and also the NuGet package restore process ensured.

Once run, in the main **ent** the open page, you'll be able to see four buttons, two per `<h2>` headings:

### 6.1. HtmlComponents Approach

This one uses code similar to the current Ext.NET MVC Razor syntax. `@Html.Button()` and `@Html.Button("text")` as a simple overload to fill the button's `value` attribute.

### 6.2. TagHelpers Approach

This is a new concept introduced by ASPNet Core (since early version 1), and this is closer to the legacy WebForms, as it allows us to use a xml-driven syntax to build the components. It has some limitations that should be assessed before deciding whether it is a feasible feature -- for instance, it does not participate in the request (no form data/post is accessible at the time TagHelpers are expanded), but this is possibly not a problem for a range of situations.

### 6.3. Code-behing "ish" Approach

This section repeats the two *HtmlComponents* and *TagHelpers* blocks above, but drawing the code from code-behind. It currently is a "hackism" to get the output string off the components and write them to a passthru-string (`HtmlString` class) model variable at page load time (`OnGet()` model call).

It implements both parameterless and parameter (content) usage of the buttons.

### 6.4. Index.cshtml

Currently, the **ent** client project uses the **Pages/** concept to lay the pages across the website. It is a mid-term between the `aspx` and `MVC5` concept, as for this, each page is a pair of files (*.cshtml* view and *.cshtml.cs* model), without an explicit controller file.

ASPNET Core still supports the old **Model/**, **View/** and **Controller/** folders structure (actual MVC), and it should work without issues.

## 7. Troubleshooting

This section will point a few pitfalls you may find during the project setup + run. You'd likely to fall in one of those in particular if you make changes to the `enlib` project.

### 7.1. NuGet package refreshing

The build process should now clear the cache of a built package if a same version is rebuilt than one in cache, but if for some reason it fails or you are dubious of the process, the cache can be reset by deleting the version directory in the user's home directory **${HOME}/.nuget/packages/** folder.

The currently built packages are **enlib** and **enlib.tpl.Empty**, and version **0.0.1-draft0** so the directories should be:

- **${HOME}/.nuget/packages/enlib/0.0.1-draft0**
- **${HOME}/.nuget/packages/enlib.tpl.Empty/0.0.1-draft0**

Dotnet core no longer uses the project-constrained **packages/** directory, so when the cache is cleared, the next time a project is built, it will pull the packages off the local NuGet repository, even if the project was not `dotnet clean` beforehand.

An example command to wipe out the NuGet cache off the `enlib` package, on OS X, would be:

```
rm -rf ~/.nuget/packages/enlib/0.0.1-draft0
```

**Note:** The first character in the path is a tilde (~), it can be replaced by either your home directory (/Users/myself) or the bash environment variable `${HOME}`. On windows, the environment variable is **%USERPROFILE%** (**${USERPROFILE}** under cygwin).

### 7.2. dotnet run - Can't bind to address: already in use

If `dotnet run` complains about not being able to run the webserver, you may have another copy already running, or just the port is not available on the system. By default, the server listens on port 5000.

If you want to change the port, edit the `ent/Properties/launchSettings.json` file. It will have an entry for VisualStudio's *iisexpress*, and another below (under `profiles` block) that the `dotnet` CLI uses.

Refrain from committing the `launchSettings.json` file as your change would break somebory else's run. I removed it from .gitignore but should have just force-committed it instead.

### 7.3. build - unable to replace DLL, file in use

Under windows, if the server is run via `dotnet run`, the website and library assemblies will be locked, even if the project is not running in debug mode. So, if the project is run via the CLI, you must stop the server before attemnpting to rebuild it.

Under OS X or Linux, the project can be built and the assemblies will be updated, but the version loaded by the webserver will not refresh. The server needs to be restarted in order to re-read the fresh built assemblies.

If the project has been run in Visual Studio -- at least under windows, its IISExpress version won't lock up the DLLs, and as the project is rebuilt the assemblies are promptly refreshed in the running website instance. Running from VS4Mac was not tested.

## 8. Missing features

### 8.1. The NuGet package is useless on new projects

The **enlib** NuGet package does not set up anything other than making available the `enlib` namespace to the project, in favor of the new `dotnet` CLI templating system.

**Note:** A README and copyright/license files should be added to the main package, though.

### 8.2. No Html.X()

Wrapping up the `Html.X().Button()` concept should take a little work, it was not so trivial as just going ahead with `Html.Button()`, so that's what we have for now. At least understanding how Ext.NET currently implements this will take a little more efforts as there are lots of code about that should not be required for that specific aspect.

### 8.3. Browser doesn't open

When `dotnet run` executes, a webserver will be started in the address informed on the terminal, but no browser window will be opened at all. The `dotnet` CLI [does not currently support calling the browser at all](https://github.com/dotnet/cli/issues/8487). It's necessary to open a chrome or safari window and point it to the informed URL in the terminal to see the runnint website.

There are some quick terminal commands like `open http://localhost:5000` on OS X to open the browser from another terminal window.

## 9. Tested environments

- Windows 10, VS2017, v15.9.4
- Windows 10, Dotnet CLI, 2.2.101
- Wubdiws 19m VSCode, 1.30.1
- OS X Mojave 10.14.2, Dotnet CLI, 2.2.101
- OS X Mojave 10.14.2, VSCode, 1.30.1
(add yours here if you try with different versions or VS4Mac).

## 10. Templating system

The templating system allows a more flexible mechanism instead of complex NuGet package install systems. 

It is similar to the old VSIX system, but the distribution system is the same nuget.org through specific "Template" NuGet packages.

.NET templating advantages:
- Allows to quickly deploy an empty, functional project.
- Making presets of commonly used project set ups is not difficult, without the need of extra installed tools (like webactivatorex)
- Avoids difficult and unreliable scripts or procedures to install on unpredictable project layouts.
- They don't need to be updated every time the main library is updated -- just set up nuget package reference to install the latest version
- The template projects are but runnable projects that are transformed at package-time to be generic (adapt namespaces, package names)
- [Well documented](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore21), [with samples](https://github.com/dotnet/dotnet-template-samples)
- Offers powerful API and customization level for possibly edge cases (may become complex)
- The customization is through commented out code, so won't affect the template project ability to be built and tested in-place.

Some potential disadvantages
- Requires the projects to be started on Ext.NET
- Will not cover cases where already established projects migrate to Ext.NET (for this, manual setting up should be desirable over the risk of overwritting code anyway)
- It is a new procedure, likely not known or expected by developer accustomed on older versions of .NET.
- Requires one NuGet package per template
- The actual process to build the template package is not officially supported by the `dotnet` CLI, so it requires a few undocumented and "hacky" steps to work (but so far it looks perfect, and sticks to the `dotnet` tool).

## 11. Conclusion

With this, you should be able to give the project a first look, and whether it works on your system. Well, and understand a lot of its inner workings!
