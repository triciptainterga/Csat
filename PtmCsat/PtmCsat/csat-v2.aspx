<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="PtmCsat.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Survey Kepuasan Pelanggan</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <style>
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
    </style>
</head>

<body>
    <div class="main-content d-flex flex-column justify-content-center py-4">
        <div class="container">
            <h1 class="text-white text-center mb-4 h2" style="font-weight: bold;">Survey Kepuasan Pelanggan</h1>

            <form class="mx-auto" style="max-width: 500px;">
                <div
                    class="bg-white border rounded d-flex justify-content-between align-items-center px-4 py-4 mb-4 star-rating">
                    <i class="fas fa-star fs-1"></i>
                    <i class="fas fa-star fs-1"></i>
                    <i class="fas fa-star fs-1"></i>
                    <i class="fas fa-star fs-1"></i>
                    <i class="fas fa-star fs-1"></i>
                </div>

                <div class="mb-3">
                    <label for="comment" class="form-label text-white small">Komentar:</label>
                    <textarea class="form-control" id="comment" rows="4"
                        placeholder="Tulis komentar Anda..."></textarea>
                </div>

                <div class="text-start">
                    <button type="submit" class="btn btn-primary btn-sm px-4">Submit</button>
                </div>

            </form>

            <!-- Kontak & Logo -->
            <div class="contact-info mt-2" style="max-width: 500px; margin: 0 auto;">
                <div class="row align-items-start">
                    <!-- Kolom kiri: teks kontak -->
                    <div class="col-8">
                        <p class="fw-semibold">Hubungi Kami:</p>
                        <p><span class="d-inline-block" style="width: 80px;">Call</span>: 135</p>
                        <p>
                            <span class="d-inline-block" style="width: 80px;">Email</span>:
                            <a href="mailto:cc135@pertamina.com"
                                class="text-white text-decoration-underline">cc135@pertamina.com</a>
                        </p>
                        <p><span class="d-inline-block" style="width: 80px;">IG</span>: Pertamina.135</p>
                        <p><span class="d-inline-block" style="width: 80px;">Facebook</span>: Pertamina Call Center 135
                        </p>
                        <p><span class="d-inline-block" style="width: 80px;">X</span>: Pertamina135</p>
                    </div>

                    <!-- Kolom kanan: logo -->
                    <div class="col-4 text-end">
                        <img src="images/logo-cc135.png" alt="Logo Pertamina"
                             style="width: 120px; height: 120px; object-fit: cover; margin-top: 20px;">
                    </div>
                </div>
            </div>

        </div>
    </div>

    <!-- Footer sederhana -->
    <footer class="container">
        <p class="text-center mt-3 mb-0 text-white">&copy; 2025 PT Pertamina (Persero). All rights reserved.</p>
    </footer>

    <!-- Bootstrap JS (optional) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
