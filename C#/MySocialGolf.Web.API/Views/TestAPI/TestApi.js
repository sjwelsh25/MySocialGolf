var MySocialGolf_TestApi = {

    RunTest: function (id) {
        var globalUrl = 'http://localhost:37217/';

        // do an Ajax back to Run the Server Side tests
        $.ajax({
            type: 'POST',
            url: globalUrl + 'testapi/RunTest',
            data: { id },
            cache: false,
            success: function (testApiDto) {
                $('#successDiv').text(testApiDto.TestAPIMessage);
                // make tick visible
                if (testApiDto.LastStatusCode == "200"){
                    $("#GreenTickImage_" + id).show();
                    $("#RedCrossImage_" + id).hide();
                    $('#successDiv').removeClass('alert-danger').addClass('alert-success');
                }
                else {
                    $("#RedCrossImage_" + id).show();
                    $("#GreenTickImage_" + id).hide();
                    $('#successDiv').removeClass('alert-success').addClass('alert-danger');
                }
                $('#lastResponseDiv_' + id).text(testApiDto.LastResponseCalculated);
            },
            error: function(xhr, status, error) {
                $('#successDiv').text('Error calling api: ' + xhr.responseText);
                $('#successDiv').removeClass('alert-success').addClass('alert-danger');
                $("#RedCrossImage_" + id).show();
            }
        });

        return true;
    }
};



$(document).ready(function () {

    $('.editButton').on('click', function (event) {

        var target = event.target || event.srcElement;
        var id = $(target).attr("data-id");
        // alert(id + " button clicked.");

        MySocialGolf_TestApi.RunTest(id);
        
    });

    return false;

});