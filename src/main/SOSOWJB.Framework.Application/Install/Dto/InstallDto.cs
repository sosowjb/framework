﻿using System.ComponentModel.DataAnnotations;
using SOSOWJB.Framework.Configuration.Host.Dto;

namespace SOSOWJB.Framework.Install.Dto
{
    public class InstallDto
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string AdminPassword { get; set; }

        [Required]
        public string WebSiteUrl { get; set; }

        public string ServerUrl { get; set; }

        [Required]
        public string DefaultLanguage { get; set; }

        public EmailSettingsEditDto SmtpSettings { get; set; }

        public HostBillingSettingsEditDto BillInfo { get; set; }
    }
}