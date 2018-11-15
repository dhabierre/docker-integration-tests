namespace WebApi.Documents
{
    using System;
    using Nest;

    [ElasticsearchType(Name = "sample")]
    public class SampleDocument
    {
        [Text(Name = "id")]
        public string Id { get; set; }

        [Boolean(Name = "isCritical")]
        public bool IsCritical { get; set; }

        [Text(Name = "message")]
        public string Message { get; set; }

        [Date(Name = "timestamp")]
        public DateTime Date { get; set; }
    }
}