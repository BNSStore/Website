var product = (function () {
    
    $(function () {
        $("#add-product-button").click(function () {
            addProduct();
        });
        $("#del-product-button").click(function () {
            delProduct();
        });

        $("#change-product-name-button").click(function () {
            changeProductName();
        });
        $("#change-product-category-button").click(function () {
            changeProductCategory();
        });
        $("#change-product-image-button").click(function () {
            changeProductImage();
        });
        $("#change-product-price-button").click(function () {
            changeProductPrice();
        });
        $("#change-product-employee-price-button").click(function () {
            changeProductEmployeePrice();
        });
        $("#change-product-on-sale-price-button").click(function () {
            changeProductOnSalePrice();
        });

        $("#add-category-button").click(function () {
            addCategory();
        });
        $("#del-category-button").click(function () {
            delCategory();
        });
        $("#change-category-name-button").click(function () {
            changeCategoryName();
        });
    });

    function addProduct() {
        var productName = $("#add-product-name").val();
        var productName = $("#add-product-price").val();
        var productName = $("#add-product-employee-price").val();
        var productName = $("#add-product-category-name").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "AddProduct", productName: productName, productPrice: productPrice, employeePrice: employeePrice, categoryName: categoryName },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#add-product-name").val("");
                $("#add-product-price").val("");
                $("#add-product-employee-price").val("");
                $("#add-product-category-name").val("");
            } else {
                $("#add-product-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function delProduct() {
        var productName = $("#del-product-name").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "DelProduct", productName: productName},
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#del-product-name").val("");
            } else {
                $("#del-product-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeProductName() {
        var productName = $("#update-product-name").val();
        var newProductName = $("#change-product-name-new-name").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "ChangeProductName", productName: productName, newProductName: newProductName},
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#change-product-name-new-name").val("");
                $("#update-product-name").val(newProductName);
            } else {
                $("#change-product-name-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeProductCategory() {
        var productName = $("#update-product-name").val();
        var categoryName = $("#change-product-category-name").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "ChangeProductCategory", productName: productName, categoryName: categoryName },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#change-product-category-name").val("");
            } else {
                $("#change-product-category-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeProductPrice() {
        var productName = $("#update-product-name").val();
        var productPrice = $("#change-product-price-price").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "ChangeProductPrice", productName: productName, productPrice: productPrice },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#change-product-price-price").val("");
            } else {
                $("#change-product-price-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeProductEmployeePrice() {
        var productName = $("#update-product-name").val();
        var employeePrice = $("#change-product-employee-price-price").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "ChangeProductEmployeePrice", productName: productName, employeePrice: employeePrice },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#change-product-employee-price-price").val("");
            } else {
                $("#change-product-employee-price-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeProductOnSalePrice() {
        var productName = $("#update-product-name").val();
        var onSalePrice = $("#change-product-on-sale-price-price").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "ChangeProductOnSalePrice", productName: productName, onSalePrice: onSalePrice },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#change-product-on-sale-price-price").val("");
            } else {
                $("#change-product-on-sale-price-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeProductImage() {
        var productName = $("#update-product-name").val();
        var file = document.getElementById('change-product-image-image').files[0];
        var data = new FormData();
        data.append("method", "ChangeProductImage");
        data.append("productName", productName);
        data.append("file", file);

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '//store.bnsstore.com/ControlPanel/', true);
        xhr.onload = function () {
            if (xhr.status === 200) {
                $("#change-product-image-image").val("");
            } else {
                $("#change-product-image-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        };
        xhr.send(data);
    }

    function addCategory() {
        var categoryName = $("#add-category-name").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "AddCategory", categoryName: categoryName },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#add-category-name").val("");
            } else {
                $("#add-category-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function delCategory() {
        var categoryName = $("#del-category-name").val();
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "DelCategory", categoryName: categoryName },
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#del-category-name").val("");
            } else {
                $("#del-category-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

    function changeCategoryName() {
        var categoryName = $("#change-category-name-name").val();
        var newCategoryName = $("#change-category-name-new-name").val();
        if (newCategoryName.trim() == "") {
            return;
        }
        $.ajax({
            type: "POST",
            url: "//store.bnsstore.com/ControlPanel/",
            data: { method: "ChangeCategoryName", categoryName: categoryName, newCategoryName: newCategoryName},
            dataType: "html",
            async: false
        }).done(function (data) {
            console.log(data);
            if (data != null && data != "" && data == "success") {
                $("#change-category-name-name").val("");
                $("#change-category-name-new-name").val("");
            } else {
                $("#change-category-name-button").velocity({ backgroundColor: "#ed3e18" }, 1000).velocity({ backgroundColor: "#62d317" }, 500);
            }
        });
    }

})();