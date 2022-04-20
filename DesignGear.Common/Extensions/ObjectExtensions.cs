using AutoMapper;

namespace DesignGear.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static TOutput MapTo<TOutput>(this object obj, IMapper mapper)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            return mapper.Map<TOutput>(obj);
        }
    }
}
