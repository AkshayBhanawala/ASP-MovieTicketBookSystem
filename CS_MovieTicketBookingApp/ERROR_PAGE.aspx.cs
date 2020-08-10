using System;

namespace CS_MovieTicketBookingApp
{
    partial class ERROR_PAGE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["ERROR"] == null))
            {
                L_ERROR_HEADING.Text = "OOPS!! AN ERROR HAS OCCURRED... &nbsp;&nbsp; <sapn style='font-size:50px;'>😰</span>";
                L_ERROR_DETAIL_HEADING.Text = "ERROR DETAILS:- ";
                switch (Session["ERROR"].ToString())
                {
                    case "NO_LOGIN":
                        {
                            L_ERROR_DESCRIPTION1.Text = "<span style='font-size:20px; color:#CC0000;'>You are not Logged In !!!</span> &nbsp;&nbsp;&nbsp; <span style='font-size:50px'>😕</span>";
                            L_ERROR_DESCRIPTION2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h4>GO <span style='font-size:30px;'> 👉 </span><a href='Users/LOGIN.aspx'class='head_link'>HERE</a><span style='font-size:30px'> 👈 </span> to Login !!</h4>";
                            break;
                        }

                    case "NOT_AUTHORISED":
                        {
                            L_ERROR_DESCRIPTION1.Text = "<span style='font-size:50px'>😒</span> &nbsp;&nbsp;&nbsp; <span style='font-size:20px; color:#CC0000;'>You are not Authorised to Access This Page !!!</span> &nbsp;&nbsp;&nbsp; <span style='font-size:50px'>👿</span>";
                            L_ERROR_DESCRIPTION2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h4>GO <span style='font-size:30px;'> 👉 </span><a href='Users/LOGIN.aspx' class='head_link'>HERE</a><span style='font-size:30px'> 👈 </span> to Move to your HOME !!</h4>";
                            break;
                        }

                    case "MID_NOT_VALID":
                        {
                            L_ERROR_DESCRIPTION1.Text = "<span style='font-size:50px'>😒</span> &nbsp;&nbsp;&nbsp; <span style='font-size:20px; color:#CC0000;'>RESOURCE HAS BEEN COMPROMISED !!!</span> &nbsp;&nbsp;&nbsp; <span style='font-size:50px'>👿</span>";
                            L_ERROR_DESCRIPTION2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h4>TRY AGAIN & DON'T MODIFY RESOURCE THIS TIME !!!</h4>";
                            break;
                        }

                    default:
                        {
                            L_ERROR_DESCRIPTION1.Text = Session["ERROR"].ToString();
                            break;
                        }
                }
                Session.Remove("ERROR");
            }
        }
    }
}