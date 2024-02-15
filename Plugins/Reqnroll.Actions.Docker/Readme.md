# Reqnroll.Actions.Docker

[![Nuget](https://img.shields.io/nuget/v/Reqnroll.Actions.Docker)](https://www.nuget.org/packages/Reqnroll.Actions.Docker/)

This Reqnroll.Actions will help you by using Docker together with Reqnroll. 

## Included Features

- Start Docker containers from Dockerfile/docker-compose.yml at the start of the test scenario.
- Stop Docker containers from Dockerfile/docker-compose.yml at the start of the test scenario.

## Configuration

You can configure this plugin via the  `reqnroll.actions.json`.

Example:

``` json
{
  "docker": {
    "file": "../../../../docker-compose.yml"
  }
}
```

## How to use it

Add the NuGet- package to you project and specify the path to your Dockerfile/docker-compose.yml.  
Check that the `reqnroll.actions.yml` is set to `Copy Always` in `Copy to Output directory`.

After that, the containers are started and shutdown when a test scenario starts and ends.

## How to get it

Add the latest version of the `Reqnroll.Actions.Docker` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/Reqnroll.Actions.Docker)](https://www.nuget.org/packages/Reqnroll.Actions.Docker/)
