FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /packages
USER root

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-stage
WORKDIR /build-source

COPY ["./nuget.config", "./"]
COPY ["./common.props", "./"]
COPY ["./common.version.props", "./"]
COPY ["./Eds.Shared.sln", "./"]

COPY ["./src/Eds.Shared.Contracts/Eds.Shared.Contracts.csproj", "./src/Eds.Shared.Contracts/"]
COPY ["./src/Eds.Shared.Helper/Eds.Shared.Helper.csproj", "./src/Eds.Shared.Helper/"]
COPY ["./src/Eds.Shared.Localization/Eds.Shared.Localization.csproj", "./src/Eds.Shared.Localization/"]
COPY ["./src/Eds.Shared.Hosting/Eds.Shared.Hosting.csproj", "./src/Eds.Shared.Hosting/"]
COPY ["./src/Eds.Shared.Hosting.Gateways/Eds.Shared.Hosting.Gateways.csproj", "./src/Eds.Shared.Hosting.Gateways/"]
COPY ["./src/Eds.Shared.Hosting.Microservices/Eds.Shared.Hosting.Microservices.csproj", "./src/Eds.Shared.Hosting.Microservices/"]

RUN dotnet restore "./Eds.Shared.sln" --verbosity minimal

COPY ["./src/Eds.Shared.Contracts/.", "./src/Eds.Shared.Contracts/"]
COPY ["./src/Eds.Shared.Helper/.", "./src/Eds.Shared.Helper/"]
COPY ["./src/Eds.Shared.Localization/.", "./src/Eds.Shared.Localization/"]
COPY ["./src/Eds.Shared.Hosting/.", "./src/Eds.Shared.Hosting/"]
COPY ["./src/Eds.Shared.Hosting.Gateways/.", "./src/Eds.Shared.Hosting.Gateways/"]
COPY ["./src/Eds.Shared.Hosting.Microservices/.", "./src/Eds.Shared.Hosting.Microservices/"]

RUN dotnet build "./Eds.Shared.sln" --no-restore --configuration Release --verbosity minimal

RUN dotnet test "./Eds.Shared.sln" --no-restore --no-build --configuration Release --verbosity minimal

RUN --mount=type=secret,id=VERSION_NUMBER \
    export VERSION_NUMBER=$(cat /run/secrets/VERSION_NUMBER) && \
    echo ${VERSION_NUMBER} > ./version_number 

RUN --mount=type=secret,id=ACTION_NUMBER \
    export ACTION_NUMBER=$(cat /run/secrets/ACTION_NUMBER) && \
    echo ${ACTION_NUMBER} > ./action_number

RUN dotnet pack "./Eds.Shared.sln" --no-restore --no-build --configuration Release --output ./packages -p:PackageVersion=$(cat ./version_number).$(cat ./action_number)

FROM base AS final
WORKDIR /packages
COPY --from=build-stage /build-source/packages .

RUN --mount=type=secret,id=NUGET_SOURCE \
    export NUGET_SOURCE=$(cat /run/secrets/NUGET_SOURCE) && \
    echo ${NUGET_SOURCE} > ./nuget_source

RUN --mount=type=secret,id=NUGET_SECRET \
    export NUGET_SECRET=$(cat /run/secrets/NUGET_SECRET) && \
    echo ${NUGET_SECRET} > ./nuget_secret

RUN dotnet nuget push *.nupkg --source $(cat ./nuget_source) --api-key $(cat ./nuget_secret) --skip-duplicate
