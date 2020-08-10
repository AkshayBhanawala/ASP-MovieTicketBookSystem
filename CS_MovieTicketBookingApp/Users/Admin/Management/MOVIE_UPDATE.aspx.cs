using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CS_MovieTicketBookingApp.Users.Admin.Management
{
    partial class MOVIE_UPDATE : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlCommand CMD;
        private SqlDataAdapter DA;
        private DataSet DS = new DataSet();
        private string QUERY = "";

        private string MOVIE_ID, MOVIE_NAME, GENRE, OLD_IMAGE_URL, NEW_IMAGE_URL, QUERY_STRING, DESCRIPTION;
        private int RELEASE_DAY, RELEASE_MONTH, RELEASE_YEAR, TICKET_PRICE, RATING;

        private bool USE_OLD_COVER = true;

        private string ImagesDirectory = AppDomain.CurrentDomain.GetData("ImagesDirectory").ToString();

        protected void SET_DATE_VARS(DateTime DATE_TO_SET)
        {
            RELEASE_DAY = DATE_TO_SET.Day;
            RELEASE_MONTH = DATE_TO_SET.Month;
            RELEASE_YEAR = DATE_TO_SET.Year;
        }

        protected string get_MonthName(int MN)
        { return (new DateTime(2000, MN, 1)).ToString("MMMM"); }
        protected int get_MonthNumber(string MN)
        { return (Convert.ToDateTime(MN + "01, 2000")).Month; }

        protected string GET_DATE_STRING(int DAY_OF_DATE, int MONTH_OF_DATE, int YEAR_OF_DATE)
        {
            return DAY_OF_DATE + " " + get_MonthName(MONTH_OF_DATE) + " " + YEAR_OF_DATE;
        }

        protected void BTN_SHOW_HIDE_RD_CAL_Click(object sender, EventArgs e)
        {
            if (CAL_RELEASE_DATE.Visible)
            {
                CAL_RELEASE_DATE.Visible = false;
                BTN_SHOW_HIDE_RD_CAL.Text = "SHOW CALENDAR";
            }
            else
            {
                CAL_RELEASE_DATE.Visible = true;
                BTN_SHOW_HIDE_RD_CAL.Text = "HIDE CALENDAR";
            }
            BTN_UPDATE_MOVIE.Focus();
        }

        protected void CAL_RELEASE_DATE_SelectionChanged(object sender, EventArgs e)
        {
            SET_DATE_VARS(CAL_RELEASE_DATE.SelectedDate);
            TB_RELEASE_DATE.Text = GET_DATE_STRING(RELEASE_DAY, RELEASE_MONTH, RELEASE_YEAR);
            BTN_SHOW_HIDE_RD_CAL_Click(BTN_SHOW_HIDE_RD_CAL, new EventArgs());
            VALIDATE_CONTROL("TB_RELEASE_DATE");
        }

        protected void FILL_DS()
        {
            QUERY = "SELECT * FROM MOVIES";
            DS.Clear();
            DA = new SqlDataAdapter(QUERY, CN);
            DA.Fill(DS);
            QUERY = "";
        }

        protected string CONVERT_DATE(string TEMP_DAY, string TEMP_MONTH, string TEMP_YEAR)
        {
            return TEMP_DAY + " " + TEMP_MONTH + " " + TEMP_YEAR;
        }

        protected void FILL_TB_VALUES(int SELECTED_INDEX)
        {
            try
            {
                FILL_DS();
                var ROW_DATA = DS.Tables[0].Rows[SELECTED_INDEX];
                string RELEASE_DATE = CONVERT_DATE(ROW_DATA[2].ToString(), get_MonthName(int.Parse(ROW_DATA[3].ToString())), ROW_DATA[4].ToString());
                TB_MOVIE_NAME.Text = Server.HtmlDecode(ROW_DATA[1].ToString());
                TB_RELEASE_DATE.Text = RELEASE_DATE;
                CAL_RELEASE_DATE.SelectedDate = Convert.ToDateTime(RELEASE_DATE);
                TB_GENRE.Text = ROW_DATA[5] + " ";
                TB_TICKET_PRICE.Text = ROW_DATA[6].ToString();
                OLD_IMAGE_URL = ROW_DATA[7].ToString();
                IMG_CURRENT_COVER_IMAGE.ImageUrl = "~/" + ROW_DATA[7];
                DDL_RATING.SelectedValue = ROW_DATA[8].ToString();
            }
            catch (Exception ex)
            {
                /*Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx");*/
            }
        }
        
        protected void DDL_MOVIE_ID_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in DDL_MOVIE_ID.Items)
            { item.Text = Server.HtmlDecode(item.Text); }
        }

        protected void DDL_MOVIE_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FILL_TB_VALUES(DDL_MOVIE_ID.SelectedIndex);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FILL_DDL_MOVIE_ID();
                try
                { DDL_MOVIE_ID.SelectedIndex = 0;}
                catch (Exception ex)
                {
                    /*L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                    L_RESPONSE.Text = "ERROR";
                    L_ERROR_DESCRIPTION.Text = ex.Message;*/
                }
                FILL_TB_VALUES(0);
            }
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
            }
            catch (Exception ex)
            {
                /*Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx");*/
            }
            QUERY = "";
        }

        protected bool VALIDATE_CONTROL(string CONTROL_ID)
        {
            bool CONTROL_VALID_FLAG_1, CONTROL_VALID_FLAG_2;
            switch (CONTROL_ID)
            {
                case "TB_MOVIE_NAME":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_MOVIE_NAME.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_MOVIE_NAME.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_MOVIE_NAME.CssClass = "input_field_valid";
                            CAL_RELEASE_DATE.Focus();
                            return true;
                        }
                        else
                        {
                            TB_MOVIE_NAME.CssClass = "input_field_error";
                            TB_MOVIE_NAME.Focus();
                            return false;
                        }
                    }

                case "TB_RELEASE_DATE":
                    {
                        CUSTV_RELEASE_DATE_ServerValidate();
                        CONTROL_VALID_FLAG_1 = CUSTV_RELEASE_DATE.IsValid;
                        if (CONTROL_VALID_FLAG_1)
                        {
                            TB_RELEASE_DATE.CssClass = "input_field_valid";
                            CAL_RELEASE_DATE.CssClass = "input_field_valid";
                            TB_GENRE.Focus();
                            return true;
                        }
                        else
                        {
                            TB_RELEASE_DATE.CssClass = "input_field_error";
                            CAL_RELEASE_DATE.CssClass = "input_field_error";
                            CAL_RELEASE_DATE.Focus();
                            return false;
                        }
                    }

                case "TB_GENRE":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_GENRE.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_GENRE.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_GENRE.CssClass = "input_field_valid";
                            TB_TICKET_PRICE.Focus();
                            return true;
                        }
                        else
                        {
                            TB_GENRE.CssClass = "input_field_error";
                            TB_GENRE.Focus();
                            return false;
                        }
                    }

                case "TB_TICKET_PRICE":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_TICKET_PRICE.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_TICKET_PRICE.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_TICKET_PRICE.CssClass = "input_field_valid";
                            FU_COVER_IMAGE.Focus();
                            return true;
                        }
                        else
                        {
                            TB_TICKET_PRICE.CssClass = "input_field_error";
                            TB_TICKET_PRICE.Focus();
                            return false;
                        }
                    }

                case "FU_COVER_IMAGE":
                    {
                        CUSTV_COVER_IMAGE_ServerValidate();
                        CONTROL_VALID_FLAG_1 = CUSTV_COVER_IMAGE.IsValid;
                        if (CONTROL_VALID_FLAG_1)
                        {
                            FU_COVER_IMAGE.CssClass = "input_field_valid";
                            BTN_UPDATE_MOVIE.Focus();
                            return true;
                        }
                        else
                        {
                            FU_COVER_IMAGE.CssClass = "input_field_error";
                            FU_COVER_IMAGE.Focus();
                            return false;
                        }
                    }
            }

            return default(bool);
        }

        protected void CUSTV_RELEASE_DATE_ServerValidate()
        {
            DateTime TEMP_RDATE = CAL_RELEASE_DATE.SelectedDate;
            if ((DateTime.Today - TEMP_RDATE).Days < 0)
                CUSTV_RELEASE_DATE.IsValid = false;
        }

        protected void CUSTV_COVER_IMAGE_ServerValidate()
        {
            if (FU_COVER_IMAGE.HasFile)
            {
                if (FU_COVER_IMAGE.PostedFile.ContentType == "image/jpeg" | FU_COVER_IMAGE.PostedFile.ContentType == "image/png")
                {
                    if (FU_COVER_IMAGE.PostedFile.ContentLength / 1024 <= 1024)
                    {
                        CUSTV_COVER_IMAGE.IsValid = true;
                        USE_OLD_COVER = false;
                    }
                    else
                    {
                        CUSTV_COVER_IMAGE.IsValid = false;
                        CUSTV_COVER_IMAGE.ErrorMessage = "COVER IMAGE - SIZE MUST BE <= 1024 KB !!!";
                    }
                }
                else
                {
                    CUSTV_COVER_IMAGE.IsValid = false;
                    CUSTV_COVER_IMAGE.ErrorMessage = "COVER IMAGE - ONLY JPG OR PNG IMAGE IS VALID !!!";
                }
            }
            else
            {
                USE_OLD_COVER = true;
                CUSTV_COVER_IMAGE.IsValid = true;
            }
        }

        protected void RESET_FIELDS()
        {
            try
            {
                var FORM_CONTROLS = new[] { TB_MOVIE_NAME, TB_RELEASE_DATE, TB_GENRE, TB_TICKET_PRICE };
                foreach (var OBJ in FORM_CONTROLS)
                {
                    OBJ.Text = "";
                    OBJ.CssClass = "input_field_initial";
                }
                TB_RELEASE_DATE.CssClass = "input_field_initial";
                CAL_RELEASE_DATE.CssClass = "input_field_initial";
                FU_COVER_IMAGE.CssClass = "input_field_initial";
                FILL_DDL_MOVIE_ID();
                DDL_MOVIE_ID.SelectedIndex = 0;
                FILL_TB_VALUES(0);
            }
            catch (Exception ex)
            {
                /*L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;*/
            }
        }

        protected void BTN_UPDATE_MOVIE_Click(object sender, EventArgs e)
        {
            WebControl[] FORM_CONTROLS = new WebControl[] { TB_MOVIE_NAME, TB_RELEASE_DATE, TB_GENRE, TB_TICKET_PRICE, FU_COVER_IMAGE };
            bool IS_FORM_VALID = true;
            foreach (WebControl OBJ in FORM_CONTROLS)
            {
                bool IS_CONTROL_VALID = VALIDATE_CONTROL(OBJ.ID.ToString());
                if (!IS_CONTROL_VALID)
                {
                    IS_FORM_VALID = false;
                    break;
                }
            }
            if (IS_FORM_VALID)
            {
                if (!VALIDATE_CONTROL(FU_COVER_IMAGE.ID.ToString()))
                    IS_FORM_VALID = false;
            }
            if (IS_FORM_VALID)
            {
                MOVIE_ID = DDL_MOVIE_ID.SelectedValue;
                MOVIE_NAME = TB_MOVIE_NAME.Text.ToUpper();
                SET_DATE_VARS(CAL_RELEASE_DATE.SelectedDate);
                GENRE = TB_GENRE.Text;
                TICKET_PRICE = int.Parse(TB_TICKET_PRICE.Text);
                OLD_IMAGE_URL = IMG_CURRENT_COVER_IMAGE.ImageUrl.Substring(2);
                NEW_IMAGE_URL = OLD_IMAGE_URL;
                if (!USE_OLD_COVER)
                    NEW_IMAGE_URL = "Images/" + MOVIE_ID + "_Poster" + Path.GetExtension(FU_COVER_IMAGE.PostedFile.FileName);
                RATING = int.Parse(DDL_RATING.SelectedValue);
                QUERY_STRING = "?MID=" + MOVIE_ID;
                QUERY = "UPDATE MOVIES SET MOVIE_NAME='" + MOVIE_NAME + "', RELEASE_DAY=" + RELEASE_DAY + ", RELEASE_MONTH='" + RELEASE_MONTH + "', RELEASE_YEAR=" + RELEASE_YEAR + ", GENRE='" + GENRE + "', TICKET_PRICE=" + TICKET_PRICE + ", IMAGE_URL='" + NEW_IMAGE_URL + "', RATING=" + RATING + ", QUERY_STRING='" + QUERY_STRING + "', DESCRIPTION='" + DESCRIPTION + "' WHERE MOVIE_ID='" + MOVIE_ID + "'";
                try
                {
                    if (!USE_OLD_COVER)
                    {
                        try
                        {
                            File.Delete(Server.MapPath("~") + @"Images\" + Path.GetFileName(OLD_IMAGE_URL));
                        }
                        catch (Exception EX)
                        {
                        }
                        FU_COVER_IMAGE.PostedFile.SaveAs(ImagesDirectory + @"\" + Path.GetFileName(NEW_IMAGE_URL));
                    }
                    if (!(CN.State == ConnectionState.Open))
                    {
                        CN.Close();
                        CN.Open();
                    }
                    CMD = new SqlCommand(QUERY, CN);
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