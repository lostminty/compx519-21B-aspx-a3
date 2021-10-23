using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace COMPX519_ASPWebForm
{
    public partial class items : System.Web.UI.Page
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
                        string prodID = Request.QueryString["id"];  //Get the productCode from the URL

                        /* The query for product's details */
                        using (MySqlCommand cmd = new MySqlCommand("select * from `products` where productCode='" + prodID + "';"))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    string productsTable = "";
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        prod.Value = row["productCode"].ToString();
                                        productsTable += "<div class=\"boxes\" id=\"A\">";  //creating the first box for product
                                        productsTable += "<a href=\"items.aspx?id=";
                                        productsTable += row["productCode"] + "\">";
                                        productsTable += row["productName"] + "	";
                                        productsTable += "</a> </div>";
                                        productsTable += "<div class=\"boxes\" id=\"B\">";  //second box in the row for product's description
                                        productsTable += row["productDescription"] + " </div>";
                                        productsTable += "<div class=\"boxes\" id=\"C\">";  //third box in the row for price
                                        productsTable += "NZD" + row["MSRP"] + " </div>";
                                    }
                                    ProductsList.Text = productsTable;
                                }
                            }
                        }

                        /* The query for product's reviews */
                        using (MySqlCommand cmd = new MySqlCommand("select * from reviews where productCode='" + prodID + "';"))
                        {
                            using (MySqlDataAdapter sda = new MySqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    string productsReview = "";

                                    /* loop through each review */
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        productsReview += "<b>";
                                        productsReview += row["name"];                  //Reviewer
                                        productsReview += "</b> <i> says </i>";
                                        productsReview += row["review"] + " </div>";    //The review
                                        productsReview += "<br><hr>";
                                    }
                                    ProductsReview.Text = productsReview;
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


        /*
         * This function is executed once the review is submit 
         * The function add reviewe to the DB
         * Then query all reviews, include the new review, to display 
         */
        protected void Button2_Click(object sender, System.EventArgs e)
        {
            /* C# code block to connect with the database */
            string constr = "server=localhost;port=3306;user=admin;password=9Seirarb6;database=mysql;";
            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string prodID = prod.Value;
                    /* The query for inserting new review */
                    using (MySqlCommand cmd = new MySqlCommand("insert into `reviews` values('" + tb2.Text + "', '"+ txt.Text +"', '"+ prodID + "')"))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();  // execute the insert query

                        tb2.Text = "";          // clear reviewer textbox
                        txt.Text = "";          // clear review textbox
                    }

                    /* The query for product's reviews */
                    using (MySqlCommand cmd = new MySqlCommand("select * from reviews where productCode='" + prodID + "';"))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                        
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                string productsReview = "";

                                /* loop through each review */
                                foreach (DataRow row in dt.Rows)
                                {
                                    productsReview += "<b>";
                                    productsReview += row["name"];                  //Reviewer
                                    productsReview += "</b> <i> says </i>";
                                    productsReview += row["review"] + " </div>";    //The review
                                    productsReview += "<br><hr>";
                                }
                                ProductsReview.Text = productsReview;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProductsReview.Text = ex.Message + " " + tb2.Text + " " + txt.Text; //print the error in case of error
            }
        }
    }
}
