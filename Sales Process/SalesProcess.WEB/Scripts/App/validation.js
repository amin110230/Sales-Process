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
