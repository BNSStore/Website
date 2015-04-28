var verifyEmail = (function () {
    $(function () {
        var color;
        if ($("#verify-email-failed").text() == "true") {
            color = "#ed3e18";
        } else {
            color = "#62d317";
        }
        $("#verify-email-container").velocity({ "color": color }, 5000);
        updateCounter(5);
    });

    function updateCounter(counter) {
        $("#verify-email-redirect-counter").text(counter);
        
        if (counter == 0) {
            window.location.replace("//bnsstore.com");
        } else {
            setTimeout(function () { updateCounter(counter - 1); }, 1000);
        }
        
    }

})();