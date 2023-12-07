using System.Text.Json.Serialization;

namespace Domain.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QuestionType
{
    Multiple,
    Boolean
}