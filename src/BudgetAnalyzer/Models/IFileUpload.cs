using System;

namespace BudgetAnalyzer.Models
{
    public interface IFileUpload
    {
        int Id { get; }

        byte[] Content { get; }

        string ContentType { get; }

        string FileName { get; }

        DateTimeOffset CreatedAt { get; }
    }
}