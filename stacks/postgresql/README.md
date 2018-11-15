# postgresql-integration-tests

## Running stack

```sh
docker-compose -f docker-compose-v1.0.0.yml up     # see stack output
docker-compose -f docker-compose-v1.0.0.yml up -d  # deamon running
```

To access to Postgresql, use:

- Adminer > http://127.0.0.1:8081 (login: **postgres**, password: **postgres**, database: **MyDatabase**)
- PGAdmin > http://127.0.0.1:8082 (login: **pgadmin@pgadmin.org**, password: **pgadmin**)

Information to connect:

- Login: **postgres**
- Password: **postgres**
- Database: **MyDatabase**

## Stopping stack

```sh
docker-compose -f docker-compose-v1.0.0.yml down
```

## Importants

All scripts inside **v1.0.0\\** directory will be executed at the container startup.

If strange behaviors occurred when executing **docker-compose** be sure that each file uses **LF** (and not **CRLF**).