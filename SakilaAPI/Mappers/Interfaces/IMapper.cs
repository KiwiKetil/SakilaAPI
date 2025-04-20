namespace SakilaAPI.Mappers.Interfaces;

public interface IMapper<TEntity, TDto>
{
    TDto MapToDto(TEntity entity);
    TEntity MapToEntity(TDto dto);
}