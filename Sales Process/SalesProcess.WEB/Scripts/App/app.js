'use strict';
toastr.options = {
    maxOpened : 1,
    autoDismiss: true,
    progressBar: true,
    closeButton : true,
    positionClass: "toast-bottom-right",
    fadeOut: 1000,
    timeOut: 2000
};

function AlertError(msg) {
    toastr.clear()
    toastr.error(msg);
}

function AlertSuccess(msg) {
    toastr.clear()
    toastr.success(msg);
}

function AlertWarning(msg) {
    toastr.clear()
    toastr.warning(msg);
}

function GetDateStr(dateStr) {
    //var dateString = "\/Date(1334514600000)\/".substr(6);
    var dateString = dateStr.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = ((day < 10) ? "0" + day : day) + "/" + ((month < 10) ? "0" + month : month) + "/" + year;
    return date;
}

function NavbarActive(bar_name) {
    $('.nav-link').each(function () {
        var name = $(this).attr('rel');
        $(this).removeClass('active');
        if (name === bar_name)
            $(this).addClass('active');
    });
}