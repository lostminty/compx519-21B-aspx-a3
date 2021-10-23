 <!-- HTMLL code that includes the css stylesheet and creates the form with the search textbox and button -->

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
        <form class='inline-block-center' action="results.php" method="GET">
            <input id='tb1' type="text" name="keywords">
            <input id='but1' type="submit" value="Search">
            <br><br>
        </form>
        <div class="outermost_div">
			<?php
				/* PHP code to connect with the database, formulate the query and execute it on the db */
				$prodCode = $_GET["id"]; //Get the productCode from the URL
				$con = mysqli_connect("localhost", "root", "", "test");
				$query = "select * from `products` where productCode='$prodCode'";
				$result = mysqli_query($con, $query);
				
				if($result !=FALSE) //If some result is returned
				{
					/*Wrap datafrom database in HTML tags for display */
					while($row = mysqli_fetch_assoc($result)) {
						echo '<div class="boxes" id="A">';
						echo '<a href="items.php?id=';
						echo $row['productCode']." \">";
						echo $row['productName'] . "	";
						echo '</a>';
						echo '</div>';
						echo '<div class="boxes" id="B">';
						echo $row['productDescription'] . "	";
						echo '</div>';
						echo '<div class="boxes" id="C">';
						echo "NZD ".$row['MSRP'] . "	";
						echo '</div>';
					}
				}
				else
					echo mysql_error(); //print the error in case of error
				
			?>
	    </div>
		<!-- HTML for the feedback form -->
		<!-- The form sends data to review.php -->
		<form action="review.php" method="GET">
			<h2> leave this product a review </h2>
			<div>
			<input type='text' id='tb2' name='person'> <br>
			<textarea id="txt" name="txt">
			</textarea>
			<!-- Hidden field that doesn't get displayed but holds the product code to send to review.php -->
			<input type='hidden' name='prod' value='<?php echo $prodCode ?>'>
			<input id='but2' type='submit' value='submit'>
			</div>
		</form>
		<!-- Once everything has been displayedon the HTML page. Make a database connection and get all the reviews for this product -->
		<?php
			/* Create the query and run it on the db */
			$con = mysqli_connect("localhost", "root", "", "test");
			$query = "select * from reviews where productCode='$prodCode'";
			$result = mysqli_query($con, $query);
			/*display the reviews */
			while($row = mysqli_fetch_assoc($result)) 
			{
				echo "<b>".$row['name'] . "</b> <i> says </i>". $row['review']. " <br><hr>";
			}
			mysqli_close($con);
		   
		   
		?>
            </body>
</html>
