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
    $.removeCookie(name);
}

var getAuthCookie = function () {
    var cookie = readCookie('schibsted.Auth');
    return cookie;
}

var checkPageSecurity = function (pageRole) {
    var currentCookie = getAuthCookie();

    if (currentCookie === undefined)
        window.location.href = '../index.html';

    var currentUserRole = getCurrentUserRole();

    if (currentUserRole != "ADMIN" && currentUserRole != pageRole )
    {
        alert('Sus permisos no permiten visualizar esta página.');
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