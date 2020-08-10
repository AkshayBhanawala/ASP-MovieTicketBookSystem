<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_No.master" AutoEventWireup="True" CodeBehind="~/Users/REGISTRATION_USER.aspx.cs" Inherits="CS_MovieTicketBookingApp.Users.REGISTRATION_USER" %>

<asp:Content ID="HEAD_REGISTRATION_USER_CONTENT" ContentPlaceHolderID="SUB_HEAD_LOGIN_NO_CONTENTPLACEHOLDER" Runat="Server">
    <title>USER REGISTRATION</title>
</asp:Content>
<asp:Content ID="BODY_REGISTRATION_USER_CONTENT" ContentPlaceHolderID="SUB_BODY_LOGIN_NO_CONTENTPLACEHOLDER" Runat="Server">
    <center>
        <br><br>
        <div class="div_form">
            <table cellspacing="2.5" cellpadding="2.5">
                <tr>
                    <td colspan="4">
                        <hr/>
                        <asp:ValidationSummary ID="VS1" runat="server" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                            ValidationGroup="VG1" EnableClientScript="False" HeaderText="Following Error(s) in Form:-" />
                        <hr/>
                    </td>
                </tr>
                <tr>
                    <td class="text"> USERNAME: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_USERNAME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_USERNAME" runat="server" Text="✘" ErrorMessage="USERNAME - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_USERNAME" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="REV_USERNAME" runat="server" ControlToValidate="TB_USERNAME"
                            Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                            ErrorMessage="USERNAME - Must Start with Alphabet & must have Minimum than 3 characters !!!"
                            ValidationExpression="^[A-Za-z]{1}[A-Za-z0-9_]{2,}$" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:CustomValidator ID="CUSTV_USERNAME" runat="server" Text="✘" ControlToValidate="TB_USERNAME"
                            Display="Dynamic" ErrorMessage="USERNAME - ALREADY EXIST !!!" Font-Names="Arial" Font-Size="13px"
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="VG1" />
                    </td>
                </tr>
                <tr>
                    <td class="text"> EMAIL: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_EMAIL" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_EMAIL" runat="server" Text="✘" ErrorMessage="EMAIL - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_EMAIL" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="REV_EMAIL" runat="server" ControlToValidate="TB_EMAIL"
                            Display="Dynamic" Text="✘" ErrorMessage="EMAIL - Must be in a Correct Format !!!" Font-Names="Arial"
                            Font-Size="13px" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:CustomValidator ID="CUSTV_EMAIL" runat="server" Text="✘" ControlToValidate="TB_EMAIL"
                            Display="Dynamic" ErrorMessage="EMAIL - ALREADY EXIST !!!" Font-Names="Arial" Font-Size="13px"
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="VG1" />
                    </td>
                </tr>
                <tr>
                    <td class="text"> NEW PASSWORD: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_NEW_PASSWORD" runat="server" CssClass="input_field_initial" TextMode="Password"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_NEW_PASSWORD" runat="server" Text="✘" ErrorMessage="NEW PASSWORD - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_NEW_PASSWORD" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="REV_NEW_PASSWORD" runat="server" ControlToValidate="TB_NEW_PASSWORD"
                            Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                            ErrorMessage="NEW PASSWORD - At Minimum A Special Character, A Digit, A Alphabet are Required & Must have more than 6 Characters !!!"
                            ValidationExpression="^(?=.*[A-Za-z])(?=.*[0-9])(?=.*[$@$!%*#?&amp;])[A-Za-z0-9$@$!%*#?&amp;]{6,}$"
                            ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    </td>
                </tr>
                <tr>
                    <td class="text"> CONFIRM PASSWORD: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_CONFIRM_PASSWORD" runat="server" CssClass="input_field_initial" TextMode="Password"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_CONFIRM_PASSWORD" runat="server" Text="✘" ErrorMessage="CONFIRM PASSWORD - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_CONFIRM_PASSWORD" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:CompareValidator ID="CMPV_CONFIRM_PASSWORD" runat="server" ControlToCompare="TB_NEW_PASSWORD"
                            ControlToValidate="TB_CONFIRM_PASSWORD" Display="Dynamic" Text="✘" ForeColor="Red"
                            ErrorMessage="CONFIRM PASSWORD - Doesn't Match with Above Password !!!" Font-Names="Arial" Font-Size="13px"
                            ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    </td>
                </tr>
                <tr>
                    <td class="text"> FIRST NAME: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_FIRST_NAME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_FIRST_NAME" runat="server" Text="✘" ErrorMessage="FIRST NAME - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_FIRST_NAME" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="REV_FIRST_NAME" runat="server" ControlToValidate="TB_FIRST_NAME"
                            Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                            ErrorMessage="FIRST NAME - Must Only Consist of Alphabet & must have Minimum 2 characters !!!"
                            ValidationExpression="^[A-Za-z]{2,}$" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    </td>
                </tr>
                <tr>
                    <td class="text"> LAST NAME: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_LAST_NAME" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_LAST_NAME" runat="server" Text="✘" ErrorMessage="LAST NAME - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_LAST_NAME" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="REV_LAST_NAME" runat="server" ControlToValidate="TB_LAST_NAME"
                            Display="Dynamic" Text="✘" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                            ErrorMessage="LAST NAME - Must Only Consist of Alphabet & must have Minimum 2 characters !!!"
                            ValidationExpression="^[A-Za-z]{2,}$" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                    </td>
                </tr>
                <tr>
                    <td class="text"> AGE:- </td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDL_AGE" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                            CausesValidation="True" ValidationGroup="VG1" required/>
                    </td>
                    <td style="padding-left:20PX;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="text"> MOBILE NO: </td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_MOBILE_NO" runat="server" CssClass="input_field_initial" style="text-transform:uppercase;"
                            CausesValidation="True" ValidationGroup="VG1" required OnTextChanged="TB_TextChanged"/>
                    </td>
                    <td style="padding-left:20PX;">
                        <asp:RequiredFieldValidator ID="RFV_MOBILE_NO" runat="server" Text="✘" ErrorMessage="MOBILE NO - EMPTY !!!"
                            ForeColor="Red" ControlToValidate="TB_MOBILE_NO" Display="Dynamic" Font-Names="Arial"
                            Font-Size="13px" ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:RegularExpressionValidator ID="REV_MOBILE_NO" runat="server" ControlToValidate="TB_MOBILE_NO"
                            Display="Dynamic" Text="✘" ErrorMessage="MOBILE NO - Must be of 10 Digit & must not Start with 0 or 1 or 2 !!!"
                            Font-Names="Arial" Font-Size="13px" ForeColor="Red" ValidationExpression="^[^012]{1}[0-9]{9}$"
                            ValidationGroup="VG1" EnableClientScript="False" SetFocusOnError="True" />
                        <asp:CustomValidator ID="CUSTV_MOBILE_NO" runat="server" Text="✘" ControlToValidate="TB_MOBILE_NO"
                            Display="Dynamic" ErrorMessage="MOBILE NO - ALREADY EXIST !!!" Font-Names="Arial" Font-Size="13px"
                            ForeColor="Red" SetFocusOnError="True" ValidationGroup="VG1" />
                    </td>
                </tr>
                <tr><td colspan="4"><hr/></td></tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="center" colspan="1">
                        <asp:Button ID="BTN_REGISTER" runat="server" CssClass="button_green" text="REGISTER" ValidationGroup="VG1" OnClick="BTN_REGISTER_Click" />
                    </td>
                    <td align="center" colspan="1">
                        <asp:Button ID="BTN_RESET" runat="server" CssClass="button_red" text="RESET" OnClick="BTN_RESET_Click"
                            CausesValidation="False" ClientIDMode="Static" EnableViewState="False" UseSubmitBehavior="False"
                            ValidationGroup="VG2" ViewStateMode="Disabled"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <hr/>
                        <asp:Label ID="L_RESPONSE" runat="server" Font-Names="Arial" Font-Size="18px" /><br>
                        <asp:Label ID="L_ERROR_DESCRIPTION" runat="server" Font-Names="Arial" Font-Size="13px" ForeColor="Red"
                            width="50%" />
                    </td>
                </tr>
            </table>
            <asp:Timer ID="T1" runat="server" Enabled="True" Interval="3000" OnTick="T1_Tick">
            </asp:Timer>
            <asp:ScriptManager ID="SM1" runat="server">
            </asp:ScriptManager>
        </div>
    </center>
</asp:Content>

