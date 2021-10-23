<%@ Page Language="C#" Inherits="COMPX519_ASPWebForm.index" %>
<!DOCTYPE html>
<html>
    <head>
        <meta charset='UTF-8'>
        <title>A One replicas</title>
        <meta charset="UTF-8">
        <link rel="stylesheet" href="gridwork-done.css">
    </head>
<body>        
    <h1> <div class='top'><a class='toptext' href="index.aspx">A-ONE REPLICAS </a> </div></h1>
    <form class='inline-block-center'action="results.aspx" method="GET">
        <input id='tb1' type="text" name="keywords">
        <input id='but1' type="submit" value="Search">
        <br><br>
    </form>
            
    <div class="outermost_div">
        <asp:Literal id="productLineList" runat="server"/>  <!-- Results of query will be placed here -->
        
    </div>
</body>
</html>