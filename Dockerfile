FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copie o arquivo .csproj do diretório correto
COPY Crud.API/Crud.API.csproj Crud.API/

# Restaure as dependências
RUN dotnet restore "Crud.API/Crud.API.csproj"

# Copie o restante dos arquivos do projeto
COPY . .

WORKDIR "/src/Crud.API"

# Compile o projeto
RUN dotnet build "Crud.API.csproj" -c Release -o /app/build

# Publicação da aplicação
FROM build AS publish
RUN dotnet publish "Crud.API.csproj" -c Release -o /app/publish --no-restore

# Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copie os arquivos publicados da etapa anterior
COPY --from=publish /app/publish .

# Exponha as portas padrão

ENV ASPNETCORE_URLS="http://+:80;https://+:443"

EXPOSE 80
EXPOSE 443


# Defina o ponto de entrada
ENTRYPOINT ["dotnet", "Crud.API.dll"]
