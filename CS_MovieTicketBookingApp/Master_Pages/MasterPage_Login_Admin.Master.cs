using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CS_MovieTicketBookingApp.Master_Pages
{
    public partial class MasterPage_Login_Admin : System.Web.UI.MasterPage
    {
        private string USERNAME = "";
        private string PASSWORD = "";
        private bool IS_ADMIN = false;

        protected bool IS_USER_VALID()
        {
            var CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
            SqlCommand CMD;
            SqlDataReader DR;
            string QUERY = "SELECT IS_ADMIN FROM USERS WHERE USERNAME='" + USERNAME + "' and PASSWORD='" + PASSWORD + "'";
            try
            {
                if (!(CN.State == ConnectionState.Open))
                {
                    CN.Close();
                    CN.Open();
                }
                CMD = new SqlCommand(QUERY, CN);
                DR = CMD.ExecuteReader();
                DR.Read();
                if (DR.HasRows)
                {
                    IS_ADMIN = (Boolean)DR[0];
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Session["ERROR"] = ex.Message.ToString();
                Response.Redirect("~/ERROR_PAGE.aspx");
            }

            return default(bool);
        }

        protected void CHECK_LOGIN()
        {
            if (Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"])
            {
                IS_ADMIN = (Boolean)Session["IS_ADMIN"];
                if (!IS_ADMIN)
                {
                    Session["ERROR"] = "NOT_AUTHORISED";
                    Response.Redirect("~/ERROR_PAGE.aspx");
                }
            }
            else if (!(Request.Cookies["USER"] == null))
            {
                USERNAME = Request.Cookies["USER"]["UN"];
                PASSWORD = Request.Cookies["USER"]["PW"];
                if (IS_USER_VALID())
                {
                    Session["UN"] = USERNAME;
                    Session["PW"] = PASSWORD;
                    Session["IsLoggedIn"] = true;
                    Session["IS_ADMIN"] = IS_ADMIN;
                    CHECK_LOGIN();
                }
                else
                {
                    Request.Cookies.Remove("USER");
                    Response.Cookies["USER"].Expires = DateTime.Today.AddDays(-1);
                    Response.Redirect("~/Users/LOGIN.aspx");
                }
            }
            else
            {
                Session["ERROR"] = "NO_LOGIN";
                Response.Redirect("~/ERROR_PAGE.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CHECK_LOGIN();
        }
    }
}