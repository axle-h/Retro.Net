#!/bin/bash
set -e
./clean.sh

dotnet publish Retro.Net.Api/Retro.Net.Api.csproj -c Release -o bin/publish
mv Retro.Net.Api/bin/publish bin

cd gameboy-client
./build.sh
cd ..
mkdir -p bin/wwwroot
cp -r gameboy-client/dist/* bin/wwwroot

