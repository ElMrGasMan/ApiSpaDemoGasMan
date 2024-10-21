// Obtener elementos
const checkbox = document.getElementById('checkbox');
const body = document.body;
const links = document.querySelectorAll('.barraOpciones a');
const mainDiv = document.getElementById('div_Main');

// Manejo del toggle de la barra lateral
checkbox.addEventListener('change', function() {
    if (this.checked) {
        body.classList.add('menu-abierto');
    } else {
        body.classList.remove('menu-abierto');
    }
});

// Contenido dinámico para las diferentes secciones
const secciones = {
    'Clientes': `
        <style>
        :root {
            --verdeColor: #5fa510;
            --verdeOscuroColor: #315012;
            --rosaColor: #dc1659;
            --rosaOscuro: #a11245;
            --amarilloColor: #e1bc00;
            --amarilloOscuro: #d29500;
            --rosaClaro: rgb(248, 213, 219);
            --grisOscuro: #3c3c3c;
        }

        main {
            background-color: var(--rosaClaro);
            padding: 20px;
            border-radius: 10px;
        }

        section {
            background-color: var(--rosaOscuro);
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }

        .barraSuperior {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        h1 {
            color: var(--amarilloColor);
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .barraSuperior input {
            margin-right: 10px;
            background-color: var(--rosaClaro);
            padding: 8px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        button {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            padding: 8px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            margin-right: 10px;
        }

        .lineaSeparacion {
            border-top: 2px solid var(--amarilloColor);
            margin: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--rosaClaro);
            cursor: pointer;
        }

        th {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            font-size: 16px;
        }

        tbody tr:nth-child(even) {
            background-color: var(--rosaClaro);
        }

        tbody tr:hover {
            background-color: var(--amarilloColor);
        }

        i {
            font-size: 24px;
            margin-left: auto;
            color: var(--verdeOscuroColor);
        }

        .botonesAccion {
            display: flex;
            gap: 10px;
            margin-top: 10px;
        }
    </style>

        <section class="opcionMenuSelecionada">
             <div class="barraSuperior">
                <h1>Clientes</h1>
                <input type="text" id="inputBuscar" placeholder="Buscar" oninput="buscarClientes()">
            </div>

            <div class="lineaSeparacion"></div>

            <div class="tabla">
                <table>
                    <thead>
                        <tr>
                            <th onclick="cambiarCriterioBusqueda('ID')">ID</th>
                            <th onclick="cambiarCriterioBusqueda('Nombre')">Nombre</th>
                            <th onclick="cambiarCriterioBusqueda('Email')">Email</th>
                        </tr>
                    </thead>
                    <tbody id="tablaClientes">
                        <!-- Aquí se generarán las filas con JS -->
                    </tbody>
                </table>
            </div>
        </section>
    `,
    'Reservas': `
        <style>
        :root {
            --verdeColor: #5fa510;
            --verdeOscuroColor: #315012;
            --rosaColor: #dc1659;
            --rosaOscuro: #a11245;
            --amarilloColor: #e1bc00;
            --amarilloOscuro: #d29500;
            --rosaClaro: rgb(248, 213, 219);
            --grisOscuro: #3c3c3c;
        }

        main {
            background-color: var(--rosaClaro);
            padding: 20px;
            border-radius: 10px;
        }

        section {
            background-color: var(--rosaOscuro);
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }

        .barraSuperior {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        h1 {
            color: var(--amarilloColor);
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .barraSuperior input,
        .barraSuperior input[type="date"] {
            margin-right: 10px;
            background-color: var(--rosaClaro);
            padding: 8px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .barraSuperior input[type="date"] {
            flex: none;
        }

        button {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            padding: 8px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            margin-right: 10px;
        }

        .ordenarSelect {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            border: none;
            border-radius: 5px;
            padding: 8px 15px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
        }

        .lineaSeparacion {
            border-top: 2px solid var(--amarilloColor);
            margin: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--rosaClaro);
            cursor: pointer;
        }

        th {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            font-size: 16px;
        }

        tbody tr:nth-child(even) {
            background-color: var(--rosaClaro);
        }

        tbody tr:hover {
            background-color: var(--amarilloColor);
        }

        i {
            font-size: 24px;
            margin-left: auto;
            color: var(--verdeOscuroColor);
        }

        .botonesAccion {
            display: flex;
            gap: 10px;
            margin-top: 10px;
        }
    </style>

        <section class="opcionMenuSelecionada">
            <div class="barraSuperior">
                <h1>Reservas</h1>
                <input type="text" id="inputBuscarReserva" placeholder="Buscar por clienteId" oninput="buscarReservas()">
                
                <select id="opcionesOrdenarReserva" class="ordenarSelect" onchange="ordenarYFiltrarReservas()">
                    <option value="" disabled selected>Ordenar</option>
                    <option value="nombreAZ">Cliente A-Z</option>
                    <option value="nombreZA">Cliente Z-A</option>
                </select>
            </div>

            <div class="lineaSeparacion"></div>

            <div class="tabla">
                <table>
                    <thead>
                        <tr>
                            <th onclick="cambiarCriterioBusqueda('ID')">ID</th>
                            <th onclick="cambiarCriterioBusqueda('usuarioCliente')">usuarioCliente</th>
                            <th onclick="cambiarCriterioBusqueda('nombreIdentificador')">nombreIdentificador</th>
                        </tr>
                    </thead>
                    <tbody id="tablaReservas">
                        <!-- Aquí se generarán las filas con JS -->
                    </tbody>
                </table>
            </div>

            <div class="lineaSeparacion"></div>
        </section>
    `,
    'Informes de Pagos': `
        <style>
        :root {
            --verdeColor: #5fa510;
            --verdeOscuroColor: #315012;
            --rosaColor: #dc1659;
            --rosaOscuro: #a11245;
            --amarilloColor: #e1bc00;
            --amarilloOscuro: #d29500;
            --rosaClaro: rgb(248, 213, 219);
            --grisOscuro: #3c3c3c;
        }

        main {
            background-color: var(--rosaClaro);
            padding: 20px;
            border-radius: 10px;
        }

        section {
            background-color: var(--rosaOscuro);
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }

        .barraSuperior {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        h1 {
            color: var(--amarilloColor);
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .barraSuperior input,
        .barraSuperior input[type="date"] {
            margin-right: 10px;
            background-color: var(--rosaClaro);
            padding: 8px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .barraSuperior input[type="date"] {
            flex: none;
        }

        button {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            padding: 8px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            margin-right: 10px;
        }

        .ordenarSelect {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            border: none;
            border-radius: 5px;
            padding: 8px 15px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
        }

        .lineaSeparacion {
            border-top: 2px solid var(--amarilloColor);
            margin: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--rosaClaro);
            cursor: pointer;
        }

        th {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            font-size: 16px;
        }

        tbody tr:nth-child(even) {
            background-color: var(--rosaClaro);
        }

        tbody tr:hover {
            background-color: var(--amarilloColor);
        }

        i {
            font-size: 24px;
            margin-left: auto;
            color: var(--verdeOscuroColor);
        }

        .botonesAccion {
            display: flex;
            gap: 10px;
            margin-top: 10px;
        }
    </style>

         <section class="opcionMenuSelecionada">
            <div class="barraSuperior">
                <h1>Pagos</h1>
                <input type="text" id="inputBuscarPago" placeholder="Buscar por usuarioId" oninput="buscarPagos()">

                <select id="filtroPagado" class="ordenarSelect">
                    <option value="todos">Todos</option>
                    <option value="true">Pagado</option>
                    <option value="false">No Pagado</option>
                </select>

                <select id="opcionesOrdenarPago" class="ordenarSelect" onchange="ordenarYFiltrarPagos()">
                    <option value="" disabled selected>Ordenar</option>
                    <option value="formatoPagoAZ">Formato de Pago A-Z</option>
                    <option value="formatoPagoZA">Formato de Pago Z-A</option>
                    <option value="montoAsc">Monto Ascendente</option>
                    <option value="montoDesc">Monto Descendente</option>
                </select>
            </div>

            <div class="lineaSeparacion"></div>

            <div class="tabla">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>usuarioId</th>
                            <th>reservaId</th>
                            <th>formatoPago</th>
                            <th>montoTotal</th>
                            <th>Pagado</th>
                        </tr>
                    </thead>
                    <tbody id="tablaPagos">
                        <!-- Aquí se generarán las filas con JS -->
                    </tbody>
                </table>
            </div>

            <div class="lineaSeparacion"></div>
        </section>
    `,
    'Servicios': `
            <style>
        :root {
            --verdeColor: #5fa510;
            --verdeOscuroColor: #315012;
            --rosaColor: #dc1659;
            --rosaOscuro: #a11245;
            --amarilloColor: #e1bc00;
            --amarilloOscuro: #d29500;
            --rosaClaro: rgb(248, 213, 219);
            --grisOscuro: #3c3c3c;
        }

        main {
            background-color: var(--rosaClaro);
            padding: 20px;
            border-radius: 10px;
        }

        section {
            background-color: var(--rosaOscuro);
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }

        .barraSuperior {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        h1 {
            color: var(--amarilloColor);
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .barraSuperior input,
        .barraSuperior input[type="date"] {
            margin-right: 10px;
            background-color: var(--rosaClaro);
            padding: 8px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .barraSuperior input[type="date"] {
            flex: none;
        }

        button {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            padding: 8px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            margin-right: 10px;
        }

        .ordenarSelect {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            border: none;
            border-radius: 5px;
            padding: 8px 15px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
        }

        .lineaSeparacion {
            border-top: 2px solid var(--amarilloColor);
            margin: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--rosaClaro);
            cursor: pointer;
        }

        th {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            font-size: 16px;
        }

        tbody tr:nth-child(even) {
            background-color: var(--rosaClaro);
        }

        tbody tr:hover {
            background-color: var(--amarilloColor);
        }

        i {
            font-size: 24px;
            margin-left: auto;
            color: var(--verdeOscuroColor);
        }

        .botonesAccion {
            display: flex;
            gap: 10px;
            margin-top: 10px;
        }
    </style>

        <section class="opcionMenuSelecionada">
            <div class="barraSuperior">
                <h1>Servicios</h1>
                <input type="text" id="inputBuscarServicio" placeholder="Buscar por tipoServicio" oninput="buscarServicios()">

                <select id="opcionesOrdenarServicio" class="ordenarSelect" onchange="ordenarYFiltrarServicios()">
                    <option value="" disabled selected>Ordenar</option>
                    <option value="precioAsc">Precio Ascendente</option>
                    <option value="precioDesc">Precio Descendente</option>
                    <option value="duracionAsc">Duración Ascendente</option>
                </select>
            </div>

            <div class="lineaSeparacion"></div>

            <div class="tabla">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>titulo</th>
                            <th>tipoServicio</th>
                            <th>descripcion</th>
                            <th>rutaImagen</th>
                            <th>duracionMinut</th>
                            <th>precio</th>
                            <th>empleadoId</th>
                        </tr>
                    </thead>
                    <tbody id="tablaServicios">
                        <!-- Aquí se generarán las filas con JS -->
                    </tbody>
                </table>
            </div>

            <div class="lineaSeparacion"></div>
        </section>
    `,
    'Administrador de Empleados': `
            <style>
        :root {
            --verdeColor: #5fa510;
            --verdeOscuroColor: #315012;
            --rosaColor: #dc1659;
            --rosaOscuro: #a11245;
            --amarilloColor: #e1bc00;
            --amarilloOscuro: #d29500;
            --rosaClaro: rgb(248, 213, 219);
            --grisOscuro: #3c3c3c;
        }

        main {
            background-color: var(--rosaClaro);
            padding: 20px;
            border-radius: 10px;
        }

        section {
            background-color: var(--rosaOscuro);
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }

        .barraSuperior {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        h1 {
            color: var(--amarilloColor);
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .barraSuperior input,
        .barraSuperior input[type="date"] {
            margin-right: 10px;
            background-color: var(--rosaClaro);
            padding: 8px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .barraSuperior input[type="date"] {
            flex: none;
        }

        button {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            padding: 8px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            margin-right: 10px;
        }

        .ordenarSelect {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            border: none;
            border-radius: 5px;
            padding: 8px 15px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
        }

        .lineaSeparacion {
            border-top: 2px solid var(--amarilloColor);
            margin: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--rosaClaro);
            cursor: pointer;
        }

        th {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            font-size: 16px;
        }

        tbody tr:nth-child(even) {
            background-color: var(--rosaClaro);
        }

        tbody tr:hover {
            background-color: var(--amarilloColor);
        }

        i {
            font-size: 24px;
            margin-left: auto;
            color: var(--verdeOscuroColor);
        }

        .botonesAccion {
            display: flex;
            gap: 10px;
            margin-top: 10px;
        }
    </style>

        <section class="opcionMenuSelecionada">
            <div class="barraSuperior">
                <h1>Empleados</h1>
                <input type="text" id="inputBuscarEmpleado" placeholder="Buscar por nombre" oninput="buscarEmpleados()">
            </div>

            <div class="lineaSeparacion"></div>

            <div class="tabla">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre</th>
                            <th>Email</th>
                            <th>Roles</th>
                        </tr>
                    </thead>
                    <tbody id="tablaEmpleados">
                        <!-- Aquí se generarán las filas con JS -->
                    </tbody>
                </table>
            </div>

            <div class="lineaSeparacion"></div>

            <div class="botonesAccion">
                <button>Agregar</button>
                <button>Modificar</button>
                <button>Eliminar</button>
            </div>
        </section>
    `,
    'Noticias': `
            <style>
        :root {
            --verdeColor: #5fa510;
            --verdeOscuroColor: #315012;
            --rosaColor: #dc1659;
            --rosaOscuro: #a11245;
            --amarilloColor: #e1bc00;
            --amarilloOscuro: #d29500;
            --rosaClaro: rgb(248, 213, 219);
            --grisOscuro: #3c3c3c;
        }

        main {
            background-color: var(--rosaClaro);
            padding: 20px;
            border-radius: 10px;
        }

        section {
            background-color: var(--rosaOscuro);
            border-radius: 10px;
            padding: 20px;
            margin-top: 20px;
        }

        .barraSuperior {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        h1 {
            color: var(--amarilloColor);
            margin: 0;
            font-size: 24px;
            font-weight: bold;
        }

        .barraSuperior input,
        .barraSuperior input[type="date"] {
            margin-right: 10px;
            background-color: var(--rosaClaro);
            padding: 8px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .barraSuperior input[type="date"] {
            flex: none;
        }

        button {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            padding: 8px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            margin-right: 10px;
        }

        .ordenarSelect {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            border: none;
            border-radius: 5px;
            padding: 8px 15px;
            font-family: "Comic Sans MS", cursive;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
        }

        .lineaSeparacion {
            border-top: 2px solid var(--amarilloColor);
            margin: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid var(--rosaClaro);
            cursor: pointer;
        }

        th {
            background-color: var(--verdeOscuroColor);
            color: var(--verdeColor);
            font-size: 16px;
        }

        tbody tr:nth-child(even) {
            background-color: var(--rosaClaro);
        }

        tbody tr:hover {
            background-color: var(--amarilloColor);
        }

        i {
            font-size: 24px;
            margin-left: auto;
            color: var(--verdeOscuroColor);
        }

        .botonesAccion {
            display: flex;
            gap: 10px;
            margin-top: 10px;
        }
    </style>

        <section class="opcionMenuSelecionada">
            <div class="barraSuperior">
                <h1>Noticias</h1>
                <input type="text" id="inputBuscarNoticia" placeholder="Buscar por título" oninput="buscarNoticias()">

                <select id="opcionesOrdenarNoticia" class="ordenarSelect" onchange="ordenarYFiltrarNoticias()">
                    <option value="" disabled selected>Ordenar</option>
                    <option value="tituloAZ">Título A-Z</option>
                    <option value="tituloZA">Título Z-A</option>
                    <option value="fechaAsc">Fecha de menor a mayor</option>
                    <option value="fechaDesc">Fecha de mayor a menor</option>
                </select>
            </div>

            <div class="lineaSeparacion"></div>

            <div class="tabla">
                <table>
                    <thead>
                        <tr>
                            <th onclick="cambiarCriterioBusquedaNoticia('ID')">ID</th>
                            <th onclick="cambiarCriterioBusquedaNoticia('Titulo')">Titulo</th>
                            <th onclick="cambiarCriterioBusquedaNoticia('tipo')">Tipo</th>
                            <th onclick="cambiarCriterioBusquedaNoticia('rutaImagen')">Ruta Imagen</th>
                            <th onclick="cambiarCriterioBusquedaNoticia('rutaPDF')">Ruta PDF</th>
                            <th onclick="cambiarCriterioBusquedaNoticia('fecha')">Fecha Publicacion</th>
                        </tr>
                    </thead>
                    <tbody id="tablaNoticias">
                        <!-- Aquí se generarán las filas con JS -->
                    </tbody>
                </table>
            </div>

            <div class="lineaSeparacion"></div>

            <div class="botonesAccion">
                <button>Agregar</button>
                <button>Modificar</button>
                <button>Eliminar</button>
            </div>
        </section>
    `
};

document.addEventListener('DOMContentLoaded', () => {
    // Inicialmente no cargarás los usuarios porque la tabla de clientes solo se muestra cuando se selecciona la sección.
    // cargarUsuariosDesdeAPI();  // No se necesita aquí, se llamará cuando se haga clic en la sección 'Clientes'.
});

// Variables globales
let criterioBusqueda = 'Nombre';
let usuarios = []; // Se almacenarán aquí los usuarios cargados desde la API.

// Función para cargar usuarios desde la API
async function cargarUsuariosDesdeAPI() {
    try {
        const response = await fetch('https://apispademo.somee.com/api/Usuario/getAllUsersByRol/Cliente');
        if (!response.ok) {
            throw new Error('Error al obtener los usuarios');
        }

        usuarios = await response.json();
        // Filtrar solo usuarios con el rol exacto "Cliente"
        const usuariosFiltrados = usuarios.filter(usuario => usuario.roles.includes('Cliente') && usuario.roles.length === 1);
        cargarUsuarios(usuariosFiltrados);
    } catch (error) {
        console.error('Error al cargar los usuarios:', error);
    }
}

// Función para cargar los usuarios en la tabla
function cargarUsuarios(usuarios) {
    const tablaClientes = document.getElementById('tablaClientes');
    if (!tablaClientes) {
        console.error('Elemento tablaClientes no encontrado');
        return;
    }
    
    tablaClientes.innerHTML = ''; // Limpiar la tabla antes de llenarla

    usuarios.forEach(usuario => {
        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${usuario.id}</td>
            <td>${usuario.userName}</td>
            <td>${usuario.email}</td>
        `;
        tablaClientes.appendChild(fila);
    });
}

// Función para buscar clientes
function buscarClientes() {
    const inputBuscar = document.getElementById('inputBuscar');
    if (!inputBuscar) {
        console.error('Elemento inputBuscar no encontrado');
        return;
    }

    const texto = inputBuscar.value.toLowerCase();

    // Filtramos primero por usuarios que tienen solo el rol de "Cliente"
    const usuariosFiltrados = usuarios
        .filter(usuario => usuario.roles.includes('Cliente') && usuario.roles.length === 1)
        .filter(usuario => { // Aplica el filtro de búsqueda
            switch (criterioBusqueda) {
                case 'ID':
                    return usuario.id.toString().startsWith(texto);
                case 'Nombre':
                    return usuario.userName.toLowerCase().includes(texto);
                case 'Email':
                    return usuario.email.toLowerCase().includes(texto);
                default:
                    return false;
            }
        });

    cargarUsuarios(usuariosFiltrados);
}


// Añadir el evento 'click' a cada enlace de la barra lateral
links.forEach(link => {
    link.addEventListener('click', function(e) {
        e.preventDefault(); // Evitar el comportamiento predeterminado de los enlaces
        const opcionSeleccionada = this.textContent.trim(); // Obtener el texto del enlace

        if (secciones[opcionSeleccionada]) {
            mainDiv.innerHTML = `<main>${secciones[opcionSeleccionada]}</main>`;

            if (opcionSeleccionada === 'Clientes') {
                cargarFiltros();  // Función para inicializar los filtros de clientes
                cargarUsuariosDesdeAPI();  // Cargar los usuarios desde la API cada vez que se entre en la sección de clientes
            } else if (opcionSeleccionada === 'Reservas') {
                cargarFiltrosReservas(); // Función para inicializar los filtros de reservas
                cargarReservasDesdeAPI();  // Cargar las reservas desde la API cada vez que se entre en la sección de reservas
            } else if (opcionSeleccionada === 'Informes de Pagos') {
                cargarFiltrosPagos(); // Función para inicializar los filtros de pagos
                cargarPagosDesdeAPI();  // Cargar los pagos desde la API cada vez que se entre en la sección de pagos
            } else if (opcionSeleccionada === 'Servicios') {
                cargarFiltrosServicios(); // Función para inicializar los filtros de servicios
                cargarServiciosDesdeAPI();  // Cargar los servicios desde la API cada vez que se entre en la sección de servicios
            } else if (opcionSeleccionada === 'Administrador de Empleados') {
                cargarFiltrosEmpleados(); // Función para inicializar los filtros de empleados
                cargarEmpleadosDesdeAPI();  // Cargar los empleados desde la API cada vez que se entre en la sección de empleados
            } else if (opcionSeleccionada === 'Noticias') {
                cargarFiltrosNoticias(); // Función para inicializar los filtros de noticias
                cargarNoticiasDesdeAPI();  // Cargar las noticias desde la API cada vez que se entre en la sección de noticias
            }
        }
    });
});

// Función para inicializar el comportamiento de los filtros
function cargarFiltros() {
    const inputBuscar = document.getElementById('inputBuscar');
    if (inputBuscar) {
        inputBuscar.addEventListener('input', buscarClientes); // Añadir evento para buscar clientes
    }
}

// Función para cambiar el criterio de búsqueda
function cambiarCriterioBusqueda(nuevoCriterio) {
    criterioBusqueda = nuevoCriterio;
    const inputBuscar = document.getElementById('inputBuscar');
    if (inputBuscar) {
        inputBuscar.placeholder = `Buscar por ${nuevoCriterio}`;
    }
}

// Variables globales para almacenar los filtros
let filtroOrden = '';

// Función para guardar filtros en el localStorage
function guardarFiltros() {
    localStorage.setItem('filtroOrden', filtroOrden);
}

// Función para cargar los filtros del localStorage
function cargarFiltrosDelLocalStorage() {
    const opcionesOrdenar = document.getElementById('opcionesOrdenar');
    if (!opcionesOrdenar) {
        console.error('Elemento opcionesOrdenar no encontrado');
        return;
    }

    filtroOrden = localStorage.getItem('filtroOrden') || '';
    opcionesOrdenar.value = filtroOrden;

    // Aplicar los filtros
    ordenarYFiltrarClientes();
}

function ordenarYFiltrarClientes() {
    const criterio = document.getElementById('opcionesOrdenar').value;

    // Guardar el filtro de orden seleccionado
    filtroOrden = criterio;
    guardarFiltros();

    let usuariosOrdenados = [...usuarios];
    switch (criterio) {
        case 'nombreAZ':
            usuariosOrdenados.sort((a, b) => a.userName.localeCompare(b.userName));
            break;
        case 'nombreZA':
            usuariosOrdenados.sort((a, b) => b.userName.localeCompare(a.userName));
            break;
        default:
            break;
    }
    cargarUsuarios(usuariosOrdenados);
}




// ============================= RESERVAS ========================================================

// Variables globales para la sección de Reservas
let reservas = [];

// Función para cargar reservas desde la API
async function cargarReservasDesdeAPI() {
    try {
        const response = await fetch('https://apispademo.somee.com/api/Reserva?conTurnos=false&conPago=false');
        if (!response.ok) {
            throw new Error('Error al obtener las reservas');
        }

        reservas = await response.json();
        cargarReservas(reservas);
    } catch (error) {
        console.error('Error al cargar las reservas:', error);
    }
}

// Función para cargar las reservas en la tabla
function cargarReservas(reservas) {
    const tablaReservas = document.getElementById('tablaReservas');
    if (!tablaReservas) {
        console.error('Elemento tablaReservas no encontrado');
        return;
    }

    tablaReservas.innerHTML = ''; // Limpiar la tabla antes de llenarla

    reservas.forEach(reserva => {
        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${reserva.reservaId}</td>
            <td>${reserva.clienteId}</td>
            <td>${reserva.nombreIdentificador}</td>
        `;
        tablaReservas.appendChild(fila);
    });
}

// Función para buscar reservas por clienteId
function buscarReservas() {
    const inputBuscar = document.getElementById('inputBuscarReserva');
    if (!inputBuscar) {
        console.error('Elemento inputBuscar no encontrado');
        return;
    }

    const texto = inputBuscar.value.toLowerCase();
    const reservasFiltradas = reservas.filter(reserva => reserva.clienteId.toLowerCase().includes(texto));
    cargarReservas(reservasFiltradas);
}

// Función para inicializar el comportamiento de los filtros de Reservas
function cargarFiltrosReservas() {
    const inputBuscar = document.getElementById('inputBuscarReserva');
    if (inputBuscar) {
        inputBuscar.addEventListener('input', buscarReservas); // Añadir evento para buscar reservas por clienteId
    }
}

// Función para ordenar y filtrar reservas por clienteId
function ordenarYFiltrarReservas() {
    const criterio = document.getElementById('opcionesOrdenarReserva').value;

    let reservasOrdenadas = [...reservas];
    switch (criterio) {
        case 'nombreAZ':
            reservasOrdenadas.sort((a, b) => a.clienteId.localeCompare(b.clienteId));
            break;
        case 'nombreZA':
            reservasOrdenadas.sort((a, b) => b.clienteId.localeCompare(a.clienteId));
            break;
        default:
            break;
    }
    cargarReservas(reservasOrdenadas);
}


// ============================================= PAGOS ===============================================

// Variables globales para la sección de Pagos
let pagos = [];

// Función para cargar pagos desde la API
async function cargarPagosDesdeAPI() {
    try {
        const response = await fetch('https://apispademo.somee.com/api/Pago');
        if (!response.ok) {
            throw new Error('Error al obtener los pagos');
        }

        const data = await response.json();
        console.log('Datos obtenidos de la API:', data); // Verificar qué datos llegan desde la API

        pagos = data;
        cargarPagos(pagos);
    } catch (error) {
        console.error('Error al cargar los pagos:', error);
    }
}

// Función para cargar los pagos en la tabla
function cargarPagos(pagos) {
    const tablaPagos = document.getElementById('tablaPagos');
    if (!tablaPagos) {
        console.error('Elemento tablaPagos no encontrado');
        return;
    }

    tablaPagos.innerHTML = ''; // Limpiar la tabla antes de llenarla

    pagos.forEach(pago => {
        console.log('Agregando pago:', pago); // Verificar que estamos agregando cada pago

        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${pago.pagoId}</td>
            <td>${pago.usuarioId}</td>
            <td>${pago.reservaId}</td>
            <td>${pago.formatoPago}</td>
            <td>${pago.montoTotal}</td>
            <td>${pago.pagado ? 'Sí' : 'No'}</td>
        `;
        tablaPagos.appendChild(fila);
    });
}

// Función para buscar pagos por usuarioId
function buscarPagos() {
    const inputBuscar = document.getElementById('inputBuscarPago');
    if (!inputBuscar) {
        console.error('Elemento inputBuscar no encontrado');
        return;
    }

    const texto = inputBuscar.value.toLowerCase();
    const pagosFiltrados = pagos.filter(pago => pago.usuarioId.toLowerCase().includes(texto));
    cargarPagos(pagosFiltrados);
}

// Función para inicializar el comportamiento de los filtros de Pagos
function cargarFiltrosPagos() {
    const inputBuscar = document.getElementById('inputBuscarPago');
    if (inputBuscar) {
        inputBuscar.addEventListener('input', buscarPagos); // Añadir evento para buscar pagos por usuarioId
    }

    const selectPagado = document.getElementById('filtroPagado');
    if (selectPagado) {
        selectPagado.addEventListener('change', filtrarPorPagado); // Añadir evento para filtrar por pagado
    }
}

// Función para filtrar por pagado (true/false/todos)
function filtrarPorPagado() {
    const selectPagado = document.getElementById('filtroPagado').value;

    let pagosFiltrados = [...pagos];
    if (selectPagado === 'true') {
        pagosFiltrados = pagos.filter(pago => pago.pagado === true);
    } else if (selectPagado === 'false') {
        pagosFiltrados = pagos.filter(pago => pago.pagado === false);
    }

    cargarPagos(pagosFiltrados);
}

// Función para ordenar y filtrar pagos
function ordenarYFiltrarPagos() {
    const criterio = document.getElementById('opcionesOrdenarPago').value;

    let pagosOrdenados = [...pagos];
    switch (criterio) {
        case 'formatoPagoAZ':
            pagosOrdenados.sort((a, b) => a.formatoPago.localeCompare(b.formatoPago));
            break;
        case 'formatoPagoZA':
            pagosOrdenados.sort((a, b) => b.formatoPago.localeCompare(a.formatoPago));
            break;
        case 'montoAsc':
            pagosOrdenados.sort((a, b) => a.montoTotal - b.montoTotal);
            break;
        case 'montoDesc':
            pagosOrdenados.sort((a, b) => b.montoTotal - a.montoTotal);
            break;
        default:
            break;
    }
    cargarPagos(pagosOrdenados);
}


//========================================= SERVICIOS ===============================================

// Variables globales para la sección de Servicios
let servicios = [];

// Función para cargar servicios desde la API
async function cargarServiciosDesdeAPI() {
    try {
        const response = await fetch('https://apispademo.somee.com/api/Servicio');
        if (!response.ok) {
            throw new Error('Error al obtener los servicios');
        }

        servicios = await response.json();
        cargarServicios(servicios);
    } catch (error) {
        console.error('Error al cargar los servicios:', error);
    }
}

// Función para cargar los servicios en la tabla
function cargarServicios(servicios) {
    const tablaServicios = document.getElementById('tablaServicios');
    if (!tablaServicios) {
        console.error('Elemento tablaServicios no encontrado');
        return;
    }

    tablaServicios.innerHTML = ''; // Limpiar la tabla antes de llenarla

    servicios.forEach(servicio => {
        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${servicio.servicioId}</td>
            <td>${servicio.titulo}</td>
            <td>${servicio.tipoServicio}</td>
            <td>${servicio.descripcion}</td>
            <td>${servicio.rutaImagen}</td>
            <td>${servicio.duracionMinut}</td>
            <td>${servicio.precio}</td>
            <td>${servicio.usuarioId}</td> <!-- empleadoId -->
        `;
        tablaServicios.appendChild(fila);
    });
}

// Función para buscar servicios por tipoServicio
function buscarServicios() {
    const inputBuscar = document.getElementById('inputBuscarServicio');
    if (!inputBuscar) {
        console.error('Elemento inputBuscar no encontrado');
        return;
    }

    const texto = inputBuscar.value.toLowerCase();
    const serviciosFiltrados = servicios.filter(servicio => servicio.tipoServicio.toLowerCase().includes(texto));
    cargarServicios(serviciosFiltrados);
}

// Función para inicializar el comportamiento de los filtros de Servicios
function cargarFiltrosServicios() {
    const inputBuscar = document.getElementById('inputBuscarServicio');
    if (inputBuscar) {
        inputBuscar.addEventListener('input', buscarServicios); // Añadir evento para buscar servicios por tipoServicio
    }
}

// Función para ordenar y filtrar servicios
function ordenarYFiltrarServicios() {
    const criterio = document.getElementById('opcionesOrdenarServicio').value;

    let serviciosOrdenados = [...servicios];
    switch (criterio) {
        case 'precioAsc':
            serviciosOrdenados.sort((a, b) => a.precio - b.precio);
            break;
        case 'precioDesc':
            serviciosOrdenados.sort((a, b) => b.precio - a.precio);
            break;
        case 'duracionAsc':
            serviciosOrdenados.sort((a, b) => a.duracionMinut - b.duracionMinut);
            break;
        default:
            break;
    }
    cargarServicios(serviciosOrdenados);
}


// =============================== ADMINISTRADOR DE EMPLEADOS ====================================

// Variables globales para la sección de Empleados
let empleados = [];

// Función para cargar empleados desde la API
async function cargarEmpleadosDesdeAPI() {
    try {
        // Hacer las dos solicitudes para obtener empleados y secretarios
        const [responseEmpleados, responseSecretarios] = await Promise.all([
            fetch('https://apispademo.somee.com/api/Usuario/getAllUsersByRol/Empleado'),
            fetch('https://apispademo.somee.com/api/Usuario/getAllUsersByRol/Secretario')
        ]);

        if (!responseEmpleados.ok || !responseSecretarios.ok) {
            throw new Error('Error al obtener los empleados o secretarios');
        }

        const empleadosData = await responseEmpleados.json();
        const secretariosData = await responseSecretarios.json();

        // Combinar ambos arrays y eliminar duplicados
        const allEmpleados = [...empleadosData, ...secretariosData];
        empleados = [...new Map(allEmpleados.map(user => [user.id, user])).values()]; // Eliminar duplicados por ID

        cargarEmpleados(empleados);
    } catch (error) {
        console.error('Error al cargar los empleados:', error);
    }
}

// Función para cargar los empleados en la tabla
function cargarEmpleados(empleados) {
    const tablaEmpleados = document.getElementById('tablaEmpleados');
    if (!tablaEmpleados) {
        console.error('Elemento tablaEmpleados no encontrado');
        return;
    }

    tablaEmpleados.innerHTML = ''; // Limpiar la tabla antes de llenarla

    empleados.forEach(empleado => {
        const roles = empleado.roles.join(', '); // Combinar los roles en una sola cadena separada por comas
        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${empleado.id}</td>
            <td>${empleado.userName}</td>
            <td>${empleado.email}</td>
            <td>${roles}</td>
        `;
        tablaEmpleados.appendChild(fila);
    });
}

// Función para buscar empleados por nombre
function buscarEmpleados() {
    const inputBuscar = document.getElementById('inputBuscarEmpleado');
    if (!inputBuscar) {
        console.error('Elemento inputBuscar no encontrado');
        return;
    }

    const texto = inputBuscar.value.toLowerCase();
    const empleadosFiltrados = empleados.filter(empleado => empleado.userName.toLowerCase().includes(texto));
    cargarEmpleados(empleadosFiltrados);
}

// Función para inicializar los filtros de empleados
function cargarFiltrosEmpleados() {
    const inputBuscar = document.getElementById('inputBuscarEmpleado');
    if (inputBuscar) {
        inputBuscar.addEventListener('input', buscarEmpleados); // Añadir evento para buscar empleados por nombre
    }
}

// ========================================== NOTICIAS ===================================================

// Variables globales para la sección de Noticias
let noticias = [];
let criterioBusquedaNoticia = 'titulo'; // Criterio predeterminado para buscar en Noticias

// Función para cargar noticias desde la API
async function cargarNoticiasDesdeAPI() {
    try {
        const response = await fetch('https://apispademo.somee.com/api/Noticia');
        if (!response.ok) {
            throw new Error('Error al obtener las noticias');
        }

        noticias = await response.json();
        cargarNoticias(noticias);
    } catch (error) {
        console.error('Error al cargar las noticias:', error);
    }
}

// Función para cargar las noticias en la tabla
function cargarNoticias(noticias) {
    const tablaNoticias = document.getElementById('tablaNoticias');
    if (!tablaNoticias) {
        console.error('Elemento tablaNoticias no encontrado');
        return;
    }

    tablaNoticias.innerHTML = ''; // Limpiar la tabla antes de llenarla

    noticias.forEach(noticia => {
        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${noticia.noticiaId}</td>
            <td>${noticia.titulo}</td>
            <td>${noticia.tipo || 'N/A'}</td> <!-- Asume que el tipo puede no estar disponible -->
            <td>${noticia.rutaImagen}</td>
            <td>${noticia.rutaPDF}</td>
            <td>${noticia.fechaPublicacion}</td>
        `;
        tablaNoticias.appendChild(fila);
    });
}

// Función para cambiar el criterio de búsqueda
function cambiarCriterioBusquedaNoticia(nuevoCriterio) {
    criterioBusquedaNoticia = nuevoCriterio;
    const inputBuscar = document.getElementById('inputBuscarNoticia');
    if (inputBuscar) {
        inputBuscar.placeholder = `Buscar por ${nuevoCriterio}`;
    }
}

// Función para buscar noticias según el criterio actual
function buscarNoticias() {
    const inputBuscar = document.getElementById('inputBuscarNoticia');
    if (!inputBuscar) {
        console.error('Elemento inputBuscar no encontrado');
        return;
    }

    const texto = inputBuscar.value.toLowerCase();
    const noticiasFiltradas = noticias.filter(noticia => {
        switch (criterioBusquedaNoticia) {
            case 'ID':
                return noticia.noticiaId.toString().startsWith(texto);
            case 'titulo':
                return noticia.titulo.toLowerCase().includes(texto);
            case 'tipo':
                return noticia.tipo.toLowerCase().includes(texto);
            case 'rutaImagen':
                return noticia.rutaImagen.toLowerCase().includes(texto);
            case 'rutaPDF':
                return noticia.rutaPDF.toLowerCase().includes(texto);
            case 'fecha':
                return noticia.fechaPublicacion.includes(texto);
            default:
                return false;
        }
    });
    cargarNoticias(noticiasFiltradas);
}

// Función para inicializar los filtros de Noticias
function cargarFiltrosNoticias() {
    const inputBuscar = document.getElementById('inputBuscarNoticia');
    if (inputBuscar) {
        inputBuscar.addEventListener('input', buscarNoticias); // Añadir evento para buscar según el criterio
    }

    // Añadir los eventos para cambiar el criterio al hacer clic en los encabezados de la tabla
    const encabezados = document.querySelectorAll('#tablaNoticias th');
    encabezados.forEach(th => {
        th.addEventListener('click', () => {
            cambiarCriterioBusquedaNoticia(th.getAttribute('data-criterio'));
        });
    });
}