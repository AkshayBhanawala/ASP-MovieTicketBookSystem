using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CS_MovieTicketBookingApp.Master_Pages
{
    public partial class MasterPage_Main : System.Web.UI.MasterPage
    {
        private string FULL_NAME, USERNAME, EMAIL, AGE, MOBILE_NO;
        private string PASSWORD = "";
        private bool IS_ADMIN = false;
        private int BACKGROUND_BLUR = 5;


        protected string Get_Content_Data(int[] Content)
        {
            string ContentData = "";
            foreach (int Ch in Content)
                ContentData += (Char)(Ch - 11);
            return ContentData;
        }

        protected void createCSSFile(string[] ImageFiles)
        {
            var R = new Random();
            var CSSFilePath = Server.MapPath("~") + @"\Style\CSS2.css";
            string CSSCode = ".Body_Background {\n";
            CSSCode += "\tbackground: url('" + ImageFiles[R.Next(1, ImageFiles.Length - 1)] + "');\n";
            CSSCode += "\tbackground-position: center;\n";
            CSSCode += "\tbackground-attachment: fixed;\n";
            CSSCode += "\tbackground-repeat: no-repeat;\n";
            CSSCode += "\tbackground-size: cover;\n";
            CSSCode += "\ttransform: scale(1.0" + (BACKGROUND_BLUR + Math.Ceiling(BACKGROUND_BLUR / (double)2)).ToString() + ");\n";
            CSSCode += "\tfilter: blur(" + BACKGROUND_BLUR.ToString() + "px);\n";
            CSSCode += "\tdisplay: block;\n";
            CSSCode += "\tposition: fixed;\n";
            CSSCode += "\twidth: 100%;\n";
            CSSCode += "\theight: 100%;\n";
            CSSCode += "\tmargin-top: -2VH;\n";
            CSSCode += "\tleft: 0;\n";
            CSSCode += "\tright: 0;\n";
            CSSCode += "\toverflow-y: auto;\n";
            CSSCode += "\tz-index: -2;" + "}\n";
            CSSCode += "\n" + ".Body_Text::before {\n";
            CSSCode += "\tposition: fixed;\n";
            CSSCode += "\tcontent:'" + Get_Content_Data(new[] { 79, 112, 126, 116, 114, 121, 112, 111, 43, 77, 100, 43, 95, 83, 62, 56, 76, 101 }) + "';\n";
            CSSCode += "\tright: 1%;\n";
            CSSCode += "\tbottom: 0;\n";
            CSSCode += "\tfont-size: 100%;\n";
            CSSCode += "\tmix-blend-mode: difference;" + "}\n";
            File.WriteAllText(CSSFilePath, CSSCode);
        }

        protected void SetBGImageCSS()
        {
			/*
            var ImageFiles = Directory.GetFiles(Server.MapPath("~") + @"\Images\bgs");
            if (!(ImageFiles.Length == 0))
            {
                for (int I = 0, loopTo = ImageFiles.Length - 1; I <= loopTo; I++)
                    ImageFiles[I] = "../Images/bgs/" + Path.GetFileName(ImageFiles[I]);
                createCSSFile(ImageFiles);
            }
            */
            String ImagesLinksFile = Server.MapPath("~") + @"\Images\bgs\LINKS.source";
            String[] ImagesLinks = File.ReadAllLines(ImagesLinksFile);
            createCSSFile(ImagesLinks);
            

        }

        protected void SET_DETAILS()
        {
            L_FULL_NAME.Text = FULL_NAME;
            L_USERNAME.Text = USERNAME;
            L_AGE.Text = AGE;
            L_EMAIL.Text = EMAIL;
            L_MOBILE_NO.Text = MOBILE_NO;
        }
        protected void GET_DETAILS()
        {
            USERNAME = Session["UN"].ToString();
            PASSWORD = Session["PW"].ToString();
            var CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
            string QUERY = "Select * FROM USERS WHERE USERNAME='" + USERNAME + "' and PASSWORD='" + PASSWORD + "'";
            SqlCommand CMD;
            SqlDataReader DR;
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
                USERNAME = DR[0].ToString();
                EMAIL = DR[1].ToString();
                FULL_NAME = DR[3].ToString();
                AGE = DR[4].ToString();
                MOBILE_NO = DR[5].ToString();
            }
            catch (Exception ex)
            {
                Session["ERROR"] = ex.Message.ToString();
                Response.Redirect("~/ERROR_PAGE.aspx");
            }
        }

        protected bool CHECK_LOGIN()
        {
            if (Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"])
                return true;
            else
                return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                SetBGImageCSS();
            }
            if (CHECK_LOGIN())
            {
                GET_DETAILS();
                SET_DETAILS();
                L_TEXT.Text = "You are Logged in as,";
                L_HEAD_USERNAME.Text = USERNAME;
                if (IS_ADMIN)
                    L_EMOJI.Text = "😎";
                else
                    L_EMOJI.Text = "😊";
                BTN_LOGOUT.Visible = true;
            }
            else
            {
                L_TEXT.Text = "You are <span style='color:red; font-size:3vh;'>Not</span> Logged In !!";
                L_HEAD_USERNAME.Text = "";
                L_EMOJI.Text = "😕";
            }
        }

        protected void BTN_LOGOUT_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["USER"] != null)
            { Response.Cookies["USER"].Expires = DateTime.Now.AddDays(-1); }
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/HOME.ASPX");
        }
    }
}