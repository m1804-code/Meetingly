﻿using meetingly_webapi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetingly_webapi.Models
{
    public class ScheduledDate
    {
        public int Id { get; set; }
        public required string Topic { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(1);
        public AvailabilityType AvailabilityType { get; set; }
        public EventType EventType { get; set; }
        public Status Status { get; set; }
        public Source Source { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
