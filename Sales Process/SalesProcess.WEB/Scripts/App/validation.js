'use strict';

$('.positive').on('keyup', function (e) {
    console.log(e.keyCode);
});

function ValidateProductCU() {
    var validation = 'VALID';

    var name = $('#ProductName').val();
    var code = $('#ProductCode').val();
    var unit = $('#ProductUnit').val();
    var price = $('#ProductPrice').val();
    var quantity = $('#StockQuantity').val();
    if (name === '') {
        validation = 'Provide Product Name';
    }
    else if (code === '') {
        validation = 'Provide Product Code';
    }
    else if (unit === '') {
        validation = 'Provide Product Unit';
    }
    else if (price === '' || isNaN(price)) {
        validation = 'Provide numeric value for price';
    }
    else if (quantity === '' || isNaN(quantity)) {
        validation = 'Provide numeric value for Stock Quantity';
    }

    return validation;
}

function ValidateBuyerCU() {
    var validation = 'VALID';

    var name = $('#BuyersName').val();
    var code = $('#BuyersCode').val();
    var region = $('#BuyersRegion').val();
    var mobile = $('#BuyersMobile').val();
    var email = $('#BuyersEmail').val();
    if (name === '') {
        validation = 'Provide Buyer Name';
    }
    else if (code === '') {
        validation = 'Provide Buyer Code';
    }
    else if (region === '') {
        validation = 'Provide Buyer Region';
    }
    else if (mobile === '' || !(/^\d{11}$/.test(mobile))) {
        validation = 'Provide numeric value for Mobile Number';
    }
    else if (email === '' || !isEmail(email)) {
        validation = 'Provide valid Email Address';
    }

    return validation;
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
