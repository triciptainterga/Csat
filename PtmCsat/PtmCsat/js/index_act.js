$(document).ready(function () {
	  $("#resultCSAT").val('Sangat Puas');
			
        // === SET NILAI DARI URL ===
    if (getParameterByName("ticid") == null || getParameterByName("kanal") == null )
            window.location = "Error.html";

    // Cek kanal yang valid
    const allowedChannels = ["Email", "Voice", "Live Chat"];
    let status = allowedChannels.includes(getParameterByName("kanal"));

    // Jika kanal tidak valid, redirect ke Error.html
    if (!status) {
        window.location = "Error.html";
    }


    
        $("#channelname").val(getParameterByName("kanal"));
        $("#ticketnumber").val(getParameterByName("ticid"));

        // === GET CONTACT DARI ASMX ===
        $.ajax({
            type: "POST",
            url: "asmx/CsatService.asmx/GetCsatContent",
            data: JSON.stringify({ 'TicketNumber': getParameterByName("ticid") }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var json = JSON.parse(data.d);
                if (json.length > 0) {
                    var _contact = `
                        <p>${json[0].Header}</p>
                        <p>${json[0].Contact_As}</p>`;
                    $('#contact').html(_contact);

                    if (json[0].TenantID == '4') {
                        $('#Ticket').hide();
                        $('#Kanal').hide();
                    } else {
                        $('#Ticket').show();
                        $('#Kanal').show();
                    }
                } else {
                    Swal.fire({
                        text: 'No Ticket tidak terdaftar.',
                        icon: 'warning'
                    }).then(() => {
                        window.location = "Error.html";
                    });
                }
            },
            error: function (xhr, status, err) {
                hideOverlay();
                console.log(xhr.responseText);
            }
        });

        // === INIT CSAT ===
        csatInitiate(getParameterByName("ticid"), getParameterByName("kanal"));

        // === LOGIKA RATING BINTANG ===
$('.star-item').on('click', function () {
    var selectedValue = $(this).data('value');

    // Mapping angka ke teks rating
    var ratingText = "";
    switch (selectedValue) {
        case 1: ratingText = "Sangat Tidak Puas"; break;
        case 2: ratingText = "Tidak Puas"; break;
        case 3: ratingText = "Cukup Puas"; break;
        case 4: ratingText = "Puas"; break;
        case 5: ratingText = "Sangat Puas"; break;
    }

    // Set ke hidden input dan result
    $('#ratingValue').val(selectedValue);   // angka
    $('#resultCSAT').val(ratingText);       // teks yang akan dikirim ke server
    $('#ratingResult').text('Rating Anda: ' + ratingText);

    // Pewarnaan bintang
    $('.star-item').each(function () {
        var starValue = $(this).data('value');
        if (starValue <= selectedValue) {
            $(this).find('i').css('color', '#FFD700'); // Kuning
        } else {
            $(this).find('i').css('color', '#ccc'); // Abu-abu
        }
    });
});



        // === SUBMIT ===
    $("#startProcess").click(function (event) {

        event.preventDefault();
            let selectedRating = $("#ratingResult").html();
            let description = selectedRating.split("-")[1]?.trim() || "";

            let vUniqueID = $("#UniqueID").val();
            let vTicketNumber = $("#ticketnumber").val();
            let vChannel = $("#channelname").val();
            let vResultCSAT = $("#resultCSAT").val();

            let vIsiKeterangan = $("#comments").val();
            let UserName = 'Admin';

            if (!vResultCSAT) {
                Swal.fire({ text: 'Mohon isi form survey terlebih dahulu', icon: 'warning' });
                return false;
            }

            if (!vIsiKeterangan) {
                Swal.fire({ text: 'Mohon isi komentar terlebih dahulu', icon: 'warning' });
                return false;
            }

            showOverlay();
            var form_data = JSON.stringify({
                UniqueID: vUniqueID,
                TicketNumber: vTicketNumber,
                Channel: vChannel,
                ResultCSAT: vResultCSAT,
                IsiKeterangan: vIsiKeterangan,
                UserName: UserName
            });

            $.ajax({
                type: "POST",
                url: "asmx/CsatService.asmx/ws_csat_create",
                data: form_data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    hideOverlay();
                    var json = JSON.parse(data.d);
                    if (json[0].Result == "1") {
                        window.location = "multiple.html";
                    } else if (json[0].Result == "2") {
                        window.location = "expired.html";
                    } else {
                        window.location = "selesai.html";
                    }
                },
                error: function (xhr, status, err) {
                    hideOverlay();
                    console.log(xhr.responseText);
                }
            });
        });

        // === Fungsi bantu ===
        function showOverlay() {
            $(".overlay").fadeIn();
        }

        function hideOverlay() {
            $(".overlay").fadeOut();
        }

        function getParameterByName(name, url = window.location.href) {
            name = name.replace(/[\[\]]/g, '\\$&');
            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)');
            var results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, ' '));
        }

        function csatInitiate(ticid, kanal) {
            showOverlay();
            var form_data = JSON.stringify({ TicketNumber: ticid, Channel: kanal });

            $.ajax({
                type: "POST",
                url: "asmx/CsatService.asmx/ws_csat_initiate",
                data: form_data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    hideOverlay();
                    var json = JSON.parse(data.d);
                    for (let i = 0; i < json.length; i++) {
                        $("#UniqueID").val(json[i].UniqueID);
                        $("#channelname").val(kanal);
                        $("#ticketnumber").val(ticid);

                        if (json[i].Result == "1")
                            window.location = "multiple.html";
                    }
                },
                error: function (xhr, status, err) {
                    hideOverlay();
                    console.log(xhr.responseText);
                }
            });
        }
    });