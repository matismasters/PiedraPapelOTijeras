# PiedraPapelOTijeras

Este repositorio contiene una implementación en .NET 8 de "Piedra, Papel o Tijeras" junto a su respectiva batería de pruebas automatizadas con xUnit.

## Requisitos previos

- .NET SDK 8.0 (o superior compatible con `net8.0`).
- Acceso a internet para descargar las dependencias de NuGet la primera vez.

En caso de no contar con el SDK instalado, puede utilizarse el script `scripts/install_dependencies.sh`, el cual descargará el SDK oficial mediante `dotnet-install.sh`, restaurará los paquetes NuGet y ejecutará la suite de pruebas.

> 💡 **Sugerencia:** si instalás el SDK con el script y abrís una nueva terminal, añadí las siguientes variables a tu sesión para poder seguir invocando `dotnet` sin rutas absolutas:
>
> ```bash
> export DOTNET_ROOT="$HOME/.dotnet"
> export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
> ```

## Uso rápido

```bash
./scripts/install_dependencies.sh
```

El script realizará las siguientes acciones:

1. Verificará si el comando `dotnet` está disponible.
2. Si no lo encuentra, descargará e instalará la versión 8.0.403 del SDK (o la que se defina en la variable de entorno `DOTNET_VERSION`).
3. Ejecutará `dotnet restore` sobre la solución `PiedraPapelOTijeras.sln`.
4. Correrá `dotnet test` para asegurar que la aplicación y las pruebas compilen correctamente.

## Ejecución manual

Si prefiere realizar los pasos manualmente:

```bash
# Restaurar dependencias
DOTNET_CLI_TELEMETRY_OPTOUT=1 dotnet restore PiedraPapelOTijeras.sln

# Ejecutar las pruebas
DOTNET_CLI_TELEMETRY_OPTOUT=1 dotnet test PiedraPapelOTijeras.sln

# Ejecutar la aplicación de consola (modo interactivo)
DOTNET_CLI_TELEMETRY_OPTOUT=1 dotnet run --project PiedraPapelOTijeras/PiedraPapelOTijeras.csproj

# Ejecutar la aplicación proveyendo entradas por stdin
printf 'Alice\nBob\n1\n2\n3\n' | DOTNET_CLI_TELEMETRY_OPTOUT=1 dotnet run --project PiedraPapelOTijeras/PiedraPapelOTijeras.csproj
```

> **Nota:** En entornos con restricciones de red (por ejemplo, sin acceso a `https://dot.net` o a los feeds de NuGet) será necesario proveer el SDK y los paquetes NuGet desde un espejo interno o mediante descargas offline. Asegúrese de contar con acceso de red adecuado antes de ejecutar los comandos anteriores.
