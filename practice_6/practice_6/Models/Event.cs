namespace practice_6.Models
{
    public class Event
    {
        public string Type { get; set; } = string.Empty;
        public string SrcNumber { get; set; } = string.Empty;
        public string DstNumber { get; set; } = string.Empty;
        public int? Duration { get; set; }
    }
}

