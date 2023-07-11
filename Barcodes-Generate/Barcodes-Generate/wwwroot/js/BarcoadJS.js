// Barcoad Submit


function BarcoadSubmit() {
    // Serialize the form data
    var formData = $('.MainBarcoadDiv form').serialize();

    $.ajax({
        url: '/Barcoad/BarcoadGeneratedData',
        type: 'POST', 
        data: formData,
        success: function (response) {
            $('#BarcoadData').html(response); 

            console.log("Success");
        },
        error: function (xhr, textStatus, errorThrown) {
            alert("Something went wrong");
        }
    });
}