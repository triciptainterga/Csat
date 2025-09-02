<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="csat.aspx.vb" Inherits="PtmCsat.csat" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Customer Satisfaction Survey</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="js/index_act.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <style>
        label, h1 {
            color: white !important;
            font-weight: bold;
        }

        body {
            background-image: url("images/background biru.jpg");
            background-size: cover;
            /* Gambar mengisi penuh */
            background-repeat: no-repeat;
            /* Tidak diulang */
            background-position: center;
            /* Posisi tengah */
            background-attachment: fixed;
            /* Tetap saat scroll */
            font-family: 'Inter', sans-serif;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .main-content {
            flex-grow: 1;
        }

        .star-rating i {
            font-size: 1.5rem;
            color: #ffc107;
        }

        .contact-info {
            color: white;
            font-size: 0.875rem;
        }

            .contact-info p {
                margin-bottom: 0.3rem;
            }

        .bg-white {
            background-color: #fff !important;
        }

        .star-rating-container {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
            padding: 20px 30px;
            display: flex;
            justify-content: center;
            gap: 20px;
            margin-bottom: 20px;
        }

            .star-rating-container i {
                font-size: 4.5rem; /* Besar: 40px */
                color: #FFD700; /* Warna kuning terang */
            }

        .star-label {
            margin-top: 18px;
            font-size: 1.2rem;
            color: blue;
            text-align: center;
            line-height: 1.1;
            word-wrap: break-word;
        }

        .rating-result {
            margin-top: 10px;
            font-weight: bold;
            color: white;
            text-align: center;
        }
    </style>
    <script>
        function selectImage(img) {
            // Remove 'selected' class from all images
            var images = document.querySelectorAll('.image-container img');
            images.forEach(function (image) {
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
    <%--<header>
        <div class="container">
            <div id="branding">
                <h1>Pertamina Customer Satisfaction</h1>
            </div>

        </div>
    </header>--%>


    <div class="main-content d-flex flex-column justify-content-center py-8">
        <div class="container">
            <h1 class="text-white text-center mb-4 h2" style="font-weight: bold;margin-top:15%">Survey Kepuasan Pelanggan</h1>
            <form class="mx-auto" style="max-width: 500px; margin-left: 25%" runat="server">
                <!-- Hidden Fields -->
                <input type="hidden" id="resultCSAT" name="resultCSAT" />
                <asp:HiddenField ID="UniqueID" runat="server" />

                <!-- Ticket Number -->
                <div id="Ticket" class="form-group" style="display: none;">
                    <label for="ticketnumber" class="fw-bold">Ticket Number:</label>
                    <input type="text"
                        id="ticketnumber"
                        name="ticketnumber"
                        class="form-control"
                        readonly />
                </div>


                <!-- Channel Name -->
                <div id="Kanal" class="form-group" style="display: none;">
                    <label for="channelname">Channel:</label>
                    <input type="text" id="channelname" name="channelname" class="form-control" readonly />
                </div>

                <!-- Star Rating -->
                        <div class="form-group mb-4">
                            <label class="form-label text-white fw-bold">Rating Kepuasan:</label>
                            <div class="star-rating-container" id="starRating">
                                <div class="star-item" data-value="1">
                                    <i class="fas fa-star"></i>
                                    <div class="star-label">Sangat<br>
                                        Tidak Puas</div>
                                </div>
                                <div class="star-item" data-value="2">
                                    <i class="fas fa-star"></i>
                                    <div class="star-label">Tidak<br>
                                        Puas</div>
                                </div>
                                <div class="star-item" data-value="3">
                                    <i class="fas fa-star"></i>
                                    <div class="star-label">Cukup<br>
                                        Puas</div>
                                </div>
                                <div class="star-item" data-value="4">
                                    <i class="fas fa-star"></i>
                                    <div class="star-label">Puas</div>
                                </div>
                                <div class="star-item" data-value="5">
                                    <i class="fas fa-star"></i>
                                    <div class="star-label">Sangat<br>
                                        Puas</div>
                                </div>
                            </div>
                            <input type="hidden" id="ratingValue" value="0">
                            <div class="rating-result" id="ratingResult"></div>
                        </div>



                <!-- Comment -->
                <div class="form-group">
                    <label for="comments" class="form-label">Komentar:</label>
                    <textarea class="form-control" id="comments" name="comments" rows="4" placeholder="Tulis komentar Anda..." required></textarea>
                </div>

                <!-- Submit Button -->
                <div class="form-group text-start">
                    <button type="submit" class="btn btn-primary btn-sm px-4" id="startProcess">Submit</button>
                </div>

                <div class="row">
                    <!-- Kolom kiri: teks kontak -->
                    <div class="col-md-9 col-12 text-white">
                        <div id="contact" class="contact-info mt-4" style="max-width: 700px; margin: 0 auto;">
                        </div>
                    </div>

                    <!-- Kolom kanan: logo -->
                    <div class="col-md-3 col-12 text-md-end text-center">
                        <img src="images/logo-cc135.png" alt="Logo Pertamina"
                            style="width: 120px; height: 120px; object-fit: contain;">
                    </div>
                </div>
                 </form>
        </div>
    
    
    </div>

   


     

   
   

</body>
</html>

