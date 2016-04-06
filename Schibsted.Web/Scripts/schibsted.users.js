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

    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: url,
        contentType: "application/json"
    }).done(function (res) {
        getAllUsers();
        alert('New user added');
        
    });
}

var showUser = function (id) {
    var url = "http://localhost:25759/api/Users/" + id;

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        success: function (response) {

            console.log(response);
            showUserForm(response);
        }
    });
}

var updateUser = function()
{
    event.preventDefault();

    var url = "http://localhost:25759/api/Users";

    var model = {
        Id: $("#username").val(),
        UserName: $("#username").val(),
        Password: $("#password").val(),
        Role: $("#cmbRoles_ItemID").find('option:selected').text()
    };

    console.log(model);
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

    $('#id').val(data.Id);
    $('#username').val(data.UserName);
    $('#password').val(data.Password);

    $("#cmbRoles_ItemID option").each(function () {

        console.log($(this).text());

        if ($(this).text() == data.Role) {
            $(this).attr('selected', 'selected');
        }
    });

    $("#submit").val('Update');
    $("#submit").unbind("click");
    $("#submit").click(updateUser);

}

var showAddForm = function () {

    $('.form').show();

    $('#id').val(0);
    $('#username').val('');
    $('#password').val('');
    $("#cmbRoles_ItemID").val($("#cmbRoles_ItemID option:first").val());

    $("#submit").val('Add');

    $("#submit").unbind("click");
    $("#submit").click(addUser);


}