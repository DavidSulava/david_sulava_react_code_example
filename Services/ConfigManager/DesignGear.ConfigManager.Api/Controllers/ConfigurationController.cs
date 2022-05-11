using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ConfigManager.Api.Controllers {
    //Значение параметра компонента, переданное с фронта
    public class VmParameterValue { 
        public int ParameterId { get; set; }
        public string Value { get; set; }
    }
    
    //Список значений параметров компонента
    public class VmConfigurationParameterValues {
        public Guid ComponentId { get; set; }
        public ICollection<VmParameterValue> ParameterValues { get; set; }
    }

    //Данные для создания конфигурации
    public class VmConfigurationCreate {
        public Guid OrganizationId { get; set; } //Передаем организацию, т.к. внутри CM нет подробной инфы о ProductVersionId
        public Guid ProductId { get; set; } //Передаем продукт, т.к. внутри CM нет подробной инфы о ProductVersionId
        public Guid ProductVersionId { get; set; }
        public ICollection<string> Emails { get; set; } //Кого оповестить, когда кофигурация будет готова (пока не используем, делаем синхронно)

        //Значения параметров, которые выбрал пользователь для создания конфигурации
        public ICollection<VmConfigurationParameterValues> ConfigurationParameters { get; set; }

        //Другие свойства
    }

    //Класс для отображения списка конфигураций в гриде (дерево, т.к. могут быть подкомпоненты)
    public class VmConfigurationItem {
        public ICollection<VmConfigurationItem> Childs { get; set; }
        //Основной набор свойств
    }

    //Класс, содержащий подробную информацию о конфигурации
    public class VmConfiguration {

    }

    //Описание параметра, по схеме похожее на то что есть в xsd, но усеченное (только то что нужно фронту)
    public class VmParameterDefinition {

    }

    //Список параметров конфигурации, в том числе параметры дочерних конфигураций (дерево)
    public class VmComponentParameterDefinitions {
        public Guid ComponentId { get; set; }
        public string ComponentName { get; set; }
        public ICollection<VmParameterDefinition> ParameterDefinitions { get; set; }
        public ICollection<VmComponentParameterDefinitions> ChildComponents { get; set; }
    }

    /*public class ConfigurationController : ControllerBase {
        public ConfigurationController() {

        }

        [HttpPost]
        public async Task CreateConfigurationAsync(VmConfigurationCreate create) {

        }

        [HttpGet]
        public async Task<ICollection<VmConfigurationItem>> GetConfigurationItems() {

        }

        [HttpGet("{id}")]
        public async Task<VmConfiguration> GetConfigurationAsync([FromRoute] Guid id) {

        }

        [HttpDelete("{id}")]
        public async Task DeleteConfigurationAsync([FromRoute] Guid id) {

        }

        //Пока возвращаем файл в потоке, потом скорее всего это будет ссылка на файл в хранилище
        [HttpGet("{id}/svf")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid id) {

        }

        [HttpGet("parameters")]
        public async Task<VmConfigurationParameters> GetComponentParameterDefinitions(Guid productVersionId) {

        }
    }*/
}
