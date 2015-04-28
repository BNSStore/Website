var marking = (function () {

    $(function(){
        $(".unmarked-shift-mark-input").on("input", function () {
            $(this).val($(this).val().replace(/[^0-9]+/gi, ""));
            if ($(this).val() > 100) {
                $(this).val(100);
            }
        });
        $(".unmarked-shift-submit-button").click(function () {
            var userID = $(this).attr("name");
            var date = $("#unmarked-shift-date-" + userID).attr("name");
            var mark = $("#unmarked-shift-mark-input-" + userID).val();
            var comment = $("#unmarked-shift-comment-" + userID).val();
            mark = parseInt(mark);
            updateMarkAndComment(userID, date, mark, comment);
        });
    });

    function updateMarkAndComment(userID, date, mark, comment){
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/Marking/",
            data: { userID: userID, date: date, mark: mark, comment: comment },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#unmarked-shift-container-" + userID).velocity({ opacity: 0 }, 1000, function () {
                    $("#unmarked-shift-container-" + userID).remove();
                });
            } else {
                $("#unmarked-shift-submit-button-" + userID).velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

})();