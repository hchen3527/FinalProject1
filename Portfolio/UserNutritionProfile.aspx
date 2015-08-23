<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserNutritionProfile.aspx.cs" Inherits="Portfolio.UserNutritionProfile" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="CSS/style.css" />
<script type="text/javascript" src="Scripts/jquery-latest.js"></script> 
<script type="text/javascript" src="Scripts/jquery.tablesorter.js"></script> 
<script type="text/javascript" src="Scripts/jquery.tablesorter.pager.js"></script> 
<script type="text/javascript">
    Date.prototype.yyyymmdd = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
        var dd = this.getDate().toString();
        return yyyy + "-" + (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]); // padding
    };

    $(function () {
        $("#Add_Date_From").val($("#Add_Date_From").attr("val"));
        $("#Add_Date_To").val($("#Add_Date_To").attr("val"));

        $("#nutritionProfileTable")
            .tablesorter({ widthFixed: true, widgets: ['zebra'] })
            .tablesorterPager({ container: $("#pager"), size: 30});
    });

    function Search() {
        var filterData = {
            "Type": "ProfileSearch",
            "AddDateFrom": $("#Add_Date_From").val(),
            "AddDateTo": $("#Add_Date_To").val()
        };

        $.ajax({
            url: "Handler/NutritionHandler.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: filterData,
            responseType: "json",
            success: OnComplete,
            error: PopulateTable
        });

        return false;
    }

    function OnComplete(result) {
        alert("Hello");
    }

    function PopulateTable(result) {
        $("#NutritionProfileHolder").html(result.response);
        $("#nutritionProfileTable")
            .tablesorter({ widthFixed: true, widgets: ['zebra'] })
            .tablesorterPager({ container: $("#pager"), size: $("#pagesizer").val() });
    }
</script>
<style type="text/css">
    .FilterLabel
    {
        text-align: right;
    }

    .FilterInput
    {
        text-align: left;
    }

    .Number
    {
        text-align: right;
    }
</style>
<head runat="server">
    <title>User Profile</title>
</head>
<body>
    <div id="selectedHolder" style="float:left;">
    <table style="border: 3px solid #8dbdd8; background-color: #CDCDCD;">
        <thead>
            <tr>
                <th colspan="2" style="border-bottom:2px solid #8dbdd8;">Date Range</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="FilterLabel">Add Date:</td>
                <td id="FoodNameDisplay" class="FilterInput"><input type="date" id="Add_Date_From" runat="server" /> to <input type="date" id="Add_Date_To" runat="server" /></td>
            </tr>
            <tr>
                <td style="text-align: right;" colspan="2">
                    <img style="height:25px; width:50px;" src="Images/search.png" alt="Search"  onclick="javascript:Search();" />
                </td>
            </tr>
        </tbody>
    </table>
</div>

    <div id="UserInformationHolder">
        <asp:Table id="userInformationTable" runat="server">

        </asp:Table>
    </div>
    <br />
    <br />
    <div id="NutritionProfileHolder">
        <asp:Table id="nutritionProfileTable" class="tablesorter" runat="server">

        </asp:Table>
    </div>
    <div id="pager" class="pager">
    <form>
        <img src="Images/first.png" class="first"/>
        <img src="Images/prev.png" class="prev"/>
        <input type="text" class="pagedisplay" readonly="true"/>
        <img src="Images/next.png" class="next"/>
        <img src="Images/last.png" class="last"/>
        <select id="pagesizer" class="pagesize">
            <option  value="10">10</option>
            <option value="20">20</option>
            <option selected="selected" value="30">30</option>
            <option  value="40">40</option>
            <option  value="40">50</option>
        </select>
    </form>
</div>
<br />
<br />
<br />
<a href="NutritionalDisplay.aspx">Food Search</a>
</body>
</html>
