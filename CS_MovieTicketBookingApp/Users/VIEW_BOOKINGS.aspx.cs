using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CS_MovieTicketBookingApp.Users
{
    partial class VIEW_BOOKINGS : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlDataAdapter DA;
        private DataSet DS;
        private string QUERY = "";

        protected void FILL_GRID()
        {
            string USERNAME = Session["UN"].ToString();
            QUERY = "SELECT BOOKING_ID AS 'BOOKING ID', MOVIE_NAME AS 'MOVIE NAME', TOTAL_SEATS AS 'TOTAL SEATS', SEAT_NOS AS 'SEAT NOS', SHOW_DATE AS 'SHOW DATE', SHOW_TIME AS 'SHOW TIME', TOTAL_PRICE AS 'TOTAL PRICE' FROM BOOKINGS WHERE USERNAME='" + USERNAME + "'";
            DA = new SqlDataAdapter(QUERY, CN);
            DS = new DataSet();
            DA.Fill(DS);
            if (!(DS.Tables[0].Rows.Count == 0))
            {
                L_NO_DATA.Text = "";
                GV_VIEW_BOOKINGS.DataSource = DS.Tables[0];
                GV_VIEW_BOOKINGS.DataBind();
            }
            else
                L_NO_DATA.Text = "THERE ARE NO TICKETS BOOKED FROM YOUR ACCOUNT !!!";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FILL_GRID();
        }
    }
}