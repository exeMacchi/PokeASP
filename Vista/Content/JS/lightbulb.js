const lightbulbIcons = document.querySelectorAll(".bi-lightbulb");

lightbulbIcons.forEach(lightbulb => {
    lightbulb.addEventListener("mouseenter", () => {
        lightbulb.classList.replace("bi-lightbulb", "bi-lightbulb-fill");
    });

    lightbulb.addEventListener("mouseleave", () => {
        lightbulb.classList.replace("bi-lightbulb-fill", "bi-lightbulb");
    });
})

