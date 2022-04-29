using System;
using System.Runtime.Serialization;

namespace DesignGear.Common.Exceptions
{
    [Serializable]
    public class OperationErrorException : BaseException
    {
        public OperationErrorException(int code)
            : base(code)
        {
        }

        public OperationErrorException(int code, string message)
            : base(code, message)
        {
        }

        public OperationErrorException(int code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }

        protected OperationErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}