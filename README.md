# Docker-Compose for Integration Tests

When developing applications, it is often necessary to use services that talk to a database system. Unit testing these services can be cumbersome because mocking database is strenuous. Making slight changes to the schema implies rewriting at least some, if not all of the mocks. The same goes for API changes in the DBAL. To avoid this, it is smarter to test these specific services against a real database that is destroyed after testing.

Docker is the perfect system for running integration tests as you can spin up containers in a few seconds and kill them when the test completes.

## Requirements

- Docker on Linux or [Docker for Windows](https://store.docker.com/editions/community/docker-ce-desktop-windows) (Linux containers set-up) / v18.09.0 or later
- Docker Compose (comes with Docker for Windows)

## Execute local databases with Docker

This repository provides Docker-Compose samples for spinning up Docker containers and using them for your local developments and tests.

- [elasticsearch](stacks/elasticsearch)
- [mssql](stacks/mssql)
- [postgresql](stacks/postgresql)

### Versioning Support

Each stack has a default **1.0.0\\** directory that contains structure and data.

When creating a new database version (A.B.C)

1. create a new directory version with the new structure and data
2. create a new **docker-compose-vA.B.C.yml** file.

This way it is possible to spin up a specific version.

## Execute Integration Tests with Docker

This repository also provides a [full stack examples](demos).

## Important

If you plan to save data on your Source Control be careful to obfuscate / fake sensitive data!