#!/bin/bash
set -euo pipefail
repo=$(cd "$(dirname "$0")/../.." && pwd)
project=$(mktemp -d "${TMPDIR:-/tmp}/fotokehr-web-project.XXXXXX")
trap 'rm -rf "$project"' EXIT
cat > "$project/Evidence.csproj" <<PROJECT
<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable></PropertyGroup><ItemGroup><ProjectReference Include="$repo/src/SafeSort.Core/SafeSort.Core.csproj" /></ItemGroup></Project>
PROJECT
cp "$repo/website/evidence/Program.cs" "$project/Program.cs"
"${DOTNET:-$HOME/.dotnet/dotnet}" run --project "$project/Evidence.csproj" -- "$repo"
