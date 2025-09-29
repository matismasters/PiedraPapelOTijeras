#!/usr/bin/env bash
set -euo pipefail

# Default SDK version aligned with the solution's target framework.
DOTNET_VERSION="${DOTNET_VERSION:-8.0.403}"

add_path_if_needed() {
  local new_path="$1"
  case ":$PATH:" in
    *":$new_path:"*) return 0 ;;
    *) export PATH="$new_path:$PATH" ;;
  esac
}

ensure_dotnet() {
  if command -v dotnet >/dev/null 2>&1; then
    return 0
  fi

  echo "dotnet command not found. Attempting to install .NET SDK ${DOTNET_VERSION}." >&2
  local install_script="/tmp/dotnet-install.sh"
  if ! command -v curl >/dev/null 2>&1; then
    echo "curl is required to download dotnet-install.sh. Please install curl and rerun." >&2
    exit 1
  fi

  curl -fsSL https://dot.net/v1/dotnet-install.sh -o "$install_script"
  bash "$install_script" --version "$DOTNET_VERSION"
  add_path_if_needed "$HOME/.dotnet"
  add_path_if_needed "$HOME/.dotnet/tools"
}

restore_solution() {
  echo "Restoring NuGet packages..." >&2
  dotnet restore PiedraPapelOTijeras.sln
}

run_tests() {
  echo "Running test suite..." >&2
  dotnet test PiedraPapelOTijeras.sln
}

main() {
  ensure_dotnet
  restore_solution
  run_tests
}

main "$@"
