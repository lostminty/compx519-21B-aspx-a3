<?php
	/*store the parameters from the URL in local variables */
    $reviewer=$_GET['person'];
    $text=$_GET['txt'];
	$prodCode=$_GET['prod'];
	/*connect to the db */
    $con = mysqli_connect("localhost", "root", "", "test");
	/*insert query to insert the review in the db */
    $query = "insert into `reviews` values('$reviewer', '$text', '$prodCode')";
    $result = mysqli_query($con, $query);

	if($result)
		//If the query is successful redirect the user back to the page */
		header("Location: items.php?id=$prodCode");
	else
		//If not successful display error
    {
      echo "The user input ". $text. " could not be parsed <br>";
		    die("Error in database query");
    }

    mysqli_close($con);

?>
