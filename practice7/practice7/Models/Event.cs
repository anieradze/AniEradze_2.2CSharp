namespace practice7.Models
{
    public class Event
    {
        public string Type { get; set; }
        public string SrcNumber { get; set; }
        public string? DstNumber { get; set; }
        public string? Time { get; set; }
        public int? Duration { get; set; }
        public List<double> SrcLoc { get; set; }
        public List<double> DstLoc { get; set; }
    }
}
