using AutoMapper;
using Core.Interfaces.Mappings;
using Microsoft.Extensions.Logging;

namespace Core.Mappings
{
    /// <summary>
    /// Service for mapping objects using AutoMapper.
    /// </summary>
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MapperService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapperService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger instance.</param>
        public MapperService(IMapper mapper, ILogger<MapperService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Maps the source object to a new object of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The mapped object of type <typeparamref name="TDestination"/>.</returns>
        public TDestination Map<TDestination>(object source)
        {
            if (source == null)
            {
                _logger.LogError("Source object is null.");
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                return _mapper.Map<TDestination>(source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while mapping object to {DestinationType}.", typeof(TDestination).Name);
                throw;
            }
        }

        /// <summary>
        /// Maps the source object to a new object of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The mapped object of type <typeparamref name="TDestination"/>.</returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                _logger.LogError("Source object is null.");
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                return _mapper.Map<TSource, TDestination>(source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while mapping object from {SourceType} to {DestinationType}.", typeof(TSource).Name, typeof(TDestination).Name);
                throw;
            }
        }
    }
}
