/** Verificar si la contraseña y la confirmación de contraseñas coinciden. */
function checkPasswordsMatch() {
    const firstPass = document.querySelector("#txtFirstPass");
    const secondPass = document.querySelector("#txtSecondPass");
    const errorAlert = document.querySelector("#lbPassAlert");
    const btnSubmit = document.querySelector("#btnSubmit");

    if (firstPass.value !== "" && secondPass.value !== "") {
        if (firstPass.value !== secondPass.value) {
            firstPass.className = "form-control border border-danger border-3";
            secondPass.className = "form-control border border-danger border-3";
            errorAlert.style.display = "block";
            btnSubmit.setAttribute("disabled", "true");
        }
        else {
            firstPass.className = "form-control border border-success border-3";
            secondPass.className = "form-control border border-success border-3";
            errorAlert.style.display = "none";
            btnSubmit.removeAttribute("disabled");
        }
    }
    else {
        firstPass.className = "form-control border border-danger border-3";
        secondPass.className = "form-control border border-danger border-3";
        errorAlert.style.display = "block";
        btnSubmit.setAttribute("disabled", "true");
    }
}
