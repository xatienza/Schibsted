var getAllUsers = function () {

    var url = "http://localhost:25759/api/Users";

    $.ajax({
        type: "GET",
        // Tell jQuery we're expecting JSONP
        url: url,
        contentType: "application/json",
        success: function (response) {
            //console.log(response); // server response
            showAllUsers(response);
        }   
    });
};

var showAllUsers = function (users) {
    $.each(users, function (i, item) {

        var delButton = ' <button class="users_delete" id="delete' + item.Id + '" value="Delete" onclick="deleteUser(' + item.Id + ')">Delete</button>';
        var delUpdate = ' <button class="users_update" id="update' + item.Id + '" value="Update" onclick="showUpdateUserForm(' + item.Id + ')">Update</button>';

        var actions = delUpdate;

        if (item.UserName != 'admin')
            var actions = actions + delButton;

        $('<tr>').append(
        $('<td>').text(item.UserName),
        $('<td>').text(item.Id),
        $('<td>').html(actions)).appendTo('#users_table');
    });


}

var addUser = function (event) {

    event.preventDefault();

    var url = "http://localhost:25759/api/Users";

    var model = {
        UserName: $("#username").val(),
        Password: $("#password").val(),
        Role: $("#cmbRoles_ItemID").find('option:selected').text()
    };
    
    console.log(model);

    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: url,
        contentType: "application/json"
    }).done(function (res) {

        setCookie('schibsted.Auth', res);

        console.log('res', res);
        // Do something with the result :)
    });
}

var deleteUser = function(selector){
    console.log(selector);
}

var showUpdateUserForm = function (selector) {
    console.log(selector);
}