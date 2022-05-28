FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ntk.IdentityServer.STS.Identity/Ntk.IdentityServer.STS.Identity.csproj", "src/Ntk.IdentityServer.STS.Identity/"]
COPY ["src/Ntk.IdentityServer.Shared.Configuration/Ntk.IdentityServer.Shared.Configuration.csproj", "src/Ntk.IdentityServer.Shared.Configuration/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Shared/Ntk.IdentityServer.Admin.EntityFramework.Shared.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Shared/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Identity/Ntk.IdentityServer.Admin.EntityFramework.Identity.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Identity/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework/Ntk.IdentityServer.Admin.EntityFramework.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Extensions/Ntk.IdentityServer.Admin.EntityFramework.Extensions.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Extensions/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Configuration/Ntk.IdentityServer.Admin.EntityFramework.Configuration.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Configuration/"]
COPY ["src/Ntk.IdentityServer.Shared/Ntk.IdentityServer.Shared.csproj", "src/Ntk.IdentityServer.Shared/"]
COPY ["src/Ntk.IdentityServer.Admin.BusinessLogic.Identity/Ntk.IdentityServer.Admin.BusinessLogic.Identity.csproj", "src/Ntk.IdentityServer.Admin.BusinessLogic.Identity/"]
COPY ["src/Ntk.IdentityServer.Admin.BusinessLogic.Shared/Ntk.IdentityServer.Admin.BusinessLogic.Shared.csproj", "src/Ntk.IdentityServer.Admin.BusinessLogic.Shared/"]
RUN dotnet restore "src/Ntk.IdentityServer.STS.Identity/Ntk.IdentityServer.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/Ntk.IdentityServer.STS.Identity"
RUN dotnet build "Ntk.IdentityServer.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ntk.IdentityServer.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Ntk.IdentityServer.STS.Identity.dll"]