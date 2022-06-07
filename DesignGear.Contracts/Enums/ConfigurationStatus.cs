namespace DesignGear.Contracts.Enums
{
    [Flags]
    public enum ConfigurationStatus {
        InQueue = 1,
        InProcess = 2,
        /*Ограничить кол-во запросов для заявок на конфигурацию с данным статусом (или прекратить делать запросы совсем или увеличивать интревал между запросами)*/
        ServiceUnavailableError = 4,
        IncorrectRequestError = 8,
        InvalidConfiguration = 16,
        Ready = 32
    }
}
