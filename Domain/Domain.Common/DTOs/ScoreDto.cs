namespace Domain.Common.DTOs;

public record ScoreDto(double Score, Guid PlayerId, DateTime Created);