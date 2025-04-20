using SakilaAPI.DTOs.Actor;
using SakilaAPI.Models;

namespace SakilaAPI.Services.Interfaces;

public interface IActorService
{
    Task<IEnumerable<ActorDto>>GetActorsAsync(int page, int pageSize);
    Task<ActorDto?>GetActorByIdAsync(int id);
}