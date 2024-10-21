const paginas =[
    {
        tituloServicio: "Masajes",

        imagen_Ficha1: "/sources/Servicios/Servicios/masajesAntiStress.jpg",
        titulo_Ficha1: "Anti-stress",
        descripcion_Ficha1: "Sumérgete en un oasis de calma con nuestro masaje anti-estrés, diseñado para deshacerte de las tensiones diarias. Disfruta de movimientos suaves y envolventes que revitalizan tu cuerpo y mente, dejándote en un estado de total relajación.",

        imagen_Ficha2: "/sources/Servicios/Servicios/masajesDecontracturantes.jpg",
        titulo_Ficha2: "Descontracturantes",
        descripcion_Ficha2: "Libérate de las contracturas y el malestar muscular con nuestro masaje descontracturante. Con técnicas específicas para aliviar tensiones, sentirás cómo el dolor se disuelve y tu cuerpo recupera su movilidad y confort.",

        imagen_Ficha3: "/sources/Servicios/Servicios/masajesConPiedrasCalientes.jpg",
        titulo_Ficha3: "Masajes con piedras calientes",
        descripcion_Ficha3: "Experimenta el lujo de un masaje con piedras calientes, donde el calor terapéutico de las piedras volcánicas se combina con movimientos experta para aliviar el estrés y mejorar tu circulación. Perfecto para una sensación de bienestar profundo.",

        imagen_Ficha4: "/sources/Servicios/Servicios/masajesCirculatorios.jpg",
        titulo_Ficha4: "Circulatorios",
        descripcion_Ficha4: "Dale a tu cuerpo el impulso que necesita con nuestro masaje circulatorio. Diseñado para mejorar la circulación sanguínea y linfática, este masaje alivia la pesadez en las piernas y rejuvenece todo tu ser.",
    },
    {
        tituloServicio: "Belleza",

        imagen_Ficha1: "/sources/Servicios/Servicios/liftingDePestanias.jpeg",
        titulo_Ficha1: "Lifting de pestaña",
        descripcion_Ficha1: "Realza tu mirada con nuestro lifting de pestañas. Obtén unas pestañas naturalmente curvadas y voluminosas que destacan tus ojos sin necesidad de extensiones. Ideal para un look fresco y radiante todos los días.",

        imagen_Ficha2: "/sources/Servicios/Servicios/depilacionFacial.jpg",
        titulo_Ficha2: "Depilación facial",
        descripcion_Ficha2: "Disfruta de una piel suave y libre de vello con nuestra depilación facial. Utilizamos técnicas precisas para eliminar el vello de manera eficiente, dejándote con una piel perfectamente limpia y suave.",

        imagen_Ficha3: "/sources/Servicios/Servicios/bellezaDeManosYPies.jpg",
        titulo_Ficha3: "Belleza de manos y pies",
        descripcion_Ficha3: "Mimamos tus manos y pies con nuestro tratamiento completo de manicura y pedicura. Desde el limado hasta el esmaltado, te garantizamos una experiencia rejuvenecedora que deja tus extremidades radiantes y perfectamente cuidadas.",

        imagen_Ficha4: "",
        titulo_Ficha4: "",
        descripcion_Ficha4: "",
    },
    {
        tituloServicio: "Tratamientos Faciales",

        imagen_Ficha1: "/sources/Servicios/Servicios/puntaDeDiamante.jpg",
        titulo_Ficha1: "Punta de Diamante (Microexfoliación)",
        descripcion_Ficha1: "Rejuvenece tu piel con nuestra microexfoliación de punta de diamante. Este tratamiento elimina las células muertas y promueve una piel fresca y luminosa, dejando tu rostro con un aspecto renovado y radiante.",

        imagen_Ficha2: "/sources/Servicios/Servicios/hidratacionFacial.jpg",
        titulo_Ficha2: "Limpieza profunda + Hidratación",
        descripcion_Ficha2: "Revitaliza tu rostro con nuestra limpieza profunda combinada con una hidratación intensiva. Elimina impurezas y proporciona una hidratación profunda, devolviendo a tu piel su vitalidad y frescura.",

        imagen_Ficha3: "/sources/Servicios/Servicios/criofrecuenciaFacial.jpg",
        titulo_Ficha3: "Criofrecuencia facial",
        descripcion_Ficha3: "Experimenta un lifting facial instantáneo con nuestra criofrecuencia. Esta innovadora tecnología de frío crea un efecto térmico que reafirma y tonifica tu piel, brindando resultados visibles y rejuvenecedores al instante.",

        imagen_Ficha4: "",
        titulo_Ficha4: "",
        descripcion_Ficha4: "",
    },
    {
        tituloServicio: "Tratamientos Corporales",

        imagen_Ficha1: "/sources/Servicios/Servicios/velaslim.jpg",
        titulo_Ficha1: "VelaSlim",
        descripcion_Ficha1: "Redefine tu figura con nuestro tratamiento VelaSlim, que combina radiofrecuencia, infrarrojos y succión mecánica para reducir la celulitis y la circunferencia corporal. Ideal para lograr una piel más firme y esbelta.",

        imagen_Ficha2: "/sources/Servicios/Servicios/dermohealth.jpg",
        titulo_Ficha2: "DermoHealth",
        descripcion_Ficha2: "Despierta la vitalidad de tu piel con nuestro tratamiento DermoHealth. Este innovador tratamiento estimula la microcirculación y realiza un drenaje linfático, ayudando a movilizar los tejidos y mejorar la apariencia general de la piel.",

        imagen_Ficha3: "/sources/Servicios/Servicios/criofrecuenciaCorporal.jpg",
        titulo_Ficha3: "Criofrecuencia corporal",
        descripcion_Ficha3: "Disfruta de un lifting corporal con nuestra criofrecuencia, que utiliza calor y frío para reafirmar tu piel y reducir la flacidez. Obtén resultados visibles con un tratamiento que rejuvenece tu cuerpo al instante.",

        imagen_Ficha4: "/sources/Servicios/Servicios/ultracavitacion.jpg",
        titulo_Ficha4: "Ultracavitación",
        descripcion_Ficha4: "Logra una figura esculpida con nuestra técnica de ultracavitación. Utilizamos ultrasonido para desintegrar las células de grasa y reducir medidas, ofreciendo una solución efectiva para la reducción corporal y la celulitis.",
    },
];

function irAPagina(indice) {
    localStorage.setItem('indiceServicio', indice); // Guardar el índice seleccionado
    window.location.href = "/tipoDeServicios.html"; // Ir a la siguiente página
}
console.log(localStorage.getItem('indiceServicio'));

document.addEventListener('DOMContentLoaded', function() {
    const indice = localStorage.getItem('indiceServicio'); // Recuperar el índice almacenado
    console.log("Índice recuperado:", indice);
    if (indice !== null) {
        cargarPagina(indice); // Llamar a la función cargarPagina con el índice recuperado
    }
});

function cargarPagina(indice) {
    const ficha1 = document.getElementById("ficha1");
    const ficha2 = document.getElementById("ficha2");
    const ficha3 = document.getElementById("ficha3");
    const ficha4 = document.getElementById("ficha4");
    const tituloServicio = document.getElementById("tituloServicio");

    console.log(document.getElementById("tituloServicio"));

    const imagen_Ficha1 = document.getElementById("imagen_Ficha1");
    const titulo_Ficha1 = document.getElementById("titulo_Ficha1");
    const descripcion_Ficha1 = document.getElementById("descripcion_Ficha1");

    console.log(imagen_Ficha1, titulo_Ficha1, descripcion_Ficha1);

    const imagen_Ficha2 = document.getElementById("imagen_Ficha2");
    const titulo_Ficha2 = document.getElementById("titulo_Ficha2");
    const descripcion_Ficha2 = document.getElementById("descripcion_Ficha2");

    console.log(imagen_Ficha2, titulo_Ficha2, descripcion_Ficha2);

    const imagen_Ficha3 = document.getElementById("imagen_Ficha3");
    const titulo_Ficha3 = document.getElementById("titulo_Ficha3");
    const descripcion_Ficha3 = document.getElementById("descripcion_Ficha3");

    console.log(imagen_Ficha3, titulo_Ficha3, descripcion_Ficha3);

    const imagen_Ficha4 = document.getElementById("imagen_Ficha4");
    const titulo_Ficha4 = document.getElementById("titulo_Ficha4");
    const descripcion_Ficha4 = document.getElementById("descripcion_Ficha4");

    console.log(imagen_Ficha4, titulo_Ficha4, descripcion_Ficha4);

    // Asignar los valores de la página seleccionada
    const servicio = paginas[indice];
    // tituloServicio.textContent = servicio.tituloServicio;

    imagen_Ficha1.src = servicio.imagen_Ficha1;
    titulo_Ficha1.textContent = servicio.titulo_Ficha1;
    descripcion_Ficha1.textContent = servicio.descripcion_Ficha1;

    imagen_Ficha2.src = servicio.imagen_Ficha2;
    titulo_Ficha2.textContent = servicio.titulo_Ficha2;
    descripcion_Ficha2.textContent = servicio.descripcion_Ficha2;

    imagen_Ficha3.src = servicio.imagen_Ficha3;
    titulo_Ficha3.textContent = servicio.titulo_Ficha3;
    descripcion_Ficha3.textContent = servicio.descripcion_Ficha3;


    if (servicio.imagen_Ficha4 === "" && servicio.titulo_Ficha4 === "" && servicio.descripcion_Ficha4 === "") {
        // Eliminar ficha 4 del DOM si está vacía
        ficha4.remove();
        // Centrar ficha 3
        ficha3.style.margin = "auto"; // Centramos horizontalmente
        ficha3.classList.add("full-width");
    } else {
        // Mostrar ficha 4 si tiene contenido
        ficha4.style.display = "block";
        imagen_Ficha4.src = servicio.imagen_Ficha4;
        titulo_Ficha4.textContent = servicio.titulo_Ficha4;
        descripcion_Ficha4.textContent = servicio.descripcion_Ficha4;
        // Asegurar que la ficha 3 no esté centrada si hay 4 fichas
        ficha3.style.margin = "0";
    }


    // imagen_Ficha4.src = servicio.imagen_Ficha4;
    // titulo_Ficha4.textContent = servicio.titulo_Ficha4;
    // descripcion_Ficha4.textContent = servicio.descripcion_Ficha4;
   
}
