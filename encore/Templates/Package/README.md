## Template/Packages projects directory

This directory contains **template package crafting** projects for Ext.NET Core **project templates** in the upper level.

The projects here are not really .NET Core projects, but stubs that use the new csproj format and the `dotnet` client to
especially craft the Nuget Template Packages based for each **project template**.

The projects here basically include the **template.json** and **package.nuspec** files that drive up the NuGet Template Package.

### Project files info

**template.json**: should be moved within the package file into the `.template.config/template.json` path. This rule should be constant
in the accompanying **package.nuspec** file. The file contains information about:
- **identity**: which should match the packaging project's name (not the template project's); will be the name of the template's NuGet
package used with `dotnet new --install`. This should match the project name because the project name determines the NuGet package output
file name, and will be the internal identifier used by `dotnet new --uninstall`.
- **classification**: strings like "tags" that can be used as filters for locating the template and that are displayed in `dotnet new --list`.
- **name**: as the "long/pretty" name displayed in `dotnet new --list`.
- **shortName**: that should also be a unique name that serves as a shorthand to create the project with `dotnet new`

**package.nuspec**: A usual [NuGet nuspec file](https://docs.microsoft.com/en-us/nuget/reference/nuspec) that will contain entries
mapping all files from within the corresponding **template project** into the target **NuGet Template Package**.
- All project files should be placed within the **/content/** directory within the package so, if you have a **/Pages/Index.cshtml**
file in the project template, it should go to **/content/Pages/Index.cshtml**.
- The default nuspec file contains placeholders to the package description inherited from the .csproj file which, in turn, imports **/common/productInfo.targets**. Usually for new packages, only the file list
should be maintained.

### General documentation

General documentation on dotnet core project templates is available at the
[Custom Templates for dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates) article at Microsoft website.
