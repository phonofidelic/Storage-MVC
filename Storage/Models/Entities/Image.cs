namespace Storage.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; } = default!;
        public string Alt { get; set; } = default!;
    }
}