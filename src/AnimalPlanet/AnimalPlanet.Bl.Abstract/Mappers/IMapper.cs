namespace AnimalPlanet.Bl.Abstract.Mappers
{
    public interface IMapper<TEntity, TModel>
    {
        public TModel Map(TEntity entity);
        public TEntity MapBack(TModel model);
        public TEntity MapUpdate(TEntity entity, TModel model);
    }
}
