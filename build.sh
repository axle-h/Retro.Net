#!/bin/bash
dotnet restore Retro.Net.sln
dotnet build -c="Release" Retro.Net.sln
dotnet test Retro.Net.Tests/Retro.Net.Tests.csproj