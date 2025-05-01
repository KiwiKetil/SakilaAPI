using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;

namespace SakilaAPI.Mappers;

public class ActorUpdateMapper : IMapper<Actor, ActorUpdateDto>
{
    public ActorUpdateDto MapToDto(Actor entity)
    {       
        throw new NotImplementedException();               
    }

    public Actor MapToEntity(ActorUpdateDto dto)
    {
        return new Actor
        {
            FirstName = dto.FirstName.Trim().ToUpper(),
            LastName = dto.LastName.Trim().ToUpper()
        };
    }
}