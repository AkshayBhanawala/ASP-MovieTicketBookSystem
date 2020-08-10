<%@ Page Language="C#" MasterPageFile="~/Master_Pages/MasterPage_Login_No.master" AutoEventWireup="True" CodeBehind="~/HOME.aspx.cs" Inherits="CS_MovieTicketBookingApp.HOME"%>

<asp:Content ID="HEAD_HOME_CONTENT" ContentPlaceHolderID="SUB_HEAD_LOGIN_NO_CONTENTPLACEHOLDER" Runat="Server">
	<title>MOVIES - HOME PAGE</title>
</asp:Content>
<asp:Content ID="BODY_HOME_CONTENT" ContentPlaceHolderID="SUB_BODY_LOGIN_NO_CONTENTPLACEHOLDER" Runat="Server">
    <h1>MOVIES</h1>
    <hr>
    <div class="div_container_movies">
	    <asp:DataList ID="DL_MOVIES" runat="server" RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False" RepeatLayout="Flow">
	    	<ItemTemplate>
	    		<div class="movie_item_div_outside" >
	    			<a href='<%# "BOOK_TICKET.aspx?MID=" + Eval("MOVIE_ID")%>' class="movie_item_a" >
	    				<div><img src='<%# "./" + Eval("IMAGE_URL")%>' class="movie_item_image" width="200" height="300" /></div>
	    				<span class="movie_item_main_span">
	    					<span class="movie_item_span_collection">
	    						<span class="movie_item_span movie_item_rating">
	    							<%# Eval("RATING") + " / 10"%>
	    						</span>
	    						<br />
	    						<span class="movie_item_span movie_item_genre">
	    							<%# Eval("GENRE")%>
	    						</span>
	    						<br />
	    						<span class="movie_item_span movie_item_release_date">
	    							<%# Eval("RELEASE_DAY") + " " + get_MonthName(int.Parse(Eval("RELEASE_MONTH").ToString())) + " " + Eval("RELEASE_YEAR")%>
	    						</span>
	    						<br />
	    						<span class="movie_item_buy_button">
	    							BUY TICKETS
	    						</span>
	    					</span>
	    				</span>
	    			</a>
	    			<a href='<%# "BOOK_TICKET.aspx?MID=" + Eval("MOVIE_ID")%>' class="movie_item_name">
	    				<%# Eval("MOVIE_NAME")%>
	    			</a>
	    		</div>
	    	</ItemTemplate>
	    </asp:DataList>
    </div>
	<br />
    <hr>
	<asp:Label ID="L_NO_MOVIES" runat="server" Text="NO MOVIES ADDED BY ADMINS, SORRY !!!" Visible="False"
		Font-Bold="True" Font-Names="Tahoma" Font-Size="X-Large" ForeColor="Red" />
</asp:Content>

