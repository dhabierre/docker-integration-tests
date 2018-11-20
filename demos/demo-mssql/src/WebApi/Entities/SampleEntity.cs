namespace WebApi.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SampleEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsCritical { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}