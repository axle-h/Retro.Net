### STAGE 1: Build Angular Frontend ###
FROM node:8-alpine AS node-build-env

COPY gameboy-client/package.json gameboy-client/package-lock.json ./

RUN npm set progress=false && npm config set depth 0 && npm cache clean --force

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm i && mkdir /ng-app && cp -R ./node_modules ./ng-app

WORKDIR /ng-app

ADD gameboy-client .

## Build the angular app in production mode and store the artifacts in dist folder
RUN npm run ng build -- --prod

### STAGE 2: Build .NET Core Backend ###
FROM microsoft/aspnetcore-build:1.0-2.0 AS netcore-build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY **/*.csproj ./
RUN dotnet restore Retro.Net.csproj
RUN dotnet restore GameBoy.Net.csproj
RUN dotnet restore Retro.Net.Api.csproj

# Copy everything else and build
COPY Retro.Net ./Retro.Net
COPY GameBoy.Net ./GameBoy.Net
COPY Retro.Net.Api ./Retro.Net.Api
WORKDIR /app/Retro.Net.Api
RUN dotnet publish -c Release -o ../out

### STAGE 3: Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=netcore-build-env /app/out .
COPY --from=node-build-env /ng-app/dist ./wwwroot
ENTRYPOINT ["dotnet", "Retro.Net.Api.dll"]