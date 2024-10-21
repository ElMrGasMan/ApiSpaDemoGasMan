// Arreglos para preguntas y reseñas
const preguntas = [
    { pregunta: "¿El masaje relajante incluye aceites esenciales?", respuesta: "Hola! Sí, utilizamos aceites esenciales de alta calidad.", fecha: "22/08/2024" },
    { pregunta: "¿Tienen promociones para el Día de la Madre?", respuesta: "Hola! Sí, tenemos un paquete especial con un descuento del 20%.", fecha: "21/08/2024" },
    { pregunta: "¿Cuánto dura el tratamiento facial?", respuesta: "Hola! Aproximadamente 60 minutos.", fecha: "20/08/2024" },
    { pregunta: "¿Se pueden hacer reservas online?", respuesta: "Hola! Sí, puedes reservar directamente desde nuestra página web.", fecha: "19/08/2024" },
    { pregunta: "¿El spa ofrece servicios para parejas?", respuesta: "Hola! Claro, tenemos paquetes para parejas con descuentos especiales.", fecha: "18/08/2024" }
];

const reseñas = [
    { calificacion: "★★★★★", reseña: "El masaje de piedras calientes fue increíble, me dejó completamente relajado.", fecha: "14/08/2023" },
    { calificacion: "★★★★☆", reseña: "El ambiente del spa es muy acogedor, pero me gustaría que la sala de espera fuera más grande.", fecha: "10/08/2023" },
    { calificacion: "★★★★★", reseña: "El tratamiento facial me dejó la piel súper suave y luminosa.", fecha: "05/08/2023" },
    { calificacion: "★★★★☆", reseña: "La atención fue excelente, aunque el precio es un poco elevado.", fecha: "01/08/2023" }
];

// Variable global para almacenar la calificación seleccionada
let calificacionSeleccionada = 0;

// Añadir eventos a las estrellas para permitir la selección
document.querySelectorAll('.estrella').forEach(estrella => {
    estrella.addEventListener('click', function() {
        calificacionSeleccionada = parseInt(this.getAttribute('data-value'));
        actualizarEstrellas(calificacionSeleccionada);
    });
});

// Función para actualizar las estrellas visualmente
function actualizarEstrellas(calificacion) {
    const estrellas = document.querySelectorAll('.estrella');
    estrellas.forEach((estrella, index) => {
        if (index < calificacion) {
            estrella.classList.add('seleccionada');
        } else {
            estrella.classList.remove('seleccionada');
        }
    });
}

function actualizarListaPreguntas() {
    const listaPreguntas = document.getElementById('preguntas-lista');
    listaPreguntas.innerHTML = ''; // Limpiar la lista actual
    preguntas.forEach(pregunta => {
        const divPregunta = document.createElement('div');
        divPregunta.className = 'pregunta';
        divPregunta.innerHTML = `<p>${pregunta.pregunta}</p><p class="respuesta">${pregunta.respuesta} <span class="fecha">${pregunta.fecha}</span></p>`;
        listaPreguntas.appendChild(divPregunta);
    });
}

function actualizarListaReseñas() {
    const listaReseñas = document.getElementById('reseñas-lista');
    listaReseñas.innerHTML = ''; // Limpiar la lista actual
    reseñas.forEach(reseña => {
        const divReseña = document.createElement('div');
        divReseña.className = 'reseña';
        divReseña.innerHTML = `<div class="calificacion">${reseña.calificacion}</div><p>${reseña.reseña} <span class="fecha">${reseña.fecha}</span></p>`;
        listaReseñas.appendChild(divReseña);
    });
}

function agregarPregunta() {
    const preguntaInput = document.getElementById('nueva-pregunta');
    const preguntaTexto = preguntaInput.value;
    if (preguntaTexto) {
        const nuevaPregunta = { pregunta: preguntaTexto, respuesta: "Aún no hay respuesta", fecha: nuevaFecha() };
        preguntas.push(nuevaPregunta);
        actualizarListaPreguntas();
        preguntaInput.value = '';  // Limpiar el input
    }
}

function agregarReseña() {
    const reseñaInput = document.getElementById('nueva-reseña');
    const reseñaTexto = reseñaInput.value;
    
    if (reseñaTexto && calificacionSeleccionada > 0) {
        // Crear la nueva reseña con la cantidad de estrellas seleccionadas
        const nuevaReseña = {
            calificacion: '★'.repeat(calificacionSeleccionada) + '☆'.repeat(5 - calificacionSeleccionada),
            reseña: reseñaTexto,
            fecha: nuevaFecha()
        };
        reseñas.push(nuevaReseña);
        actualizarListaReseñas();
        reseñaInput.value = '';  // Limpiar el input
        calificacionSeleccionada = 0; // Reiniciar la calificación seleccionada
        actualizarEstrellas(0); // Restablecer el estilo de las estrellas
    }
}

function nuevaFecha() {
    const hoy = new Date();
    const dia = String(hoy.getDate()).padStart(2, '0');
    const mes = String(hoy.getMonth() + 1).padStart(2, '0'); // Meses empiezan en 0
    const año = hoy.getFullYear();
    return `${dia}/${mes}/${año}`;
}

// Inicializar las listas al cargar la página
window.onload = function() {
    actualizarListaPreguntas();
    actualizarListaReseñas();
    actualizarEstrellas(0);  // Inicializar estrellas sin ninguna seleccionada
};
// Función para abrir el modal con el contenido adecuado
function abrirModal(tipo) {
    const modal = document.getElementById('modal');
    const modalBody = document.getElementById('modal-body');
    
    // Mostrar preguntas o reseñas en el modal
    if (tipo === 'preguntas') {
      modalBody.innerHTML = document.getElementById('preguntas-lista').innerHTML;
    } else if (tipo === 'reseñas') {
      modalBody.innerHTML = document.getElementById('reseñas-lista').innerHTML;
    }
    
    modal.style.display = 'block';
    
    // Deshabilitar el scroll de la página principal
    document.body.classList.add('no-scroll');
  }
  
  // Función para cerrar el modal
  function cerrarModal() {
    const modal = document.getElementById('modal');
    modal.style.display = 'none';
    
    // Habilitar el scroll de la página principal
    document.body.classList.remove('no-scroll');
  }
  
  // Cerrar modal al hacer clic fuera de él
  window.onclick = function(event) {
    const modal = document.getElementById('modal');
    if (event.target == modal) {
      cerrarModal();
    }
  }
// Guardar la posición del scroll
let scrollPosition = 0;

// Limitar las preguntas y reseñas que se muestran
function limitarPreguntasYReseñas() {
    const maxPreguntas = 5;
    const maxReseñas = 4;

    // Limitar preguntas
    const preguntasLista = document.querySelectorAll('#preguntas-lista li');
    if (preguntasLista.length > maxPreguntas) {
        for (let i = maxPreguntas; i < preguntasLista.length; i++) {
            preguntasLista[i].style.display = 'none';
        }
        document.getElementById('ver-mas-preguntas').style.display = 'block';
    }

    // Limitar reseñas
    const reseñasLista = document.querySelectorAll('#reseñas-lista li');
    if (reseñasLista.length > maxReseñas) {
        for (let i = maxReseñas; i < reseñasLista.length; i++) {
            reseñasLista[i].style.display = 'none';
        }
        document.getElementById('ver-mas-resenas').style.display = 'block';
    }
}

// Función para abrir el modal
function abrirModal(tipo) {
    const modal = document.getElementById('modal');
    const modalBody = document.getElementById('modal-body');

    // Guardar la posición del scroll
    scrollPosition = window.pageYOffset;

    // Mostrar preguntas o reseñas en el modal
    if (tipo === 'preguntas') {
        modalBody.innerHTML = document.getElementById('preguntas-lista').innerHTML;
    } else if (tipo === 'reseñas') {
        modalBody.innerHTML = document.getElementById('reseñas-lista').innerHTML;
    }

    modal.style.display = 'block';

    // Deshabilitar scroll de la página
    document.body.classList.add('no-scroll');
}

// Función para cerrar el modal
function cerrarModal() {
    const modal = document.getElementById('modal');
    modal.style.display = 'none';

    // Habilitar scroll en la página principal
    document.body.classList.remove('no-scroll');

    // Redirigir a la sección de preguntas y reseñas
    window.location.href = '#pregunta_y_resenas';
}

// Cerrar el modal cuando se hace clic fuera del contenido
window.onclick = function(event) {
    const modal = document.getElementById('modal');
    if (event.target === modal) {
        cerrarModal();
    }
}

// Ejecutar la función al cargar la página
document.addEventListener('DOMContentLoaded', limitarPreguntasYReseñas);
