var buttonPlusList = document.querySelectorAll('.button-plus');
var buttonMinusList = document.querySelectorAll('.button-minus');
var quantityInputList = document.querySelectorAll('.quantity');
var priceElementList = document.querySelectorAll('.price');
var totalPriceElement = document.querySelector('.total-price');
// var totalpriceElement2 = document.getElementById("total-price");
var totalpriceElementList = document.querySelectorAll('.totalprice');

// Tạo hàm tính tổng giá cho một sản phẩm
function calculateProductPrice(quantity, price) {
return quantity * price;
}

// Tạo hàm tính tổng giá cho toàn bộ danh sách sản phẩm
function calculateTotalPrice() {
var totalPrice = 0;

for (var i = 0; i < quantityInputList.length; i++) {
    var quantity = parseInt(quantityInputList[i].value);
    var price = parseFloat(priceElementList[i].innerText);
    var productPrice = calculateProductPrice(quantity, price);
    totalpriceElementList[i].innerText = productPrice + " ₫";
    totalPrice += productPrice;
}

totalPriceElement.innerText = totalPrice + " ₫";
// totalpriceElement2.innerText = totalPrice + " ₫";
}

// Đăng ký sự kiện khi nhấn nút +
buttonPlusList.forEach(function (button) {
button.addEventListener('click', function () {
    var input = button.parentNode.parentNode.querySelector('.quantity');
    var currentValue = parseInt(input.value);
    input.value = currentValue + 1;
    calculateTotalPrice();
});
});

// Đăng ký sự kiện khi nhấn nút -
buttonMinusList.forEach(function (button) {
button.addEventListener('click', function () {
    var input = button.parentNode.parentNode.querySelector('.quantity');
    var currentValue = parseInt(input.value);

    if (currentValue > 1) {
    input.value = currentValue - 1;
    calculateTotalPrice();
    }
});
});

// Đăng ký sự kiện khi thay đổi số lượng sản phẩm
quantityInputList.forEach(function (input) {
input.addEventListener('change', calculateTotalPrice);
});

// Xử lý khi ấn click vào nút "Xóa"
document.addEventListener('click', function (event) {

    // Kiểm tra xem người dùng đã click vào nút "Xóa" chưa
    if (event.target.classList.contains('delete-button')) {
        // Lấy productId từ thuộc tính dữ liệu (data attribute) của nút "Xóa"
        var productId = event.target.getAttribute('data-productid');
        var urldelete = event.target.getAttribute("data-urldelete");

        // Xác nhận xóa sản phẩm
        if (confirm('Bạn có chắc muốn xóa sản phẩm khỏi giỏ hàng không?')) {
            // Gửi yêu cầu xóa sản phẩm tới server sử dụng AJAX
            $.ajax({
                type: "POST",
                url: urldelete,
                data: {
                    productId: productId
                },
                dataType: "json",
                success: function () {
                    // Xóa sản phẩm khỏi giao diện tại phía client nếu xóa thành công
                    $(event.target).closest('tr').remove();
                    console.log('thành công');
                  },
                  error: function () {
                    // Xử lý phản hồi thất bại (nếu cần)
                    alert('Có lỗi xảy ra khi xóa sản phẩm khỏi giỏ hàng.');
                  },
            });
        }
    }
});