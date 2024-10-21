document.getElementById('formAgregarServicio').addEventListener('submit', async function(e) {
    e.preventDefault(); // Prevenir el comportamiento por defecto del formulario

    const usuarioId = document.getElementById('usuarioId').value;
    const titulo = document.getElementById('titulo').value;
    const tipoServicio = document.getElementById('tipoServicio').value;
    const descripcion = document.getElementById('descripcion').value;
    const rutaImagen = document.getElementById('rutaImagen').files[0]; // Obtener archivo de imagen
    const duracionMinut = document.getElementById('duracionMinut').value;
    const precio = document.getElementById('precio').value;
    const tiempoLimiteHoras = document.getElementById('tiempoLimiteHoras').value;

    // Simulamos que los archivos se suben a una carpeta "uploads" en el front-end
    const rutaImagenGuardada = `./sources/Servicios/${rutaImagen.name}`;

    const nuevoServicio = {
        usuarioId: usuarioId,
        titulo: titulo,
        tipoServicio: tipoServicio,
        descripcion: descripcion,
        rutaImagen: rutaImagenGuardada,
        duracionMinut: parseInt(duracionMinut),
        precio: parseFloat(precio),
        tiempoLimiteHoras: parseInt(tiempoLimiteHoras) 
    };

    try {
        const response = await fetch('https://apispademo.somee.com/api/Servicio', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'accept': '*/*'
            },
            body: JSON.stringify(nuevoServicio)
        });

        if (!response.ok) {
            throw new Error('Error al agregar el servicio');
        }

        alert('Servicio agregado exitosamente');
        window.location.href = 'indexPersonal.html'; // Redirigir a la página principal después de agregar
    } catch (error) {
        console.error('Error al agregar el servicio:', error);
        alert('Ocurrió un error al intentar agregar el servicio.');
    }
});
