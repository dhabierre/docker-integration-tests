# mssql-integration-tests

## Running stack

```sh
docker-compose -f docker-compose-v1.0.0.yml up     # see stack output
docker-compose -f docker-compose-v1.0.0.yml up -d  # deamon running
```

To access to MSSQL Server, use [Microsoft SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).

Information to connect:

- Login: **sa**
- Password: **P@55w0rd**

## Stopping stack

```sh
docker-compose -f docker-compose-v1.0.0.yml down
```

## Importants

If strange behaviors occurred when executing **docker-compose** be sure that each file uses **LF** (and not **CRLF**).