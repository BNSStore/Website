var sales = (function () {

    var salesHub;

    $(function () {

        $(".product-subtract-button").click(function () {
            var id = $(this).attr("name");
            var count = $("#product-count-" + id).val();
            var employeeCount = $("#product-employee-count-" + id).val();
            count = parseInt(count);
            employeeCount = parseInt(employeeCount);
            updateSaleCountServer(id, count - 1, employeeCount);
        });
        $(".product-add-button").click(function () {
            var id = $(this).attr("name");
            var count = $("#product-count-" + id).val();
            var employeeCount = $("#product-employee-count-" + id).val();
            count = parseInt(count);
            employeeCount = parseInt(employeeCount);
            updateSaleCountServer(id, count + 1, employeeCount);
        });
        $(".product-employee-subtract-button").click(function () {
            var id = $(this).attr("name");
            var count = $("#product-count-" + id).val();
            var employeeCount = $("#product-employee-count-" + id).val();
            count = parseInt(count);
            employeeCount = parseInt(employeeCount);
            updateSaleCountServer(id, count, employeeCount - 1);
        });
        $(".product-employee-add-button").click(function () {
            var id = $(this).attr("name");
            var count = $("#product-count-" + id).val();
            var employeeCount = $("#product-employee-count-" + id).val();
            count = parseInt(count);
            employeeCount = parseInt(employeeCount);
            updateSaleCountServer(id, count, employeeCount + 1);
        });

        $(".product-count").on("input", function () {
            $(this).val($(this).val().replace(/[^0-9]+/gi, ""));
            if ($(this).val().length == 0) {
                $(this).val(0);
            }
            var id = $(this).attr("name");
            var count = $(this).val();
            var employeeCount = $("#product-employee-count-" + id).val();
            count = parseInt(count);
            employeeCount = parseInt(employeeCount);
            updateSaleCountServer(id, count, employeeCount);
        });

        $(".product-employee-count").on("input", function () {
            $(this).val($(this).val().replace(/[^0-9]+/gi, ""));
            if ($(this).val().length == 0) {
                $(this).val(0);
            }
            var id = $(this).attr("name");
            var count = $("#product-count-" + id).val();
            var employeeCount = $(this).val();
            count = parseInt(count);
            employeeCount = parseInt(employeeCount);
            updateSaleCountServer(id, count, employeeCount);
        });

        salesHub = $.connection.salesHub;
        salesHub.client.loginSuccess = loginSuccessClient;
        salesHub.client.updateSaleCount = updateSaleCountClient;
        salesHub.client.updateTotal = updateTotalClient;
        $.connection.hub.start().done(function () {
            login();
        });

    });

    function login() {
        var userID = getCookie("userID");
        var sessionToken = getCookie("sessionToken");
        var store = $("#store").text();
        if (userID != null && sessionToken != null && sessionToken != "" && store != null && store != "") {
            salesHub.server.login(userID, sessionToken, store);
        }
        
    }

    function loginSuccessClient() {
        $("#sales-overlay").css({ "display": "none" });
    }

    function updateSaleCountServer(productID, count, employeeCount) {
        if (count >= 0 && employeeCount >= 0) {
            salesHub.server.updateSaleCount(productID, count, employeeCount);
        }
        
    }

    function updateSaleCountClient(productID, count, employeeCount) {
        $("#product-count-" + productID).val(count);
        $("#product-employee-count-" + productID).val(employeeCount);
    }

    function updateTotalClient(total) {
        $("#sales-total").text("$" + total);
    }

    function getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(";");
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == " ") c = c.substring(1);
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    }

    window.onbeforeunload = function (event) {
        $.connection.hub.stop();
        return null;
    };

})();