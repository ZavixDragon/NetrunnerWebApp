﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetrunnerWebApp.Models
{
    public class Mail
    {
        public string Recipient { get; set; }
        public string Header { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}