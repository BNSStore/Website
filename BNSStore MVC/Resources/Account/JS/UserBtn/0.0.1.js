var userMenu = (function () {
    var userMenuLock = false;
    var userMenuMouseOver = false;

    $(function () {
        $("#user-menu").mouseenter(function () {
            userMenuMouseOver = true;
            showUserMenu();
        });
        $("#user-menu").mouseleave(function () {
            userMenuMouseOver = false;
            window.setTimeout(hideUserMenu, 500);
        });
        $("#user-btn").mouseenter(function () {
            showUserMenu();
            lockUserMenu();
        });
        $("#user-btn").mouseleave(function () {
            unlockUserMenu();
        });
        $("#user-menu-login-button").click(function () {
            login();
        });
        $("#user-menu-logout-button").click(function () {
            document.cookie = "userID=" + "; expires=Thu, 01 Jan 1970 00:00:01 UTC; path=/; domain=.bnsstore.com";
            document.cookie = "sessionToken=" + "; expires=Thu, 01 Jan 1970 00:00:01 UTC; path=/; domain=.bnsstore.com";
            location.reload();

        });
        $("#user-menu-username").keypress(function (event) {
            if (event.which == 13) {
                login();
            }
        });
        $("#user-menu-password").keypress(function (event) {
            if(event.which == 13){
                login();
            }
        });
    });
    function showUserMenu() {
        $('#user-menu').css({ display: 'inline-block' }).velocity("stop").velocity({ opacity: 1 }, 500);
        $("#user-menu-border").css({ display: 'block' }).velocity("stop").velocity({ "width": "1vw", "opacity": "1" }, 500);
        $('#user-btn').velocity("stop").velocity({ "border-right-width": "1vw" }, 500);
        $(window).trigger("resize");

    }
    function lockUserMenu() {
        userMenuLock = true;
    }
    function unlockUserMenu() {
        userMenuLock = false;
        window.setTimeout(hideUserMenu, 500);
    }
    function hideUserMenu() {
        if (!userMenuLock && !userMenuMouseOver) {
            $('#user-btn').velocity("stop").velocity({ "border-right-width": "0" }, 500);
            $("#user-menu-border").velocity("stop").velocity({ "width": "0", "opacity": "0" }, 500, function () {
                $(this).css({ display: 'none' });
            });
            $('#user-menu').velocity("stop").velocity({ opacity: 0}, 500, function () {
                $(this).css({ display: 'none' });
            });
        }
    }

    function login() {
        var username = $("#user-menu-username").val();
        var password = $("#user-menu-password").val();
        $.ajax({
            type: "POST",
            url: "//account.bnsstore.com/Login/simple=true/",
            data: { username: username, password: password },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data != "false") {
                data = data.split("|");
                var userID = data[0];
                var sessionToken = data[1];
                document.cookie = "userID=" + userID + "; expires=Thu, 18 Dec 2099 12:00:00 UTC; path=/; domain=.bnsstore.com";
                document.cookie = "sessionToken=" + sessionToken + "; expires=Thu, 18 Dec 2099 12:00:00 UTC; path=/; domain=.bnsstore.com";
                document.cookie = "langName=" + "; expires=Thu, 01 Jan 1970 00:00:01 UTC; path=/; domain=.bnsstore.com";
                location.reload();
            } else {
                $('#user-menu-username, #user-menu-password').velocity("stop").velocity({ backgroundColor: "#ed3e18" }, 1500).velocity({ backgroundColor: "#ffffff" }, 500);
                $('input:-webkit-autofill').stop();
                $('input:-webkit-autofill').animate({ boxShadow: "0 0 0px 200px #ed3e18 inset" }, 1500, function () {
                    $('input:-webkit-autofill').animate({ boxShadow: "0 0 0px 200px #ffffff inset" }, 500);
                });
            }
        });
    }



})();
