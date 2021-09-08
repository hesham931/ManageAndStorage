let NameValidation = /(^[a-z A-Z أ-ي]+)/,
    OthersValidation = /(^[0-9]+)/;

document.getElementsByTagName("form")[0].addEventListener("submit", (el)=>{
    let Name = document.getElementById("Name"),
        SalePrice = document.getElementById("SalePrice"),
        BuyingPrice = document.getElementById("BuyingPrice"),
        AvilableItems = document.getElementById("AvilableItems"),


        NameOk = NameValidation.exec(Name.value),
        SalePriceOk = OthersValidation.exec(SalePrice.value),
        BuyingPriceOk = OthersValidation.exec(BuyingPrice.value),
        AvilableItemsOk = OthersValidation.exec(AvilableItems.value);

    if(!NameOk){
        el.preventDefault();
        Name.style = "border-bottom:solid red 3px;";
        Name.placeholder = "Not Vaild Name!";
        Name.value = "";
    }
    else{
        Name.style = "border:none;";
    }
    if(!SalePriceOk){
        el.preventDefault();
        SalePrice.style = "border-bottom:solid red 3px;";
        SalePrice.placeholder = "Not Vaild Sale Price!";
        SalePrice.value = "";
    }
    else{
        SalePrice.style = "border:none;";
    }
    if(!BuyingPriceOk){
        el.preventDefault();
        BuyingPrice.style = "border-bottom:solid red 3px;";
        BuyingPrice.placeholder = "Not Vaild Buying Price!";
        BuyingPrice.value = "";
    }
    else{
        BuyingPrice.style = "border:none;";
    }
    if(!AvilableItemsOk){
        el.preventDefault();
        AvilableItems.style = "border-bottom:solid red 3px;";
        AvilableItems.placeholder = "Not Vaild Avilable Items!";
        AvilableItems.value = "";
    }
    else{
        AvilableItems.style = "border:none;";
    }
}, false);