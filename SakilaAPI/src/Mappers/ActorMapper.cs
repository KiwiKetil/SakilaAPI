using System.Globalization;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;

namespace SakilaAPI.Mappers;

public class ActorMapper : IMapper<Actor, ActorDto>
{
    private readonly TextInfo ti = CultureInfo.InvariantCulture.TextInfo;
    public ActorDto MapToDto(Actor entity)
    {    
        return new ActorDto
        (
            ActorId : entity.ActorId,
            FirstName : ti.ToTitleCase(entity.FirstName.ToLowerInvariant()),
            LastName : ti.ToTitleCase(entity.LastName.ToLowerInvariant()),
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