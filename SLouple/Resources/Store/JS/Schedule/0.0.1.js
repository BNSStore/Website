var schedule = (function () {
    var date;
    $(function () {
        if (document.URL.toLowerCase().indexOf("ShowAll=true".toLowerCase()) > 0) {
            $("#show-all-checkbox").prop("checked", true);
        }
        $("#show-all-checkbox").change(function () {
            if (this.checked) {
                location.href = "//" +window.location.host + "/Schedule/ShowAll=true/";
            } else {
                location.href = "//" +window.location.host + "/Schedule/";
            }
        });

        $(".self").click(function () {
            $("#shift-request-popup-container").css("display", "block");
            date = $(this).attr("id");
        });

        $("#shift-request-popup-container").click(function () {
            $(this).css("display", "none");
        }).children().click(function (e) {
            return false;
        });

        $("#shift-request-submit").click(function () {
            var firstName = $("#shift-request-first-name-input").val();
            var lastName = $("#shift-request-last-name-input").val();
            $.ajax({
                type: "POST",
                url: "//store.bnsstore.com/ShiftRequest/",
                data: { method: "add", firstName: firstName, lastName : lastName, date : date},
                dataType: "html",
                async: false
            }).done(function (data) {
                console.log(data);
                if (data != null && data != "" && data == "success") {
                    $("#shift-request-first-name-input").val("");
                    $("#shift-request-last-name-input").val("");
                    $("#shift-request-popup-container").css("display", "none");
                } else {
                    $("#shift-request-submit").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
                }
            });
        });
    });

})();