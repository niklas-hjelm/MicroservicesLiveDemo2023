namespace Domain.Common.DTOs;

public record QuizDto(
    string Title, 
    string Description, 
    string Author, 
    DateTime CreatedAt, 
    ICollection<string> Categories, 
    ICollection<string> Questions
    );