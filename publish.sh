#!/bin/bash

csproj="src/Presentation/TransactionAuthorizer.Presentation.CLI/TransactionAuthorizer.Presentation.CLI.csproj"

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