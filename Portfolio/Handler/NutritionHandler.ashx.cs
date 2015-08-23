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
            if (context.Request.Params["Type"] == "Search")
            {
                context.Response.Write(this.FoodSearch(context));
            }
            else if (context.Request.Params["Type"] == "DropDown")
            {
                context.Response.Write(this.DropDownPopulate(context));
            }
            else if (context.Request.Params["Type"] == "NutritionProfileAdd")
            {
                context.Response.Write(this.NutritionProfileAdd(context));
            }
            else if (context.Request.Params["Type"] == "ProfileSearch")
            {
                context.Response.Write(this.ProfileSearch(context));
            }
            else if (context.Request.Params["Type"] == "UpdateEntry")
            {
                context.Response.Write(this.UpdateEntry(context));
            }
            else if (context.Request.Params["Type"] == "DeleteEntry")
            {
                context.Response.Write(this.DeleteEntry(context));
            }
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
                            tr.Attributes.Add("onclick", "SelectFood(this);");
                            tr.Attributes.Add("foodkey", reader["Food_Key"].ToString());
                            tr.Attributes.Add("foodunitkey", reader["Food_Unit_Key"].ToString());
                            tr.Attributes.Add("foodname", reader["Name"].ToString());
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

        private string DropDownPopulate(HttpContext context)
        {
            DropDownList list = new DropDownList();
            list.ID = "FoodUnitList";

            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Food_Units_Get", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.Parameters.Add("@Food_Key", SqlDbType.Int);
                    command.Parameters["@Food_Key"].Value = context.Request.Params["FoodKey"];
                    conn.Open();
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    list.DataSource = reader;
                    list.DataTextField = "Unit_Name";
                    list.DataValueField = "Unit_Key";
                    list.DataBind();
                    conn.Close();
                    using (var command2 = new SqlCommand("dbo.Food_Unit_Get", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        command2.Parameters.Add("@Food_Key", SqlDbType.Int);
                        command2.Parameters["@Food_Key"].Value = context.Request.Params["FoodKey"];
                        // Create parameter with Direction as Output (and correct name and type)
                        SqlParameter outputIdParam = new SqlParameter("@Unit_Key", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.Add(outputIdParam);
                        command2.ExecuteNonQuery();
                        list.SelectedValue = outputIdParam.Value.ToString();
                        conn.Close();
                    }
                }
            }

            StringWriter theStringWriter = new StringWriter();
            HtmlTextWriter theHtmlTextWriter = new HtmlTextWriter(theStringWriter);

            // Render the table row control into the writer
            list.RenderControl(theHtmlTextWriter);

            // Return the string via the StringWriter
            return theStringWriter.ToString();
        }

        private string NutritionProfileAdd(HttpContext context)
        {
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Nutrition_Profile_Add", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@User_Key", SqlDbType.Int);
                    command.Parameters["@User_Key"].Value = "1"; //Hardcoded to my User_Key
                    command.Parameters.Add("@Food_Key", SqlDbType.Int);
                    command.Parameters["@Food_Key"].Value = context.Request.Params["FoodKey"];
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal);
                    command.Parameters["@Quantity"].Value = context.Request.Params["Quantity"];
                    command.Parameters.Add("@Unit_Key", SqlDbType.Decimal);
                    command.Parameters["@Unit_Key"].Value = context.Request.Params["UnitKey"];
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return "Food was successfully added to profile";
        }

        private string ProfileSearch(HttpContext context)
        {
            Table table = new Table();
            table.ID = "nutritionProfileTable";
            table.CssClass = "tablesorter";
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Nutrition_Profile_Get", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@User_Key", SqlDbType.Int);
                    command.Parameters["@User_Key"].Value = 1; //Hard-coded to my User_Key for now
                    var date = new DateTime();
                    if (DateTime.TryParse(context.Request.Params["AddDateFrom"], out date))
                    {
                        command.Parameters.Add("@Add_Date_From", SqlDbType.DateTime);
                        command.Parameters["@Add_Date_From"].Value = context.Request.Params["AddDateFrom"];
                    }

                    if (DateTime.TryParse(context.Request.Params["AddDateTo"], out date))
                    {
                        command.Parameters.Add("@Add_Date_To", SqlDbType.DateTime);
                        command.Parameters["@Add_Date_To"].Value = context.Request.Params["AddDateTo"];
                    }

                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows)
                    {
                        TableHeaderRow header = new TableHeaderRow();
                        header.Cells.Add(new TableHeaderCell { Text = "Food Name" });
                        header.Cells.Add(new TableHeaderCell { Text = "Quantity" });
                        header.Cells.Add(new TableHeaderCell { Text = "Unit" });
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
                            var link = new HyperLink();
                            link.Text = reader["Name"].ToString();
                            link.NavigateUrl = "NutritionProfileForm.aspx?Nutrition_History_Key=" + reader["Nutrition_History_Key"].ToString() + "&Add_Date_From=" + context.Request.Params["AddDateFrom"] + "&Add_Date_To=" + context.Request.Params["AddDateTo"];
                            var cell = new TableCell();
                            cell.Controls.Add(link);
                            tr.Cells.Add(cell);
                            tr.Cells.Add(new TableCell { Text = reader["Quantity"].ToString(), CssClass = "Number" });
                            tr.Cells.Add(new TableCell { Text = reader["Unit_Name"].ToString() });
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

        private string UpdateEntry(HttpContext context)
        {
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Nutrition_Profile_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@User_Key", SqlDbType.Int);
                    command.Parameters["@User_Key"].Value = "1"; //Hardcoded to my User_Key
                    command.Parameters.Add("@Nutrition_History_Key", SqlDbType.Int);
                    command.Parameters["@Nutrition_History_Key"].Value = context.Request.Params["NutritionHistoryKey"];
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal);
                    command.Parameters["@Quantity"].Value = context.Request.Params["Quantity"];
                    command.Parameters.Add("@Unit_Key", SqlDbType.Decimal);
                    command.Parameters["@Unit_Key"].Value = context.Request.Params["UnitKey"];
                    var date = new DateTime();
                    if (DateTime.TryParse(context.Request.Params["AddDate"], out date))
                    {
                        command.Parameters.Add("@Add_Date", SqlDbType.DateTime);
                        command.Parameters["@Add_Date"].Value = context.Request.Params["AddDate"];
                    }
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return "Record was successfully updated";
        }

        private string DeleteEntry(HttpContext context)
        {
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.Nutrition_Profile_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@User_Key", SqlDbType.Int);
                    command.Parameters["@User_Key"].Value = "1"; //Hardcoded to my User_Key
                    command.Parameters.Add("@Nutrition_History_Key", SqlDbType.Int);
                    command.Parameters["@Nutrition_History_Key"].Value = context.Request.Params["NutritionHistoryKey"];
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return "Record was successfully deleted";
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