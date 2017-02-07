using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loyalty.Controllers 
{
    public class HttpModuleListener : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
        }

        public void OnBeginRequest(object sender, EventArgs e)
        {
            // BeginRequest(this, new EventArgs());

            // Check if user is login or not

        }
    }
}