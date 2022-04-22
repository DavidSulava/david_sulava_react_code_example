namespace DesignGear.Common.Diagnostics
{
    public static class ErrCodes
    {
        // Common error codes.
        public static readonly int UNEXPECTED_ERROR = 0x0001;

        // Database error codes.
        public static readonly int TRANSACTION_ALREADY_STARTEDT = 0xA001;

        public static readonly int TRANSACTION_NOT_STARTED = 0xA002;

        // Data error codes.
        public static readonly int ENTITY_NOT_FOUND = 0xB001;

        public static readonly int ENTITY_WITH_SAME_PROPERTIES_EXISTS = 0xB002;
        public static readonly int ENTITY_TOO_LARGE = 0xB003;

        public static readonly int ENTITY_CANNOT_BE_DELETED = 0xB004;

        // Security error codes.
        public static readonly int THREAD_PRINCIPAL_IS_NULL = 0xC001;

        public static readonly int CANNOT_CREATE_USER = 0xC002;
    }
}