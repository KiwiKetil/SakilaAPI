using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;

namespace SakilaAPI.Mappers;

public class ActoRegistrationMapper : IMapper<Actor, ActorRegistrationDto>
{
    public ActorRegistrationDto MapToDto(Actor entity)
    {
        throw new NotImplementedException();        
    }

    public Actor MapToEntity(ActorRegistrationDto dto)
    {
        return new Actor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }
}