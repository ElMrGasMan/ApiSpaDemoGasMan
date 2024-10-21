document.getElementById('formAgregarNoticia').addEventListener('submit', async function(e) {
    e.preventDefault(); // Prevenir el comportamiento por defecto del formulario

    const titulo = document.getElementById('titulo').value;
    const fechaPublicacion = new Date().toISOString().split('T')[0]; // Fecha actual en formato YYYY-MM-DD

    const rutaImagen = document.getElementById('rutaImagen').files[0]; // Obtener archivo de imagen
    const rutaPDF = document.getElementById('rutaPDF').files[0]; // Obtener archivo PDF

    // Simulamos que los archivos se suben a una carpeta "uploads" en el front-end
    const rutaImagenGuardada = `./sources/Noticias/${rutaImagen.name}`;
    const rutaPDFGuardada = `./sources/Noticias/${rutaPDF.name}`;
    
    // Verificamos que los archivos se hayan seleccionado correctamente
    if (!rutaImagen || !rutaPDF) {
        alert("Por favor, selecciona una imagen y un PDF.");
        return;
    }

    // Enviar los datos de la nueva noticia
    const nuevaNoticia = {
        titulo: titulo,
        fechaPublicacion: fechaPublicacion,
        rutaImagen: rutaImagenGuardada,
        rutaPDF: rutaPDFGuardada
    };

    try {
        const response = await fetch('https://apispademo.somee.com/api/Noticia', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevaNoticia)
        });

        if (!response.ok) {
            throw new Error('Error al agregar la noticia');
        }

        alert('Noticia agregada exitosamente');
        window.location.href = 'indexPersonal.html'; // Redirigir a la página principal después de agregar
    } catch (error) {
        console.error('Error al agregar la noticia:', error);
        alert('Ocurrió un error al intentar agregar la noticia.');
    }
});
