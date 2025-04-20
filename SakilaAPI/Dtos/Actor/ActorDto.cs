namespace SakilaAPI.DTOs.Actor;

public record ActorDto
(
    ushort ActorId,
    string FirstName,
    string LastName,
    DateTime LastUpdate    
);