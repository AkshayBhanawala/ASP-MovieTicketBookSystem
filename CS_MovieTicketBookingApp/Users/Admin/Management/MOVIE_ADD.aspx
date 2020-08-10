<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_Admin.master" AutoEventWireup="True" CodeBehind="~/Users/Admin/Management/MOVIE_ADD.aspx.cs" Inherits="CS_MovieTicketBookingApp.Users.Admin.Management.MOVIE_ADD" %>

<asp:Content ID="HEAD_MOVIE_ADD_CONTENT" ContentPlaceHolderID="SUB_HEAD_LOGIN_ADMIN_CONTENTPLACEHOLDER" Runat="Server">
    <title>ADD A NEW MOVIE</title>
    <style>
        .hide {
            display: none;
        }
        .show {
            display: inherit;
        }
    </style>
</asp:Content>
<asp:Content ID="BODY_MOVIE_ADD_CONTENT" ContentPlaceHolderID="SUB_BODY_LOGIN_ADMIN_CONTENTPLACEHOLDER" Runat="Server">
    <center>
        <br><br>
    <div class="div_form">
        <table>
            <tr><td colspan="3"><br /></td></tr>
            <tr><td colspan="3"><hr /></td></tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="VS1" runat="server" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        ValidationGroup="VG1" EnableClientScript="False" HeaderText="Following Error(s) in Form:-" />
                </td>
            </tr>
            <tr><td colspan="3"><hr /></td></tr>
            <tr>
                <td class="text"> MOVIE NAME:- </td>
                <td>
                    <asp:TextBox ID="TB_MOVIE_NAME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" ValidationGroup="VG1" />
                </td>
                <td style="padding-left:20PX;">
                    <asp:RequiredFieldValidator ID="RFV_MOVIE_NAME" runat="server" Text="✘" ErrorMessage="MOVIE NAME - EMPTY !!!"
                        ForeColor="Red" ControlToValidate="TB_MOVIE_NAME" Display="Dynamic" Font-Names="Arial"
                        Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    <asp:RegularExpressionValidator ID="REV_MOVIE_NAME" runat="server" ControlToValidate="TB_MOVIE_NAME"
                        Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        ErrorMessage="MOVIE NAME - Not a Valid Name !!!" ValidationExpression="^[A-Za-z]{1}[A-Za-z0-9_:\-' ]+$"
                        ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td class="text" style="vertical-align:top"> RELEASE DATE:- </td>
                <td align="center">
                    <asp:TextBox ID="TB_RELEASE_DATE" runat="server" CssClass="input_field_initial" ReadOnly="true" />
                    <br />
                    <asp:Button ID="BTN_SHOW_HIDE_RD_CAL" runat="server" Text="SHOW CALENDAR"  CssClass="button_orange"
                        OnClick="BTN_SHOW_HIDE_RD_CAL_Click" CausesValidation="False" UseSubmitBehavior="False"/>
                    <asp:Calendar ID="CAL_RELEASE_DATE" CssClass="hide" runat="server" BackColor="#bbbbbb" Caption="RELEASE DATE" CaptionAlign="Top" CellPadding="0"
                        CellSpacing="2" FirstDayOfWeek="Sunday" Font-Names="Arial" ForeColor="#999999" NextPrevFormat="ShortMonth"
                        ShowGridLines="True" ToolTip="Select a RELEASE Date" TitleStyle-ForeColor="#303030" OnSelectionChanged="CAL_RELEASE_DATE_SelectionChanged">
                        <DayHeaderStyle BackColor="#666666" ForeColor="#CCCCCC" />
                        <DayStyle BorderStyle="Dotted" ForeColor="#0066FF" />
                        <NextPrevStyle Font-Names="Tahoma" ForeColor="#0066FF" />
                        <OtherMonthDayStyle Font-Size="0px" />
                        <SelectedDayStyle BackColor="#0066FF" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="True" ForeColor="#333333" />
                        <TodayDayStyle ForeColor="Yellow" />
                    </asp:Calendar>
                </td>
                <td> 
                    <asp:CustomValidator ID="CUSTV_RELEASE_DATE" runat="server" text="✘" ControlToValidate="TB_RELEASE_DATE"
                        Display="Dynamic" ErrorMessage="RELEASE DATE - CAN'T BE BEFORE TODAY !!!" Font-Names="Arial"
                        Font-Size="13px" ForeColor="Red" SetFocusOnError="True" ValidationGroup="VG1" />
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td class="text"> GENRE:- </td>
                <td>
                    <asp:TextBox ID="TB_GENRE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" ValidationGroup="VG1" />
                </td>
                <td style="padding-left:20PX;">
                    <asp:RequiredFieldValidator ID="RFV_GENRE" runat="server" Text="✘" ErrorMessage="PRICE - EMPTY !!!"
                        ForeColor="Red" ControlToValidate="TB_GENRE" Display="Dynamic" Font-Names="Arial"
                        Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    <asp:RegularExpressionValidator ID="REV_GENRE" runat="server" ControlToValidate="TB_GENRE"
                        Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        ErrorMessage="GENRE - Must Have Minimum 1 Genre Type!!! &lt;BR  /&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; [add (SPACE) at the end of Each GENRE Type]"
                        ValidationExpression="^(.{2,}?){1,}$"
                        ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> TICKET PRICE:- </td>
                <td>
                    <asp:TextBox ID="TB_TICKET_PRICE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" ValidationGroup="VG1" />
                </td>
                <td style="padding-left:20PX;">
                    <asp:RequiredFieldValidator ID="RFV_TICKET_PRICE" runat="server" Text="✘" ErrorMessage="TICKET PRICE - EMPTY !!!"
                        ForeColor="Red" ControlToValidate="TB_TICKET_PRICE" Display="Dynamic" Font-Names="Arial"
                        Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    <asp:RegularExpressionValidator ID="REV_TICKET_PRICE" runat="server" ControlToValidate="TB_TICKET_PRICE"
                        Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        ErrorMessage="TICKET PRICE - Must be a Integer Value > 0 !!!"
                        ValidationExpression="^[1-9]{1}[0-9]*$"
                        ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> COVER IMAGE:- </td>
                <td>
                    <asp:FileUpload ID="FU_COVER_IMAGE" runat="server" CssClass="input_field_initial" />
                </td>
                <td style="padding-left:20PX;">
                    <asp:CustomValidator ID="CUSTV_COVER_IMAGE" runat="server" text="✘" ControlToValidate="FU_COVER_IMAGE"
                        Display="Dynamic" Font-Names="Arial" Font-Size="13px" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="VG1" />
                </td>
            </tr>
            <tr>
                <td class="text"> RATING:- </td>
                <td>
                    <asp:DropDownList ID="DDL_RATING" runat="server" CssClass="input_field_initial" style="width:101%;">
                        <asp:ListItem Text="1" Value="1" />
                        <asp:ListItem Text="2" Value="2" />
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="4" Value="4" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="6" Value="6" />
                        <asp:ListItem Text="7" Value="7" />
                        <asp:ListItem Text="8" Value="8" />
                        <asp:ListItem Text="9" Value="9" />
                        <asp:ListItem Text="10" Value="10" />
                    </asp:DropDownList>
                </td>
                <td style="padding-left:20PX;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="text"> DESCRIPTION:- </td>
                <td>
                    <asp:TextBox ID="TB_DESCRIPTION" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" Rows="4" TextMode="MultiLine" />
                </td>
                <td style="padding-left:20PX;">
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr><td colspan="3"><hr /></td></tr>
            <tr>
                <td>&nbsp;</td>
                <td align="center">
                    <asp:Button ID="BTN_ADD_MOVIE" runat="server" CssClass="button_green" text="ADD MOVIE" ValidationGroup="VG1" OnClick="BTN_ADD_MOVIE_Click" />
                    <asp:Button ID="BTN_RESET" runat="server" CssClass="button_red" text="RESET" OnClick="BTN_RESET_Click"
                        CausesValidation="False" ClientIDMode="Static" EnableViewState="False" UseSubmitBehavior="False"
                        ValidationGroup="VG2" ViewStateMode="Disabled"/>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr><td colspan="3"><hr /></td></tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="L_RESPONSE" runat="server" Font-Names="Arial" Font-Size="18px" /><br>
                    <asp:Label ID="L_ERROR_DESCRIPTION" runat="server" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        width="50%" />
                </td>
            </tr>
            <tr>
                <td colspan="3"><hr />
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

