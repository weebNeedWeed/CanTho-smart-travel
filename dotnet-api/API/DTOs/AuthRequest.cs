namespace API.DTOs;

public record AuthRequest(
    string Username,
    string Password);