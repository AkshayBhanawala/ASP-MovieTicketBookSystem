using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CS_MovieTicketBookingApp.Users.Admin.Management
{
    partial class MOVIE_DELETE : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlCommand CMD;
        private SqlDataAdapter DA;
        private DataSet DS = new DataSet();
        private string QUERY = "";
        private string QUERY2 = "";

        private string MOVIE_ID, MOVIE_NAME, RELEASE_DATE, GENRE, TICKET_PRICE, IMAGE_URL, RATING;

        protected void FILL_DS()
        {
            QUERY = "SELECT * FROM MOVIES";
            DS.Clear();
            DA = new SqlDataAdapter(QUERY, CN);
            DA.Fill(DS);
            QUERY = "";
        }

        protected void FILL_DDL_MOVIE_ID()
        {
            try
            {
                FILL_DS();
                DDL_MOVIE_ID.DataSource = DS.Tables[0];
                DDL_MOVIE_ID.DataValueField = DS.Tables[0].Columns[0].ToString();
                DDL_MOVIE_ID.DataTextField = DS.Tables[0].Columns[1].ToString();
                DDL_MOVIE_ID.DataBind();
                CN.Close();
            }
            catch (Exception ex)
            {
                /*Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx");*/
            }
            QUERY = "";
        }

        protected string get_MonthName(int MN)
        { return (new DateTime(2000, MN, 1)).ToString("MMMM"); }
        protected int get_MonthNumber(string MN)
        { return (Convert.ToDateTime(MN + "01, 2000")).Month; }
        protected string GET_DATE_STRING(int DAY_OF_DATE, int MONTH_OF_DATE, int YEAR_OF_DATE)
        {
            return DAY_OF_DATE + " " + get_MonthName(MONTH_OF_DATE) + " " + YEAR_OF_DATE;
        }

        protected string GET_DATE_STRING(string DAY_OF_DATE, string MONTH_OF_DATE, string YEAR_OF_DATE)
        {
            return DAY_OF_DATE + " " + get_MonthName(int.Parse(MONTH_OF_DATE)) + " " + YEAR_OF_DATE;
        }

        protected void FILL_PAGE_DATA(int SELECTED_INDEX)
        {
            try
            {
                FILL_DS();
                var ROW_DATA = DS.Tables[0].Rows[SELECTED_INDEX];
                MOVIE_ID = ROW_DATA[0].ToString();
                MOVIE_NAME = ROW_DATA[1].ToString();
                RELEASE_DATE = GET_DATE_STRING(ROW_DATA[2].ToString(), ROW_DATA[3].ToString(), ROW_DATA[4].ToString());
                GENRE = ROW_DATA[5].ToString();
                TICKET_PRICE = ROW_DATA[6].ToString();
                IMAGE_URL = @"~\" + ROW_DATA[7];
                RATING = ROW_DATA[8].ToString();
                TAB_MOVIE_INFO.Visible = true;
                L_MOVIE_ID.Text = MOVIE_ID;
                L_MOVIE_NAME.Text = MOVIE_NAME;
                L_MOVIE_RELEASE_DATE.Text = RELEASE_DATE;
                L_MOVIE_GENRE.Text = GENRE;
                L_MOVIE_TICKET_PRICE.Text = TICKET_PRICE;
                IMG_MOVIE_COVER.ImageUrl = IMAGE_URL;
                L_MOVIE_RATING.Text = RATING;
                CN.Close();
            }
            catch (Exception ex)
            {
                /*Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx");*/
            }
            CN.Close();
        }

        protected void DDL_MOVIE_ID_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in DDL_MOVIE_ID.Items)
            { item.Text = Server.HtmlDecode(item.Text); }
        }

        protected void DDL_MOVIE_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FILL_PAGE_DATA(DDL_MOVIE_ID.SelectedIndex);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FILL_DDL_MOVIE_ID();
                FILL_PAGE_DATA(DDL_MOVIE_ID.SelectedIndex);
            }
        }

        protected void RESET_FIELDS()
        {
            try
            {
                FILL_DDL_MOVIE_ID();
                DDL_MOVIE_ID.SelectedIndex = 0;
                FILL_PAGE_DATA(DDL_MOVIE_ID.SelectedIndex);
            }
            catch (Exception ex)
            {
                /*L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;*/
            }
        }

        protected void BTN_DELETE_MOVIE_Click(object sender, EventArgs e)
        {
            L_RESPONSE.Text = "";
            L_ERROR_DESCRIPTION.Text = "";
            try
            {
                FILL_PAGE_DATA(DDL_MOVIE_ID.SelectedIndex);
                QUERY = "DELETE FROM MOVIES WHERE MOVIE_ID='" + DDL_MOVIE_ID.SelectedValue + "'";
                QUERY2 = "DELETE FROM SEATS WHERE MOVIE_ID='" + DDL_MOVIE_ID.SelectedValue + "'";
                File.Delete(Server.MapPath("~") + IMAGE_URL.Substring(2));
                if (!(CN.State == ConnectionState.Open))
                {
                    CN.Close();
                    CN.Open();
                }
                CMD = new SqlCommand(QUERY, CN);
                CMD.ExecuteNonQuery();
                CMD = new SqlCommand(QUERY2, CN);
                CMD.ExecuteNonQuery();
                L_RESPONSE.ForeColor = System.Drawing.Color.Green;
                L_RESPONSE.Text = "SUCCESS";
                T1.Enabled = true;
                RESET_FIELDS();
                CN.Close();
            }
            catch (Exception ex)
            {
                /*L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;*/
            }
            CN.Close();
            QUERY = "";
        }

        protected void BTN_RESET_Click(object sender, EventArgs e)
        {
            RESET_FIELDS();
        }

        protected void T1_Tick(object sender, EventArgs e)
        {
            L_RESPONSE.Text = "";
            L_ERROR_DESCRIPTION.Text = "";
            T1.Enabled = false;
        }
    }
}