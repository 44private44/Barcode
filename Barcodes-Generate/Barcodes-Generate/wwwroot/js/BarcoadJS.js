// Barcoad Submit

function BarcoadSubmit() {
    // Serialize the form data
    var formData = $('.MainBarcoadDiv form').serialize();

    // Show loader
    $('#loader').show();

    $.ajax({
        url: '/Barcoad/BarcoadGeneratedData',
        type: 'POST',
        data: formData,
        success: function (response) {
            // Hide loader
            $('#loader').hide();    

            $('#BarcoadData').html(response);
        },
        error: function (xhr, textStatus, errorThrown) {
            // Hide loader
            $('#loader').hide();
            alert("Something went wrong");
        }
    });
}

