var getAllUsers = function () {

    var url = "http://localhost:25759/api/Users";

    $.ajax({
        type: "GET",
        // Tell jQuery we're expecting JSONP
        url: url,
        contentType: "application/json",
        success: function (response) {
            console.log(response); // server response
            showAllUsers(response);
        }   
    });
};

var showAllUsers = function (users) {
    $.each(users, function (i, item) {
        $('<tr>').append(
        $('<td>').text(item.UserName),
        $('<td>').text(item.Id)).appendTo('#users_table');
        // $('#records_table').append($tr);
        //console.log($tr.wrap('<p>').html());
    });
}