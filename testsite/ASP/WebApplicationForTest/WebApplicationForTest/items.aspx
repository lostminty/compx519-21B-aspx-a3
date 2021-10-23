<%@ Page Language="C#" Inherits="COMPX519_ASPWebForm.items" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset='UTF-8'>
    <title>A One replicas</title>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="gridwork-done.css">
</head>
<body>
    <h1> <div class='top'><a class='toptext' href="index.aspx">A-ONE REPLICAS </a> </div></h1>
        <form class='inline-block-center' action="results.aspx" method="GET">
            <input id='tb1' type="text" name="keywords">
            <input id='but1' type="submit" value="Search">
            <br><br>
        </form>
        <div class="outermost_div">
            <asp:Literal id="ProductsList" runat="server" />    <!-- Results of query for product's details will be placed here -->
        </div>
        
        
        <!-- HTML for the feedback form -->
        <!-- The form sends data to run at server with Button2_Click function -->
        <form id="reviewForm" method="get" runat="server">
            <h2> leave this product a review </h2>
            <div>
            <asp:TextBox id="tb2" runat="server"/><br>
            <asp:TextBox id="txt" runat="server"/>
            <!-- Hidden field that doesn't get displayed but holds the product code -->
            <asp:HiddenField id="prod" runat="server" />
            <asp:Button id="but2" runat="server" Text="submit" OnClick="Button2_Click" />
            </div>
        </form>
        
        <asp:Literal id="ProductsReview" runat="server" />      <!-- Results of query for product's reviews will be placed here -->
</body>
</html>