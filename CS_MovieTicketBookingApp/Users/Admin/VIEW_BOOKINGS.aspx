<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_Admin.master" AutoEventWireup="True" CodeBehind="~/Users/VIEW_BOOKINGS.aspx.cs" Inherits="CS_MovieTicketBookingApp.Users.VIEW_BOOKINGS" %>

<asp:Content ID="HEAD_VIEW_APP" ContentPlaceHolderID="SUB_HEAD_LOGIN_ADMIN_CONTENTPLACEHOLDER" Runat="Server">
    <title>VIEW YOUR BOOKINGS</title>
</asp:Content>
<asp:Content ID="BODY_VIEW_BOOKING" ContentPlaceHolderID="SUB_BODY_LOGIN_ADMIN_CONTENTPLACEHOLDER" Runat="Server">
    <br /><br /><br /><br /><br />
    <asp:Label ID="L_NO_DATA" runat="server" Font-Names="Arial" Font-Size="X-Large" ForeColor="Red" width="50%"/>
    <div>
        <asp:GridView ID="GV_VIEW_BOOKINGS" runat="server" cssclass="table_booking_info" />
    </div>
</asp:Content>

