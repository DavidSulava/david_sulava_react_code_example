﻿namespace DesignGear.Contracts.Models
{
    public class VmProductCreate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
