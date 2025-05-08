using System.Globalization;
using SakilaAPI.DTOs.Actor;
using SakilaAPI.Mappers.Interfaces;
using SakilaAPI.Models;

namespace SakilaAPI.Mappers;

public class ActoRegistrationMapper : IMapper<Actor, ActorRegistrationDto>
{
     private readonly TextInfo ti = CultureInfo.InvariantCulture.TextInfo;
     
    public ActorRegistrationDto MapToDto(Actor entity)
    {
        throw new NotImplementedException();        
    }

    public Actor MapToEntity(ActorRegistrationDto dto)
    {
        return new Actor
        {
            FirstName = dto.FirstName.Trim().ToUpper(),
            LastName = dto.LastName.Trim().ToUpper()
        };
    }
}