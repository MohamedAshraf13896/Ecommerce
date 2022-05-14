

let sessionUserProducts = [];

if (!localStorage.getItem('CartProductList'))
    getCartFromClaim();
else {
    sessionUserProducts = JSON.parse(localStorage.getItem('CartProductList'));
}

let elm = document.getElementById('con');


LoadModalContent();
 
function AddTOcart(productId, proName, proPrice ,proImg) {
  
    let foundProduct = sessionUserProducts.find(p => p.Id == productId)
    if (foundProduct) {
        foundProduct.qun = ++foundProduct.qun;
    }
    else {

        sessionUserProducts.push({ Id: +productId, qun: 1, Name: proName, price: +proPrice, img: proImg });
    }

    ////save to local storage
    localStorage.setItem("CartProductList", JSON.stringify(sessionUserProducts));
    document.getElementById('cartItemNumber').innerText = CartTotalItem();
    ShowSnake();
    LoadModalContent();
}

function LoadModalContent() {
    elm.innerHTML = '';
    if (sessionUserProducts.length!=0)
        for (let product of sessionUserProducts) {
            elm.innerHTML += CartItem(product)
        }
    else
        elm.innerHTML = '<h1> GO and Buy somthing 😘 </h1> ' ;
    document.getElementById('cartItemNumber').innerText = CartTotalItem();

}

function deleteFromCart(proId) {

    deleteProduct(proId);
    LoadModalContent();
    document.getElementById('cartItemNumber').innerText = CartTotalItem();
    //
    localStorage.setItem("CartProductList", JSON.stringify(sessionUserProducts));

}

function ChangeQun(id, opration) {
    let QunElem = document.getElementById(id);
    let QunElemDetails = document.getElementById('DetailsCounter');
    let PriceElem = document.getElementById(id + '-');
    let Qun = +QunElem.innerText;
    switch (opration) {
        case '+':
            QunElem.innerText = ++Qun;
            break;
        case '-':
            if (Qun == 0)
                return
            QunElem.innerText = --Qun;
            break;
    }
    UpdateProductQun(id, Qun);
    PriceElem.innerText = '$' + getProduct(id).price * Qun;
    if (QunElemDetails)
        QunElemDetails.value = Qun;
    //
    localStorage.setItem("CartProductList", JSON.stringify(sessionUserProducts));
    document.getElementById('cartItemNumber').innerText = CartTotalItem();


}


function UpdateProductQun(proID, qun) {
    sessionUserProducts = sessionUserProducts.map((obj) => {
        if (obj.Id == proID)
            return {
                ...obj,
                qun: qun
            }
        return obj;
    })
}

function getProduct(proId) {
    return sessionUserProducts.find(p => p.Id == proId)
}

function deleteProduct(proId) {
    sessionUserProducts = sessionUserProducts.filter((obj) => {
        return obj.Id != proId;
    })
    
}

function GotToLogOut() {
    let userCart = localStorage.getItem("CartProductList") || "[]"
    //$.ajax({
    //    url: "/Account/logout?cart=" + userCart
    //});

    //clear local 
    window.location.replace(`/Account/logout?cart=${userCart}`);
    localStorage.removeItem('CartProductList');
}
function CheckOut() {
    $('#exampleModal').modal('hide');
    let userCart = localStorage.getItem("CartProductList") || ""
    window.location.replace(`/Orders/Create?cart=${userCart}`);
    //$.ajax({
    //    url: "/Orders/Create?cart=" + userCart,
    //    success: function (dtat) {
    //        console.log(data);
    //    }
    //});
}

function getCartFromClaim() {
    
    fetch("/account/getUserCartClaim")
        .then(response => response.json())
        .then(data => {
            sessionUserProducts = JSON.parse(data)
            // cart for not loged in users
            if (sessionUserProducts.length == 0 && localStorage.getItem('CartProductList') )
                sessionUserProducts = JSON.parse(localStorage.getItem('CartProductList'));
            LoadModalContent();
            document.getElementById('cartItemNumber').innerText = sessionUserProducts.length;

            localStorage.setItem("CartProductList", JSON.stringify(sessionUserProducts));

        })
        .catch(err => console.log(err))

    
}

// snakebar 

function ShowSnake() {
    console.log("show");
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 2000);
}


//cart item 

function CartItem(product) {
    return (
        `
               <hr class="my-4">
                  <div class="row mb-4 d-flex justify-content-between align-items-center">
                    <div class="col-md-2 col-lg-2 col-xl-2">
                      <img
                        src="/assets/img/product/${product.img}"
                        class="img-fluid rounded-3" alt="Cotton T-shirt">
                    </div>
                    <div class="col-md-3 col-lg-3 col-xl-3">
                      <h6 class="text-muted">${product.Name}</h6>

                    </div>
                    <div class="col-md-3 col-lg-3 col-xl-2 d-flex">
                      <button class="btn btn-link px-2"
                        onclick="ChangeQun('${product.Id}','-')">
                        <i class="fas fa-minus"></i>
                      </button>
                            <span id="${product.Id}" style="font-size: x-large;font-weight: 600;">${product.qun}</span>
                 
                  
                      <button class="btn btn-link px-2"
                        onclick="ChangeQun('${product.Id}','+')">
                        <i class="fas fa-plus"></i>
                      </button>
                    </div>
                    <div class="col-md-3 col-lg-2 col-xl-2 offset-lg-1">
                      <h6 id="${product.Id}-" class="mb-0">$${product.price * product.qun}</h6>
                    </div>
                    <div class="col-md-1 col-lg-1 col-xl-1 text-end">
                    <span class="iconify"  onclick="deleteFromCart('${product.Id}')" data-icon="ep:delete-filled" style="color: red;"></span>
                    </div>
                  </div>

`
        )
}
function CartTotalItem() {
    let totalProduct = 0;
    sessionUserProducts.map((i) => { totalProduct+= i.qun })

    return totalProduct;
}
