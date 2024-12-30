function toggleCalendar() {
    var calendar = document.getElementById('<%= Calendar1.ClientID %>');

    // Alternar la visibilidad del calendario
    if (calendar.style.display === "none") {
        calendar.style.display = "block";  // Mostrar calendario
    } else {
        calendar.style.display = "none";   // Ocultar calendario
    }
}
