var ticket = (function () {

    var index = 0;
    var transTime = 500;
    var animationLock = false;
    var submited = false;
    var ratingValueTrans = "";
    $(function () {
        ratingValueTrans = $("#ticket-rating-value").text();
        $('#ticket-category-selectmenu').selectmenu({width : "50vw"});
        $("#ticket-rating-slider").slider({
            range: "max",
            min: 0,
            max: 10,
            value: 5,
            step: 1,
            slide: function (event, ui) {
                $("#ticket-rating-value").text(ratingValueTrans + ": " + ui.value);
            }
        });
        $("#ticket-rating-value").text(ratingValueTrans + ": " + $("#ticket-rating-slider").slider("value"));
        $("#ticket-submit-button").button();

        $("#ticket-intro-title").autoTextSize(0.5, 0, null, false);
        $("#ticket-title-title").autoTextSize(0.4, 0, null, false);
        $("#ticket-title-textbox").autoTextSize(0.4, 0, $("#ticket-title-input"), false, {'minFontSize' : 12});
        $("#ticket-category-title").autoTextSize(0.4, 0, null, false);
        $(".ui-selectmenu-text").autoTextSize(0.5, 0, $("#ticket-category-input"), false);
        $("#ticket-comment-title").autoTextSize(0.4, 0, null, false);
        $("#ticket-comment-textarea").autoTextSize(0.7, 0, $("#ticket-comment-input"), false, { 'minFontSize': 12 });
        $("#ticket-rating-title").autoTextSize(0.3, 0, null, false);
        $("#ticket-rating-value").autoTextSize(0.3, 0, null, false);
        $("#ticket-contact-title").autoTextSize(0.18, 0, $("#ticket-contact-name-input"), false);
        $("#ticket-contact-name-title").autoTextSize(0.2, 0, null, false);
        $("#ticket-contact-email-title").autoTextSize(0.2, 0, null, false);
        $("#ticket-contact-phone-title").autoTextSize(0.2, 0, null, false);
        $("#ticket-contact-name-input").autoTextSize(0.2, 0, null, false);
        $("#ticket-contact-email-input").autoTextSize(0.2, 0, null, false);
        $("#ticket-contact-phone-input").autoTextSize(0.2, 0, null, false);
        $("#ticket-submit-button").autoTextSize(0.2, 0, null, false);

        $("#ticket-submit-button").on("click", function () {
            submit();
        });
        $("#ticket-category-selectmenu-button").on("click", function () {
            $(".ui-menu-item").autoTextSize(0.8, 0, $("#ticket-category-input"), false);
        });
        $("#ticket-container").on("swiperight", function () {
            preSlide();
        });
        $("#ticket-container").on("swipeleft", function () {
            nextSlide();
        });
        $("#ticket-arrow-left").css("opacity", 0).on("click", function () {
            preSlide();
        });
        $("#ticket-arrow-right").on("click", function () {
            nextSlide();
        });
        animate();
    });

    function nextSlide() {
        if (animationLock || submited) {
            return;
        }
        if (index < $(".ticket-subcontainer").length - 1) {
            index++;
            animate();
        }
    }

    function preSlide() {
        if (animationLock || submited) {
            return;
        }
        if (index > 0) {
            index--;
            animate();
        }
    }

    function animate() {
        animationLock = true;
        $("input, textarea").blur();
        $("#ticket-container").velocity({ left: "-" + (index * 100) + "vw" }, transTime, function () {
            animationLock = false;
        });
        if (index == 0) {
            $("#ticket-arrow-left").velocity({ opacity: 0 }, transTime);
        } else {
            $("#ticket-arrow-left").velocity({ opacity: 1 }, transTime);
        }
        if (index == $(".ticket-subcontainer").length - 1) {
            $("#ticket-arrow-right").velocity({ opacity: 0 }, transTime);
        } else {
            $("#ticket-arrow-right").velocity({ opacity: 1 }, transTime);
        }
    }

    var colorChangeTime = 250;

    function submit() {
        var title = $("#ticket-title-textbox").val();
        var categoryID = $("#ticket-category-selectmenu").val();
        var comment = $("#ticket-comment-textarea").val();
        var rating = $("#ticket-rating-slider").slider("value");
        var name = $("#ticket-contact-name-textbox").val();
        var email = $("#ticket-contact-email-textbox").val();
        var phone = $("#ticket-contact-phone-textbox").val();

        if (title == "") {
            index = 1;
            animate();
            $("#ticket-title-textbox").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#ffffff" }, 1000);
            return;
        }
        if (comment == "") {
            index = 3;
            animate();
            $("#ticket-comment-textarea").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#ffffff" }, 1000);
            return;
        }
        if (submited) {
            return;
        }
        submited = true;
        var xmlhttp = new XMLHttpRequest();
        $("#ticket-arrow-left, #ticket-arrow-right").velocity({opacity : 0}, transTime);
        $("#ticket-submit-button .ui-button-text").velocity({ opacity: 0 }, 500, function () {
            $("#ticket-submit-button").blur()
            .velocity({ width: "7vh", height: "7vh", borderWidth: "12vh", borderRadius: "12vh", borderColor: "#0094ff", backgroundColor: "#0094ff" }, 1000, function () {
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4) {
                        if (xmlhttp.status == 200 && xmlhttp.responseText == "success") {
                            $("#ticket-submit-button").velocity({ borderRightColor: "#00FA9A" }, colorChangeTime)
                            .velocity({ borderBottomColor: "#00FA9A" }, colorChangeTime)
                            .velocity({ borderLeftColor: "#00FA9A" }, colorChangeTime)
                            .velocity({ borderTopColor: "#00FA9A", backgroundColor: "#00FA9A" }, colorChangeTime, function () {
                                $("#ticket-submit-button").velocity({ width: "50vw", height: "50vh", borderWidth: "0", borderRadius: "0" }, 1000, function () {
                                    $("#ticket-submit-button .ui-button-text").text($("#ticket-submited-text").text()).velocity({ opacity: 1 }, 500);
                                });
                            });
                        } else {
                            $("#ticket-submit-button").velocity({ borderColor: "#ed3e18", backgroundColor: "#ed3e18" }, colorChangeTime, function () {
                                $("#ticket-submit-button").velocity({ width: "50vw", height: "50vh", borderWidth: "0", borderRadius: "0" }, 1000, function () {
                                    $("#ticket-submit-button .ui-button-text").text($("#ticket-submitfailed-text").text()).velocity({ opacity: 1 }, 500);
                                    submited = false;
                                    $("#ticket-arrow-left").velocity({ opacity: 1 }, transTime);
                                });
                            });
                        }
                    }
                }
                xmlhttp.open("POST", "", true);
                xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                xmlhttp.send("title=" + title + "&categoryID=" + categoryID + "&comment=" + comment + "&rating=" + rating
                + "&name=" + name + "&email=" + email + "&phone=" + phone);
            });
            
        });
    }
})();
