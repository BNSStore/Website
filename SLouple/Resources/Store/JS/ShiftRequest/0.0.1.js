var shiftRequest = (function () {

    $(function () {
        $(".shift-request-accept").click(function () {
            var senderID = $(this).siblings(".shift-request-name").attr("id");
            var date = $(this).siblings(".shift-request-date").attr("id");
            acceptShiftRequest($(this), senderID, date);
        });
        $(".shift-request-decline").click(function () {
            var senderID = $(this).siblings(".shift-request-name").attr("id");
            var date = $(this).siblings(".shift-request-date").attr("id");
            declineShiftRequest($(this), senderID, date);
        });
    });

    function acceptShiftRequest(button, senderID, date) {
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ShiftRequest/",
            data: { method: "accept", senderID : senderID, date: date},
            dataType: "html",
            async: false,
            xhrFields: {
                withCredentials: true
            }

        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#shift-request-container " + "[id*='" + date + "']").remove();
            } else {
                button.velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
            if ($("#shift-request-container .shift-request").length == 0) {
                $("#shift-request-container").remove();
            }
        });
    }

    function declineShiftRequest(button, senderID, date) {
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ShiftRequest/",
            data: { method: "decline", senderID: senderID, date: date},
            dataType: "html",
            async: false,
            xhrFields: {
                withCredentials: true
            }
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                button.parent().remove();
            } else {
                button.velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
            if ($("#shift-request-container .shift-request").length == 0) {
                $("#shift-request-container").remove();
            }
        });
    }

})();