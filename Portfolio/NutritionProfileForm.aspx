<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NutritionProfileForm.aspx.cs" Inherits="Portfolio.NutritionProfileForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="CSS/style.css" />
<script type="text/javascript" src="Scripts/jquery-latest.js"></script> 
<script type="text/javascript">
    function Update() {
        var filterData = {
            "Type": "UpdateEntry",
            "NutritionHistoryKey": $("#tableHeader").attr("nutritionhistorykey"),
            "Quantity": $("#FoodQuantityDisplay").val(),
            "AddDate": $("#AddDateDisplay").val(),
            "UnitKey": $("#UnitDisplay").val()
        };

        $.ajax({
            url: "Handler/NutritionHandler.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: filterData,
            responseType: "json",
            success: OnComplete,
            error: Redirect
        });

        return false;
    }

    function Delete() {
        var filterData = {
            "Type": "DeleteEntry",
            "NutritionHistoryKey": $("#tableHeader").attr("nutritionhistorykey"),
        };

        $.ajax({
            url: "Handler/NutritionHandler.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: filterData,
            responseType: "json",
            success: OnComplete,
            error: Redirect
        });

        return false;
    }

    function OnComplete(result) {
        alert("Hello");
    }

    function Redirect(result) {
        alert(result.response);
        window.location = "UserNutritionProfile.aspx?Add_Date_From=" + $("#tableHeader").attr("adddatefrom") + "&Add_Date_To=" + $("#tableHeader").attr("adddateto");
    }

    function CheckIsNum(object) {
        var val = $(object).val();
        if (val == "") {
            return;
        }

        if (isNaN(parseFloat(val)) || isFinite(val) == false) {
            $(object).val(0);
        }
        else if (parseFloat(val) < 0) {
            $(object).val(0);
        }
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
    <title>Nutrition Profile Entry</title>
</head>
<body>
    <div>
    <form>
        <table id="nutritionEntryTable"  style="margin: auto; border: 3px solid #8dbdd8; background-color: #CDCDCD;">
            <asp:TableHeaderRow id="tableHeader" runat="server">
            </asp:TableHeaderRow>
            <tbody>
              <tr>
                  <td class="FilterLabel">Quantity:</td>
                  <td class="FilterInput"><input id="FoodQuantityDisplay" type="text" style="width:100px;" value="" onblur="javascript:CheckIsNum(this);" runat="server" /></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Unit:</td>
                  <td id="UnitDisplayHolder" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Add Date:</td>
                  <td class="FilterInput"><input type="date" id="AddDateDisplay" runat="server" /></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Calories:</td>
                  <td id="CaloriesDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Water:</td>
                  <td id="WaterDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Protein:</td>
                  <td id="ProteinDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Lipid:</td>
                  <td id="LipidDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Carbohydrate:</td>
                  <td id="CarbohydrateDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Fiber:</td>
                  <td id="FiberDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Sugar:</td>
                  <td id="SugarDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Calcium:</td>
                  <td id="CalciumDisplay" class="FilterInput" runat="server"></td>
              </tr>
              <tr>
                  <td class="FilterLabel">Iron:</td>
                  <td id="IronDisplay" class="FilterInput" runat="server"></td>
              </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td class="FilterInput""><img style="height:25px; width:50px;" src="Images/Update.png" alt="Update"  onclick="javascript:Update();" /></td>
                    <td class="FilterLabel"><img style="height:25px; width:50px;" src="Images/Delete.png" alt="Delete"  onclick="javascript:Delete();" /></td>
                </tr>
            </tfoot>
        </table>
    </form>
    </div>
</body>
</html>
