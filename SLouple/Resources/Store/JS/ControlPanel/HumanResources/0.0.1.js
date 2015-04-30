var humanResources = (function () {
    
    $(function () {
        $("#add-shift-button").click(function () {
            addShift();
        });
        $("#del-shift-button").click(function () {
            delShift();
        });
    });

    function addShift() {
        var name = $("#add-shift-name").val().trim().split(" ");
        if(name.length < 2){
            return;
        }
        var firstName = name[0];
        var lastName = name[1];
        var year = $("#add-shift-year").val();
        var month = $("#add-shift-month").val();
        var day = $("#add-shift-day").val();
        var date = year + "/" + month + "/" + day;
        var store = $("#add-shift-store").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "AddShift", firstName: firstName, lastName: lastName, date: date, store: store },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#add-shift-first-name").val("").focus();
                $("#add-shift-last-name").val("");
            } else {
                $("#add-shift-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function delShift() {
        var name = $("#del-shift-name").val().trim().split(" ");
        if (name.length < 2) {
            return;
        }
        var firstName = name[0];
        var lastName = name[1];
        var year = $("#del-shift-year").val();
        var month = $("#del-shift-month").val();
        var day = $("#del-shift-day").val();
        var date = year + "/" + month + "/" + day;
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "DelShift", firstName: firstName, lastName: lastName, date: date},
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#del-shift-first-name").val("").focus();
                $("#del-shift-last-name").val("");
            } else {
                $("#del-shift-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });

    }

})();