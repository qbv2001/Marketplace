// Lấy thẻ select box bằng id
var sortOption = document.getElementById('sortOption');

// Gán sự kiện khi thay đổi giá trị của select box
sortOption.addEventListener('change', function() {
    // Chuyển trang
    var urlaction = this.getAttribute("data-urlaction");
    urlaction += "&sortby="+sortOption.value;
    console.log(urlaction);
    window.location.href = urlaction;
});