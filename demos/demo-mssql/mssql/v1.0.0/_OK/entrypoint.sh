#!/bin/bash
database=MyDatabase
wait_time=20s
password=P@55w0rd

# wait for MSSQL Server to come up
echo [entrypoint] importing data will start in $wait_time...
sleep $wait_time
echo [entrypoint] importing data...

# create the database
/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i ./init.sql

# create the tables
for entry in "tables/*.sql"
do
  echo [entrypoint, table] executing $entry
  /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i $entry
done

# import data from the csv files
for entry in "data/*.csv"
do
  # transform /data/MyTable.csv -> MyTable
  shortname=$(echo $entry | cut -f 1 -d '.' | cut -f 2 -d '/')
  tableName=$database.dbo.$shortname
  echo [entrypoint, data] importing $tableName from $entry
  /opt/mssql-tools/bin/bcp $tableName in $entry -c -t',' -F 2 -S 0.0.0.0 -U sa -P $password
done