﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAPIArchitectureTemplate.Startup))]

namespace WebAPIArchitectureTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
