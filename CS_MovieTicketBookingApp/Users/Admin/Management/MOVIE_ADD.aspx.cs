using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CS_MovieTicketBookingApp.Users.Admin.Management
{
    partial class MOVIE_ADD : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlCommand CMD;
        private SqlDataReader DR;
        private string QUERY1, QUERY2;

        private string MOVIE_NAME, GENRE, IMAGE_URL, QUERY_STRING, DESCRIPTION;
        private int MOVIE_ID, RELEASE_DAY, RELEASE_MONTH, RELEASE_YEAR, TICKET_PRICE, RATING;

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
            if (CAL_RELEASE_DATE.CssClass.IndexOf("hide") < 0)
            {
                CAL_RELEASE_DATE.CssClass += "hide";
                BTN_SHOW_HIDE_RD_CAL.Text = "SHOW CALENDAR";
            }
            else
            {
                CAL_RELEASE_DATE.CssClass = CAL_RELEASE_DATE.CssClass.Replace("hide", "");
                BTN_SHOW_HIDE_RD_CAL.Text = "HIDE CALENDAR";
            }
            BTN_ADD_MOVIE.Focus();
        }

        protected void CAL_RELEASE_DATE_SelectionChanged(object sender, EventArgs e)
        {
            SET_DATE_VARS(CAL_RELEASE_DATE.SelectedDate);
            TB_RELEASE_DATE.Text = GET_DATE_STRING(RELEASE_DAY, CAL_RELEASE_DATE.SelectedDate.Month, RELEASE_YEAR);
            BTN_SHOW_HIDE_RD_CAL_Click(BTN_SHOW_HIDE_RD_CAL, new EventArgs());
            VALIDATE_CONTROL("TB_RELEASE_DATE");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CAL_RELEASE_DATE.SelectedDate = DateTime.Today;
                TB_RELEASE_DATE.Text = DateTime.Today.Day + " " + get_MonthName(DateTime.Today.Month) + " " + DateTime.Today.Year;
            }
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
                            TB_GENRE.Focus();
                            return true;
                        }
                        else
                        {
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
                            BTN_ADD_MOVIE.Focus();
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
            /*DateTime TEMP_RDATE = CAL_RELEASE_DATE.SelectedDate;
            if ((DateTime.Today - TEMP_RDATE).Days < 0)
            { CUSTV_RELEASE_DATE.IsValid = false; }*/
        }

        protected void CUSTV_COVER_IMAGE_ServerValidate()
        {
            if (FU_COVER_IMAGE.HasFile)
            {
                if (FU_COVER_IMAGE.PostedFile.ContentType == "image/jpeg" | FU_COVER_IMAGE.PostedFile.ContentType == "image/png")
                {
                    if (FU_COVER_IMAGE.PostedFile.ContentLength / 1024 <= 1024)
                        CUSTV_COVER_IMAGE.IsValid = true;
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
                CUSTV_COVER_IMAGE.IsValid = false;
                CUSTV_COVER_IMAGE.ErrorMessage = "COVER IMAGE - SELECT A COVER IMAGE !!!";
            }
        }

        protected void RESET_FIELDS()
        {
            var FORM_CONTROLS = new[] { TB_MOVIE_NAME, TB_RELEASE_DATE, TB_GENRE, TB_TICKET_PRICE };
            foreach (var OBJ in FORM_CONTROLS)
            {
                OBJ.Text = "";
                OBJ.CssClass = "input_field_initial";
            }
            TB_RELEASE_DATE.Text = DateTime.Today.Day + " " + get_MonthName(DateTime.Today.Month) + " " + DateTime.Today.Year;
            TB_RELEASE_DATE.CssClass = "input_field_initial";
            CAL_RELEASE_DATE.SelectedDate = DateTime.Today;
            FU_COVER_IMAGE.CssClass = "input_field_initial";
        }

        protected void Update_MOVIE_ID()
        {
            QUERY1 = "SELECT IDENT_CURRENT ('MOVIES') AS Current_Identity";
            MOVIE_ID = 1;
            try
            {
                if (!(CN.State == ConnectionState.Open))
                {
                    CN.Close();
                    CN.Open();
                }
                CMD = new SqlCommand(QUERY1, CN);
                DR = CMD.ExecuteReader();
                DR.Read();
                MOVIE_ID = int.Parse(DR[0].ToString());
                DR.Close();
                CN.Close();
                QUERY1 = "";
            }
            catch (Exception ex)
            {
                CN.Close();
                QUERY1 = "";
                L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;
            }
        }

        private string getFormattedName(string data)
        {
            return data.Replace("\'", "&apos;");
        }

        protected void BTN_ADD_MOVIE_Click(object sender, EventArgs e)
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
                { IS_FORM_VALID = false; }
            }
            if (IS_FORM_VALID)
            {
                Update_MOVIE_ID();
                MOVIE_NAME = getFormattedName(TB_MOVIE_NAME.Text.ToUpper());
                SET_DATE_VARS(CAL_RELEASE_DATE.SelectedDate);
                GENRE = TB_GENRE.Text.ToUpper();
                TICKET_PRICE = int.Parse(TB_TICKET_PRICE.Text);
                IMAGE_URL = "Images/" + getFormattedName(FU_COVER_IMAGE.PostedFile.FileName);
                RATING = int.Parse(DDL_RATING.SelectedValue);
                DESCRIPTION = TB_DESCRIPTION.Text;
                QUERY1 = "INSERT INTO MOVIES(MOVIE_NAME, RELEASE_DAY, RELEASE_MONTH, RELEASE_YEAR, GENRE, TICKET_PRICE, IMAGE_URL, RATING, DESCRIPTION) VALUES('" + MOVIE_NAME + "'," + RELEASE_DAY + ",'" + RELEASE_MONTH + "'," + RELEASE_YEAR + ",'" + GENRE + "'," + TICKET_PRICE + ",'" + IMAGE_URL + "'," + RATING + ",'" + DESCRIPTION + "')";
                try
                {
                    FU_COVER_IMAGE.PostedFile.SaveAs(ImagesDirectory + @"\" + FU_COVER_IMAGE.PostedFile.FileName);
                    if (!(CN.State == ConnectionState.Open))
                    {
                        CN.Close();
                        CN.Open();
                    }
                    CMD = new SqlCommand(QUERY1, CN);
                    CMD.ExecuteNonQuery();
                    DateTime TEMP_ORIGINAL_DATE = new DateTime(RELEASE_YEAR, RELEASE_MONTH, RELEASE_DAY);
                    var TIMIMGS_LIST = new[] { "07:30 AM", "11:30 AM", "01:30 PM", "04:00 PM", "07:00 PM", "10:00 PM" };
                    for (int I = 1; I <= 7; I++)
                    {
                        string TEMP_CUSTOM_DATE = GET_DATE_STRING(TEMP_ORIGINAL_DATE.Day, TEMP_ORIGINAL_DATE.Month, TEMP_ORIGINAL_DATE.Year);
                        foreach (string TIME in TIMIMGS_LIST)
                        {
                            QUERY2 = "INSERT INTO SEATS(MOVIE_ID, SHOW_DATE, SHOW_TIME) VALUES('" + MOVIE_ID + "','" + TEMP_CUSTOM_DATE + "','" + TIME + "')";
                            CMD = new SqlCommand(QUERY2, CN);
                            CMD.ExecuteNonQuery();
                        }
                        TEMP_ORIGINAL_DATE = TEMP_ORIGINAL_DATE.AddDays(1);
                    }
                    L_ERROR_DESCRIPTION.Text = "";
                    L_RESPONSE.ForeColor = System.Drawing.Color.Green;
                    L_RESPONSE.Text = "SUCCESS";
                    T1.Enabled = true;
                    RESET_FIELDS();
                    CN.Close();
                }
                catch (Exception ex)
                {
                    CN.Close();
                    L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                    L_RESPONSE.Text = "ERROR";
                    L_ERROR_DESCRIPTION.Text = ex.Message;
                }
                CN.Close();
                QUERY1 = "";
                QUERY2 = "";
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