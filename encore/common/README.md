## Common project files/includes

This directory contains common files used throughout the project, promoting a
centerplace to keep settings that might be repetitive otherwise.

## Description of files in this directory

**nuget_cache_clear.bat**: Batch script to wipe NuGet package given project name and file (containing version). Will be run by the targets file below on Windows systems.

**nuget_cache_clear.sh**: Bash script to wipe NuGet package given project name and file (containing version). Will be run by the targets file below on OS X and Linux systems.

**nuget_cache_clear.targets**: NuGet packages' cache clearing post-build procedure. To clear off cache of the NuGet package built by the project, in case a same version is rebuilt.

**productInfo.targets**: Ext.NET general version and other product informative data.
