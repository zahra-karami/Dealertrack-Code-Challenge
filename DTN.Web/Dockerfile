#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["DTN.Web/DTN.Web.csproj", "DTN.Web/"]
COPY ["DTN.Model/DTN.Model.csproj", "DTN.Model/"]
COPY ["DTN.Logic/DTN.Logic.csproj", "DTN.Logic/"]
RUN dotnet restore "DTN.Web/DTN.Web.csproj"
COPY . .
WORKDIR "/src/DTN.Web"
RUN dotnet build "DTN.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DTN.Web.csproj" -c Release -o /app/publish

FROM node AS node-builder
WORKDIR /node
COPY ./ClientApp /node
RUN npm install
RUN npm build

FROM base AS final
WORKDIR /app
RUN mkdir /app/wwwroot
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DTN.Web.dll"]
