@Echo Removing old files
del /Q Package\lib\net45\*.*
del /Q Package\tools\*.*
del /Q Package\content\*.*

@Echo Copying new files
xcopy ..\VisualCompareMode\bin\Release\net461\VisualCompareMode.dll Package\lib\net45
xcopy ..\VisualCompareMode\modules\_protected\VisualCompareMode\*.*  Package\content\modules\_protected\VisualCompareMode\ /S /Y
xcopy ..\VisualCompareMode\web.config.transform Package\content

@Echo Packing files
"..\.nuget\nuget.exe" pack package\VisualCompareMode.nuspec

@Echo Moving package
move /Y *.nupkg c:\project\nuget.local\