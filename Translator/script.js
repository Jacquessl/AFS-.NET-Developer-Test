$(document).ready(function() {
    $('#sendTextButton').click(function() {
        var textToTranslate = $('.form_field').val();
        var apiUrl = 'http://localhost:5246/Translator/' + encodeURIComponent(textToTranslate);
        $.ajax({
            url: apiUrl,
            method: 'GET',
            success: function(response) {
                $('#responseContainer').text(response);
                $('#responseContainer').val(response).addClass('show'); 
            },
            error: function(xhr, status, error) {
                console.error('Error: ', error);

                $('#responseContainer').text('Error occured');
            }
        });
    });
});
$('.form_field').on('input', function() {
    if (this.value.length > 200) {
        this.value = this.value.slice(0, 200);
    }
});