#!/bin/bash

echo 'Building...'

dotnet build

echo 'Built!'

for file in examples/*
do   
    echo 'File: $file';
    src/Presentation/TransactionAuthorizer.Presentation.CLI/bin/Debug/net5.0/TransactionAuthorizer.Presentation.CLI.exe < $file
    echo '-------------'
done