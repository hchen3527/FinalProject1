<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NutritionalDisplay.aspx.cs" Inherits="Portfolio.NutritionalDisplay" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="CSS/style.css" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script type="text/javascript" src="Scripts/jquery.tablesorter.js"></script> 
<script type="text/javascript" src="Scripts/jquery.tablesorter.pager.js"></script> 
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
<script type="text/javascript">
    $(function () {
        $("#domainsTable")
            .tablesorter({ widthFixed: true, widgets: ['zebra'] })
            .tablesorterPager({ container: $("#pager"), size: 30 });
    });
    $(function () {
        $("#dialog").dialog();
    });
</script>
<head runat="server">
    <title>Food Nutrition</title>
</head>
<body>
<div id="dialog" title="Basic dialog">
  <p>This is the default dialog which is useful for displaying information. The dialog window can be moved, resized and closed with the 'x' icon.</p>
</div>
<asp:Table id="domainsTable" class="tablesorter" runat="server"> 
</asp:Table>
<div id="pager" class="pager">
    <form>
        <img src="Images/first.png" class="first"/>
        <img src="Images/prev.png" class="prev"/>
        <input type="text" class="pagedisplay" readonly="true"/>
        <img src="Images/next.png" class="next"/>
        <img src="Images/last.png" class="last"/>
        <select class="pagesize">
            <option  value="10">10</option>
            <option value="20">20</option>
            <option selected="selected" value="30">30</option>
            <option  value="40">40</option>
            <option  value="40">50</option>
        </select>
    </form>
</div>
</body>
</html>