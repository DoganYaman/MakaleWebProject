var notid = -1;

$('#myModal').on('show.bs.modal', function (e) {
    
    var btn = $(e.relatedTarget);
     notid = btn.data("notid");

    $('#myModal_body').load("/Yorum/YorumGoster/" + notid);

});

function yorumislem(btn, mode, yorumid, textid) {
    var editmod = $(btn).data("editmode");
 

    if (mode === "edit") {
        if (!editmod) {
            $(btn).removeClass("btn-warning");
            $(btn).addClass("btn-success");
            $(btn).find("span").removeClass("glyphicon-edit");
            $(btn).find("span").addClass("glyphicon-ok")
            $(btn).data("editmode", true);
            $(textid).addClass("editable");
            $(textid).attr("contenteditable", true);
        }
        else {
            $(btn).data("editmode", false);
            $(btn).removeClass("btn-success");
            $(btn).addClass("btn-warning"); $(btn).find("span").removeClass("glyphicon-ok");

            $(btn).find("span").addClass("glyphicon-edit");

            $(textid).removeClass("editable");
            $(textid).attr("contenteditable", false);


            var yorum = $(textid).text();

            $.ajax({
                method: "POST",
                url: "/Yorum/YorumUpdate/" + yorumid,
                data: { text: yorum }
            }).done(function (data) {
                if (data.sonuc) {
                    $('#myModal_body').load("/Yorum/YorumGoster/" + notid);
                }
                else {
                    alert("Yorum güncellenemedi.");
                }

            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            });

        }

    }
    else if (mode === "delete") {

        var onay = confirm("Yorum silinsin mi?")

        if (!onay)
            return false;

        $.ajax({
            method: "GET",
            url: "/Yorum/YorumSil/" + yorumid
        }).done(function (data) {
            if (data.sonuc) {
                $('#myModal_body').load("/Yorum/YorumGoster/" + notid);
            }
            else {
                alert("Yorum silinemedi.");
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });


    }
    else if (mode === "yorumekle") {
         
        var txt = $("#yorum_text").val();

        $.ajax({
            method: "POST",
            url: "/Yorum/YorumEkle/",
            data: { "YorumText": txt, "notid": notid }
        }).done(function (data) {
            if (data.sonuc) {

                $('#myModal_body').load("/Yorum/YorumGoster/" + notid);
            }
            else {
                alert("Yorum eklenemedi.")
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.")
        })
        //else if (mode === "yorumekle")
        //{
        //    console.log("deneme");
        //    var yorum = $("#yorum_text").val();

        //    $.ajax({
        //        method: "POST",
        //        url: "/Yorum/YorumEkle",
        //        data: { "notid": notid, "YorumText":yorum }
        //    }).done(function (data) {
        //        if (data.sonuc) {
        //            $('#myModal_body').load("/Yorum/YorumGoster/" + notid);

        //        }
        //        else {
        //            alert("Yorum eklenemedi.");
        //        }
        //    }).fail(function () {
        //        alert("Sunucu ile bağlantı kurulamadı.");
        //    });
        //}
    };

}
