FROM mcr.microsoft.com/dotnet/sdk:5.0-focal as serverBuilder
WORKDIR /app
COPY ./server ./
RUN dotnet restore ./WebApi/WebApi.csproj
RUN ls ./
RUN dotnet publish ./WebApi/WebApi.csproj -c Release -o out
RUN ls /app/out

FROM node:alpine as clientBuilder
WORKDIR /app
COPY ./client/package*.json ./
RUN npm install
COPY ./client/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
COPY --from=serverBuilder /app/out/ ./
COPY --from=clientBuilder /app/build/ ./ClientApp
COPY --from=clientBuilder /app/pics/ ./ClientApp
ENTRYPOINT [ "dotnet", "WebApi.dll" ]
