document.getElementById('contactForm').addEventListener('submit', function(event) {
    event.preventDefault();

    var verificationInput = document.getElementById('verification').value;
    var correctKeyword = "SPASB";

    if (verificationInput === correctKeyword) {
        document.getElementById('successNotification').style.display = 'block';
        document.getElementById('errorNotification').style.display = 'none';

        // Limpiar el formulario
        document.getElementById('contactForm').reset();
        
        setTimeout(function(){
            document.getElementById('successNotification').style.display = 'none';
        }, 1000);
    } else {
        document.getElementById('errorNotification').style.display = 'block';
        setTimeout(function(){
            document.getElementById('errorNotification').style.display = 'none';
        }, 1000);
    }
});

