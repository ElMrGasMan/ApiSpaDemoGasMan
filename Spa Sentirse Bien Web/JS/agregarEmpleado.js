document.getElementById('formAgregarEmpleado').addEventListener('submit', async function(e) {
    e.preventDefault(); // Prevenir el comportamiento por defecto del formulario

    const username = document.getElementById('username').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const rol = document.getElementById('rol').value; // Obtiene si es Empleado o Secretario

    const esSecretario = rol === 'true'; // Si selecciona Secretario será true, si no, será false.

    // Construcción del endpoint con el valor de esSecretario
    const endpoint = `https://apispademo.somee.com/api/Account/registerEmpleado?esSecretario=${esSecretario}`;

    const nuevoEmpleado = {
        username: username,
        email: email,
        password: password
    };

    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json-patch+json', // Cambiar el Content-Type a application/json-patch+json
                'accept': '*/*'
            },
            body: JSON.stringify(nuevoEmpleado)
        });

        if (!response.ok) {
            throw new Error('Error al registrar el empleado o secretario');
        }

        alert('Empleado/Secretario registrado exitosamente');
        window.location.href = 'indexPersonal.html'; // Redirigir a la página principal después de registrar
    } catch (error) {
        console.error('Error al registrar el empleado/secretario:', error);
        alert('Ocurrió un error al intentar registrar el empleado/secretario.');
    }
});
