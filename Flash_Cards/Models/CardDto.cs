namespace Models
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int StackPosition { get; set; }
    }
}