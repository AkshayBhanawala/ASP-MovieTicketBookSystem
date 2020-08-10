<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_No.master" AutoEventWireup="True" CodeBehind="~/Users/LOGIN.aspx.cs" Inherits="CS_MovieTicketBookingApp.Users.LOGIN" %>

<asp:Content ID="HEAD_LOGIN_CONTENT" ContentPlaceHolderID="SUB_HEAD_LOGIN_NO_CONTENTPLACEHOLDER" runat="Server">
    <title>USER LOGIN</title>
</asp:Content>
<asp:Content ID="BODY_LOGIN_CONTENT" ContentPlaceHolderID="SUB_BODY_LOGIN_NO_CONTENTPLACEHOLDER" runat="Server">
    <center>
        <br/><br/>
        <div class="div_form">
            <asp:Timer ID="T1" runat="server" Enabled="False" Interval="2000" OnTick="T1_Tick">
            </asp:Timer>
            <asp:ScriptManager ID="SM1" runat="server">
            </asp:ScriptManager>
            <table>
                <tr>
                    <td colspan="2" align="center">
                        <hr/>
                        <asp:Label ID="L_MESSAGE1" runat="server" CssClass="input_error" /> 
                        <br/>
                        <asp:Label ID="L_MESSAGE2" runat="server" CssClass="input_error" />
                    </td>
                </tr>
                <tr><td colspan="2"><hr/></td></tr>
                <tr>
                    <td class="text"> USERNAME:- </td>
                    <td> <asp:TextBox ID="TB_USERNAME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" required TabIndex="1" /></td>
                </tr>
                <tr>
                    <td class="text"> PASSWORD:- </td>
                    <td> <asp:TextBox ID="TB_PASSWORD" runat="server" CssClass="input_field_initial" TextMode="Password" required TabIndex="2" /> </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                    <asp:CheckBox ID="CB_REMEMBER" runat="server" CssClass="checkbox_css" text=" REMEMBER ME" TabIndex="3" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <hr/>
                        <asp:Button ID="BTN_LOGIN" runat="server" CssClass="button_green" text="LOGIN" style="width:50%;" TabIndex="4" OnClick="BTN_LOGIN_Click" />
                        <span class="checkmark" />
                        <hr/>
                        <br/>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>

