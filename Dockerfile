### STAGE 1: Build Angular Frontend ###
FROM trion/ng-cli-karma:1.7.3 AS ng-build-env
WORKDIR /app

# Copy project structure first and restore dependencies as a distinct layer.
COPY gameboy-client/package.json gameboy-client/package-lock.json ./
RUN npm set progress=false && npm config set depth 0 && npm cache verify
RUN npm install

# Copy rest, test and build.
COPY gameboy-client .
RUN npm run ng test -- --watch false --single-run true --no-progress
RUN npm run ng build -- --prod --no-progress

### STAGE 2: Build .NET Core Backend ###
FROM microsoft/aspnetcore-build:2.0 AS netcore-build-env
WORKDIR /app

# Copy project structure first and restore dependencies as a distinct layer.
# This is disgusting that we have to list each file but docker does not support preserving structure when globbing.
COPY Retro.Net/Retro.Net.csproj Retro.Net/
COPY GameBoy.Net/GameBoy.Net.csproj GameBoy.Net/
COPY Retro.Net.Api/Retro.Net.Api.csproj Retro.Net.Api/
COPY Retro.Net.Tests/Retro.Net.Tests.csproj Retro.Net.Tests/
COPY Retro.Net.sln .
RUN dotnet restore

# Copy everything else.
COPY Retro.Net ./Retro.Net
COPY GameBoy.Net ./GameBoy.Net
COPY Retro.Net.Api ./Retro.Net.Api
COPY Retro.Net.Tests ./Retro.Net.Tests

# Test.
WORKDIR /app/Retro.Net.Tests
RUN dotnet test -c Release

# Build.
WORKDIR /app/Retro.Net.Api
RUN dotnet publish -c Release -o ../dist

### STAGE 3: Build runtime image ###
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=netcore-build-env /app/dist .
COPY --from=ng-build-env /app/dist ./wwwroot
ENTRYPOINT ["dotnet", "Retro.Net.Api.dll"]