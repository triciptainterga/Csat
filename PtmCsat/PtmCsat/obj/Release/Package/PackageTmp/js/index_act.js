$(document).ready(function () {
    csatInitiate(getParameterByName("ticid"), getParameterByName("kanal"));
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

    function csatInitiate(ticid,kanal) {
        showOverlay();
        let vTicketNumber = $("#ticketnumber").val();
        let vChannel = $("#channelname").val();
       

        var form_data = JSON.stringify({ TicketNumber: ticid, Channel: kanal});
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
                for (i = 0; i < json.length; i++) {
                    console.log(json[i].UniqueID);
                    $("#UniqueID").val(json[i].UniqueID);
                    $("#channelname").val(kanal);
                    $("#ticketnumber").val(ticid);

                    if (json[i].Result == "0") {
                        window.location = "selesai.html";
                    } else if(json[i].Result == "1"){
                        window.location = "multiple.html";
                    } else if (json[i].Result == "2") {
                        window.location = "expired.html";
                    }

                }

               

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
            showOverlay();
            console.log("hehe POST CSAT");
            
            let vUniqueID = $("#UniqueID").val();
            let vTicketNumber = $("#ticketnumber").val();
            let vChannel = $("#channelname").val();
            let vResultCSAT = $("#resultCSAT").val();
            let vIsiKeterangan = $("#comments").val();

        var form_data = JSON.stringify({ UniqueID: vUniqueID, TicketNumber: vTicketNumber, Channel: vChannel, ResultCSAT: vResultCSAT, IsiKeterangan: vIsiKeterangan, UserName: 'Admin'});
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
                    } else if(json[0].Result == "1"){
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

