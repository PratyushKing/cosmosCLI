#!/bin/sh
rm cosmos
dotnet publish -c release --self-contained --runtime linux-x64 --framework net7.0
cp bin/Release/net7.0/linux-x64/publish/cosmos .
echo "Executable sent to base source directory!"
