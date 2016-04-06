var getAllInComboBox = function (combo) {

    var url = "http://localhost:25759/api/Roles";

    $.ajax({
        type: "GET",
        // Tell jQuery we're expecting JSONP
        url: url,
        contentType: "application/json",
        success: function (response) {
            //console.log(response); // server response
            bindToCombo(combo, response);
        }   
    });
};

var bindToCombo = function (combo, data) {

    $(data).each(function () {
        var option = $('<option />');
        option.attr('value', this.Id).text(this.Name);

        $(combo).append(option);
    });
}