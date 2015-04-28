var signUp = (function () {
    var canSubmit = false;
    var validEmailAddress = false;
    var validUsername = false;
    var validPassword = false;
    var validreCaptcha = false;
    var submitButtonMouseOver = false;
    $(function () {
        $("#sign-up-submit-button").mouseenter(function () {
            submitButtonMouseOver = true;
            checkSubmit();
        });

        $("#sign-up-submit-button").mouseleave(function () {
            submitButtonMouseOver = false;
            $("#sign-up-submit-button").velocity("stop").velocity({ backgroundColor: "#1bccf1" }, 500);

        });
        $("#sign-up-email-address-input").on("input", function () {
            checkEmailAddress();
        });
        $("#sign-up-username-input").on("input", function () {
            $(this).val($(this).val().replace(/[^a-z0-9_-]+/gi, ""));
            checkUsername();
        });
        $("#sign-up-password-input, #sign-up-confirm-password-input").on("input", function () {
            checkPassword();
        });
    });

    function checkEmailAddress(){
        var emailAddress = $("#sign-up-email-address-input").val();
        if (emailAddress.indexOf("@") > -1 && emailAddress.indexOf("@") != emailAddress.length - 1) {
            $("#sign-up-email-address-error-not-valid").css("opacity", "0").css("display", "none");
            $("#sign-up-email-address-error-taken").css("display", "block");
            $.ajax({
                url: "//account.bnsstore.com/SignUpCheck/checkName=emailExist/email=" + emailAddress + "/",
                async: false
            }).done(function (data) {
                if (data.toLowerCase() == "false") {
                    $("#sign-up-email-address-error-taken").css("opacity", "0");
                    validEmailAddress = true;
                } else {
                    $("#sign-up-email-address-error-taken").css("opacity", "1");
                    validEmailAddress = false;
                }
            });
        } else {
            $("#sign-up-email-address-error-taken").css("opacity", "0").css("display", "none");
            $("#sign-up-email-address-error-not-valid").css("opacity", "1").css("display", "block");
            validEmailAddress = false;
        }
    }

    function checkUsername() {
        var username = $("#sign-up-username-input").val();
        if (username.length >= 4) {
            $("#sign-up-username-error-too-short").css("opacity", "0").css("display", "none");
            $("#sign-up-username-error-taken").css("display", "block");
            $.ajax({
                url: "//account.bnsstore.com/SignUpCheck/checkName=usernameExist/username=" + username + "/",
                async : false
            }).done(function (data) {
                if (data.toLowerCase() == "false") {
                    $("#sign-up-username-error-taken").css("opacity", "0");
                    validUsername = true;
                } else {
                    $("#sign-up-username-error-taken").css("opacity", "1");
                    validUsername = false;
                }
            });
        } else {
            $("#sign-up-username-error-taken").css("opacity", "0").css("display", "none");
            $("#sign-up-username-error-too-short").css("opacity", "1").css("display", "block");
            validUsername = false;
        }
    }

    function checkPassword() {
        var password = $("#sign-up-password-input").val();
        var confirmPassword = $("#sign-up-confirm-password-input").val();
        if (password.length >= 8) {
            $("#sign-up-password-error").css("opacity", "0");
            if (confirmPassword == password) {
                $("#sign-up-confirm-password-error").css("opacity", "0");
                validPassword = true;
            } else {
                $("#sign-up-confirm-password-error").css("opacity", "1");
                validPassword = false;
            }
        } else {
            $("#sign-up-password-error").css("opacity", "1");
            validPassword = false;
        }
        
    }

    function checkreCaptcha() {
        var response = grecaptcha.getResponse();
        if (response != "") {
            validreCaptcha = true;
        } else {
            validreCaptcha = false;
        }
    }

    function checkAll(){
        checkEmailAddress();
        checkUsername();
        checkPassword();
        canSubmit();
    }

    function checkSubmit() {
        if (submitButtonMouseOver) {
            checkreCaptcha();
            if (validEmailAddress && validUsername && validPassword && validreCaptcha) {
                $("#sign-up-submit-button").attr("type", "submit");
                $("#sign-up-submit-button").velocity("stop").velocity({ backgroundColor: "#62d317" }, 500);
                canSubmit = true;
            } else {
                $("#sign-up-submit-button").attr("type", "button");
                $("#sign-up-submit-button").velocity("stop").velocity({ backgroundColor: "#ed3e18" }, 500);
                canSubmit = false;
            }
            window.setTimeout(function () { checkSubmit(); }, 500);
        }
    }

    $(window).resize(function () {
        if ($(window).width() < 800) {
            $("#sign-up-side-content-container").css("display", "none");
            $("#sign-up-form").css("width", "80%");
            $("#sign-up-form").css("margin", "0 10% 0 10%");
            $("#sign-up-username-container, #sign-up-display-name-container,#sign-up-password-container,#sign-up-confirm-password-container").
            css("width", "100%");
        } else if ($(window).width() < 1100) {
            $("#sign-up-side-content-container").css("display", "inline-block");
            $("#sign-up-form").css("width", "45%");
            $("#sign-up-form").css("margin", "0 0 0 50%");
            $("#sign-up-username-container, #sign-up-display-name-container,#sign-up-password-container,#sign-up-confirm-password-container").
            css("width", "100%");
        }else {
            $("#sign-up-side-content-container").css("display", "inline-block");
            $("#sign-up-form").css("width", "45%");
            $("#sign-up-form").css("margin", "0 0 0 50%");
            $("#sign-up-username-container, #sign-up-display-name-container,#sign-up-password-container,#sign-up-confirm-password-container").
            css("width", "48%");

        }
    });
    
    return {
    };

})();