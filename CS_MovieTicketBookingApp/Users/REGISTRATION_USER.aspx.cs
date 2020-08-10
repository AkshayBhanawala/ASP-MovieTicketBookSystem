using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CS_MovieTicketBookingApp.Users
{
    partial class REGISTRATION_USER : System.Web.UI.Page
    {
        private SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MOVIE_TICKET_BOOKING"].ConnectionString);
        private SqlCommand CMD;
        private SqlDataReader DR;
        private string QUERY = "";

        private string USERNAME, EMAIL, NEW_PASSWORD, CONFIRM_PASSWORD, FIRST_NAME, LAST_NAME, AGE, MOBILE_NO;


        protected bool VALIDATE_CONTROL(string CONTROL_ID)
        {
            bool CONTROL_VALID_FLAG_1, CONTROL_VALID_FLAG_2, CONTROL_VALID_FLAG_3;
            switch (CONTROL_ID)
            {
                case "TB_USERNAME":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_USERNAME.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_USERNAME.IsValid;
                        CONTROL_VALID_FLAG_3 = CUSTV_USERNAME.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2 & CONTROL_VALID_FLAG_3)
                        {
                            TB_USERNAME.CssClass = "input_field_valid";
                            TB_NEW_PASSWORD.Focus();
                            return true;
                        }
                        else
                        {
                            TB_USERNAME.CssClass = "input_field_error";
                            TB_USERNAME.Focus();
                            return false;
                        }
                    }

                case "TB_EMAIL":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_EMAIL.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_EMAIL.IsValid;
                        CONTROL_VALID_FLAG_3 = CUSTV_EMAIL.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2 & CONTROL_VALID_FLAG_3)
                        {
                            TB_EMAIL.CssClass = "input_field_valid";
                            TB_MOBILE_NO.Focus();
                            return true;
                        }
                        else
                        {
                            TB_EMAIL.CssClass = "input_field_error";
                            TB_EMAIL.Focus();
                            return false;
                        }
                    }

                case "TB_NEW_PASSWORD":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_NEW_PASSWORD.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_NEW_PASSWORD.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_NEW_PASSWORD.CssClass = "input_field_valid";
                            TB_CONFIRM_PASSWORD.Focus();
                            return true;
                        }
                        else
                        {
                            TB_NEW_PASSWORD.CssClass = "input_field_error";
                            TB_NEW_PASSWORD.Focus();
                            return false;
                        }
                    }

                case "TB_CONFIRM_PASSWORD":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_CONFIRM_PASSWORD.IsValid;
                        CONTROL_VALID_FLAG_2 = CMPV_CONFIRM_PASSWORD.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_CONFIRM_PASSWORD.CssClass = "input_field_valid";
                            TB_EMAIL.Focus();
                            return true;
                        }
                        else
                        {
                            TB_CONFIRM_PASSWORD.CssClass = "input_field_error";
                            TB_CONFIRM_PASSWORD.Focus();
                            return false;
                        }
                    }

                case "TB_FIRST_NAME":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_FIRST_NAME.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_FIRST_NAME.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_FIRST_NAME.CssClass = "input_field_valid";
                            TB_LAST_NAME.Focus();
                            return true;
                        }
                        else
                        {
                            TB_FIRST_NAME.CssClass = "input_field_error";
                            TB_FIRST_NAME.Focus();
                            return false;
                        }
                    }

                case "TB_LAST_NAME":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_LAST_NAME.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_LAST_NAME.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2)
                        {
                            TB_LAST_NAME.CssClass = "input_field_valid";
                            DDL_AGE.Focus();
                            return true;
                        }
                        else
                        {
                            TB_LAST_NAME.CssClass = "input_field_error";
                            TB_LAST_NAME.Focus();
                            return false;
                        }
                    }

                case "TB_MOBILE_NO":
                    {
                        CONTROL_VALID_FLAG_1 = RFV_MOBILE_NO.IsValid;
                        CONTROL_VALID_FLAG_2 = REV_MOBILE_NO.IsValid;
                        CONTROL_VALID_FLAG_3 = CUSTV_MOBILE_NO.IsValid;
                        if (CONTROL_VALID_FLAG_1 & CONTROL_VALID_FLAG_2 & CONTROL_VALID_FLAG_3)
                        {
                            TB_MOBILE_NO.CssClass = "input_field_valid";
                            BTN_REGISTER.Focus();
                            return true;
                        }
                        else
                        {
                            TB_MOBILE_NO.CssClass = "input_field_error";
                            TB_MOBILE_NO.Focus();
                            return false;
                        }
                    }
            }

            return default(bool);
        }

        protected void TB_TextChanged(object sender, EventArgs e)
        {
            VALIDATE_CONTROL(((WebControl)sender).ID.ToString());
        }

        protected void T1_Tick(object sender, EventArgs e)
        {
            L_RESPONSE.Text = "";
            L_ERROR_DESCRIPTION.Text = "";
            T1.Enabled = false;
        }

        protected bool IS_PRIMARYKEY_DATA_VALID(string FIELD_NAME, string VALUE)
        {
            QUERY = "SELECT EMAIL FROM USERS WHERE " + FIELD_NAME + "='" + VALUE + "'";
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
                CN.Close();
                if (DR.HasRows)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                CN.Close();
                L_RESPONSE.ForeColor = System.Drawing.Color.Red;
                L_RESPONSE.Text = "ERROR";
                L_ERROR_DESCRIPTION.Text = ex.Message;
                return false;
            }
            CN.Close();
        }

        protected void CV_USERNAME_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IS_PRIMARYKEY_DATA_VALID("USERNAME", args.Value.ToString()))
            {
                args.IsValid = false;
                CUSTV_USERNAME.IsValid = false;
                VALIDATE_CONTROL("TB_USERNAME");
            }
        }
        protected void CV_EMAIL_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IS_PRIMARYKEY_DATA_VALID("EMAIL", args.Value.ToString()))
            {
                args.IsValid = false;
                CUSTV_EMAIL.IsValid = false;
                VALIDATE_CONTROL("TB_EMAIL");
            }
        }

        protected void CV_MOBILE_NO_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IS_PRIMARYKEY_DATA_VALID("MOBILE_NO", args.Value.ToString()))
            {
                args.IsValid = false;
                CUSTV_MOBILE_NO.IsValid = false;
                VALIDATE_CONTROL("TB_MOBILE_NO");
            }
        }

        protected void RESET_FIELDS()
        {
            var FORM_CONTROLS = new[] { TB_USERNAME, TB_NEW_PASSWORD, TB_CONFIRM_PASSWORD, TB_EMAIL, TB_MOBILE_NO, TB_FIRST_NAME, TB_LAST_NAME };
            foreach (var OBJ in FORM_CONTROLS)
            {
                OBJ.Text = "";
                OBJ.CssClass = "input_field_initial";
            }
        }

        protected void BTN_REGISTER_Click(object sender, EventArgs e)
        {
            var FORM_CONTROLS = new[] { TB_USERNAME, TB_NEW_PASSWORD, TB_CONFIRM_PASSWORD, TB_EMAIL, TB_MOBILE_NO, TB_FIRST_NAME, TB_LAST_NAME };
            bool IS_FORM_VALID = true;
            foreach (var OBJ in FORM_CONTROLS)
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
                USERNAME = TB_USERNAME.Text.ToUpper();
                EMAIL = TB_EMAIL.Text.ToUpper();
                NEW_PASSWORD = TB_NEW_PASSWORD.Text;
                CONFIRM_PASSWORD = TB_CONFIRM_PASSWORD.Text;
                FIRST_NAME = TB_FIRST_NAME.Text.ToUpper();
                LAST_NAME = TB_LAST_NAME.Text.ToUpper();
                AGE = DDL_AGE.SelectedItem.Text;
                MOBILE_NO = TB_MOBILE_NO.Text;
                QUERY = "INSERT INTO USERS (USERNAME, EMAIL, PASSWORD, FULL_NAME, AGE, MOBILE_NO) VALUES('" + USERNAME + "','" + EMAIL + "','" + NEW_PASSWORD + "','" + FIRST_NAME + " " + LAST_NAME + "','" + AGE + "','" + MOBILE_NO + "')";
                try
                {
                    if (!(CN.State == ConnectionState.Open))
                    {
                        CN.Close();
                        CN.Open();
                    }
                    CMD = new SqlCommand(QUERY, CN);
                    CMD.ExecuteNonQuery();
                    L_RESPONSE.ForeColor = System.Drawing.Color.Green;
                    L_RESPONSE.Text = "SUCCESS";
                    L_ERROR_DESCRIPTION.Text = "";
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
                QUERY = "";
            }
        }

        protected void BTN_RESET_Click(object sender, EventArgs e)
        {
            RESET_FIELDS();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 5; i <= 100; i++)
                    DDL_AGE.Items.Add(i.ToString());
                DDL_AGE.SelectedIndex = 0;
            }
        }
    }
}