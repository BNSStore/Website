var slideIndex = 0;
var skip = true;
var slideInterval = 5000;
var transTime = 750;
var transLock = false;
$(document).ready(function () {
    for (i = 0; i < $(".slide-container").length ; i++) {
        if (i == slideIndex) {
            $("#slide-container-" + i + ", #title-container-" + i).css({ left: "0%" });
        } else {
            $("#slide-container-" + i + ", #title-container-" + i).css({ left: "100%" });
        }
    }
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
    }else if (slideIndex < 0) {
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
    var slideLeft = 0;
    if (right) {
        slideLeft = 100;
    } else {
        slideLeft = -100;
    }
    //$("#arrows").hide();
    $("#slide-container-" + preIndex + ", #title-container-" + preIndex).css({ left: "0%" }).velocity({ left: -slideLeft + "%" }, transTime, function () {
        //$("#arrows").show();
        transLock = false;
    });
    $("#slide-container-" + index + ", #title-container-" + index).css({ left: slideLeft + "%" }).velocity({ left: "0%" }, transTime);
}