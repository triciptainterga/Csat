<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="csat.aspx.vb" Inherits="PtmCsat.csat" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Pertamina Customer Satisfaction</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
	<script src="js/index_act.js"></script>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }
        .container {
            width: 80%;
            margin: auto;
            overflow: hidden;
        }
        header {
            background: #50b3a2;
            color: #fff;
            padding-top: 20px;
            min-height: 50px;
            border-bottom: #0e29d5 3px solid;
        }
        header a {
            color: #fff;
            text-decoration: none;
            text-transform: uppercase;
            font-size: 16px;
        }
        header ul {
            padding: 0;
            list-style: none;
        }
        header li {
            float: left;
            display: inline;
            padding: 0 20px 0 20px;
        }
        header #branding {
            float: left;
        }
        header #branding h1 {
            margin: 0;
        }
        header nav {
            float: right;
            margin-top: 10px;
        }
        section {
            padding: 20px;
            margin: 20px 0;
            background: #fff;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        footer {
            background: #50b3a2;
            color: #fff;
            text-align: center;
            padding: 20px 0;
            margin-top: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
        }
        .form-group input, .form-group textarea, .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .form-group button {
            background: #50b3a2;
            color: #fff;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
        }
        .form-group button:hover {
            background: #45a091;
        }
		.image-container {
            display: flex;
            gap: 10px;
            justify-content: left;
            margin-left: 20px;
        }
        .image-container img {
            width: 64px;
            height: 64px;
            border: 4px solid transparent;
            cursor: pointer;
            transition: transform 0.3s ease, border-color 0.3s ease;
        }
        .image-container img:hover {
            transform: scale(1.1);
        }
        .image-container img.selected {
            border-color: #50b3a2; /* Border color when selected */
        }
        /* CSS untuk overlay */
        .overlay {
            display: none; /* Mulai dengan disembunyikan */
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Warna hitam dengan transparansi */
            z-index: 9999; /* Pastikan di atas elemen lain */
            text-align: center;
            padding-top: 20%;
            color: white;
            font-size: 24px;
        }
    </style>
	<script>
        function selectImage(img) {
            // Remove 'selected' class from all images
            var images = document.querySelectorAll('.image-container img');
            images.forEach(function(image) {
                image.classList.remove('selected');
            });
            
            // Add 'selected' class to the clicked image
            img.classList.add('selected');
            
            // Get the value of the selected image
            var selectedValue = img.getAttribute('data-value');
            console.log('Selected Image Value:', selectedValue);
			$("#resultCSAT").val(selectedValue);
        }
    </script>
	<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
<body>
    <div class="overlay">Processing...</div>
    <header>
        <div class="container">
            <div id="branding">
                <h1>Sibernet Customer Satisfaction</h1>
            </div>
           
        </div>
    </header>

    <section id="survey" class="container">
        <h2>Customer Satisfaction Survey</h2>
        <form runat="server">
		<input type="hidden" id="resultCSAT" name="resultCSAT" >
        <asp:HiddenField  ID="UniqueID" runat="server"></asp:HiddenField>

            <div class="form-group">
                <label for="name">Ticket Number:</label>
                <input type="text" id="ticketnumber" name="ticketnumber" readonly>
            </div>
            <div class="form-group">
                <label for="Channel">Channel:</label>
                <input type="text" id="channelname" name="channelname" readonly>
            </div>
            <!--<div class="form-group">
                <label for="satisfaction">Satisfaction Level:</label>
                <select id="satisfaction" name="satisfaction" required>
                    
                    <option value="satisfied">Satisfied</option>
                    <option value="neutral">Neutral</option>
                    <option value="dissatisfied">Dissatisfied</option>
                   
                </select>
            </div>-->
			<div class="form-group">
			<label for="satisfaction">Satisfaction Level:</label>
				<!--<div class="row">
					
					
					<div class="col-xs-4 col-sm-4 col-md-4">
						<div class="image-container">
						<img src="images/puas.png" width="64" onclick="selectImage('Satisfied')">
						</div>
					</div>
					<div class="col-xs-4 col-sm-4 col-md-4">
						<div class="image-container">
						<img src="images/biasa.png" width="64" onclick="selectImage('Neutral')">
						</div>
					</div>
					<div class="col-xs-4 col-sm-4 col-md-4">
						<div class="image-container">
						<img src="images/kecewa.png" width="64" onclick="selectImage('Dissatisfied')">
						</div>
					</div>
				</div>-->
				
				<div class="row">
					<div class="col-12">
						<div class="image-container">
							<img src="images/puas.png" alt="Sample Image 1" data-value="Satisfied" onclick="selectImage(this)">
							<img src="images/biasa.png" alt="Sample Image 2" data-value="Neutral" onclick="selectImage(this)">
							<img src="images/kecewa.png" alt="Sample Image 3" data-value="Dissatisfied" onclick="selectImage(this)">
						</div>
					</div>
				</div>
            </div>
            <div class="form-group">
                <label for="comments">Comments:</label>
                <textarea id="comments" name="comments" rows="4" required></textarea>
            </div>
            <div class="form-group">
                <a href="#" class="btn btn-success" id="startProcess">Submit</a>
            </div>
        </form>
    </section>

    <!--<section id="testimonials" class="container">
        <h2>Customer Testimonials</h2>
        <p>"Excellent service, very satisfied!" - John Doe</p>
        <p>"Good quality products, will buy again." - Jane Smith</p>
        <p>"Friendly customer support and quick delivery." - Alan Brown</p>
    </section>-->

    <section id="contact" class="container">
        <h2>Contact Us</h2>
        <p>If you have any questions or concerns, please contact us at:</p>
        <p>Email: info@Pertaina.co.id</p>
       
    </section>

    <footer>
        <p>Pertamina &copy; 2024</p>
    </footer>
</body>
</html>

