$(function () {

    var divliste = [];

    $("div[data-notid]").each(function (i, e) {
        divliste.push($(e).data("notid"));
    });

    $.ajax({
        method: "POST",
        url: "/Not/LikeMakale",
        data: { dizi: divliste }
    }).done(function (data) {

        if (data.sonuc != null && data.sonuc.length > 0) {

            for (var i = 0; i < data.sonuc.length; i++) {

                var id = data.sonuc[i];
                var likenot = $("div[data-notid=" + id + "]");
                var btn = likenot.find("button[data-liked]");
                btn.data("liked", true);

                var span = btn.children().first();
                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");
            }
        }

    }).fail(function () {

    });


    $("button[data-liked]").click(function () {
        var btn = $(this)
        var liked = btn.data("liked");
        var notid = btn.data("notid");
        var spankalp = btn.find("span.likekalp");
        var spanlikesayisi = btn.find("span.likesayisi");

        $.ajax({
            method: "POST",
            url: "/Not/LikeDurumuUpdate",
            data: { "notid": notid, "like": !liked }
        }).done(function (data) {

            if (data.hata) {
                alert("Beğeni işlemi gerçekleşemedi.");
            }
            else {

                liked = !liked
                btn.data("liked", liked);
                spanlikesayisi.text(data.sonuc);

                spankalp.removeClass("glyphicon-heart-empty");
                spankalp.removeClass("glyphicon-heart");

                if (liked) {
                    spankalp.addClass("glyphicon-heart");
                }
                else {
                    spankalp.addClass("glyphicon-heart-empty");
                }



            }

        }).fail(function () {

        });





    });






});