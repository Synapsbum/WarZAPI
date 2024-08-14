<?php
if(isset($_POST["key"]) && isset($_POST["username"]) && isset($_POST["filename"]) && $_POST["key"]=="RzHUZkDAWNEHQh19OjQ8qcuBYr8bkQ6bIFkbODZaxT0K857gSr")
{
	$upload_dir = "C:\\inetpub\\wwwroot\\admincp\\html\\reports\\ScreenShots\\";

	$username = $_POST["username"];
	$filename = $_POST["filename"];

	$allowedExts = array("jpeg", "txt");
	$temp = explode(".", $_FILES["upload_file"]["name"]);
	$extension = end($temp);
	if ((($_FILES["upload_file"]["type"] == "image/jpeg")
	|| ($_FILES["upload_file"]["type"] == "text/plain"))
	&& ($_FILES["upload_file"]["size"] < 2000000000)
	&& in_array($extension, $allowedExts))
	{
		if ($_FILES["upload_file"]["error"] > 0)
		{
			echo "Return Code: " . $_FILES["upload_file"]["error"] . "<br>";
		}
		else
		{			
			switch($_FILES["upload_file"]["type"])
			{
				case "image/jpeg":
					$save_ext = ".jpeg";
					break;
				case "text/plain":
					$save_ext = ".txt";
					break;
				default:
					$save_ext = ".miageciez";
					break;
			}

			if (!is_dir($upload_dir.$username))
			{
					mkdir($upload_dir.$username);         
			}

			$upload_dir .= $username."/";

			move_uploaded_file($_FILES["upload_file"]["tmp_name"],
			$upload_dir .$filename.$save_ext);
		}
	}
	else
	{
		echo "Invalid file";
	}
}
?>