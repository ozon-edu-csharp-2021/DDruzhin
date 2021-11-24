#!/bin/bash

set -e
run_cmd="dotnet OzonEdu.MerchandiseApi.dll --no-build -v d"

>&2 echo "Dry Run MerchandiseApi DB migrations"
dotnet OzonEdu.MerchandiseApi.Migrator.dll --no-build -v d -- --dryrun
>&2 echo "Dry Run MerchandiseApi DB migrations complete"

>&2 echo "Run MerchandiseApi DB migrations"
dotnet OzonEdu.MerchandiseApi.Migrator.dll --no-build -v d
>&2 echo "MerchandiseApi DB Migrations complete, starting app."

>&2 echo "Run MerchandiseApi: $run_cmd"
exec $run_cmd