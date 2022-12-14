# FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
# COPY . /app
# WORKDIR /app/DATN.Web.Api
 
# RUN dotnet restore
# RUN dotnet dev-certs https --clean
# RUN dotnet dev-certs https -t
 
# EXPOSE 5000/tcp
# ENV ASPNETCORE_URLS http://*:5000
# ENV ASPNETCORE_ENVIRONMENT docker
 
# ENTRYPOINT dotnet run

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
COPY . /app
WORKDIR /app/DATN.Web.Api
RUN dotnet restore
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -t
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app/DATN.Web.Api
EXPOSE 5000/tcp
ENTRYPOINT [ "dotnet", "/app/DATN.Web.Api/DATN.Web.Api.dll" ]

# docker run --name datnweb  -p 5000:80 -d datnweb  