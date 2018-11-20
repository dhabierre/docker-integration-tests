# elasticsearch-integration-tests

## Import an existing index

Add your existing indices inside the **v1.0.0\esdata\nodes\0\\** directory before running the stack.

How to process?

1. Go to http://elasticsearch-with-existing-indices:9200/_cat/indices?v
2. Get the UUID of the concerned index
3. Go to the Elasticsearch Server DATA directory (report to /etc/elasticsearch/elasticsearch.yml)
4. Copy the directory having the good UUID
5. Paste it in v1.0.0\esdata\nodes\0\

## Create index and data by script

Adapt and execute the **v1.0.0\scripts\create-index.ps1** script to create a custom index with data once the stack is running.

### Troubleshooting

When executing the **create-index.ps1** script the following error can occur

```text
The response content cannot be parsed because the Internet Explorer engine is not available, or Internet Explorer's first-launch configuration is not complete. Specify the UseBasicParsing parameter and try again.
```

The error is probably coming up because **Internet Explorer** has not yet been launched for the first time or not initialized yet. Launch it and select the **Use recommended security, policy, and compatibility settings** (green icon - first choice) in the **Set up Internet Explorer 11** popup. Then the error message will not come up any more.

The following error can occur when you try to insert documents and that you are running out of disk space and have exceeded the [flood stage watermark](https://www.elastic.co/guide/en/elasticsearch/reference/6.4/disk-allocator.html)

```json
"error": {
  "root_cause": [{
    "type": "cluster_block_exception",
    "reason": "blocked by: [FORBIDDEN/12/index read-only / allow delete (api)];"
  }]
}
```

Elasticsearch server output

```text
[2018-11-05T09:50:21,774][WARN ][o.e.c.r.a.DiskThresholdMonitor] [pz5Ibhz] flood stage disk watermark [95%] exceeded on [pz5IbhzCRuuBmaNJyShkDA][pz5Ibhz][/usr/share/elasticsearch/data/nodes/0] free: 10.9gb[4.5%], all indices on this node will be marked read-only
```

Have a look at how large percentage of your disk space that is still free.

Solutions

- cleanup the disk
- report to [disk-allocator](https://www.elastic.co/guide/en/elasticsearch/reference/6.4/disk-allocator.html)

## Running stack

```sh
docker-compose -f docker-compose-v1.0.0.yml up     # see stack output
docker-compose -f docker-compose-v1.0.0.yml up -d  # deamon running
```

To access to Elasticsearch use Kibana (http://127.0.0.1:5601).

## Stopping stack

```sh
docker-compose -f docker-compose-v1.0.0.yml down
```

## Important

If strange behaviors occurred when executing **docker-compose** be sure that each file uses **LF** (and not **CRLF**).