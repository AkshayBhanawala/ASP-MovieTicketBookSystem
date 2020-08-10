using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CS_MovieTicketBookingApp
{
    partial class HOME : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlDataAdapter DA;
        private DataSet DS;
        private string QUERY = "";

        protected string get_MonthName(int MN)
        { return (new DateTime(2000, MN, 1)).ToString("MMMM"); }

        protected void FILL_DS()
        {
            QUERY = "SELECT * FROM MOVIES";
            try
            {
                if (!(CN.State == ConnectionState.Open))
                {
                    CN.Close();
                    CN.Open();
                }
                DA = new SqlDataAdapter(QUERY, CN);
                DS = new DataSet();
                DA.Fill(DS);
                CN.Close();
            }
            catch (Exception ex)
            {
                CN.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FILL_DS();
            try
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    DL_MOVIES.DataSource = DS.Tables[0];
                    DL_MOVIES.DataBind();
                }
                else
                    L_NO_MOVIES.Visible = true;
            }
            catch(Exception EX)
            { }
        }
    }
}