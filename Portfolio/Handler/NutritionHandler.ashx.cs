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
    /// <summary>
    /// Summary description for NutritionHandler
    /// </summary>
    public class NutritionHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write(this.FoodSearch(context));
        }

        private string FoodSearch(HttpContext context)
        {
            string[] parameters = new string[] {"Calories", "Water", "Protein", "Lipid", "Carbohydrate", "Fiber", "Sugar", "Calcium", "Iron"};
            Table table = new Table();
            table.ID = "domainsTable";
            table.CssClass = "tablesorter";
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Food_Nutrition_Get", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@Name", SqlDbType.VarChar);
                    command.Parameters["@Name"].Value = context.Request.Params["FoodName"];
                    decimal num;
                    foreach (var parameter in parameters)
                    {
                        if (decimal.TryParse(context.Request.Params["Min" + parameter], out num))
                        {
                            command.Parameters.Add("@Min" + parameter, SqlDbType.Decimal);
                            command.Parameters["@Min" + parameter].Value = num;
                        }

                        if (decimal.TryParse(context.Request.Params["Max" + parameter], out num))
                        {
                            command.Parameters.Add("@Max" + parameter, SqlDbType.Decimal);
                            command.Parameters["@Max" + parameter].Value = num;
                        }
                    }
                    
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows)
                    {
                        TableHeaderRow header = new TableHeaderRow();
                        header.Cells.Add(new TableHeaderCell { Text = "Food Name" });
                        header.Cells.Add(new TableHeaderCell { Text = "Calories" });
                        header.Cells.Add(new TableHeaderCell { Text = "Water" });
                        header.Cells.Add(new TableHeaderCell { Text = "Protein" });
                        header.Cells.Add(new TableHeaderCell { Text = "Lipid" });
                        header.Cells.Add(new TableHeaderCell { Text = "Carbohydrate" });
                        header.Cells.Add(new TableHeaderCell { Text = "Fiber" });
                        header.Cells.Add(new TableHeaderCell { Text = "Sugar" });
                        header.Cells.Add(new TableHeaderCell { Text = "Calcium" });
                        header.Cells.Add(new TableHeaderCell { Text = "Iron" });
                        header.TableSection = TableRowSection.TableHeader;
                        table.Rows.Add(header);
                        TableRow tr;
                        while (reader.Read())
                        {
                            tr = new TableRow();
                            tr.Cells.Add(new TableCell { Text = reader["Name"].ToString() });
                            tr.Cells.Add(new TableCell { Text = reader["Calories"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Water"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Protein"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Lipid"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Carbohydrate"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Fiber"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Sugar"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Calcium"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Iron"].ToString(), CssClass = "Number" });
                            table.Rows.Add(tr);
                        }
                    }
                    conn.Close();
                }
            }

            StringWriter theStringWriter = new StringWriter();
            HtmlTextWriter theHtmlTextWriter = new HtmlTextWriter(theStringWriter);

            // Render the table row control into the writer
            table.RenderControl(theHtmlTextWriter);

            // Return the string via the StringWriter
            return theStringWriter.ToString();
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}