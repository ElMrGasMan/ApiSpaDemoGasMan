var botonIngreso = document.getElementById('mostrar_ingreso');
var botonRegistro = document.getElementById('mostrar_registro');
var sectionIngreso = document.getElementById('ingreso_section');
var sectionRegistro = document.getElementById('registro_section');
var volverBoton = document.getElementById('volver_boton');

activarAcceder();
sectionRegistro.style.display = 'none';


//EVENTOS DE CLICK
botonIngreso.addEventListener('click', function(){
    activarAcceder();
})
botonRegistro.addEventListener('click', function(){
    activarRegistrarse();
})
volverBoton.addEventListener('click', function(){
    window.location.href = './index.html';
})


function activarAcceder(){
    sectionIngreso.style.display = 'block';
    botonIngreso.style.display ='none';
    botonRegistro.style.display = 'block';
    sectionRegistro.style.display = 'none';
    volverBoton.style.display = 'block';
}
function activarRegistrarse(){
    sectionIngreso.style.display = 'none';
    botonIngreso.style.display ='block';
    botonRegistro.style.display = 'none';
    sectionRegistro.style.display = 'block';
    volverBoton.style.display = 'block';
}
var mensajeErrorRegistro = document.querySelector('.error_registro');

document.querySelector('.form_container_Registro').addEventListener('submit',async function(event){
    event.preventDefault();

    var username = document.querySelector('#nombre').value;
    var email = document.querySelector('#email').value;
    var password = document.querySelector('#nueva_contrasena').value;
    mensajeErrorRegistro.style.visibility = 'hidden'; // Muestra el mensaje de error
    const datosUsuario = {
        username: username,
        email: email,
        password: password
    };

    try {
        const response = await fetch('https://www.ApiSpaDemo.somee.com/api/Account/register', {
            method: 'POST', // El método debe ser POST para el registro
            credentials: 'include', // Esto asegura que las cookies se envíen con la solicitud
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(datosUsuario) // Convertir el objeto a una cadena JSON
        });

        if (response.ok) {
            // Procesar la respuesta si la solicitud fue exitosa
            const result = await response.json();
            console.log('Registro exitoso:', result);

            mensajeErrorRegistro.textContent = 'Registro Exitoso, proceda a ingresar con su cuenta';
            mensajeErrorRegistro.style.color = '#050';
            mensajeErrorRegistro.style.visibility = 'visible'; // Muestra el mensaje de error
        } else if(response.status == 400){
             // Manejar los errores de la solicitud
            const errorResult = await response.json();
            console.error('Error en el registro:', errorResult);

            mensajeErrorRegistro.textContent = 'nombre de usuario ocupado o la contraseña necesita mínimo 6 caracteres';
            mensajeErrorRegistro.style.color = '#700';
             mensajeErrorRegistro.style.visibility = 'visible'; // Muestra el mensaje de error
        }else {
            // Manejar los errores de la solicitud
            const errorResult = await response.json();
            console.error('Error en el registro:', errorResult);
            
            mensajeErrorRegistro.textContent = 'Ocurrió un error inesperado';
            mensajeErrorRegistro.style.color = '#700';
            mensajeErrorRegistro.style.visibility = 'visible'; 
        }
    } catch (error) {
        console.error('Error en la conexión con la API:', error);
        alert('Error en la conexión con la API.');
    }
})


var mensajeErrorIngreso = document.querySelector('.error_ingreso');
//Se evita el envio predeterminado de envio:
document.querySelector('.form_container_ingreso').addEventListener('submit',async function(event){
    event.preventDefault();

    
var nombreEmail = document.querySelector('#nombreEmail').value;
var contrasenia = document.querySelector('#contrasena').value;
mensajeErrorIngreso.style.visibility = 'hidden'; // Muestra el mensaje de error
//Datos del formulario
const datosUsuario = {
    username: nombreEmail,
    password: contrasenia
};

//Petición de ApI

    try{
        //Petición POST
        const response = await fetch('https://www.ApiSpaDemo.somee.com/api/Account/login', {
            method: 'POST',
            credentials: "include",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(datosUsuario)
        });

        //Procesamiento de la respuesta API
        const resultado = await response.json();
        if (response.status === 405) {
            // Manejar error 401: No autorizado
            console.error("Error 405");
            alert("Error:Por favor, intenta de nuevo.");
         }
        else if(response.ok){
            //Si hay login, se redirige y se muestra un mensaje por consola
            console.log("Login Exitoso: ", resultado);
            sessionStorage.setItem('username', resultado.userName);
            sessionStorage.setItem('email', resultado.email);
            sessionStorage.setItem('id', resultado.idUser);
            var nombreUsuario =  sessionStorage.getItem('username');

            console.log(nombreUsuario);
            //Redirigir
            getUserData(sessionStorage.getItem('id'))
            // window.location.href= "./index.html";
            //TODO
        }else{
            console.error("Error en el login", resultado.message);
            
            mensajeErrorIngreso.textContent = 'Error en el login, usuario o contraseña incorrecta';
            mensajeErrorIngreso.style.color = '#700';
            mensajeErrorIngreso.style.visibility = 'visible'; // Muestra el mensaje de error
        }

    }catch(error){
        console.error("Hubo un problema al realizar la petición: ", error);
        alert("Error en la conexión con la API");
    }

});


function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

// Función para eliminar una cookie
function deleteCookie(name) {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/`;
}


async function logout() {
    try {
        const response = await fetch('https://www.ApiSpaDemo.somee.com/api/Account/logout', {
            method: 'POST', // Debe ser POST para logout
            credentials: 'include',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                 // Esto es necesario para enviar cookies si estás usando autenticación basada en cookies
            },
           
        });

        if (response.ok) {
            console.log('Logout exitoso');
            deleteCookie();
            window.location.reload(); // Recarga la página o redirige a otra página
        } else {
            console.error('Error en el logout:', response.statusText);
            alert('Error al cerrar sesión');
        }
    } catch (error) {
        console.error('Error en la conexión con la API:', error);
        alert('Error en la conexión con la API');
    }
}



async function getUserData(id) {
    try {
        
        const response = await fetch(`https://www.apispademo.somee.com/api/Usuario/${id}`, {
            method: 'GET',
            credentials: 'include',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            
        });

        if (!response.ok) {
            const errorMessage = await response.text(); // Leer la respuesta en caso de error
            console.error("Error en la solicitud:", response.status, errorMessage);
            throw new Error(`Error en la solicitud: ${response.statusText}`);
        }

        const resultado = await response.json();
        console.log("Login con roles Exitoso: ", resultado);
        sessionStorage.setItem('roles', JSON.stringify(resultado.roles));

        if (resultado.roles.includes("Admin") || resultado.roles.includes("Empleado")) {
            window.location.href = "/indexPersonal.html";
        } else {
            window.location.href = "/index.html";
        }
    } catch (error) {
        console.error("Hubo un problema al realizar la petición de los datos de usuario:", error);
        alert("Error en la conexión con la API");
    }
}
