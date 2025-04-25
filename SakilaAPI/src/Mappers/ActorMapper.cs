using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;

namespace SakilaAPI.Mappers;

public class ActorMapper : IMapper<Actor, ActorDto>
{
    public ActorDto MapToDto(Actor entity)
    {
        return new ActorDto
        (
            ActorId : entity.ActorId,
            FirstName : entity.FirstName,
            LastName : entity.LastName,
            LastUpdate : entity.LastUpdate
        );
    }

    public Actor MapToEntity(ActorDto dto)
    {
        return new Actor()
        {
            ActorId = dto.ActorId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            LastUpdate = dto.LastUpdate
        };
    }
}