#!/bin/bash
set -e

csharp_dist=dist/csharp
csharp_path=Retro.Net.Api/RealTime/Messages
csharp_out=../$csharp_path
csharp_namespace=`echo $csharp_path |  tr / .`

ts_dist=dist/ts
ts_out=../gameboy-client/src/app/messages

if [ ! -f "tools/protoc/bin/protoc" ]; then
    echo "Getting protobuf compiler..."
    mkdir -p tools/protoc && cd tools/protoc
    wget -O protoc.zip https://github.com/google/protobuf/releases/download/v3.5.1/protoc-3.5.1-linux-x86_64.zip
    unzip protoc.zip
    rm -f protoc.zip
    chmod +x bin/protoc
    cd ../..
fi

echo "Cleaning up..."
rm -rf $csharp_dist $csharp_out $ts_dist $ts_out
mkdir -p $csharp_dist $csharp_out $ts_dist $ts_out

echo "Building .NET proto's with protoc..."
./tools/protoc/bin/protoc --proto_path=src \
                          --csharp_out=$csharp_dist \
                          --csharp_opt="base_namespace=$csharp_namespace" \
                          `echo src/*.proto`
cp -R $csharp_dist/* $csharp_out

echo "Building TypeScript proto's with protobufjs..."
npm install
npm start

# Fix missing Long import
sed -i '1s/^/import {Long} from "protobufjs";\
/' $ts_dist/messages.d.ts

cp $ts_dist/* $ts_out