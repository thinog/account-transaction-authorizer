#!/bin/bash

csproj="src/Presentation/TransactionAuthorizer.Presentation.CLI/TransactionAuthorizer.Presentation.CLI.csproj"

dotnet publish \
    $csproj \
    --configuration Release \
    --framework net5.0 \
    --runtime win-x64 \
    --output dist/win/ \
    -p:PublishReadyToRun=true \
    -p:PublishSingleFile=true \
    --self-contained
    #--self-contained    

dotnet publish \
    $csproj \
    --configuration Release \
    --framework net5.0 \
    --runtime linux-x64 \
    --output dist/linux/ \
    -p:PublishReadyToRun=false \
    -p:PublishSingleFile=false \
    -p:UseAppHost=true \
    --self-contained false

dotnet publish \
    $csproj \
    --configuration Release \
    --framework net5.0 \
    --runtime osx-x64 \
    --output dist/osx/ \
    -p:PublishReadyToRun=true \
    -p:PublishSingleFile=true \
    --self-contained