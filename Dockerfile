FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image ubuntu 18.04
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-bionic

# install Fuse
RUN apt-get update && apt-get install -y gnupg2
RUN export GCSFUSE_REPO=gcsfuse-bionic
RUN echo "deb http://packages.cloud.google.com/apt gcsfuse-bionic main" | tee /etc/apt/sources.list.d/gcsfuse.list
RUN curl https://packages.cloud.google.com/apt/doc/apt-key.gpg | apt-key add -
RUN apt-get update
RUN apt-get install -y gcsfuse

# copy the app from the builder
WORKDIR /app
COPY --from=build-env /app/out .

# create path to ingest files and save key
RUN mkdir /app/saved-files
RUN mkdir /app/key

# copy the key
COPY book-255910-1e410cf3767f.json /app/key/

# copy the start script and change access
COPY start.sh .
RUN chmod +x /app/start.sh

COPY appsettings.json /app
COPY appsettings.Docker.json /app

EXPOSE 8080 

# start sync folder with the bucket and the app (starting gcsfuse inside the script because the privileged)
ENTRYPOINT ["/app/start.sh"]
