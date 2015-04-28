var settings = (function () {
    
    $(function () {
        $(".settings-sidebar-button").click(function () {
            var itemName = $(this).attr("id");
            $(".settings-sidebar-button").not($(this)).velocity("stop").velocity({ backgroundColor: "#1bccf1" }, 200);
            $(this).velocity("stop").velocity({ backgroundColor: "#129ebb" }, 200);
            $(".settings-item-container").not("#settings-" + itemName + "-container").velocity("stop").velocity({ opacity: 0 }, 500).css("z-index", "0");
            $("#settings-" + itemName + "-container").css("z-index", "101").velocity("stop").velocity({ opacity: 1 }, 500);
        });

        $("#settings-sidebar #general").trigger("click");

        $("#settings-email-sub-checkbox").change(function () {
            emailSub(this.checked);
        });
    });

    function emailSub(checked) {
        $.ajax({
            type: "POST",
            url: "//account.bnsstore.com/Settings/",
            data: { method: "emailSub", sub : checked},
            dataType: "html",
            async: false

        }).done(function (data) {
            console.log(data);
        });
    }

})();