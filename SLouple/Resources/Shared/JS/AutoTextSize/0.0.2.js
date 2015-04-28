/*!
* AutoTextSize is based on:
* -----------------------
* FitText.js 1.2
*
* Copyright 2011, Dave Rupert http://daverupert.com
* Released under the WTFPL license
* http://sam.zoy.org/wtfpl/
*
* Date: Thu May 05 14:23:00 2011 -0600
* -----------------------
*/

(function ($) {

    $.fn.autoTextSize = function (kompressor, mode, refIn, useLength, options) {
        refIn = typeof refIn !== 'undefined' && refIn != null ? refIn : $(this);
        useLength = typeof useLength !== 'undefined' ? useLength : true;
        // Setup options
        var compressor = kompressor || 1,
            settings = $.extend({
                'minFontSize': 7,
                //'minFontSize': Number.NEGATIVE_INFINITY,
                'maxFontSize': Number.POSITIVE_INFINITY
            }, options);

        return this.each(function () {

            // Store the object
            var $this = $(this);
            var ref;
            if (refIn == "parent") {
                ref = $this.parent();
            } else {
                ref = refIn;
            }
            function getFontSize(mode) {
                var length = $this.text().length / 30;
                if (length < 1 || !useLength) {
                    length = 1;
                }
                if (mode == 0) {
                    var widthSize = getFontSize(1);
                    var heightSize = getFontSize(2);
                    
                    if (widthSize < heightSize) {
                        return widthSize;
                    } else {
                        return heightSize;
                    }
                } else if (mode == 1) {
                    return Math.max(Math.min(ref.width() / (compressor * 20) / length, parseFloat(settings.maxFontSize)), parseFloat(settings.minFontSize));
                } else if (mode == 2) {
                    return Math.max(Math.min(ref.height() / (compressor * 10) / length, parseFloat(settings.maxFontSize)), parseFloat(settings.minFontSize));
                }
            }
            // Resizer() resizes items based on the object width divided by the compressor * 10
            var resizer = function (mode) {
                $this.css('font-size', getFontSize(mode));
            };
            $this.resizer = resizer;
            $this.mode = mode;
            // Call once to set.
            resizer(mode);

            // Call on resize. Opera debounces their resize by default.
            $(window).resize(function () {
                resizer(mode);
            });
            $(window).on( "orientationchange", function(event) {
                resizer(mode);
            } );
            if (useLength) {
                $this.bind("DOMSubtreeModified", function () {
                    resizer(mode);
                });
            }

        });

    };

})(jQuery);