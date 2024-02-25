/** Verificar si la contraseña y la confirmación de contraseñas coinciden. */
function checkPasswordsMatch() {
    const firstPass = document.querySelector("#txtFirstPass");
    const secondPass = document.querySelector("#txtSecondPass");
    const errorAlert = document.querySelector("#lbPassAlert");

    if (firstPass.value !== "" && secondPass.value !== "") {
        if (firstPass.value !== secondPass.value) {
            firstPass.className = "form-control border border-danger border-3";
            secondPass.className = "form-control border border-danger border-3";
            errorAlert.style.display = "block";
            document.querySelector("#btnSubmit").setAttribute("disabled", "true");
        }
        else {
            firstPass.className = "form-control border border-success border-3";
            secondPass.className = "form-control border border-success border-3";
            errorAlert.style.display = "none";
            verifyInformation();
        }
    }
    else {
        firstPass.className = "form-control border border-danger border-3";
        secondPass.className = "form-control border border-danger border-3";
        errorAlert.style.display = "block";
        document.querySelector("#btnSubmit").setAttribute("disabled", "true");
    }
}

/** Verificar desde el cliente si se debe habilitar el botón de registro. */
function verifyInformation() {
    const nickname = document.querySelector("#txtNick").value;
    const email = document.querySelector("#txtEmail").value;
    const firstPass = document.querySelector("#txtFirstPass").value;
    const secondPass = document.querySelector("#txtSecondPass").value;
    const btnSubmit = document.querySelector("#btnSubmit");

    if (nickname !== "" && email !== "" && firstPass !== "" && secondPass != "") {
        btnSubmit.removeAttribute("disabled");
    }
    else {
        btnSubmit.setAttribute("disabled", "true");
    }
}
