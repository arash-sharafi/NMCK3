using System;
using System.ComponentModel.DataAnnotations;

namespace NMCK3.Infrastructure.Persistence.Models
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime OccurredOnUtc { get; set; }

        public DateTime? ProcessedOnUtc { get; set; }
        public string Error { get; set; }
    }
}
