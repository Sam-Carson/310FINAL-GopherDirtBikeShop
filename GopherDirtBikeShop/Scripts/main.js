
var uri = 'api/bike';

$(document).ready(function () {
    GetServerData();
});

function GetServerData() {
    // Send an AJAX request
    $.get(uri)
        .done(function (data) {
            $('#bikes').empty();
            console.log(data);
            // On success, 'data' contains a list of notes.
            $.each(data, function (key, item) {
                // Add a list item for the note.
                $('<li>', { text: formatItem(item) }).appendTo($('#bikes'));
            });
        });
}

function formatItem(item) {
    return item.BikeID + " : " + item.ComponentID + ' : ' + item.Type + ' : ' + item.Brand + ' : ' + item.Model + ' : ' + item.Size + ' : ' + item.Color + ' : ' + item.Description;
}

function find() {
    var id = $('#bikeID').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#bike').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#bike').text('Error: ' + err);
        });
}


function deleteBike() {
    var id = $('#deleteBikeID').val();
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        contentType: "application/json",
        success: function (response) {
            //alert("Bike successfully deleted in cloud");
            $('#deleteBikeID').val("");
            GetServerData();
        },
        error: function (response) {
            alert("Bike NOT deleted in cloud");
        }
    })
};


function addBike() {
    // bike information
    var brand = $('#newBrand').val();
    var model = $('#newModel').val();
    var type = $('#newType').val();
    var size = $('#newSize').val();
    var color = $('#newColor').val();
    var description = $('#newDescription').val();
    //bike components
    var fork = $('#newFork').val();
    var drivetrain = $('#newDrivetrain').val();
    var rearSuspension = $('#newRearSuspension').val();
    var wheelSize = $('#newWheelSize').val();

    var newBike = {
        BikeID: 0,
        Brand: brand,
        Model: model,
        Type: type,
        Size: size,
        Color: color,
        Description: description
    };
    console.log(newBike);

    $.ajax({
        url: uri,
        type: "POST",
        dataType: "json",
        contentType: 'application/json',
        data: JSON.stringify(newBike),
        success: function (response) {
            alert("New bike successfully added to cloud");
            GetServerData();
        },
        error: function (response) {
            alert("Bike NOT added in cloud");
        }
    });
}