document.addEventListener('DOMContentLoaded', () => {
    const noticias = [
      { 
        title: 'Descubre los secretos para mantener una piel radiante y saludable sin salir de tu hogar', 
        date: '2024-09-05',
        type: 'noticia',
        imageUrl: '/sources/Noticias/consejosParaLaPielImagen.jpg',
        pdfUrl: '/sources/Noticias/5ConsejosParaCuidadoPiel-1.pdf' 
      },
      { 
        title: 'Descubre la innovadora terapia que combina tecnología avanzada y ingredientes naturales para dejar tu piel resplandeciente y lista para el verano.', 
        date: '2023-12-10',
        type: 'nuevo tratamiento',
        imageUrl: '/sources/Noticias/verano.jpg',
        pdfUrl: '/sources/Noticias/NuevoTratamientoTemporada_VeranoRadiante-1.pdf' 
      },
      { 
        title: 'Celebra el amor en nuestro Día de Spa para Parejas este 14 de Febrero', 
        date: '2024-01-12',
        type: 'promociones',
        imageUrl: '/sources/Noticias/SanValentin.jpg',
        pdfUrl: '/sources/Noticias/ServicioEspecialDiaDeSanValentin.pdf' 
      },
      { 
        title: 'Renueva tu ser: Las últimas tendencias en relajación y bienestar para 2024', 
        date: '2024-02-03',
        type: 'noticias',
        imageUrl: '/sources/Noticias/Tendencias.jpg',
        pdfUrl: '/sources/Noticias/UltimasTendencias2024.pdf' 
      }
    ];
  
    const carrusel = document.querySelector('.carrusel');
    const prevButton = document.querySelector('.prev');
    const nextButton = document.querySelector('.next');
  
    let index = 0;
    const noticiasVisibles = 3;
  
    function createNoticiaElement(noticia) {
      const noticiaElement = document.createElement('div');
      noticiaElement.classList.add('noticia_op');
      
      noticiaElement.innerHTML = `
        <div class="imagen_noticia">
          <img src="${noticia.imageUrl}" alt="${noticia.title}">
        </div>
        <div class="descripcion_noticia">
          <h4>${noticia.type}</h4>
          <p>Fecha: ${noticia.date}</p>
          <h1>${noticia.title}</h1>
        </div>
      `;
      
      noticiaElement.addEventListener('click', () => {
        window.open(noticia.pdfUrl, '_blank'); // Abre el PDF en una nueva pestaña
      });
  
      return noticiaElement;
    }
  
    function mostrarNoticias() {
      carrusel.innerHTML = '';
      for (let i = 0; i < noticiasVisibles; i++) {
        const noticiaIndex = (index + i) % noticias.length;
        const noticiaElement = createNoticiaElement(noticias[noticiaIndex]);
        carrusel.appendChild(noticiaElement);
      }
    }
  
    prevButton.addEventListener('click', () => {
      index = (index - 1 + noticias.length) % noticias.length;
      mostrarNoticias();
    });
  
    nextButton.addEventListener('click', () => {
      index = (index + 1) % noticias.length;
      mostrarNoticias();
    });
  
    // Inicializa el carrusel mostrando las primeras noticias
    mostrarNoticias();
  });
  