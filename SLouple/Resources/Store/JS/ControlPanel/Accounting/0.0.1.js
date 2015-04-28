var accounting = (function () {

    $(function () {
        $("#download-sales-button").click(function () {
            downloadSales();
        });
    });

    function downloadSales() {
        var startDate = $("#download-sales-start-year").val() + "/" + $("#download-sales-start-month").val() + "/" + $("#download-sales-start-day").val();
        var endDate = $("#download-sales-end-year").val() + "/" + $("#download-sales-end-month").val() + "/" + $("#download-sales-end-day").val();
        var url = "//store.bnsstore.com/ControlPanel/";
        var form = $('<form action="' + url + '" method="post" style="display : hidden;">' +
          '<input type="text" name="method" value="' + "DownloadSales" + '" />' +
          '<input type="text" name="startDate" value="' + startDate + '" />' +
          '<input type="text" name="endDate" value="' + endDate + '" />' +
          '</form>');
        $('body').append(form);
        form.submit();
    }
})();