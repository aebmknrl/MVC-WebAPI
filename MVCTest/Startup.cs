﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using MVCTest.Logic;
using System.Web.Security;

[assembly: OwinStartup(typeof(MVCTest.Startup))]

namespace MVCTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
