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

echo "NuGet package cache path: ${cache_path}"
echo -n "Removing NuGet package from cache (if it exists): ${project_name}-${version}"
if [ -d "${cache_path}" ]; then
 if rm -rf "${cache_path}"; then
  echo ", removed."
 else
  echo ", error.
*** Error: unable to remove NuGet package from cache, package changes won't reflect on projects using it."
  exit 1
 fi
else
 echo ", not found."
fi