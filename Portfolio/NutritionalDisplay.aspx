<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NutritionalDisplay.aspx.cs" Inherits="Portfolio.NutritionalDisplay" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="CSS/style.css" />
<script type="text/javascript" src="Scripts/jquery-latest.js"></script> 
<script type="text/javascript" src="Scripts/jquery.tablesorter.js"></script> 
<script type="text/javascript" src="Scripts/jquery.tablesorter.pager.js"></script> 
<script type="text/javascript">
    $(function () {
        $("#domainsTable")
            .tablesorter({ widthFixed: true, widgets: ['zebra'] })
            .tablesorterPager({ container: $("#pager"), size: 30});
    });

    function SelectFood(food)
    {
        var $food = $(food);
        $("tr").removeClass("Selected");
        $("td").removeClass("Selected");
        $food.addClass("Selected");
        $food.children().each(function (index) {
            $(this).addClass("Selected");
        });

        $("#FoodNameDisplay").text($food.attr("foodname"));
        $("#FoodNameDisplay").attr("foodkey", ($food.attr("foodkey")));
        var filterData = {
            "Type": "DropDown",
            "FoodKey": $food.attr("foodkey")
        };

        $.ajax({
            url: "Handler/NutritionHandler.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: filterData,
            responseType: "json",
            success: OnComplete,
            error: PopulateDropdown
        });
    }

    function Search() {
        var filterData = {
            "Type": "Search",
            "FoodName": $("#FoodName").val(),
            "MinCalories": $("#MinCalories").val(),
            "MaxCalories": $("#MaxCalories").val(),
            "MinWater": $("#MinWater").val(),
            "MaxWater": $("#MaxWater").val(),
            "MinProtein": $("#MinProtein").val(),
            "MaxProtein": $("#MaxProtein").val(),
            "MinLipid": $("#MinLipid").val(),
            "MaxLipid": $("#MaxLipid").val(),
            "MinCarbohydrate": $("#MinCarbohydrate").val(),
            "MaxCarbohydrate": $("#MaxCarbohydrate").val(),
            "MinFiber": $("#MinFiber").val(),
            "MaxFiber": $("#MaxFiber").val(),
            "MinSugar": $("#MinSugar").val(),
            "MaxSugar": $("#MaxSugar").val(),
            "MinCalcium": $("#MinCalcium").val(),
            "MaxCalcium": $("#MaxCalcium").val(),
            "MinIron": $("#MinIron").val(),
            "MaxIron": $("#MaxIron").val()
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

    function NutritionProfleAdd() {
        var val = $("#FoodQuantityDisplay").val()
        if (val == "" || parseFloat(val) <= 0 || $("#FoodNameDisplay").attr("foodkey") == undefined)
        {
            return;
        }

        var filterData = {
            "Type": "NutritionProfileAdd",
            "FoodKey": $("#FoodNameDisplay").attr("foodkey"),
            "Quantity": $("#FoodQuantityDisplay").val(),
            "UnitKey": $("#FoodUnitList").val()
        };

        $.ajax({
            url: "Handler/NutritionHandler.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: filterData,
            responseType: "json",
            success: OnComplete,
            error: DisplaySuccess
        });

        return false;
    }

    function CheckIsNum(object)
    {
        var val = $(object).val();
        if (val == "")
        {
            return;
        }
            
        if (isNaN(parseFloat(val)) || isFinite(val) == false)
        {
            $(object).val(0);
        }
        else if (parseFloat(val) < 0)
        {
            $(object).val(0);
        }
    }   

    function OnComplete(result) {
        alert("Hello");
    }

    function PopulateTable(result) {
        $("#tableHolder").html(result.response);
        $("#domainsTable")
            .tablesorter({ widthFixed: true, widgets: ['zebra'] })
            .tablesorterPager({ container: $("#pager"), size: $("#pagesizer").val() });
    }

    function PopulateDropdown(result) {
        $("#FoodUnitDisplay").html(result.response);
    }

    function DisplaySuccess(result)
    {
        alert(result.response);
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

    table.tablesorter tbody tr td.Selected
    {
        background-color: lightblue;
    }
</style>
<head runat="server">
    <title>Food Nutrition</title>
</head>
<body>
<div id="selectedHolder" style="float:left;">
    <table style="border: 3px solid #8dbdd8; background-color: #CDCDCD;">
        <thead>
            <tr>
                <th colspan="2" style="border-bottom:2px solid #8dbdd8;">Food Add</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="FilterLabel">Name:</td>
                <td id="FoodNameDisplay" class="FilterInput"></td>
            </tr>
            <tr>
                <td class="FilterLabel">Quantity:</td>
                <td class="FilterInput"><input id="FoodQuantityDisplay" type="text" style="width:100px;" value="" onblur="javascript:CheckIsNum(this);" /></td>
            </tr>
            <tr>
                <td class="FilterLabel">Unit:</td>
                <td id="FoodUnitDisplay" class="FilterInput"></td>
            </tr>
            <tr>
                <td style="text-align: right;" colspan="2">
                    <img style="height:25px; width:50px;" src="Images/button_submit.png" alt="Submit"  onclick="javascript:NutritionProfleAdd();" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div id="FilterHolder">
    <table id="Filter" class="filter" style="margin: auto; border: 3px solid #8dbdd8; background-color: #CDCDCD;">
        <thead>
            <tr>
                <th colspan="6" style="border-bottom:2px solid #8dbdd8;">Food</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="FilterLabel">
                    Food Name:
                </td>
                <td class="FilterInput">
                    <input type="text" id="FoodName" />
                </td>
                <td class="FilterLabel">
                    Calories:
                </td>
                <td class="FilterInput">                  
                    <input id="MinCalories" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxCalories" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
                <td class="FilterLabel">
                    Water:
                </td>
                <td class="FilterInput">
                    <input id="MinWater" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxWater" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
            </tr>
            <tr>
                <td class="FilterLabel">
                    Protein:
                </td>
                <td class="FilterInput">
                    <input id="MinProtein" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxProtein" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
                <td class="FilterLabel">
                    Lipid:
                </td>
                <td class="FilterInput">
                    <input id="MinLipid" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxLipid" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
                <td class="FilterLabel">
                    Carbohydrate:
                </td>
                <td class="FilterInput">
                    <input id="MinCarbohydrate" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxCarbohydrate" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
            </tr>
            <tr>
                <td class="FilterLabel">
                    Fiber:
                </td>
                <td class="FilterInput">
                    <input id="MinFiber" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxFiber" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
                <td class="FilterLabel">
                    Sugar:
                </td>
                <td class="FilterInput">
                    <input id="MinSugar" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxSugar" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
                <td class="FilterLabel">
                    Calcium:
                </td>
                <td class="FilterInput">
                    <input id="MinCalcium" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxCalcium" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
            </tr>
            <tr>
                <td class="FilterLabel">
                    Iron:
                </td>
                <td class="FilterInput">
                    <input id="MinIron" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" /> to <input id="MaxIron" type="text" style="width:50px;" value="" onblur="javascript:CheckIsNum(this);" />
                </td>
                <td class="FilterLabel">

                </td>
                <td class="FilterInput">

                </td>
                <td class="FilterLabel">

                </td>
                <td class="FilterInput">
                    <img style="height:25px; width:50px;" src="Images/search.png" alt="Search"  onclick="javascript:Search();" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div id="tableHolder">
<asp:Table id="domainsTable" class="tablesorter" runat="server"> 
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
<a href="UserNutritionProfile.aspx">User Nutrition Profile</a>
</body>
</html>