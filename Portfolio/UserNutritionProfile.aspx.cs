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
    public partial class UserNutritionProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserProfilePopulate();
            this.NutritionProfilePopulate();
        }

        private void UserProfilePopulate()
        {
            using (var conn = new SqlConnection("Server=(local);DataBase=Nutrition;Integrated Security=SSPI"))
            {
                using (var command = new SqlCommand("dbo.User_Profile_Get", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.Add("@User_Key", SqlDbType.Int);
                    command.Parameters["@User_Key"].Value = 1; //Hard-coded to my User_Key for now
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    this.userInformationTable.Style.Add(HtmlTextWriterStyle.Margin, "auto");
                    this.userInformationTable.Style.Add(HtmlTextWriterStyle.BorderWidth, "3px");
                    this.userInformationTable.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                    this.userInformationTable.Style.Add(HtmlTextWriterStyle.BorderColor, "#8dbdd8");
                    this.userInformationTable.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#CDCDCD");
                    if (reader.HasRows)
                    {
                        reader.Read();
                        TableHeaderRow header = new TableHeaderRow();
                        var cell = new TableHeaderCell { Text = "Nutrition Profile", ColumnSpan = 2, };
                        cell.Style.Add("border-bottom-width", "2px");
                        cell.Style.Add("border-bottom-style", "solid");
                        cell.Style.Add("border-bottom-color", "#8dbdd8");
                        header.Cells.Add(cell);
                        this.userInformationTable.Rows.Add(header);

                        TableRow tr = new TableRow();
                        tr.Cells.Add(new TableCell { Text = "Name:", CssClass = "FilterLabel" });
                        tr.Cells.Add(new TableCell { Text = reader["First_Name"].ToString() + " " + reader["Last_Name"].ToString(), CssClass = "FilterInput" });
                        this.userInformationTable.Rows.Add(tr);

                        tr = new TableRow();
                        tr.Cells.Add(new TableCell { Text = "Address:", CssClass = "FilterLabel" });
                        tr.Cells.Add(new TableCell { Text = reader["Address"].ToString(), CssClass = "FilterInput" });
                        this.userInformationTable.Rows.Add(tr);

                        tr = new TableRow();
                        tr.Cells.Add(new TableCell { Text = "City:", CssClass = "FilterLabel" });
                        tr.Cells.Add(new TableCell { Text = reader["City"].ToString(), CssClass = "FilterInput" });
                        this.userInformationTable.Rows.Add(tr);

                        tr = new TableRow();
                        tr.Cells.Add(new TableCell { Text = "State:", CssClass = "FilterLabel" });
                        tr.Cells.Add(new TableCell { Text = reader["State"].ToString(), CssClass = "FilterInput" });
                        this.userInformationTable.Rows.Add(tr);

                        tr = new TableRow();
                        tr.Cells.Add(new TableCell { Text = "Country:", CssClass = "FilterLabel" });
                        tr.Cells.Add(new TableCell { Text = reader["Country"].ToString(), CssClass = "FilterInput" });
                        this.userInformationTable.Rows.Add(tr);
                    }
                    conn.Close();
                }
            }
        }

        private void NutritionProfilePopulate()
        {
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
                    command.Parameters.Add("@Add_Date_From", SqlDbType.DateTime);
                    command.Parameters.Add("@Add_Date_To", SqlDbType.DateTime);
                    string addDateFrom = string.IsNullOrEmpty(Request.QueryString["Add_Date_From"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request.QueryString["Add_Date_From"];
                    this.Add_Date_From.Attributes.Add("val", addDateFrom);
                    command.Parameters["@Add_Date_From"].Value = addDateFrom;
                    string addDateTo = string.IsNullOrEmpty(Request.QueryString["Add_Date_To"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request.QueryString["Add_Date_To"];
                    this.Add_Date_To.Attributes.Add("val", addDateTo);
                    command.Parameters["@Add_Date_To"].Value = addDateTo;

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
                        this.nutritionProfileTable.Rows.Add(header);
                        TableRow tr;
                        while (reader.Read())
                        {
                            tr = new TableRow();
                            var link = new HyperLink();
                            link.Text = reader["Name"].ToString();
                            addDateFrom = string.IsNullOrEmpty(Request.QueryString["Add_Date_From"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request.QueryString["Add_Date_From"];
                            addDateTo = string.IsNullOrEmpty(Request.QueryString["Add_Date_To"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request.QueryString["Add_Date_To"];
                            link.NavigateUrl = "NutritionProfileForm.aspx?Nutrition_History_Key=" + reader["Nutrition_History_Key"].ToString() + "&Add_Date_From=" + addDateFrom + "&Add_Date_To=" + addDateTo;
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
                            this.nutritionProfileTable.Rows.Add(tr);
                        }
                    }
                    conn.Close();
                }
            }
        }
    }
}