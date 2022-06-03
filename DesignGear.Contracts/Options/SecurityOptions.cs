﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Options {
    public class SecurityOptions {
        public string PasswordRecoveryUrl { get; set; }
        public int RecoveryTokenLifeTimeInSeconds { get; set; }
    }
}