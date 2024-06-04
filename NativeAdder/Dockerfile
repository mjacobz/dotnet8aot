# Use the official .NET image for building
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build

RUN apt-get update && \
	apt-get install -y --no-install-recommends clang zlib1g-dev llvm lld 

RUN apt-get -y update && apt-get -y install \
    binutils-aarch64-linux-gnu 

# Copy root file system for ARM64 cross compiling

COPY --from=mcr.microsoft.com/dotnet-buildtools/prereqs:ubuntu-22.04-cross-arm64 /crossrootfs/arm64/ /crossrootfs/arm64/

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY NativeAdder.csproj ./
RUN dotnet restore

# Copy the remaining files and publish the project
COPY . .
#RUN dotnet publish -c Release -o /app --self-contained -r linux-x64 /p:PublishAot=true
RUN dotnet publish -f net8.0 -r linux-arm64 -c Release -o /app/publish  -p:SysRoot=/crossrootfs/arm64/ 

# Final stage: Use a smaller runtime image to package the library
FROM ubuntu:22.04

# Set the working directory
WORKDIR /app

# Install necessary dependencies
RUN apt-get update && apt-get install -y libicu70

# Copy the published files from the build stage
COPY --from=build /app .

# Entry point not needed as this is a library