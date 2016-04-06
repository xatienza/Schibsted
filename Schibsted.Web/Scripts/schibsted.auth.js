var setCookie = function (name, data) {
    var expiresDate = new Date();
    expiresDate.setTime(expiresDate.getTime() + (300 * 1000));

    $.cookie.json = true;

    if ($.cookie(name) == undefined) {
        $.cookie(name, data, { expires: expiresDate });
    }
}

var readCookie = function (name) {
    $.cookie.json = true;
    var cookie = $.cookie(name);
    return cookie;
}

var deleteCookie = function (name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;' + 'path=/';
    //$.removeCookie('name', { path: '/' });
}

var getAuthCookie = function () {
    var cookie = readCookie('schibsted.Auth');
    return cookie;
}

var checkPageSecurity = function (pageRole) {
    var currentCookie = getAuthCookie();

    if (currentCookie === undefined)
        redirectToLogin();

    var currentUserRole = getCurrentUserRole();

    if (currentUserRole != "ADMIN" && currentUserRole != pageRole )
    {
        alert('Sus permisos no permiten visualizar esta página.');
        window.location.href = '../index.html';
    }
}

var checkAdminPageSecurity = function () {
    var currentCookie = getAuthCookie();

    if (currentCookie === undefined)
        redirectToLogin();

    var currentUserRole = getCurrentUserRole();

    if (currentUserRole != "ADMIN") {
        alert('No tiene permisos de administrador');
        window.location.href = '../index.html';
    }
}

var getCurrentUser= function () {
    var currentCookie = getAuthCookie();
    return currentCookie;
}

var getCurrentUserRole = function () {
    var currentUser = getCurrentUser();
    return currentUser.Role;
}

var showCurrentUserName = function (container) {
    var currentUser = getCurrentUser();
    $(container).text(currentUser.Username);
}

var redirectToLogin =  function (){
    window.location.href = '../index.html';
}

var signIn = function (event) {

    event.preventDefault();

    var registrationUrl = "http://localhost:25759/api/Auth/SignIn";

    var model = {
        UserName: $("#username").val(),
        Password: $("#password").val()
    };

    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: registrationUrl,
        contentType: "application/json"
    }).done(function (res) {

        setCookie('schibsted.Auth', res);

        console.log('res', res);
        // Do something with the result :)
    });
};

var signOut = function (event) {
    event.preventDefault();
    deleteCookie('schibsted.Auth');
    redirectToLogin();
};