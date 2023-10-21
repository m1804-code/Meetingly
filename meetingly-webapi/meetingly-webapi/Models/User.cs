namespace meetingly_webapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required ICollection<ScheduledDate> ScheduledDates { get; set; } = new List<ScheduledDate>();
    }
}
