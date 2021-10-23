using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace COMPX519_ASPWebForm
{

    public partial class index : System.Web.UI.Page
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
                        /* C# code to formulate the query and execute it on the db */
                        using (MySqlCommand cmd = new MySqlCommand("select productLine, textDescription from `productlines`"))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    String productLinesTable = "";

                                    /* loop through all results */
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        productLinesTable += "<div class=\"boxes\" id=\"A\">";  //creating the first box for product line
                                        productLinesTable += "<a href=\"pages.aspx?prodLine=";  //creating the hyperlink
                                        productLinesTable += row["productLine"] + "\">"; 
                                        productLinesTable += row["productLine"] + "	";
                                        productLinesTable += "</a> </div>"; 
                                        productLinesTable += "<div class=\"boxes\" id=\"B\">";  //second box in the row for description
                                        productLinesTable += row["textDescription"] + " </div>";
                                    }
                                    productLineList.Text = productLinesTable;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    productLineList.Text = ex.Message; //print the error in case of error
                }
            }
        }
    }
}
