$(document).ready(function () {
    
    $('.ShopId').hide();
    $('#coordinates').hide();
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function(p) {
                $("#lg").html(p.coords.longitude);
                $("#lt").html(p.coords.latitude);
                var data = { Longitude: p.coords.longitude + '', Latitude: p.coords.latitude + '' };
                $.ajax({
                    type: "POST",
                    url: "/Shop/SetLocation",
                    data: data,
                    success: function (result) {
                        if (result == status.ok) {
                            window.reload();
                        }
                        window.console.log(result);
                    }
                });
                window.reload();
            });
        } else {
            alert('Geo Location feature is not supported in this browser.');
    }  
});
//delete from main page
function Delete(obj) {
    var ele = $(obj);
    var Id = ele.data("model-id");
    var url = "/Shop/DeleteFromMainPage/" + Id;
    $.ajax({
        url: url,
        type: "POST",

        success: function () {

            ele.closest("tr").remove();
        },

        error: function () {
            alert("Fails");
        }

    });
}