var slides = (function () {
    var slideIndex = 0;
    var skip = true;
    var slideInterval = 10000;
    var transTime = 500;
    var transLock = false;

    $(document).ready(function () {
        $("#slide-container-" + slideIndex).css({ "z-index": 0 });
        $("#slide-text-container-" + slideIndex).css({ left: "2%", opacity: 1 });
        $("#slide-image-container-" + slideIndex).css({ left: "60%", opacity: 1 });

        $(".slide-container").on("swiperight", function () {
            preSlide();
        });
        $(".slide-container").on("swipeleft", function () {
            nextSlide();
        });
        $("#arrow-left-container").on("click", function () {
            preSlide();
        });
        $("#arrow-right-container").on("click", function () {
            nextSlide();
        });
    });

    $(window).load(function () {
        $("#slide-desc-" + slideIndex).velocity({ opacity: 1 }, transTime);
        slideTimer();
    });

    function slideTimer() {
        if ($('#intro-text').length == 0) {
            if (skip) {
                skip = false;
            } else {
                switchSlideByOne(true);
            }
        }
        window.setTimeout(slideTimer, slideInterval);
    }

    function nextSlide() {
        skip = true;
        switchSlideByOne(true);
    }
    function preSlide() {
        skip = true;
        switchSlideByOne(false);
    }

    function switchSlideByOne(right) {
        if (transLock) {
            return;
        }
        var preIndex = slideIndex;
        if (right) {
            slideIndex++;
        } else {
            slideIndex--;
        }
        if (slideIndex > $(".slide-container").length - 1) {
            slideIndex = 0;
        } else if (slideIndex < 0) {
            slideIndex = $(".slide-container").length - 1;
        }
        switchSlide(slideIndex, preIndex, right);
    }

    function switchSlide(index, preIndex, right) {

        transLock = true;
        if (index > $(".slide-container").length - 1) {
            index = 0;
        }
        if (index == preIndex) {
            return;
        }
        
        $("#slide-desc-" + preIndex).velocity({ opacity: 0 }, transTime, function () {
            
            $("#slide-text-container-" + preIndex).velocity({ left: "-40%", opacity: 0 }, transTime, function () {
                $("#slide-container-" + preIndex).css("z-index", -1);
                $("#slide-container-" + index).css("z-index", 0);
                $("#slide-text-container-" + index).velocity({ left: "2%", opacity: 1 }, transTime, function () {
                    $("#slide-desc-" + index).velocity({ opacity: 1}, transTime, function () {
                        transLock = false;
                        
                    });
                });
            });
        });
        $("#slide-image-container-" + preIndex).velocity({ left: "80%", opacity: 0 }, transTime * 2, function () {
            $("#slide-image-container-" + index).velocity({ left: "60%", opacity: 1 }, transTime * 2);
        });
    }

    return {
        slideIndex: slideIndex
    }
}());