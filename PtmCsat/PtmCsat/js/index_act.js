$(document).ready(function () {


    if (getParameterByName("ticid") == null)
        window.location = "Error.html";

    $("#channelname").val(getParameterByName("kanal"))
    $("#ticketnumber").val(getParameterByName("ticid"))

    $.ajax({
        type: "POST",
        url: "asmx/CsatService.asmx/GetCsatContent",
        data: JSON.stringify({ 'TicketNumber': getParameterByName("ticid") }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var json = JSON.parse(data.d);
            // alert(json)
            var i, x, result = "";
            console.log(json);

            if (json.length > 0) {
                for (i = 0; i < json.length; i++) {


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
                }
            } else {
                Swal.fire({
                    title: '',
                    text: 'No Ticket tidak terdaftar.',
                    icon: 'warning'
                }).then(() => {
                    window.location = "Error.html";
                });

            }
               



        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            hideOverlay();
            console.log(xmlHttpRequest.responseText);
            console.log(textStatus);
            console.log(errorThrown);
        }
    })

    csatInitiate(getParameterByName("ticid"), getParameterByName("kanal"));
    //fetch('contact.json')
    //    .then(response => response.json())
    //    .then(data => {
    //        var _contact = `
    //<h2>Contact Us</h2>
    //<p>If you have any questions or concerns, please contact us at:</p>
    //<p>Email: ${data.email}</p>
    //<p>Address: ${data.address}, ${data.city}, ${data.postal_code}</p>
    //<p>${data.country}</p>`;

    //        $('#contact').html(_contact);
    //    })
    //    .catch(error => console.error('Error:', error));





    const stars = document.querySelectorAll('#starRating i');
    const result = document.getElementById('ratingResult');

    const ratings = {
        1: "Sangat Tidak Puas",
        2: "Tidak Puas",
        3: "Cukup Puas",
        4: "Puas",
        5: "Sangat Puas"
    };

    stars.forEach((star, index) => {
        star.addEventListener('click', () => {
            const value = index + 1;

            // Reset warna bintang
            stars.forEach(s => s.classList.remove('selected'));

            // Tambahkan warna pada bintang yang diklik dan sebelumnya
            for (let i = 0; i < value; i++) {
                stars[i].classList.add('selected');
            }

            // Tampilkan hasil rating
            result.textContent = `${value} Bintang - ${ratings[value]}`;
        });
    });






    // Fungsi untuk menampilkan overlay
    function showOverlay() {
        $(".overlay").fadeIn();
    }




    // Fungsi untuk menyembunyikan overlay
    function hideOverlay() {
        $(".overlay").fadeOut();
    }

    function getParameterByName(name, url = window.location.href) {
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }

    function csatInitiate(ticid, kanal) {
        showOverlay();
        let vTicketNumber = $("#ticketnumber").val();
        let vChannel = $("#channelname").val();


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
                var i, x, result = "";
                console.log(json);
                //if (json.length > 0) {
                for (i = 0; i < json.length; i++) {
                    console.log(json[i].UniqueID);
                    $("#UniqueID").val(json[i].UniqueID);
                    $("#channelname").val(kanal);
                    $("#ticketnumber").val(json[i].TicketNumber);

                    if (json[i].Result == "1")
                        window.location = "multiple.html";
                    

                    //} else if(json[i].Result == "1"){
                    //    window.location = "multiple.html";
                    //} else if (json[i].Result == "2") {
                    //    window.location = "expired.html";
                    //}

                }
                // } else {
                //  window.location = "Error.html";
                //}



            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                hideOverlay();
                console.log(xmlHttpRequest.responseText);
                console.log(textStatus);
                console.log(errorThrown);
            }
        })

    }



    $("#startProcess").click(function () {


        let selectedRating = $("#ratingResult").html();
        let description = selectedRating.split("-")[1].trim();
       // alert(description);
        console.log("POST CSAT");

        let vUniqueID = $("#UniqueID").val();
        //alert(vUniqueID);
        let vTicketNumber = $("#ticketnumber").val();
        let vChannel = $("#channelname").val();
        let vResultCSAT = $("#resultCSAT").val();
        let vIsiKeterangan = $("#comments").val();
        let UserName = 'Admin';



        if (vResultCSAT == null || vResultCSAT == "") {

            Swal.fire({
                title: '',
                text: 'Mohon dapat mengisi terlebih dahulu form survey berikut',
                icon: 'warning', // You can use 'success', 'error', 'info', 'question'

            });
            return false; // Prevent the form from submitting
        }

        if (vIsiKeterangan == null || vIsiKeterangan == "") {
            Swal.fire({
                title: '',
                text: 'Mohon dapat mengisi terlebih dahulu form survey berikut	',
                icon: 'warning', // You can use 'success', 'error', 'info', 'question'

            });
            return false; // Prevent the form from submitting

        }
        showOverlay();
        var form_data = JSON.stringify({ UniqueID: vUniqueID, TicketNumber: vTicketNumber, Channel: vChannel, ResultCSAT: vResultCSAT, IsiKeterangan: vIsiKeterangan, UserName: UserName });
        $.ajax({
            type: "POST",
            url: "asmx/CsatService.asmx/ws_csat_create",
            //url: "../../apps/asmx/CsatService.asmx/ws_csat_create",
            data: form_data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                hideOverlay();
                var json = JSON.parse(data.d);
                var i, x, result = "";
                console.log(json);


                if (json[0].Result == "0") {
                    window.location = "selesai.html";
                } else if (json[0].Result == "1") {
                    window.location = "multiple.html";
                } else if (json[0].Result == "2") {
                    window.location = "expired.html";
                }
                window.location = "selesai.html";

            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                hideOverlay();
                console.log(xmlHttpRequest.responseText);
                console.log(textStatus);
                console.log(errorThrown);
            }
        })
    });
});
