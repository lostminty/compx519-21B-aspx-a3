 <!-- HTML code that includes the css stylesheet and creates the form with the search textbox and button -->
<!DOCTYPE html>
<html>
    <head>
        <meta charset='UTF-8'>
        <title>A One replicas</title>
        <meta charset="UTF-8">
        <link rel="stylesheet" href="gridwork-done.css">
    </head>
    <body>
        <h1> <div class='top'><a class='toptext' href="index.php">A-ONE REPLICAS </a> </div></h1>
        <form class='inline-block-center'action="results.php" method="GET">
            <input id='tb1' type="text" name="keywords">
            <input id='but1' type="submit" value="Search">
            <br><br>
        </form>
        <div class="outermost_div">
           
		<?php
			/* PHP code to connect with the database, formulate the query and execute it on the db */
			$con = mysqli_connect("localhost", "root", "", "test");
			$query = "select productLine, textDescription from `productlines`";
			$result = mysqli_query($con, $query);
			
			if($result !=FALSE) //if the query does not result in an error and produces output do this
			{
				/*Wrap the output of the query in html tags and print */
				while($row = mysqli_fetch_assoc($result)) {
					echo '<div class="boxes" id="A">'; //creating the first box for product line
					echo '<a href="pages.php?prodLine='; //creating the hyperlink
					echo $row['productLine']." \">";                          
					echo $row['productLine'] . "	";
					echo '</a>';
					echo '</div>';
					echo '<div class="boxes" id="B">';//second box in the row for description
					echo $row['textDescription'] . "	";
					echo '</div>';
				}
			}
			else
				echo mysql_error(); //print the error in case of error
			mysqli_close($con);
		?>
        </div>
    </body>
</html>
