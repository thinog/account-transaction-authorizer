#!/bin/bash

csproj='src/Presentation/TransactionAuthorizer.Presentation.CLI/TransactionAuthorizer.Presentation.CLI.csproj'

dotnet publish \
    $csproj \
    --configuration Release \
    --framework net5.0 \
    --runtime linux-musl-x64 \
    --output dist/linux/ \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    --self-contained

    # -p:PublishReadyToRun=false \
    # -p:PublishSingleFile=false \
    # -p:UseAppHost=true \

docker pull alpine:latest

docker build -t transaction-authorizer .