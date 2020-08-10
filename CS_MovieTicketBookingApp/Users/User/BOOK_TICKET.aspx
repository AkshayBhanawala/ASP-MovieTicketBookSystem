<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_User.master" AutoEventWireup="True" CodeBehind="~/Users/BOOK_TICKET.aspx.cs" Inherits="CS_MovieTicketBookingApp.Users.BOOK_TICKET" %>

<asp:Content ID="HEAD_BOOK_TICKET_CONTENT" ContentPlaceHolderID="SUB_HEAD_LOGIN_USER_CONTENTPLACEHOLDER" Runat="Server">
    <title>BOOK A TICKET</title>
    <style>
        .heading
        {
            font-family:Tahoma;
            font-size:35px;
        }
    </style>
</asp:Content>
<asp:Content ID="BODY_BOOK_TICKET_CONTENT" ContentPlaceHolderID="SUB_BODY_LOGIN_USER_CONTENTPLACEHOLDER" Runat="Server">
    <center>
        <br><br>
    <div class="div_form">
        <table>
            <tr><td colspan="2"><br /></td></tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <th colspan="2" class="heading">
                    BOOK A TICKET
                </th>
            </tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="VS1" runat="server" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                        ValidationGroup="VG1" EnableClientScript="False" HeaderText="Following Error(s) in Form:-" />
                </td>
            </tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td class="text"> MOVIE NAME:- </td>
                <td>
                    <asp:TextBox ID="TB_MOVIE_NAME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                        ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> RELEASE DATE:- </td>
                <td>
                    <asp:TextBox ID="TB_RELEASE_DATE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                        ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> GENRE:- </td>
                <td>
                    <asp:TextBox ID="TB_GENRE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                        ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> MINIMUM PRICE:- </td>
                <td>
                    <asp:TextBox ID="TB_MIN_PRICE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                        ReadOnly="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> SHOW DATE:- </td>
                <td>
                    <asp:DropDownList ID="DDL_SHOW_DATE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> SHOW TIME:- </td>
                <td>
                    <asp:DropDownList ID="DDL_SHOW_TIME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                <td class="text"> NO. OF TICKETS:- </td>
                <td>
                    <asp:DropDownList ID="DDL_NO_OF_TICKETS" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="font-size:20px; color:#0066FF;">
                    <asp:Label ID="L_SELECTED_SEATS_ERROR" 
                        runat="server" visible="False" Font-Names="Arial" Font-Size="12px" 
                        ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="font-size:20px; color:#0066FF;">
                    ----------: SELECT SEATS :----------
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <div class="seat_block_main" before="SCREEN" after="EXIT">
                        <div class="seats_layout seats_layout_executive">
                            <ul>
                                <li>A</li>
                                <li>B</li>
                                <li>C</li>
                                <li>D</li>
                            </ul>
                        </div>
                        <div class="seats_layout seats_layout_club">
                            <ul>
                                <li>E</li>
                                <li>F</li>
                                <li>G</li>
                                <li>H</li>
                                <li>I</li>
                            </ul>
                        </div>
                        <div class="seats_layout seats_layout_silver">
                            <ul>
                                <li>J</li>
                                <li>K</li>
                                <li>L</li>
                            </ul>
                        </div>
                        <div class="seats_layout seats_layout_gold">
                            <ul>
                                <li>M</li>
                                <li>N</li>
                                <li>O</li>
                            </ul>
                        </div>
                        <div class="seat_block_sub">
                            <asp:Panel runat="server" id="P_SEATS_LEFT" CssClass="seats" Direction="LeftToRight" HorizontalAlign="Center" ScrollBars="Auto" ClientIDMode="Static" />
                        </div>
                        <div class="seat_block_sub">
                            <asp:Panel runat="server" id="P_SEATS_CENTER" CssClass="seats" Direction="LeftToRight" HorizontalAlign="Center" ScrollBars="Auto" ClientIDMode="Static" />
                        </div>
                        <div class="seat_block_sub">
                            <asp:Panel runat="server" id="P_SEATS_RIGHT" CssClass="seats" Direction="LeftToRight" HorizontalAlign="Center" ScrollBars="Auto" ClientIDMode="Static" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr><td colspan="2"><br></td></tr>
            <tr>
                <td colspan="2" align="left">
                    <table width="100%">
                        <tr>
                            <td class="display_only_seats">
                                <input id="CB_DISPLAY_GOLD" type="checkbox" disabled gold />
                                <label for="CB_DISPLAY_GOLD"></label>
                                &nbsp;- GOLD CLASS [ + 80₹ ]
                            </td>
                            <td class="display_only_seats">
                                <input id="CB_DISPLAY_SILVER" type="checkbox" disabled silver />
                                <label for="CB_DISPLAY_SILVER"></label>
                                &nbsp;- SILVER CLASS [ + 50₹ ]
                            </td>
                        </tr>
                        <tr>
                            <td class="display_only_seats">
                                <input id="CB_DISPLAY_CLUB" type="checkbox" disabled club />
                                <label for="CB_DISPLAY_CLUB"></label>
                                &nbsp;- CLUB CLASS [ + 30₹ ]
                            </td>
                            <td class="display_only_seats">
                                <input id="CB_DISPLAY_EXECUTIVE" type="checkbox" disabled executive />
                                <label for="CB_DISPLAY_EXECUTIVE"></label>
                                &nbsp;- EXECUTIVE CLASS
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="padding-top:10px; padding-bottom:10px;">
                        <tr>
                            <td class="display_only_seats">
                                <input type="checkbox" disabled available />
                                <label for="CB_DISPLAY_GOLD"></label>
                                 - AVAILABLE
                            </td>
                            <td class="display_only_seats">
                                <input type="checkbox" disabled not_available />
                                <label for="CB_DISPLAY_GOLD"></label>
                                 - NOT AVAILABLE
                            </td>
                            <td class="display_only_seats">
                                <input type="checkbox" disabled user_selected />
                                <label for="CB_DISPLAY_GOLD"></label>
                                 - YOUR SELECTION
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td class="text"> TOTAL PRICE:- </td>
                <td>
                    <asp:TextBox ID="TB_TOTAL_PRICE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                        ReadOnly="True" />
                    <asp:Button ID="BTN_CALCULATE_TOTAL_PRICE" runat="server" CssClass="button_blue" text="CALCULATE TOTAL PRICE" OnClick="BTN_CALCULATE_TOTAL_PRICE_Click" />
                </td>
            </tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr>
                <td>&nbsp;</td>
                <td align="center">
                    <asp:Button ID="BTN_BOOK" runat="server" CssClass="button_green" text="BOOK" ValidationGroup="VG1" OnClick="BTN_BOOK_Click" />
                    <asp:Button ID="BTN_RESET" runat="server" CssClass="button_red" text="RESET"
                        CausesValidation="False" ClientIDMode="Static" EnableViewState="False" UseSubmitBehavior="False"
                        ValidationGroup="VG2" ViewStateMode="Disabled" OnClick="BTN_RESET_Click"/>
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

