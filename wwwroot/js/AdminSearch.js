document.getElementById("AdminSearch").addEventListener('input', (e) => {
    SearchAdmin(e.target.value);
})

function SearchAdmin(searchStr) {

    var productsArea = document.getElementById("productListBody");
    if (productsArea)
        $.ajax({
            url: `/Products/PartialList?name=${searchStr}`,
            success: function (data) {
                productsArea.innerHTML = data;
                $('html,body').scrollTop(0);
            }
        });

    else
        window.location.replace(`/products/List?name=${searchStr}`);
}