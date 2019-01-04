#!/bin/bash

project_name="${1}"
project_path="${2}"

if [ -z "${project_name}" ]; then
 echo "Project name not specified."
 exit 1
fi

if [ ! -e "${project_path}" ]; then
 echo "Project file not found: ${project_path}"
 exit 1
fi

version="$(egrep "<Version>[^<]+</Version>" "${project_path}" | sed -E "s/.*<Version>([^<]+)<\/Version>.*/\1/")"

if [ -z "${version}" ]; then
 # dotnet gives this version if none is specified in the csproj file.
 version="1.0.0"
fi

cache_path="${HOME}/.nuget/packages/${project_name}/${version}"

echo "NuGet packages cache path: ${cache_path}"
echo -n "Removing NuGet package from NuGet cache: ${project_name}-${version}"
rm -rf "${cache_path}"
echo ", done."