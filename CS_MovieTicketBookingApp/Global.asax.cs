using System;

namespace CS_MovieTicketBookingApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string DataBasesDirectory = Server.MapPath("~") + @"App_Data\Databases";
            string ImagesDirectory = Server.MapPath("~") + @"\Images";
            AppDomain.CurrentDomain.SetData("DataBasesDirectory", DataBasesDirectory);
            AppDomain.CurrentDomain.SetData("ImagesDirectory", ImagesDirectory);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}