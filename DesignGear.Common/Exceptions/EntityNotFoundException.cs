using DesignGear.Common.Diagnostics;
using System.Runtime.Serialization;

namespace DesignGear.Common.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException()
            : base(ErrCodes.ENTITY_NOT_FOUND)
        {
        }

        public EntityNotFoundException(Type type, object key)
            : base(ErrCodes.ENTITY_NOT_FOUND, string.Format("Entity of type '{0}' with key '{1}' not found.", type.FullName, key))
        {
            EntityType = type;
            EntityKey = key;
        }

        public EntityNotFoundException(Type type, params object[] key)
            : this(type, string.Join(", ", key))
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(ErrCodes.ENTITY_NOT_FOUND, message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Type EntityType { get; }

        public object EntityKey { get; }
    }

    [Serializable]
    public class EntityNotFoundException<TEntity> : EntityNotFoundException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(object key)
            : base(typeof(TEntity), key)
        {
        }

        public EntityNotFoundException(params object[] key)
            : base(typeof(TEntity), key)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}