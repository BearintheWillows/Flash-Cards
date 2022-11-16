namespace Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int? StackPosition { get; set; } = null;

        public int StackId { get; set; }

    }
}