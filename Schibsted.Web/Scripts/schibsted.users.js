var getAllUsers = function () {

    var url = "http://localhost:25759/api/Users";

    $.ajax({
        type: "GET",
        // Tell jQuery we're expecting JSONP
        url: url,
        contentType: "application/json",
        success: function (response) {
            //console.log(response); // server response
            $("#users_table").find("tr:gt(0)").remove();
            showAllUsers(response);
        }   
    });
};

var showAllUsers = function (users) {
    $.each(users, function (i, item) {

        var delButton = '<button class="users_delete" id="delete' + item.Id + '" value="Delete" onclick="deleteUser(' + item.Id + ')">Delete</button>';
        var delUpdate = '<button class="users_update" id="update' + item.Id + '" value="Update" onclick="showUser(' + item.Id + ')">Update</button>';

        var actions = '';

        if (item.Username != 'admin')
            actions = delUpdate + delButton;

        $('<tr>').append(
        $('<td>').text(item.Username),
        $('<td>').text(item.Role),
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

    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: url,
        contentType: "application/json",
        error: function OnError(xhr, errorType, exception) {
            responseText = jQuery.parseJSON(xhr.responseText);
            alert(responseText.Message);
        },
    }).done(function (res) {
        getAllUsers();
        alert('New user added');
        $('.form').hide();
    });
}

var showUser = function (id) {
    var url = "http://localhost:25759/api/Users/" + id;

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        success: function (response) {

            //console.log(response);
            showUserForm(response);
        }
    });
}

var updateUser = function()
{
    event.preventDefault();

   

    var model = {
        Id: $('input[type=hidden]').val(),
        UserName: $("#username").val(),
        Password: $("#password").val(),
        Role: $("#cmbRoles_ItemID").find('option:selected').text()
    };

    var url = "http://localhost:25759/api/Users/Update";

    $.ajax({
        type: "PUT",
        data: JSON.stringify(model),
        url: url,
        contentType: "application/json",
    }).done(function (res) {

        getAllUsers();
        alert('User modified');
        $('.form').hide();
    });
}

var deleteUser = function(id){
    event.preventDefault();

    var url = "http://localhost:25759/api/Users/" + id;

    $.ajax({
        type: "DELETE",
        url: url,
        contentType: "application/json"
    }).done(function (res) {
        getAllUsers();
        alert('User delete');



        // Do something with the result :)
    });

}

var showUserForm = function (data) {
    
    $('.form').show();

    $('input[type=hidden]').val(data.Id);
    $('#username').val(data.Username);
    $('#password').val(data.Password);

    $('#cmbRoles_ItemID').prop('selectedIndex', 0);
    $("#cmbRoles_ItemID option:contains(" + data.Role + ")").prop('selected', true);

    $("#submit").val('Update');
    $("#submit").unbind("click");
    $("#submit").click(updateUser);

}

var showAddForm = function () {

    $('.form').show();

    $('input[type=hidden]').val(0);
    $('#username').val('');
    $('#password').val('');

    $('#cmbRoles_ItemID').prop('selectedIndex', 0);

    $("#submit").val('Add');

    $("#submit").unbind("click");
    $("#submit").click(addUser);


}