﻿FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["src/OzonEdu.MerchandiseApi/OzonEdu.MerchandiseApi.csproj", "src/OzonEdu.MerchandiseApi/"]
RUN dotnet restore "src/OzonEdu.MerchandiseApi/OzonEdu.MerchandiseApi.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.MerchandiseApi"

RUN dotnet build "OzonEdu.MerchandiseApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.MerchandiseApi.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh