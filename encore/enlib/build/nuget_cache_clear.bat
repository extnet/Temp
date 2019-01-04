@echo off

set projnme=%1
set projpth=%2
set nuchpth="%USERPROFILE:"=%\.nuget\packages"

set /a paramcount=0
for %%p in ("%projnme:)=^)%","%projpth:)=^)%") do (
 set /a paramcount+=1
)

if %paramcount% lss 2 (
 echo usage: %0 "$(ProjectName)" "$(ProjectPath)"
 exit /b 1
)

set nupkver=
for /f "tokens=2,3 delims==<>" %%a in ('type %projpth%') do (
 if [%%a] == [Version] (
  if not [%%b] == [] (
   set nupkver=%%b
  )
 )
)

if [%nupkver%] == [] (
 set nupkver=1.0.0
)

set nupkpth="%nuchpth:"=%\%projnme:"=%\%nupkver%"

echo NuGet package cache path: %nupkpth%
echo Removing NuGet package from cache (if it exists): %projnme:"=%-%nupkver%
rmdir /s /q "%nupkpth:"=%"
echo done.
