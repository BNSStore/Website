var langListLock = false;
var langListMouseOver = false;
$(document).ready(function () {

    $("#lang-list").mouseenter(function () {
        langListMouseOver = true;
        showLangList();
    });
    $("#lang-list").mouseleave(function () {
        langListMouseOver = false;
        window.setTimeout(hideLangList, 500);
    });
    $("#lang-btn").mouseenter(function () {
        showLangList();
        lockLangList();
    });
    $("#lang-btn").mouseleave(function () {
        unlockLangList();
    });
    $(".lang-list-element").click(function () {
        document.cookie = "langName=" + $(this).attr("id") + "; expires=Thu, 18 Dec 2099 12:00:00 UTC; path=/; domain=.bnsstore.com";
        location.reload();
    });
});
function showLangList() {
    $('#lang-list').css({ display: 'inline-block' }).velocity("stop").velocity({ opacity: 1, "border-right-width": "1vw" }, 500);
    $('#lang-btn').velocity("stop").velocity({"border-right-width": "1vw" }, 500);
    $(window).trigger("resize");
    
}
function lockLangList() {
    langListLock = true;
}
function unlockLangList() {
    langListLock = false;
    window.setTimeout(hideLangList, 500);
}
function hideLangList() {
    if (!langListLock && !langListMouseOver) {
        $('#lang-btn').velocity("stop").velocity({"border-right-width": "0" }, 500);
        $('#lang-list').velocity("stop").velocity({ opacity: 0, "border-right-width": "0" }, 500, function () {
            $(this).css({ display: 'none' });
        });
    }
}