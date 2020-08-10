<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="~/ERROR_PAGE.aspx.cs" Inherits="CS_MovieTicketBookingApp.ERROR_PAGE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>ERROR OCCURRED 😱</title>
        <link type="text/css" rel="stylesheet" runat="server" href="~/Style/CSS1.css" />
        <style>
            .head_link:link
            {
                font-size:30px;
                font-weight:bold;
                color:#ff6a00;
                text-decoration:none;
            }
            .head_link:hover
            {
                color:red;
                text-decoration:underline;
            }
            .head_link:active
            {
                color:silver;
            }
        </style>
    </head>
    <body style=" margin-top:10%; margin-left:20%; background-color:#202020;">
        <asp:Label ID="L_ERROR_HEADING" runat="server" Font-Names="Tahoma" Font-Size="40px"
            Font-Bold="False" ForeColor="Red" /><br /><br />

        <asp:Label ID="L_ERROR_DETAIL_HEADING" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="12px" ForeColor="#0066FF" /><br />

        <asp:Label ID="L_ERROR_DESCRIPTION1" runat="server" Font-Names="Arial" width="100%" ForeColor="#C0C0C0" />

        <asp:Label ID="L_ERROR_DESCRIPTION2" runat="server" Font-Names="Arial" ForeColor="#CC0000"
            width="50%" />
    </body>
</html>

