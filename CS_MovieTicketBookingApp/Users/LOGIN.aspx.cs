using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CS_MovieTicketBookingApp.Users
{
    partial class LOGIN : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlCommand CMD;
        private SqlDataReader DR;
        private string QUERY = "";

        private string USERNAME = "";
        private string PASSWORD = "";
        private bool IS_ADMIN = false;
        private bool REMEMBER = false;

        protected void GET_DATA()
        {
            USERNAME = TB_USERNAME.Text;
            PASSWORD = TB_PASSWORD.Text;
            if (CB_REMEMBER.Checked)
                REMEMBER = true;
        }


        protected void performLOGIN()
        {
            QUERY = "SELECT PASSWORD,IS_ADMIN FROM USERS WHERE USERNAME='" + USERNAME + "'";
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
                if (!DR.HasRows)
                    L_MESSAGE1.Text = "✘ - USER NOT FOUND";
                else if (!(PASSWORD == DR[0].ToString()))
                    L_MESSAGE1.Text = "✘ - WRONG USERNAME & PASSWORD COMBINATION";
                else
                {
                    IS_ADMIN = (Boolean)DR[1];
                    if (REMEMBER)
                        SET_COOKIE();
                    SET_SESSION();
                    L_MESSAGE2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#202020");
                    L_MESSAGE2.Text = "--> ReDirecting in 2 Seconds..";
                    L_MESSAGE1.ForeColor = System.Drawing.Color.Green;
                    if (IS_ADMIN)
                        L_MESSAGE1.Text = "✔ - ADMIN LOGGED IN";
                    else
                        L_MESSAGE1.Text = "✔ - USER LOGGED IN";
                    T1.Enabled = true;
                    CN.Close();
                }
            }
            catch (Exception ex)
            {
                CN.Close();
                Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx", false);
            }
            CN.Close();
            QUERY = "";
        }

        protected void SET_COOKIE()
        {
            Response.Cookies["USER"]["UN"] = USERNAME;
            Response.Cookies["USER"]["PW"] = PASSWORD;
            Response.Cookies["USER"].Expires = DateTime.Today.AddDays(1);
        }

        protected void SET_SESSION()
        {
            Session["UN"] = USERNAME;
            Session["PW"] = PASSWORD;
            Session["IS_ADMIN"] = IS_ADMIN;
            Session["IsLoggedIn"] = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.Cookies["USER"] == null))
            {
                USERNAME = Request.Cookies["USER"]["UN"];
                PASSWORD = Request.Cookies["USER"]["PW"];
                performLOGIN();
            }
        }

        protected void BTN_LOGIN_Click(object sender, EventArgs e)
        {
            if (!(TB_USERNAME.Text == ""))
            {
                GET_DATA();
                performLOGIN();
            }
        }

        protected void T1_Tick(object sender, EventArgs e)
        {
            if (IS_ADMIN)
                Response.Redirect(@"~\Users\Admin\HOME_ADMIN.aspx");
            else
                Response.Redirect(@"~\Users\User\HOME_USER.aspx");
        }
    }
}