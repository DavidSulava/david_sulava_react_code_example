﻿namespace DesignGear.Contracts.Models.Notification {
    public class EmailRequestModel {
        public string TargetAddress { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}