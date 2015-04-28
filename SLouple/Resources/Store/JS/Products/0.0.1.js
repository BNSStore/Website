var ProductSearch = (function () {
    $(function () {
        var minPrice = null;
        var maxPrice = null;
        var keyword = null;
        var categoryID = null;

        var pars = window.location.pathname.substring(1).split("/");
        for (i = 0; i < pars.length; i++) {
            if (i > 0) {
                if (pars[i] == "") {
                    continue;
                }
                var pair = pars[i].split("=");
                var key = pair[0].toLowerCase();
                var value = pair[1].toLowerCase();
                switch (key) {
                    case "minprice":
                        minPrice = value;
                        break;
                    case "maxprice":
                        maxPrice = value;
                        break;
                    case "categoryid":
                        categoryID = value;
                        break;
                    case "keyword":
                        keyword = pair[1];
                        break;
                }
            }
        }
        if (keyword != null) {
            $("#keyword-textbox").val(keyword);
        }
        var sliderMin = 0;
        var sliderMax = 5;
        $("#price-range-slider").slider({
            range: true,
            min: sliderMin,
            max: sliderMax,
            step: ((sliderMax - sliderMin) / 100),
            values: [0, 50],
            slide: function (event, ui) {
                $("#price-range").text("$" + ui.values[0] + "- $" + ui.values[1]);
            }
        });
        if (minPrice != null) {
            $("#price-range-slider").slider("values", 0, minPrice);
        }
        if (maxPrice != null) {
            $("#price-range-slider").slider("values", 1, maxPrice);
        }
        $("#price-range").text("$" + $("#price-range-slider").slider("values", 0) + "- $" + $("#price-range-slider").slider("values", 1));

        $('#product-category-selectmenu').selectmenu();
        if (categoryID != null) {
            console.log(categoryID);
            $('#product-category-selectmenu').val(categoryID);
        }
        $('#product-category-selectmenu').selectmenu("refresh");
        $("#search-button").button().click(function (event) {
            search();
        });

        $('#keyword-title').autoTextSize(0.2, 0,null, false);
        $('#keyword-textbox').autoTextSize(0.2, 0, null, false);
        $('#price-range-title').autoTextSize(0.2, 0, null, false);
        $('#price-range').autoTextSize(0.2, 0, null, false);
        $('#product-category-title').autoTextSize(0.2, 0, null, false);
        $('#search-button').autoTextSize(0.2, 0, null, false);
        $(".ui-selectmenu-text").autoTextSize(0.2, 0, null, false);

    });

    function search(){
        var keyword = null;
        var minPrice = null;
        var maxPrice = null;
        var categoryID = null;
        var url = "//" +window.location.host + "/Products/"

        minPrice = $("#price-range-slider").slider("values", 0);
        if (minPrice != $("#price-range-slider").slider("option", "min")) {
            url = url + "minPrice=" + minPrice + "/";
        }
        maxPrice = $("#price-range-slider").slider("values", 1);
        if (maxPrice != $("#price-range-slider").slider("option", "max")) {
            url = url + "maxPrice=" + maxPrice + "/";
        }
        keyword = $("#keyword-textbox").val();
        if (keyword != "") {
            url = url + "keyword=" + keyword + "/";
        }
        categoryID = $('#product-category-selectmenu').val();
        if (categoryID != 0) {
            url = url + "categoryID=" + categoryID + "/";
        }
        window.location.href =  url;
    }
})();
