#!/bin/bash
set -e
./clean.sh

cd gameboy-client
./build.sh
mkdir -p ../Retro.Net.Api/wwwroot
cp -r dist/* ../Retro.Net.Api/wwwroot
cd ..
dotnet publish Retro.Net.Api/Retro.Net.Api.csproj -c Release -o bin/publish
mv Retro.Net.Api/bin/publish bin
cat > bin/run.sh <<- EOF
#!/bin/bash
dotnet Retro.Net.Api.dll
EOF
chmod +x bin/run.sh