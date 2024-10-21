const horariosOcupados = [
  "2024-09-10 09:00", "2024-09-10 10:00", "2024-09-10 11:00", 
  "2024-09-25 10:00", "2024-09-27 09:00", "2024-09-25 11:00"
];


function inicializarCalendario() {
  flatpickr("#datepicker", {
    locale: "es",
    dateFormat: "Y-m-d",
    minDate: "today",
    disable: [
      function(date) {
        return (date.getDay() === 6 || date.getDay() === 0);
      }
    ],
    onDayCreate: function(dObj, dStr, fp, dayElem) {
      const dateStr = dayElem.dateObj.toISOString().split('T')[0];
      const horariosDelDia = horariosOcupados.filter(h => h.startsWith(dateStr));

      
      if (horariosDelDia.length === 3) {
        dayElem.style.backgroundColor = "#dc3545"; 
        dayElem.style.color = "white";
        dayElem.classList.add('disabled'); 
      } else if (horariosDelDia.length === 2) {
        dayElem.style.backgroundColor = "#ff5733";
        dayElem.style.color = "white";
      } else if (horariosDelDia.length === 1) {
        dayElem.style.backgroundColor = "#ffc107";
        dayElem.style.color = "white";
      }
    }
  });
}

// Actualiza el selector de horas basado en la fecha seleccionada
function actualizarSelectorDeHoras(fechaSeleccionada) {
  const timepicker = document.getElementById("timepicker");
  
  // Limpiar opciones del selector
  timepicker.innerHTML = '';

  
  const horariosPosibles = ["09:00", "10:00", "11:00"];
  
  
  const horariosDelDia = horariosOcupados
  .filter(horario => horario.startsWith(fechaSeleccionada))
  .map(horario => {
    const [, hora] = horario.split(' ');
    return hora;
  });


  
  horariosPosibles.forEach(horario => {
    if (!horariosDelDia.includes(horario)) {
      const option = document.createElement("option");
      option.value = horario;
      option.textContent = horario;
      timepicker.appendChild(option);
    }
  });

  
  if (timepicker.options.length === 0) {
    const option = document.createElement("option");
    option.value = "";
    option.textContent = "No hay horarios disponibles";
    timepicker.appendChild(option);
  }
}


document.getElementById("confirm-button").addEventListener("click", function() {
  const fechaSeleccionada = document.getElementById("datepicker").value;
  const horaSeleccionada = document.getElementById("timepicker").value;

  if (!fechaSeleccionada) {
    alert("Por favor, selecciona una fecha");
    return;
  }

  if (!horaSeleccionada) {
    alert("Por favor, selecciona una hora disponible");
    return;
  }

  const fechaHora = `${fechaSeleccionada} ${horaSeleccionada}`;

  
  if (!horariosOcupados.includes(fechaHora)) {
    horariosOcupados.push(fechaHora);
    console.log("Horario confirmado:", fechaHora);

    inicializarCalendario();
    actualizarSelectorDeHoras(fechaSeleccionada);
  } else {
    alert("Este horario ya está ocupado. Elige otro.");
  }
});


document.getElementById("datepicker").addEventListener("change", function() {
  const fechaSeleccionada = this.value;
  actualizarSelectorDeHoras(fechaSeleccionada);
});


inicializarCalendario();
