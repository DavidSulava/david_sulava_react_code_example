﻿using DesignGear.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contracts.Models.Contractor
{
    public class Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrganizationId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string? Description { get; set; }

        public double CloudPoints { get; set; }

        public int SpaceUsed { get; set; }

        public OrganizationType OrgType { get; set; }
    }
}
