namespace API.DTOs;

public record SaveSettingsRequest(
    decimal BudgetMin,
    decimal BudgetMax,
    string[] Tags);