FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

WORKDIR /app

COPY *.sln ./
COPY Project1.BusinessLogic/*.csproj Project1.BusinessLogic/
COPY Project1.DataAccess/*.csproj Project1.DataAccess/
COPY Project1Test/*.csproj Project1Test/
COPY Project1WebApp/*.csproj Project1WebApp/

RUN dotnet restore

COPY . ./

RUN dotnet publish Project1WebApp -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS http://+:80

CMD [ "dotnet", "Project1WebApp.dll" ]
