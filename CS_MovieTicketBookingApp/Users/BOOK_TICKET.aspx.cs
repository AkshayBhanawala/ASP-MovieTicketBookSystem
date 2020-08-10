using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CS_MovieTicketBookingApp.Users
{
    public partial class BOOK_TICKET : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlCommand CMD;
        private SqlDataReader DR;
        private SqlDataAdapter DA;
        private DataSet DS;
        private string QUERY = "";

        private string MOVIE_NAME, GENRE, IMAGE_URL, QUERY_STRING, USERNAME;
        private int MOVIE_ID, RELEASE_DAY, RELEASE_MONTH, RELEASE_YEAR, TICKET_PRICE, RATING, TOTAL_AVALILABLE_SEATS, SELECTED_NO_SEATS;

        private string[] SEATS = new string[] { };
        private string[] SEAT_NUMS = new string[226];

        private string ImagesDirectory = AppDomain.CurrentDomain.GetData("ImagesDirectory").ToString();

        protected bool IS_LOGGED_IN()
        {
            if (Session["IsLoggedIn"] != null && (bool)Session["IsLoggedIn"])
            {
                USERNAME = Session["UN"].ToString();
                return true;
            }
            else
                return false;
        }

        protected bool IS_MID_VALID()
        {
            string QUERY = "SELECT * FROM MOVIES WHERE MOVIE_ID=" + MOVIE_ID;
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
                if (DR.HasRows)
                {
                    MOVIE_ID = int.Parse(DR[0].ToString());
                    MOVIE_NAME = DR[1].ToString();
                    RELEASE_DAY = int.Parse(DR[2].ToString());
                    RELEASE_MONTH = int.Parse(DR[3].ToString());
                    RELEASE_YEAR = int.Parse(DR[4].ToString());
                    GENRE = DR[5].ToString();
                    TICKET_PRICE = int.Parse(DR[6].ToString());
                    IMAGE_URL = DR[7].ToString();
                    RATING = int.Parse(DR[8].ToString());
                    CN.Close();
                    return true;
                }
                else
                {
                    CN.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                CN.Close();
                Session["ERROR"] = ex.Message.ToString();
                Response.Redirect("~/ERROR_PAGE.aspx");
            }

            return default(bool);
        }

        protected string get_MonthName(int MN)
        { return (new DateTime(2000, MN, 1)).ToString("MMMM"); }
        protected int get_MonthNumber(string MN)
        { return (Convert.ToDateTime(MN + "01, 2000")).Month; }

        protected string GET_DATE_STRING(int DAY_OF_DATE, int MONTH_OF_DATE, int YEAR_OF_DATE)
        {
            return DAY_OF_DATE + " " + get_MonthName(MONTH_OF_DATE) + " " + YEAR_OF_DATE;
        }

        protected void FILL_NO_OF_TICKETS()
        {
            DDL_NO_OF_TICKETS.Items.Clear();
            int TOTAL_BOOKED_SEATS;
            QUERY = "SELECT SUM(TOTAL_SEATS) FROM BOOKINGS WHERE MOVIE_ID=" + MOVIE_ID;
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
                if (DR.HasRows & DR[0] != null)
                {
                    TOTAL_BOOKED_SEATS = (DR[0] != DBNull.Value) ? int.Parse(DR[0].ToString()) : 0;
                }
                else
                { TOTAL_BOOKED_SEATS = 0; }
                CN.Close();
                int i = 1;
                int BOOKABLE_TOTAL_SEATS;
                int TOTAL_AVALILABLE_SEATS = 225 - TOTAL_BOOKED_SEATS;
                if (TOTAL_AVALILABLE_SEATS < 10)
                    BOOKABLE_TOTAL_SEATS = TOTAL_AVALILABLE_SEATS;
                else
                    BOOKABLE_TOTAL_SEATS = 10;
                while (i <= BOOKABLE_TOTAL_SEATS)
                {
                    DDL_NO_OF_TICKETS.Items.Add(i.ToString());
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                CN.Close();
                L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;
            }
            CN.Close();
        }

        protected void FILL_SHOW_DATE()
        {
            DDL_SHOW_DATE.Items.Clear();
            DateTime TEMP_DATE = new DateTime(RELEASE_YEAR, RELEASE_MONTH, RELEASE_DAY);
            for (int I = 1; I <= 7; I++)
            {
                string TEMP_DATE_STRING = GET_DATE_STRING(TEMP_DATE.Day, TEMP_DATE.Month, TEMP_DATE.Year);
                DDL_SHOW_DATE.Items.Add(TEMP_DATE_STRING);
                TEMP_DATE = TEMP_DATE.AddDays(1);
            }
        }

        protected void FILL_SHOW_TIME()
        {
            DDL_SHOW_TIME.Items.Clear();
            var TIMIMGS_LIST = new[] { "07:30 AM", "11:30 AM", "01:30 PM", "04:00 PM", "07:00 PM", "10:00 PM" };
            foreach (string TIME in TIMIMGS_LIST)
                DDL_SHOW_TIME.Items.Add(TIME);
        }

        protected void SET_SEAT_NUMS()
        {
            var ALPHABETS = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O' };
            int POSITION = 0;
            foreach (char ALPHABET in ALPHABETS)
            {
                for (int i = 1; i <= 15; i++)
                {
                    SEAT_NUMS.SetValue(((char)ALPHABET) + "" + i, POSITION);
                    POSITION += 1;
                }
            }
        }

        protected void FILL_SEATS()
        {
            try
            {
                SET_SEAT_NUMS();
                bool F1 = true;
                bool F2 = false;
                string SELECTED_SHOW_DATE = DDL_SHOW_DATE.SelectedValue;
                string SELECTED_SHOW_TIME = DDL_SHOW_TIME.SelectedValue;
                QUERY = "SELECT * FROM SEATS WHERE MOVIE_ID=" + MOVIE_ID + " AND SHOW_DATE='" + SELECTED_SHOW_DATE + "' AND SHOW_TIME='" + SELECTED_SHOW_TIME + "'";
                DA = new SqlDataAdapter(QUERY, CN);
                DS = new DataSet();
                DA.Fill(DS);
                CN.Close();
                QUERY = "";
                var ROW = DS.Tables[0].Rows[0];
                int I = 3;
                foreach (string SEAT_NO in SEAT_NUMS)
                {
                    var cb = new CheckBox();
                    cb.ID = SEAT_NO;
                    cb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    cb.Text = " ";
                    if (I <= 227)
                    {
                        if ((Boolean)ROW[I])
                            cb.Enabled = false;
                        if (F1)
                        {
                            P_SEATS_LEFT.Controls.Add(cb);
                            if (I % 5 == 0)
                            {
                                F1 = false;
                                F2 = true;
                            }
                        }
                        else if (F2)
                        {
                            P_SEATS_CENTER.Controls.Add(cb);
                            if (I % 5 == 0)
                                F2 = false;
                        }
                        else
                        {
                            P_SEATS_RIGHT.Controls.Add(cb);
                            if (I % 5 == 0)
                                F1 = true;
                        }
                    }
                    I += 1;
                }
            }
            catch (Exception ex)
            {
                CN.Close();
                Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IS_LOGGED_IN())
            {
                MOVIE_ID = int.Parse(Request.QueryString["MID"]);
                if (!IS_MID_VALID())
                {
                    Session["ERROR"] = "MID_NOT_VALID";
                    Response.Redirect("~/ERROR_PAGE.aspx");
                }
                else
                {
                    TB_MOVIE_NAME.Text = MOVIE_NAME;
                    TB_RELEASE_DATE.Text = GET_DATE_STRING(RELEASE_YEAR, RELEASE_MONTH, RELEASE_DAY);
                    TB_GENRE.Text = GENRE;
                    TB_MIN_PRICE.Text = TICKET_PRICE.ToString();
                }
                if (!IsPostBack)
                {
                    FILL_SHOW_DATE();
                    FILL_SHOW_TIME();
                    FILL_NO_OF_TICKETS();
                    DDL_SHOW_DATE.SelectedIndex = 0;
                    DDL_SHOW_TIME.SelectedIndex = 0;
                    DDL_NO_OF_TICKETS.SelectedIndex = 0;
                }
                FILL_SEATS();
            }
            else
            {
                Session["ERROR"] = "NO_LOGIN";
                Response.Redirect("~/ERROR_PAGE.aspx");
            }
        }

        protected void SET_SELECTED_SEATS_ARRAY()
        {
            int I = 0;
            foreach (CheckBox TEMP_CHECKBOX in P_SEATS_LEFT.Controls)
            {
                if (TEMP_CHECKBOX.Checked)
                {
                    var oldSEATS = SEATS;
                    SEATS = new string[I + 1];
                    if (oldSEATS != null)
                        Array.Copy(oldSEATS, SEATS, Math.Min(I + 1, oldSEATS.Length));
                    SEATS.SetValue(TEMP_CHECKBOX.ID.ToString(), I);
                    I += 1;
                }
            }
            foreach (CheckBox TEMP_CHECKBOX in P_SEATS_CENTER.Controls)
            {
                if (TEMP_CHECKBOX.Checked)
                {
                    var oldSEATS1 = SEATS;
                    SEATS = new string[I + 1];
                    if (oldSEATS1 != null)
                        Array.Copy(oldSEATS1, SEATS, Math.Min(I + 1, oldSEATS1.Length));
                    SEATS.SetValue(TEMP_CHECKBOX.ID.ToString(), I);
                    I += 1;
                }
            }
            foreach (CheckBox TEMP_CHECKBOX in P_SEATS_RIGHT.Controls)
            {
                if (TEMP_CHECKBOX.Checked)
                {
                    var oldSEATS2 = SEATS;
                    SEATS = new string[I + 1];
                    if (oldSEATS2 != null)
                        Array.Copy(oldSEATS2, SEATS, Math.Min(I + 1, oldSEATS2.Length));
                    SEATS.SetValue(TEMP_CHECKBOX.ID.ToString(), I);
                    I += 1;
                }
            }
        }

        protected string SEATS_UPDATE_QUERY_BUILDER()
        {
            string TEMP_QUERY = "UPDATE SEATS SET ";
            foreach (var SEAT_NO in SEATS)
            { TEMP_QUERY += SEAT_NO + "=1, "; }
            TEMP_QUERY = TEMP_QUERY.Substring(0, TEMP_QUERY.Length - 2);
            TEMP_QUERY += " WHERE MOVIE_ID=" + MOVIE_ID + " AND SHOW_DATE='" + DDL_SHOW_DATE.SelectedValue + "' AND SHOW_TIME='" + DDL_SHOW_TIME.SelectedValue + "'";
            return TEMP_QUERY;
        }

        protected void RUN_SEATS_UPDATE_QUERY()
        {
            if (!(CN.State == ConnectionState.Open))
            {
                CN.Close();
                CN.Open();
            }
            QUERY = SEATS_UPDATE_QUERY_BUILDER();
            CMD = new SqlCommand(QUERY, CN);
            CMD.ExecuteNonQuery();
            QUERY = "";
            CN.Close();
        }

        protected int CALCULATE_TOTAL_PRICE()
        {
            int TOTAL_PRICE = 0;
            char SEAT_CLASS;
            foreach (var SEAT in SEATS)
            {
                SEAT_CLASS = char.Parse(SEAT.Substring(0, 1));
                switch (SEAT_CLASS)
                {
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                        {
                            TOTAL_PRICE += TICKET_PRICE;
                            break;
                        }

                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                        {
                            TOTAL_PRICE += TICKET_PRICE + 30;
                            break;
                        }

                    case 'J':
                    case 'K':
                    case 'L':
                        {
                            TOTAL_PRICE += TICKET_PRICE + 50;
                            break;
                        }

                    case 'M':
                    case 'N':
                    case 'O':
                        {
                            TOTAL_PRICE += TICKET_PRICE + 80;
                            break;
                        }
                }
            }
            return TOTAL_PRICE;
        }

        protected void RUN_BOOKING_DATA_INSERT_QUERY()
        {
            string SELECTED_SEATS = "";
            foreach (var SEAT in SEATS)
            { SELECTED_SEATS += SEAT + ", "; }
            SELECTED_SEATS = SELECTED_SEATS.Substring(0, SELECTED_SEATS.Length - 2);
            int TOTAL_PRICE = CALCULATE_TOTAL_PRICE();
            QUERY = "INSERT INTO BOOKINGS(USERNAME, MOVIE_ID, MOVIE_NAME, TOTAL_SEATS, SEAT_NOS, SHOW_DATE, SHOW_TIME, TOTAL_PRICE) VALUES('" + USERNAME + "'," + MOVIE_ID + ",'" + MOVIE_NAME + "'," + DDL_NO_OF_TICKETS.SelectedValue + ",'" + SELECTED_SEATS + "','" + DDL_SHOW_DATE.SelectedValue + "','" + DDL_SHOW_TIME.SelectedValue + "'," + TOTAL_PRICE + ")";
            if (!(CN.State == ConnectionState.Open))
            {
                CN.Close();
                CN.Open();
            }
            CMD = new SqlCommand(QUERY, CN);
            CMD.ExecuteNonQuery();
            QUERY = "";
            CN.Close();
        }

        protected void BTN_BOOK_Click(object sender, EventArgs e)
        {
            L_SELECTED_SEATS_ERROR.Visible = false;
            SELECTED_NO_SEATS = int.Parse(DDL_NO_OF_TICKETS.SelectedValue);
            try
            {
                SET_SELECTED_SEATS_ARRAY();
                if (SEATS.Length == SELECTED_NO_SEATS)
                {
                    RUN_SEATS_UPDATE_QUERY();
                    CN.Close();
                    RUN_BOOKING_DATA_INSERT_QUERY();
                    CN.Close();
                    L_ERROR_DESCRIPTION.Text = "";
                    L_RESPONSE.ForeColor = System.Drawing.Color.Green;
                    L_RESPONSE.Text = "SUCCESS";
                    T1.Enabled = true;
                }
                else
                {
                    L_SELECTED_SEATS_ERROR.Text = "✘ - No. of Tickets doesn't Match The Seats Selection !!! ( TICKETS: " + SELECTED_NO_SEATS + " )";
                    L_SELECTED_SEATS_ERROR.Visible = true;
                    DDL_NO_OF_TICKETS.Focus();
                }
            }
            catch (Exception ex)
            {
                CN.Close();
                L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;
                Session["ERROR"] = ex.Message.ToString();
                Response.Redirect(@"~\ERROR_PAGE.aspx");
            }
        }

        protected void T1_Tick(object sender, EventArgs e)
        {
            L_RESPONSE.Text = "";
            L_ERROR_DESCRIPTION.Text = "";
            T1.Enabled = false;
            Response.Redirect("VIEW_BOOKINGS.aspx");
        }

        protected void BTN_CALCULATE_TOTAL_PRICE_Click(object sender, EventArgs e)
        {
            L_SELECTED_SEATS_ERROR.Visible = false;
            SELECTED_NO_SEATS = int.Parse(DDL_NO_OF_TICKETS.SelectedValue);
            SET_SELECTED_SEATS_ARRAY();
            if (SEATS.Length == SELECTED_NO_SEATS)
            {
                TB_TOTAL_PRICE.Text = CALCULATE_TOTAL_PRICE().ToString();
                BTN_BOOK.Focus();
            }
            else
            {
                TB_TOTAL_PRICE.Text = "";
                L_SELECTED_SEATS_ERROR.Text = "✘ - No. of Tickets doesn't Match The Seats Selection !!! ( TICKETS: " + SELECTED_NO_SEATS + " )";
                L_SELECTED_SEATS_ERROR.Visible = true;
                DDL_NO_OF_TICKETS.Focus();
            }
        }

        protected void BTN_RESET_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}