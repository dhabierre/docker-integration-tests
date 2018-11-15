# adapt the code to create your data...

$server = 'http://127.0.0.1:9200'
$indexName = 'sample'
$documentCount = 1000
$contentType = 'application/json'

Function IndexExists {
    try {
        $response = Invoke-WebRequest -Method HEAD "$server/$indexName"
        if ($response.StatusCode -eq 200) {
            return $true
        }
    }
    catch {
        # Invoke-WebRequest -Method HEAD throws an exception when the index does not exist (404 - Not Found)
        if ($_.Exception.Message -NotMatch '(404)') {
            Write-Error $_.Exception.Message
        }
    }
    return $false
}

$indexExists = IndexExists

if ($indexExists) {
    $deleteIndex = Read-Host "Do you want to delete the existing '$indexName' index? (y/n/q)"
    if ($deleteIndex -eq 'q') {
        exit
    }
    if ($deleteIndex -eq 'y') {
        Write-Host "Deleting '$indexName' index..."
        Invoke-RestMethod -Method DELETE -Uri "$server/$indexName" -UseBasicParsing | Out-Null
    }
}

$indexExists = IndexExists

if (!$indexExists) {
    Write-Host "Creating '$indexName' index..."
    Invoke-RestMethod -Method PUT -Uri "$server/$indexName" -UseBasicParsing -Body '{ "settings": { "index": { "number_of_shards": 1, "number_of_replicas": 0 } } }' -ContentType: $contentType | Out-Null
}

# Data creation & insertion - Adapt this code for your needs
# For many documents you should use the Bulk API: https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html

$current = 0;
$words = @('one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 'dog', 'cat', 'bird', 'horse', 'goat')

for ($i = 0; $i -lt $documentCount; $i++) {
    $guid = [guid]::newguid()
    $isCritical = $false
    if (0 -eq $rand % 2) {
        $isCritical = $true
    }
    $rand1 = Get-Random -Minimum 0 -Maximum $words.Length
    $word1 = $words[$rand1]
    $rand2 = Get-Random -Minimum 0 -Maximum $words.Length
    $word2 = $words[$rand2]
    $message = '{0} {1}' -f $word1, $word2
    $timestamp = Get-Date -Format o
    $document = '{{ "id": "{0}", "isCritical": {1}, "message": "{2}", "timestamp": "{3}" }}' -f $guid, $isCritical.ToString().ToLower(), $message, $timestamp
    Write-Progress -Activity "Insert documents..." -Status "$current /$documentCount" -PercentComplete (100 * $current / $documentCount)
    Invoke-WebRequest -Method POST -Uri "$server/$indexName/$indexName/$guid" -UseBasicParsing -Body $document -ContentType: $contentType | Out-Null
    $current++
}
