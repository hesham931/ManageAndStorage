let NameValidation = /(^[a-z A-Z أ-ي]+)/;

document.getElementsByTagName("form")[0].addEventListener("submit", (el)=>{
    let Name = document.getElementById("Name"),
        Description = document.getElementById("Description"),
        NameOk = NameValidation.exec(Name.value),
        DescriptionOk = true;
    if(Description.value != "") DescriptionOk = NameValidation.exec(Description.value);

    if(!NameOk){
        el.preventDefault();
        Name.style = "border:solid red 3px;";
        Name.placeholder = "Not Vaild Name!";
        Name.value = "";
    }
    else{
        Name.style = "border:none;";
    }
    if(!DescriptionOk){
        el.preventDefault();
        Description.style = "border:solid red 3px;";
        Description.placeholder = "Not Vaild Description!";
        Description.value = "";
    }
    else{
        Description.style = "border:none;";
    }
}, false);