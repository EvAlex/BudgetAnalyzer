namespace BudgetAnalyzer.Models
{
    public class FileUpload : IFileUpload
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }
    }
}