
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



function search() {
    let searchName = document.getElementById("serchPro2").value;
    var productsArea = document.getElementById("ProductsArea");
    $.ajax({
        url: `/Products/NameFilter?name=` + searchName,
        success: function (data) {
            productsArea.innerHTML = data;
        }
    });
}


let searchName = document.getElementById("serchPro2").addEventListener('input', (e) => {

    console.log(e.target.value)
    var productsArea = document.getElementById("ProductsArea");
    $.ajax({
        url: `/Products/NameFilter?name=` + e.target.value,
        success: function (data) {
            productsArea.innerHTML = data;
        }
    });
})


function PagiNameFilter(searchStr, pageN) {
    console.log(searchStr, pageN);
    var productsArea = document.getElementById("ProductsArea");
    $.ajax({
        url: `/Products/NameFilter?name=${searchStr}&page=${pageN}`,
        success: function (data) {
            productsArea.innerHTML = data;
            $('html,body').scrollTop(0);
        }
    });
}