namespace WebApiCore.Models
{
    public class Nlogs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
