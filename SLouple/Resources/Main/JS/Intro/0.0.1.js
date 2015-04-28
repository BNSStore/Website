var wordList = ["Welcome", "欢迎", "Bienvenue", "Willkommen", "Aloha", "स्वागत", "환영합니다", "ようこそ",
    "Merħba", "Добро пожаловать!", "Witam Cię", "Bem-vindos", "ಸುಸ್ವಾಗತ", "Welkom", "Բարի գալուստ!", "আদৰণি",
    "Xoş gəlmişsiniz!"];
var finished = false;

var wordIndex = 1;

var introTextEnterTime = 1000;
var introTransDelay = 100;
var introTransCalled = false;
var containerWhiteTime = 2000;
var containerTransTime = 2500;

var time = 1000;
var minTime = 50;

var textTop = 40;
var shakeStrength = 3;

var bgm = new Audio("//main.bnsstore.com/res/Audios/Home/Intro/mp3");
var switchSnd = new Audio("//main.bnsstore.com/res/Audios/Home/IntroSwitch/mp3");
var switchSndVol = 1;

//Mobile Test
var mobile = false;
var mobileStarted = false;

performanceTest();
function performanceTest() {
    var start = new Date();
    var array = new Array(1000000);
    for (var i = array.length - 1; i >= 0; i--) {
        array[i] = new Object();
    };
    array = null;
    delete array;
    var end = new Date();
    var performance = end - start;
    if (performance > 1000) {
        mobile = true;
    }
    console.log(performance);
}

$(window).load(function() {
    if (mobile) {
        $("#mobile-cover-container").css({ display: "inline-block" });
        $("#mobile-cover, #mobile-cover-container").on('touchstart click mouseenter', function () {
            mobileStart();
        });
    } else {
        $('#intro-text').velocity({ color: "#ffffff" }, introTextEnterTime, function () {
                bgm.play();
                changeText();
        });
    }
});
function mobileStart() {
    if (!mobileStarted) {
        mobileStarted = true;
        bgm.play();
        $("#mobile-cover-container").remove();
        $('#intro-text').velocity({ color: "#ffffff" }, introTextEnterTime * 1.5, function () {
            changeTextMobile();
        });
    }
}
function changeText() {
    if (finished) {
        return;
    }
    var transTime = time / 20;
    $("#intro-text").velocity({ "top": (textTop + shakeStrength) + "%" }, transTime, function () {
        $("#intro-text").html(wordList[wordIndex]);
        $("#intro-text").velocity({ "top": (textTop - shakeStrength) + "%" }, transTime, function () {

            //Play switching sound effect
                if (time < 100) {
                    switchSndVol = switchSndVol / 1.2;
                }
                switchSnd.volume = switchSndVol;
                switchSnd.pause();
                switchSnd.currentTime = 0;
                switchSnd.play();

            $("#intro-text").velocity({ "top": textTop + "%" }, transTime, function () {
                wordIndex++;
                if (wordIndex > wordList.length - 1) {
                    wordIndex = 0;
                }
                
                if (time > 500) {
                    time /= 1.2;
                } else {
                    time /= 1.1;
                    if (time < minTime) {
                        time = minTime;
                    }
                }
                if (time < introTransDelay && !introTransCalled) {
                    introTransition();
                }
                window.setTimeout(changeText, time); //Loop Back
            });
        });
    });
    
}
function changeTextMobile() {
    if (finished) {
        return;
    }
    var transTime = time / 20;
    $("#intro-text").html(wordList[wordIndex]);
    wordIndex++;
    if (wordIndex > wordList.length - 1) {
        wordIndex = 0;
    }
    if (time > 500) {
        time /= 1.2;
    } else {
        time /= 1.1;
        if (time < minTime) {
            time = minTime;
        }
        if (time < introTransDelay && !introTransCalled) {
            introTransition();
        }
    }
    window.setTimeout(changeTextMobile, time + transTime * 4); //Loop Back
}
function introTransition() {
    introTransCalled = true;
    //Black -> White
    $('#home-intro-container').velocity({ backgroundColor: '#ffffff' }, containerWhiteTime, function () {
        switchSndVol = 0;
        $('#intro-text').remove();
        //White -> Transparent
        $('#home-intro-container').velocity({ 'opacity': '0' }, containerTransTime, function () {
            finished = true;
            $('#home-intro-container').remove();
            document.cookie = "firstLoad=false; expires=Thu, 18 Dec 2099 12:00:00 UTC";
        });
    });
}