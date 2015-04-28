var chatHub = (function () {

    var messageEntry;
    var displayName = null;
    var chat;
    $(function () {
        messageEntry = $("#message-entry-clone");
        if ($("#account-warning-title").text() == "") {
            $("#chat-log").autoTextSize(1.2, 0, $("#chat-hub-container"), false, {maxFontSize : "30px"});
            $("#message-send-button").autoTextSize(0.12, 0, null, false);
            $("#message").autoTextSize(0.15, 0, null, false);
            chat = $.connection.chatHub;
            chat.client.recieveMessage = recieveMessage;
            chat.client.updateOnlineUserList = updateOnlineUserList;
            chat.connection.start().done(function () {
                login();
                $("#message-send-button").click(function () {
                    sendMessage();
                });
                $("#message").keypress(function (e) {
                    var key = e.which;
                    if (key == 13) {
                        sendMessage();
                    }
                });
                $("#state-container").css("display", "none");
            });
            chat.connection.disconnected(function () {
                setTimeout(function () {
                    chat.connection.start().done(function () {
                        login();
                    });
                }, 2000);
            });
        }

    });
    
    function recieveMessage(displayName, message, type, date) {
        var newEntry = messageEntry.clone();
        if (type == 'xx') {
            newEntry.addClass("message-type-" + type);
            newEntry.find(".message-date").text(date);
            newEntry.find(".message-display-name").text(htmlEncode(displayName));
            newEntry.find(".message-context").text(htmlEncode(message) + ": ");
            newEntry.find(".message-context").insertBefore(newEntry.find(".message-display-name"));
        } else {
            newEntry.addClass("message-type-" + type);
            newEntry.find(".message-date").text(date);
            newEntry.find(".message-display-name").text(displayName);
            newEntry.find(".message-context").text(message);
        }
        newEntry.css("opacity", "0");

        $("#chat-log").append(newEntry);
        newEntry.velocity({opacity: 1}, 300);
        var elem = document.getElementById("chat-log");
        elem.scrollTop = elem.scrollHeight;
    }

    function htmlEncode(value) {
        var encodedValue = $("<div />").text(value).html();
        return encodedValue;
    }

    function updateOnlineUserList(usernames) {
        $("#online-users").text("");
        usernames.forEach(function(entry){
            $("#online-users").append("<div class=\"online-user\">" + htmlEncode(entry) + "</div>");
        });
        $(".online-user").autoTextSize(0.5, 0, $("#online-users-container"), true);
    }

    function login() {
        $("#connecting-title").css("display", "none");
        $("#logging-in-title").css("display", "block");
        var userID = getCookie("userID");
        var sessionToken = getCookie("sessionToken");
        if (userID != null && sessionToken != null && sessionToken != "") {
            chat.server.login(userID, sessionToken);
            var css = document.createElement("style");
            css.type = "text/css";
            css.innerHTML = ".message-own" + "{ background: #eab71c; color: black }";
            document.body.appendChild(css);
        }
    }

    function sendMessage() {
        if ($("#message").val() == "") {
            return;
        }
        chat.server.sendMessage($("#message").val());
        $("#message").val("").focus();
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
        chat.connection.stop();
        return null;
    };

    return chat;
})();