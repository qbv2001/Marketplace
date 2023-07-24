var buttonPlus = document.getElementById("button-plus");
var buttonMinus = document.getElementById("button-minus");
var quantityInput = document.getElementById("quantity");

buttonPlus.addEventListener("click", function() {
    var currentValue = parseInt(quantityInput.value);
    quantityInput.value = currentValue + 1;
});

buttonMinus.addEventListener("click", function() {
    var currentValue = parseInt(quantityInput.value);
    quantityInput.value = currentValue - 1;
});

// Lắng nghe sự kiện click vào liên kết "Thêm Vào Giỏ Hàng"
document.getElementById("cartadd").addEventListener("click", function() {
    // Lấy ID sản phẩm từ thuộc tính data-product-id
    var productId = this.getAttribute("data-product-id");
    var addcarturl = this.getAttribute("data-addcart");

    $.ajax({
        type: "POST",
        url: addcarturl,
        data: {
            productId: productId,quantity: 1
        },
        dataType: "json",
        success: function (data) {
            alert("Sản phẩm đã được thêm vào giỏ hàng!");
            console.log(data);
        },
        error: function (error) {
            var url = `/login`;
            window.location.href = url;
        }
    });

});