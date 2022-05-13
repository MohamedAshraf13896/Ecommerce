
function CategoryFilter(categoryId) {
    console.log("Filter");
    var productsArea = document.getElementById("ProductsArea");
    $.ajax({
        url: "/Products/CategoryFilter/" + categoryId,
        success: function (data) {
            console.log("Get Data");
            console.log(data);
            productsArea.innerHTML = data;
        }
    });
}