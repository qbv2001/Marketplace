var currentPage = 2;
var itemsPerPage = 1; // Số lượng sản phẩm hiển thị trong mỗi lần tải thêm
var loadMoreUrl = document.currentScript.getAttribute("data-loadmoreurl");

function loadMoreProducts() {
    $.ajax({
        type: "GET",
        url: loadMoreUrl,
        data: {
            page: currentPage,
            itemsPerPage: itemsPerPage
        },
        dataType: "json",
        success: function (data) {
            if (data.length > 0) {
                currentPage++;
                displayProducts(data);
            } else {
                // Ẩn nút "Xem thêm" nếu không còn sản phẩm để tải
                $("#button-plus").hide();
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function displayProducts(products) {
    var productContainer = $("#productContainer");

    products.forEach(function (product) {
        var productHtml = `<div class="col-xl-2 col-lg-3 col-md-4 col-6">
                                <div class="card card-sm card-product-grid">
                                    <a href="detailproduct?spid=${product.productId}" class="img-wrap">
                                        <img src="${product.imageUrl}">
                                    </a>
                                    <figcaption class="info-wrap">
                                        <a href="detailproduct" class="title">${product.name}</a>
                                        <div class="price mt-1">${product.price} đ</div>
                                    </figcaption>
                                </div>
                            </div>`;
        productContainer.append(productHtml);
    });
}

// Bắt sự kiện khi nhấn nút "Xem thêm"
$("#button-plus").click(function () {
    loadMoreProducts();
});