# demo-mssql

/!\ **WORK IN PROGRESS** /!\

This demo provides a full stack example

- WebApi dotnet core
- xUnit integration tests
- MS SQL Server

Integration tests can be run

- locally with Visual Studio 2017 (the WebApi is loaded in-memory by the integration tests)
- using Docker Compose at CI-side

## Stack commands

```sh
# build stack
docker-compose.exe -f .\docker-compose.integration.yml build --no-cache

# run stack
docker-compose.exe -f .\docker-compose.integration.yml up

# destroy stack
docker-compose.exe -f .\docker-compose.integration.yml down
```

## TODO

- Retrieve xUnit report from the Docker container to the CI.