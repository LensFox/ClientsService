# Download base image from dockerfile/ubuntu
FROM ubuntu

RUN \
    apt update && \
    apt install -y mongodb

# Define working directory.
WORKDIR /data

VOLUME [ "/data/db" ]

# Define default command.
CMD mongod

# Expose ports.
#   - 27017: process
#   - 28017: http
EXPOSE 27017
EXPOSE 28017