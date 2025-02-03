﻿namespace Core.Interfaces.Mappings
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
