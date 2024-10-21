// //pestaña main


// // Código para el botón que abre la pestaña de login
// const botonAcceder = document.getElementById('boton_acceder');
// botonAcceder.addEventListener('click', function() {
//     window.location.href = "./signIn.html"; // Redirigir a la página de login
// });



// // Verificar si el usuario ya está logueado
// document.addEventListener('DOMContentLoaded', function() {
//     const userName = sessionStorage.getItem('username');
//     const email = sessionStorage.getItem('email');
//     // Supongamos que el nombre del usuario logueado se guarda en localStorage
//     const botonesHeader = document.getElementById('botones_header');


    
//     // Si el usuario está logueado, mostrar su nombre o redirigir a la página de inicio
//     if (userName && email) {
//         botonAcceder.style.display = 'none';//se oculta boton acceder
        
//             // Crear un elemento de saludo
//         const saludoUsuario = document.createElement('span');
//         const logoUsuario = document.createElement('i');
//         logoUsuario.className ="fa-solid fa-circle-user";
//         saludoUsuario.textContent = `Hola, ${userName}`;
//         saludoUsuario.classList.add('span_saludo');
//         logoUsuario.classList.add('logo_usuario');

//                 // Crear un botón de 'Cerrar sesión'
//         const botonLogout = document.createElement('button');
//         botonLogout.textContent = 'Cerrar sesión';
//         botonLogout.classList.add('boton_logout'); // Puedes agregar estilos adicionales


//         //FUNCION DE CLICK BOTON LOGOUT
//         botonLogout.addEventListener('click', async function() {
//             // Eliminar el nombre del usuario de localStorage y session storage
//             localStorage.clear();
//             sessionStorage.clear();


//             // Redireccionar a la página de inicio
//             window.location.reload(); // O redirige a una página específica
//         });

//         // Muestra la sección de usuario logueado
//         loggedInUserSection.style.display = "block";
//         guestUserSection.style.display = "none"; // Oculta la sección de nombre, apellido y email
//         // Rellena el nombre de usuario y email
//         usernameDisplay.textContent = userName;
//         emailDisplay.textContent = email;

 
//             // Añadir el saludo y el botón de logout al header
//         botonesHeader.appendChild(logoUsuario);
//         botonesHeader.appendChild(saludoUsuario);
//         botonesHeader.appendChild(botonLogout);

//         console.log(`Usuario logueado: ${userName}`);
//         // Aquí puedes redirigir a otra página si ya está logueado
//         // window.location.href = './index.html'; // Redirigir a la página de inicio

//         //Actualizar 




//     } else {
//         console.log("No hay usuario logueado. Redirigiendo al login...");
//         botonAcceder.style.display = 'block'; // Asegura que el botón de 'Acceder' esté visible si no hay usuario logueado
        
//     }
// });
///CODIGO MAIN



//pestaña main


// Código para el botón que abre la pestaña de login
const botonAcceder = document.getElementById('boton_acceder');
botonAcceder.addEventListener('click', function() {
    window.location.href = "./signIn.html"; // Redirigir a la página de login
});



// Verificar si el usuario ya está logueado
document.addEventListener('DOMContentLoaded', function() {
    const userName = sessionStorage.getItem('username');
    const email = sessionStorage.getItem('email');
    // Supongamos que el nombre del usuario logueado se guarda en localStorage
    const botonesHeader = document.getElementById('botones_header');


    
    // Si el usuario está logueado, mostrar su nombre o redirigir a la página de inicio
    if (userName && email) {
        botonAcceder.style.display = 'none';//se oculta boton acceder
        
            // Crear un elemento de saludo
        const saludoUsuario = document.createElement('span');
        const logoUsuario = document.createElement('i');
        var   inputNombre = document.getElementById('nombre');
        var   inputEmail  = document.getElementById('email');

        logoUsuario.className ="fa-solid fa-circle-user";
        saludoUsuario.textContent = `Hola, ${userName}`;
        saludoUsuario.classList.add('span_saludo');
        logoUsuario.classList.add('logo_usuario');

                // Crear un botón de 'Cerrar sesión'
        const botonLogout = document.createElement('button');
        botonLogout.textContent = 'Cerrar sesión';
        botonLogout.classList.add('boton_logout'); // Puedes agregar estilos adicionales


        //FUNCION DE CLICK BOTON LOGOUT
        botonLogout.addEventListener('click', async function() {
            // Eliminar el nombre del usuario de localStorage y session storage
            localStorage.clear();
            sessionStorage.clear();


            // Redireccionar a la página de inicio
            window.location.reload(); // O redirige a una página específica
        });

        // Muestra la sección de usuario logueado
        // loggedInUserSection.style.display = "block";
        // guestUserSection.style.display = "none"; // Oculta la sección de nombre, apellido y email
        // Rellena el nombre de usuario y email
        // usernameDisplay.textContent = userName;
        // emailDisplay.textContent = email;

 
            // Añadir el saludo y el botón de logout al header
        botonesHeader.appendChild(logoUsuario);
        botonesHeader.appendChild(saludoUsuario);
        botonesHeader.appendChild(botonLogout);
        inputNombre.value = userName;
        inputEmail.value = email;

        console.log(`Usuario logueado: ${userName}`);
        // Aquí puedes redirigir a otra página si ya está logueado
        // window.location.href = './index.html'; // Redirigir a la página de inicio

        //Actualizar 




    } else {
        console.log("No hay usuario logueado. Redirigiendo al login...");
        botonAcceder.style.display = 'block'; // Asegura que el botón de 'Acceder' esté visible si no hay usuario logueado
        
    }
});



async function getUserData() {
    try {
        
        const response = await fetch(`https://localhost:7034/api/Usuario/current`, {
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

        const resultado = await response2.json();
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






