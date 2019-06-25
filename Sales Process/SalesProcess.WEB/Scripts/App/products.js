'use strict';

function LoadProducts() {
    $.ajax({
        type: 'GET',
        url: '/Product/GetAllProducts',
        data: {
            pIsAvailable: true
        },
        success: function (data) {
            var product_table = '';
            $.each(data, function (i, item) {
                var row = '<tr class="product_' + item.Id + '">';
                row += '<td rel="ProductName">' + item.ProductName + '</td>';
                row += '<td rel="ProductCode" class="text-center">' + item.ProductCode + '</td>';
                row += '<td rel="ProductUnit" class="text-center">' + item.ProductUnit + '</td>';
                row += '<td rel="ProductPrice" class="text-center">' + item.ProductPrice + '</td>';
                row += '<td rel="StockQuantity" class="text-center">' + item.StockQuantity + '</td>';
                row += '<td class="text-center">';
                row += '<button class="btn btn-outline-primary btn-sm grid-action-button" data-toggle="modal" data-target="#ProductsModal" onclick="EditProductModal(event, ' + item.Id + ')">';
                row += '<i class="far fa-edit"></i></button>';
                row += '<button class="btn btn-outline-danger btn-sm grid-action-button" onclick="DeleteProduct(' + item.Id + ',event)">';
                row += '<i class="far fa-trash-alt"></i></button>';
                row += '</tr>';

                product_table += row;
            });
            $('.products-list').empty().append(product_table);
        },
        error: function (resp) {

        }
    });
}

function AddProductModal(event) {
    $('#AddProduct').show();
    $('#UpdateProduct').hide();
    $('#ProductsModalTitle').empty().text("New Product");
    ResetModalInputs();
}

function EditProductModal(event, id) {
    $('#AddProduct').hide();
    $('#UpdateProduct').show();
    $("#ProductsId").val(id);
    $(event.target).closest('tr').find('td').each(function () {
        if ($(this).attr('rel') !== undefined)
            $('#' + $(this).attr('rel')).val($(this).text());
    });
    $('#ProductsModalTitle').empty().text("Edit Product's Infromation");
}

function AddProduct(event) {
    var validation_out = ValidateProductCU();
    if (validation_out !== 'VALID') {
        AlertError(validation_out);
        return false;
    }
    else {
        var product = {
            Id: 0,
            ProductName: $('#ProductName').val(),
            ProductCode: $('#ProductCode').val(),
            ProductUnit: $('#ProductUnit').val(),
            ProductPrice: $('#ProductPrice').val(),
            StockQuantity: $('#StockQuantity').val(),
            IsAvailable: true,
            CreatedOn: null
        };
        $.ajax({
            type: 'POST',
            url: '/Product/AddProduct',
            data: { product: product },
            success: function (resp) {
                $('#ProductsModal').modal('toggle');
                AlertSuccess("Product has been added successfully!");
                LoadProducts();
            },
            error: function (resp) {
                AlertError("Oops! Something went wrong!");
            }
        });
    }
}

function EditProduct(event) {
    var validation_out = ValidateProductCU();
    if (validation_out !== 'VALID') {
        AlertError(validation_out);
        return false;
    }
    else {
        var product = {
            Id: $('#ProductsId').val(),
            ProductName: $('#ProductName').val(),
            ProductCode: $('#ProductCode').val(),
            ProductUnit: $('#ProductUnit').val(),
            ProductPrice: $('#ProductPrice').val(),
            StockQuantity: $('#StockQuantity').val(),
            IsAvailable: true,
            CreatedOn: null
        };
        $.ajax({
            type: 'POST',
            url: '/Product/UpdateProduct',
            data: { product: product },
            success: function (resp) {
                $('#ProductsModal').modal('toggle');
                AlertSuccess("Product info updated successfully!");

                LoadProducts();
            },
            error: function (resp) {
                AlertError("Oops! Something went wrong!");
            }
        });
    }
}

function DeleteProduct(id, event) {
    var result = confirm("Are you sure you want to delete this product?");
    if (result === true) {
        $.ajax({
            type: 'POST',
            url: '/Product/DeleteProduct',
            data: { id: id },
            success: function (resp) {
                $(event.target).closest('tr').remove();
                AlertSuccess("Product has been removed successfully!");
            },
            error: function (resp) {
                AlertError("Oops! Something went wrong!");
            }
        });
    }
}

function ResetModalInputs() {
    $('#ProductsId').val('');
    $('#ProductName').val('');
    $('#ProductCode').val('');
    $('#ProductUnit').val('');
    $('#ProductPrice').val('');
    $('#StockQuantity').val('');
}

LoadProducts();