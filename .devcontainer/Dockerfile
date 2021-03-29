#FROM bitnami/minideb:latest
FROM debian:buster-slim

# general
RUN apt-get update && \
    apt-get install curl gnupg apt-transport-https -y && \
    apt-get update
# zip unzip software-properties-common ca-certificates musl-dev

# Fixing musl errors
RUN ln -s /usr/lib/x86_64-linux-musl/libc.so /lib/libc.musl-x86_64.so.1

# docker
RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add - && \
    echo "deb [arch=amd64] https://download.docker.com/linux/debian buster stable" > /etc/apt/sources.list.d/docker.list && \
    apt-get update && \
    apt-cache policy docker-ce && \
    apt-get install docker-ce docker-ce-cli containerd.io docker-compose -y

# dotnet-sdk
RUN curl -sSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    echo "deb https://packages.microsoft.com/debian/10/prod buster main" > /etc/apt/sources.list.d/microsoft-packages.list && \
    apt-get update && \
    apt-get install dotnet-sdk-5.0 -y

ENV PATH "$PATH:/root/.dotnet/tools/"