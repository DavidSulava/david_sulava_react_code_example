using Microsoft.AspNetCore.Http;

namespace DesignGear.Contracts.Models.ConfigManager
{
    //Данные для создания конфигурации
    public class VmConfigurationCreate
    {
        public Guid OrganizationId { get; set; } //Передаем организацию, т.к. внутри CM нет подробной инфы о ProductVersionId
        public Guid ProductId { get; set; } //Передаем продукт, т.к. внутри CM нет подробной инфы о ProductVersionId
        public Guid ProductVersionId { get; set; }
        //public ICollection<string> Emails { get; set; } //Кого оповестить, когда кофигурация будет готова (пока не используем, делаем синхронно)

        //Значения параметров, которые выбрал пользователь для создания конфигурации
        //public ICollection<VmConfigurationParameterValues> ConfigurationParameters { get; set; }

        public IFormFile ModelFile { get; set; }
    }
}
