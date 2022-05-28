FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ntk.IdentityServer.Admin/Ntk.IdentityServer.Admin.csproj", "src/Ntk.IdentityServer.Admin/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.PostgreSQL/Ntk.IdentityServer.Admin.EntityFramework.PostgreSQL.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Shared/Ntk.IdentityServer.Admin.EntityFramework.Shared.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Shared/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Identity/Ntk.IdentityServer.Admin.EntityFramework.Identity.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Identity/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework/Ntk.IdentityServer.Admin.EntityFramework.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Extensions/Ntk.IdentityServer.Admin.EntityFramework.Extensions.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Extensions/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.Configuration/Ntk.IdentityServer.Admin.EntityFramework.Configuration.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.Configuration/"]
COPY ["src/Ntk.IdentityServer.Admin.BusinessLogic.Identity/Ntk.IdentityServer.Admin.BusinessLogic.Identity.csproj", "src/Ntk.IdentityServer.Admin.BusinessLogic.Identity/"]
COPY ["src/Ntk.IdentityServer.Admin.BusinessLogic.Shared/Ntk.IdentityServer.Admin.BusinessLogic.Shared.csproj", "src/Ntk.IdentityServer.Admin.BusinessLogic.Shared/"]
COPY ["src/Ntk.IdentityServer.Admin.UI/Ntk.IdentityServer.Admin.UI.csproj", "src/Ntk.IdentityServer.Admin.UI/"]
COPY ["src/Ntk.IdentityServer.Shared.Configuration/Ntk.IdentityServer.Shared.Configuration.csproj", "src/Ntk.IdentityServer.Shared.Configuration/"]
COPY ["src/Ntk.IdentityServer.Admin.BusinessLogic/Ntk.IdentityServer.Admin.BusinessLogic.csproj", "src/Ntk.IdentityServer.Admin.BusinessLogic/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.MySql/Ntk.IdentityServer.Admin.EntityFramework.MySql.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.MySql/"]
COPY ["src/Ntk.IdentityServer.Admin.EntityFramework.SqlServer/Ntk.IdentityServer.Admin.EntityFramework.SqlServer.csproj", "src/Ntk.IdentityServer.Admin.EntityFramework.SqlServer/"]
COPY ["src/Ntk.IdentityServer.Shared/Ntk.IdentityServer.Shared.csproj", "src/Ntk.IdentityServer.Shared/"]
RUN dotnet restore "src/Ntk.IdentityServer.Admin/Ntk.IdentityServer.Admin.csproj"
COPY . .
WORKDIR "/src/src/Ntk.IdentityServer.Admin"
RUN dotnet build "Ntk.IdentityServer.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ntk.IdentityServer.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Ntk.IdentityServer.Admin.dll"]