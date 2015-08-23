using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Portfolio
{
    public partial class NutritionProfileForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? foodKey = null;
            int? unitKey = null;
            this.tableHeader.Attributes.Add("nutritionhistorykey", Request.QueryString["Nutrition_History_Key"]);
            this.tableHeader.Attributes.Add("adddatefrom", Request.QueryString["Add_Date_From"]);
            this.tableHeader.Attributes.Add("adddateto", Request.QueryString["Add_Date_To"]);
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Nutrition_Profile_Entry_Get", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@User_Key", SqlDbType.Int);
                    command.Parameters["@User_Key"].Value = 1; //Hard-coded to my User_Key for now
                    command.Parameters.Add("@Nutrition_History_Key", SqlDbType.Int);
                    command.Parameters["@Nutrition_History_Key"].Value = Request.QueryString["Nutrition_History_Key"];
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows)
                    {
                        reader.Read();
                        foodKey = int.Parse(reader["Food_Key"].ToString());
                        unitKey = int.Parse(reader["Unit_Key"].ToString());
                        var cell = new TableHeaderCell { Text = reader["Name"].ToString(), ColumnSpan = 2 };
                        cell.Style.Add("border-bottom-width", "2px");
                        cell.Style.Add("border-bottom-style", "solid");
                        cell.Style.Add("border-bottom-color", "#8dbdd8");
                        tableHeader.Cells.Add(cell);

                        this.FoodQuantityDisplay.Value = reader["Quantity"].ToString();
                        DateTime datetime = DateTime.Parse(reader["Add_Date"].ToString());
                        string date = datetime.ToString("yyyy-MM-dd");
                        this.AddDateDisplay.Value = date;
                        this.CaloriesDisplay.InnerHtml = reader["Calories"].ToString();
                        this.WaterDisplay.InnerHtml = reader["Water"].ToString();
                        this.ProteinDisplay.InnerHtml = reader["Protein"].ToString();
                        this.LipidDisplay.InnerHtml = reader["Lipid"].ToString();
                        this.CarbohydrateDisplay.InnerHtml = reader["Carbohydrate"].ToString();
                        this.FiberDisplay.InnerHtml = reader["Fiber"].ToString();
                        this.SugarDisplay.InnerHtml = reader["Sugar"].ToString();
                        this.CalciumDisplay.InnerHtml = reader["Calcium"].ToString();
                        this.IronDisplay.InnerHtml = reader["Iron"].ToString();
                    }

                    conn.Close();
                }
            }

            if (foodKey.HasValue)
            {
                DropDownList list = new DropDownList();
                list.ID = "UnitDisplay";
                using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
                {
                    using (var command = new SqlCommand("dbo.Food_Units_Get", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.Add("@Food_Key", SqlDbType.Int);
                        command.Parameters["@Food_Key"].Value = foodKey.Value;
                        conn.Open();
                        var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                        list.DataSource = reader;
                        list.DataTextField = "Unit_Name";
                        list.DataValueField = "Unit_Key";
                        list.DataBind();
                        list.SelectedValue = unitKey.Value.ToString();
                        conn.Close();
                    }
                }

                StringWriter theStringWriter = new StringWriter();
                HtmlTextWriter theHtmlTextWriter = new HtmlTextWriter(theStringWriter);

                // Render the table row control into the writer
                list.RenderControl(theHtmlTextWriter);

                // Return the string via the StringWriter
                this.UnitDisplayHolder.InnerHtml = theStringWriter.ToString();
            }
        }
    }
}