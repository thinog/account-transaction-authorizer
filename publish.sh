#!/bin/bash

csproj='src/Presentation/TransactionAuthorizer.Presentation.CLI/TransactionAuthorizer.Presentation.CLI.csproj'
output_dir=dist/release/linux/

rm -rf $output_dir

dotnet publish \
    $csproj \
    --configuration Release \
    --framework net5.0 \
    --output $output_dir \
    -p:DebugType=None \
    -p:DebugSymbols=false

mkdir -p /opt/authorize
rm -rf /opt/authorize/*
cp -r $output_dir* /opt/authorize/