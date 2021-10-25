FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

#RUNTIME
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /src
COPY --from=build /src/out ./

EXPOSE 80
ENTRYPOINT [ "dotnet" , "Actinver.UI.dll" ]