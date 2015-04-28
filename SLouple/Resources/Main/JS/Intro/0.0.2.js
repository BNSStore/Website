var intro = (function () {
    var backgroundSnd = new Audio("//main.bnsstore.com/res/Audios/Home/Intro/mp3");
    var switchSnd = new Audio("//main.bnsstore.com/res/Audios/Home/IntroSwitch/mp3");
    
    var welcomeText = ["Welcome", "Bienvenue", "欢迎", "Willkommen", "Aloha", "환영합니다", "ようこそ",
        "Merħba", "Добро пожаловать!", "Witam Cię", "Bem-vindos", "ಸುಸ್ವಾಗತ", "Welkom", "Բարի գալուստ!", "আদৰণি",
        "Xoş gəlmişsiniz!", "स्वागत"
    ];
    var welcomeIndex = 1;
    var lowEnd = false;

    $(window).load(function () {
        document.cookie = "firstLoad=false; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; domain=.bnsstore.com";
        lowEnd = !performanceTest(100000, 500, true);
        //lowEnd = true;
        if (isMobile.any()) {
            $("#mobile-cover-container").css({
                display: "inline-block"
            });
            $("#mobile-cover, #mobile-cover-container").on('touchstart click', function () {
                $("#mobile-cover-container").remove();
                startIntro(lowEnd);
            });
        } else {
            startIntro(lowEnd);
        }
    });

    function performanceTest(count, maxTime, debug) {
        var startTime = new Date();
        var timeElapsed = 0;
        var array = new Array(count);
        var pass = true;
        for (var i = array.length - 1; i >= 0; i--) {
            array[i] = new Object();
            timeElapsed = new Date() - startTime;
            if (timeElapsed > maxTime) {
                pass = false;
                break;
            }
        };
        array = null;
        delete array;
        if (debug) {
            console.log("Performance Test Time: " + timeElapsed);
            //alert(timeElapsed);
        }
        return pass;
    }

    //-----Settings---------------
    var totalTime = 10000;
    var introTextEnterTime = 1000;
    var containerWhiteTime = 2000;
    var containerTransTime = 2500;

    var textTop = 40;
    var shakeStrength = 3;
    var textTransAnimationTime = 20;
    var animationDivisor = 12;

    var sndDivisor = 1.2;
    var minSndVolume = 0.05;
    //----------------------------

    var startTime = new Date();
    var timeElapsed = 0;

    var finishedAnimationStarted = false;

    function startIntro() {
        if (!lowEnd) {
            containerWhiteTime = containerWhiteTime * 1.3;
        }
        backgroundSnd.play();
        $('#intro-text').velocity({
            color: "#ffffff"
        }, introTextEnterTime * 1.5, function () {
            startTime = new Date();
            animation();
        });
    }

    function animation() {
        timeElapsed = new Date() - startTime;
        if (timeElapsed > totalTime) {
            return;
        }
        if (!finishedAnimationStarted && totalTime - timeElapsed < containerWhiteTime) {
            finishedAnimationStarted = true;
            finishingAnimation();
        }
        var animationTime = ((totalTime - containerWhiteTime - timeElapsed) ^ 2 / (totalTime - containerWhiteTime)) / animationDivisor;
        if (animationTime < 0) {
            animationTime = 0;
        }
        if (lowEnd) {
            changeWelcomeText();
            playSoundEffect();
            setTimeout(animation, animationTime + textTransAnimationTime * 3);

        } else {
            $("#intro-text").velocity({ "padding-top": shakeStrength + "vh" }, textTransAnimationTime, function () {
                changeWelcomeText();
                $("#intro-text").velocity({ "padding-top": (-shakeStrength) + "vh" }, textTransAnimationTime, function () {
                    playSoundEffect();
                    $("#intro-text").velocity({ "padding-top": 0 }, textTransAnimationTime, function () {
                        setTimeout(animation, animationTime);
                    });
                });
            });
        }
    }

    function changeWelcomeText() {
        
        $("#intro-text").html(welcomeText[welcomeIndex]);
        welcomeIndex++;
        if (welcomeIndex > welcomeText.length - 1) {
            welcomeIndex = 0;
        }
    }

    function playSoundEffect() {
        var volume = 1 - containerWhiteTime / (containerWhiteTime - (totalTime - timeElapsed));
        if (volume < minSndVolume) {
            volume = minSndVolume;
        }else if(volume > 1){
            volume = 1;
        }
        switchSnd.volume = volume;
        switchSnd.pause();
        switchSnd.currentTime = 0;
        switchSnd.play();
    }

    function finishingAnimation() {
        //Black -> White
        if (lowEnd) {
            $('#home-intro-container').velocity({ backgroundColor: '#ffffff' }, containerWhiteTime, function () {
                cleanup();
            });
        } else {
            $('#home-intro-container').velocity({ backgroundColor: '#ffffff' }, { delay: containerWhiteTime / 3 * 2 - 750 }, containerWhiteTime / 3);
            $('#intro-glow').velocity({ top: "-50vh", left: "-50vw", height: "200vh", width: "200vw", opacity: 1 }, containerWhiteTime / 3 * 2, function () {
                cleanup();
            });
        }
    }
    function cleanup() {
        switchSnd.volume = 0;
        $('#intro-text').remove();
        //White -> Transparent
        $('#home-intro-container, #intro-glow').velocity({ 'opacity': '0' }, containerTransTime, function () {
            finished = true;
            $('#home-intro-container, #intro-glow').remove();
            
        });
    }
    return {
        totalTime: totalTime,
        welcomeText: welcomeText,
        totalTime: 10000,
        introTextEnterTime: 1000,
        containerWhiteTime: 2000,
        containerTransTime: 2500
    }
})();