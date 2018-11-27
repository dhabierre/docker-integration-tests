# demos

Some full stack examples

- [demo-elasticsearch](demo-elasticsearch)
- [demo-mssql](demo-mssql)
- [demo-postgresql](demo-postgresql)

## Notes

When building a demo stack (with `docker-compose.exe -f .\docker-compose.integration.yml build --no-cache`) make sure that Visual Studio solution is not open. Otherwise the build can fail due to locked files (in .vs directory).

If you want to debug the database dependency (see the logs when running) you have to comment the **healthcheck** block in **docker-compose.integration.yml** and the service that depends on it.
