/**
 * Intercambiar el modo de vista de un input type="password".
 * @param {String} inputID Cadena que representa el 'id' del elemento HTML TextBox/input.
 * @param {String} buttonID Cadena que repersenta el 'id' del elemento HTML button.
 */
function togglePass(inputID, buttonID) {
    const passInput = document.querySelector(`#${inputID}`);
    const button = document.querySelector(`#${buttonID}`);
    const icon = document.querySelector(`#${buttonID} > i`);

    if (passInput.type === 'password') {
        passInput.type = 'text';
        icon.classList.replace("bi-eye-slash", "bi-eye");
        button.classList.replace("bg-light", "bg-dark-subtle")
    } else {
        passInput.type = 'password';
        icon.classList.replace("bi-eye", "bi-eye-slash");
        button.classList.replace("bg-dark-subtle", "bg-light")
    }
}
