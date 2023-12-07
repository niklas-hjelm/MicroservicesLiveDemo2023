using Domain.Common.Enums;

namespace Domain.Common.DTOs;

public record QuestionDto(
        QuestionType Type,
        Difficulty Difficulty,
        string Category,
        string QuestionText,
        string CorrectAnswer,
        string[] IncorrectAnswers
    );