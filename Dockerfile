FROM alpine:latest

COPY ./dist/linux/* /opt/authorize/

ENV PATH "$PATH:/opt/authorize/"