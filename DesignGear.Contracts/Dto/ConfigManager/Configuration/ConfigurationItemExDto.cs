namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationItemExDto : ConfigurationItemDto
    {
        public Guid ComponentDefinitionId { get; set; }
        public Guid ProductVersionId { get; set; }
        public Guid TargetFileId { get; set; }
        public string RootFileName { get; set; }
        public string URN { get; set; }
        public string WorkItemId { get; set; }
        public string WorkItemUrl { get; set; }
    }
}
