<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_Admin.master" AutoEventWireup="True" CodeBehind="~/Users/Admin/Management/MOVIE_DELETE.aspx.cs" Inherits="CS_MovieTicketBookingApp.Users.Admin.Management.MOVIE_DELETE" %>

<asp:Content ID="HEAD_MOVIE_DELETE_CONTENT" ContentPlaceHolderID="SUB_HEAD_LOGIN_ADMIN_CONTENTPLACEHOLDER" Runat="Server">
    <title>DELETE A MOVIE</title>
</asp:Content>
<asp:Content ID="BODY_MOVIE_DELETE_CONTENT" ContentPlaceHolderID="SUB_BODY_LOGIN_ADMIN_CONTENTPLACEHOLDER" Runat="Server">
    <center>
        <br><br>
    <div class="div_form">
        <table>
            <tr><td colspan="3"><br /></td></tr>
            <tr><td colspan="3"><hr /></td></tr>
            <tr><td colspan="3"><hr /></td></tr>
            <tr>
                <td class="text"> MOVIE NAME:- </td>
                <td>
                    <asp:DropDownList ID="DDL_MOVIE_ID" runat="server" AutoPostBack="True" CssClass="input_field_initial"
                        STYLE="text-align:center; width:101%" OnSelectedIndexChanged="DDL_MOVIE_ID_SelectedIndexChanged" OnDataBound="DDL_MOVIE_ID_DataBound">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br /><br />
                    <table align="center" id="TAB_MOVIE_INFO" runat="server" visible="false" >
                        <tr>
                            <td rowspan="6"> <asp:Image ID="IMG_MOVIE_COVER" runat="server" Width="200px" Height="300px"/> &nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td> ID: &nbsp; <asp:Label runat="server" id="L_MOVIE_ID" CssClass="input_value" /> </td>
                        </tr>
                        <tr> <td> NAME: &nbsp; <asp:Label runat="server" id="L_MOVIE_NAME" CssClass="input_value" /> </td> </tr>
                        <tr> <td> RELEASE DATE: &nbsp; <asp:Label runat="server" id="L_MOVIE_RELEASE_DATE" CssClass="input_value" /> </td> </tr>
                        <tr> <td> TICKET PRICE: &nbsp; <asp:Label runat="server" id="L_MOVIE_TICKET_PRICE" CssClass="input_value" /> </td> </tr>
                        <tr> <td> GENRE: &nbsp; <asp:Label runat="server" id="L_MOVIE_GENRE" CssClass="input_value" /> </td> </tr>
                        <tr> <td> RATING: &nbsp; <asp:Label runat="server" id="L_MOVIE_RATING" CssClass="input_value" /> </td> </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2"><br /></td></tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td>&nbsp;</td>
                <td align="center">
                    <asp:Button ID="BTN_DELETE_MOVIE" runat="server" CssClass="button_orange" text="DELETE MOVIE" OnClick="BTN_DELETE_MOVIE_Click" />
                    <asp:Button ID="BTN_RESET" runat="server" CssClass="button_red" text="RESET" OnClick="BTN_RESET_Click"
                        ClientIDMode="Static" EnableViewState="False" UseSubmitBehavior="False" ViewStateMode="Disabled"/>
                </td>
            </tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="L_RESPONSE" runat="server" Font-Names="Arial" Font-Size="18px" /><br>
                    <asp:Label ID="L_ERROR_DESCRIPTION" runat="server" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        width="50%" />
                </td>
            </tr>
            <tr>
                <td colspan="2"><hr />
                    <asp:Timer ID="T1" runat="server" Enabled="False" Interval="3000" OnTick="T1_Tick">
                    </asp:Timer>
                    <asp:ScriptManager ID="SM1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
        </table>
    </div>
        </center>
</asp:Content>

