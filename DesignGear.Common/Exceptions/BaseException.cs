using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DesignGear.Common.Exceptions
{
    [Serializable]
    public class BaseException : ApplicationException
    {
        public BaseException(int code)
        {
            Code = code;
        }

        public BaseException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        public BaseException(int code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetInt32(nameof(Code));
        }

        public int Code { get; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.AddValue(nameof(Code), Code);
            base.GetObjectData(info, context);
        }
    }
}
