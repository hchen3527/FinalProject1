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
    public partial class NutritionalDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Food_Nutrition_Get", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
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
                        this.domainsTable.Rows.Add(header);
                        TableRow tr;
                        while (reader.Read())
                        {
                            tr = new TableRow();
                            //tr.Attributes.Add("onclick", "alert('" + reader["Name"].ToString() + "');");
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
                            this.domainsTable.Rows.Add(tr);
                        }
                    }
                    conn.Close();
                }
            }
        }
    }
}