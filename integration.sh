#!/bin/bash

echo 'Building...'

csproj='src/Presentation/TransactionAuthorizer.Presentation.CLI/TransactionAuthorizer.Presentation.CLI.csproj'
output_dir=dist/debug/linux/

rm -rf $output_dir

dotnet publish \
    $csproj \
    --configuration Debug \
    --framework net5.0 \
    --output $output_dir \
    &> /dev/null

echo 'Built!'
echo '-------------'

for file in examples/*
do   
    echo -e '\033[1mFile: '$file'\033[0m';
    dist/debug/linux/authorize < $file
    echo '-------------'
done