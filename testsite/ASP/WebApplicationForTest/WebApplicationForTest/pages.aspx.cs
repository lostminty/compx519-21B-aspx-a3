using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace COMPX519_ASPWebForm
{

    public partial class pages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                /* C# code block to connect with the database */
                try
                {
                    string constr = "server=localhost;port=3306;user=admin;password=9Seirarb6;database=mysql;";
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        string prodLine = Request.QueryString["prodLine"];  //Get product line from the URL
                        /* C# code to formulate the query and execute it on the db */
                        using (MySqlCommand cmd = new MySqlCommand("select * from `products` where productLine=\""+prodLine+"\";"))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    string productsTable = "";

                                    /* loop through all results */
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        productsTable += "<div class=\"boxes\" id=\"A\">";  //creating the first box for product
                                        productsTable += "<a href=\"items.aspx?id=";
                                        productsTable += row["productCode"] + "\">";
                                        productsTable += row["productName"] + "	";
                                        productsTable += "</a> </div>";
                                        productsTable += "<div class=\"boxes\" id=\"B\">";  //second box in the row for product line
                                        productsTable += row["productLine"] + " </div>";
                                        productsTable += "<div class=\"boxes\" id=\"C\">";  //third box in the row for price
                                        productsTable += "NZD" + row["MSRP"] + " </div>";
                                    }
                                    ProductsList.Text = productsTable;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProductsList.Text = ex.Message; //print the error in case of error
                }
            }
        }
    }
}
