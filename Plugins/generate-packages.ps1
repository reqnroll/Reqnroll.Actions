del .\GeneratedNuGetPackages\*.nupkg
dotnet pack --configuration Release -o .\GeneratedNuGetPackages
del .\GeneratedNuGetPackages\ReqnrollCalculator.*.nupkg
