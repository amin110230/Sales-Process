'use strict';

LoadBuyers();

function LoadBuyers() {
    $.ajax({
        type: 'GET',
        url: '/Buyer/GetAllBuyers',
        data: {
            pIsActive: true
        },
        success: function (data) {
            var buyer_table = '';
            $.each(data, function (i, item) {
                var row = '<tr class="buyer_' + item.Id + '">';
                row += '<td rel="BuyersName">' + item.BuyersName + '</td>';
                row += '<td rel="BuyersCode" class="text-center">' + item.BuyersCode + '</td>';
                row += '<td rel="BuyersRegion" class="text-center">' + item.BuyersRegion + '</td>';
                row += '<td rel="BuyersMobile" class="text-center">' + item.BuyersMobile + '</td>';
                row += '<td rel="BuyersEmail" class="text-center">' + item.BuyersEmail + '</td>';
                row += '<td class="text-center">';
                row += '<button class="btn btn-outline-primary btn-sm" title="Payment Log" style="margin: 5px;" onclick="PaymentLog(event,' + item.Id + ',\''+item.BuyersName+'\')">';
                row += '<i class="far fa-eye"></i></button>';
                row += '<button class="btn btn-outline-primary btn-sm grid-action-button" title="Edit" data-toggle="modal" data-target="#BuyersModal" onclick="EditBuyerModal(event, ' + item.Id + ')">';
                row += '<i class="far fa-edit"></i></button>';
                row += '<button class="btn btn-outline-danger btn-sm grid-action-button" title="Delete" onclick="DeleteBuyer(' + item.Id + ',event)">';
                row += '<i class="far fa-trash-alt"></i></button>';
                row += '</td></tr>';

                buyer_table += row;
            });
            $('.buyers-list').empty().append(buyer_table);
        },
        error: function (resp) {

        }
    });
}

function AddBuyerModal(event) {
    $('#AddUser').show();
    $('#UpdateUser').hide();
    $('#BuyersModalTitle').empty().text("New Buyer");
    ResetModalInputs();
}

function EditBuyerModal(event, id) {
    $('#AddUser').hide();
    $('#UpdateUser').show();
    $("#BuyersId").val(id);
    $(event.target).closest('tr').find('td').each(function () {
        if ($(this).attr('rel') !== undefined)
            $('#' + $(this).attr('rel')).val($(this).text());
    });

    $('#BuyersModalTitle').empty().text("Edit Buyer's Infromation");
}

function AddBuyer(event) {
    var validation_out = ValidateBuyerCU();
    if (validation_out !== 'VALID') {
        AlertError(validation_out);
        return false;
    }
    else {
        var buyer = {
            id: 0,
            buyersname: $('#buyersname').val(),
            buyerscode: $('#buyerscode').val(),
            buyersregion: $('#buyersregion').val(),
            buyersmobile: $('#buyersmobile').val(),
            buyersemail: $('#buyersemail').val(),
            isactive: true,
            createdon: null
        };
        $.ajax({
            type: 'post',
            url: '/buyer/addbuyer',
            data: { buyer: buyer },
            success: function (resp) {
                $('#buyersmodal').modal('toggle');
                toastr.options.positionclass = "toast-bottom-right";
                toastr.success("buyer has been added successfully!");
                loadbuyers();
            },
            error: function (resp) {
                toastr.options.positionclass = "toast-bottom-right";
                toastr.error("oops! something went wrong!");
            }
        });
    }
}

//function AddBuyer(event) {
//    var buyer = {
//        Id: 0,
//        BuyersName: $('#BuyersName').val(),
//        BuyersCode: $('#BuyersCode').val(),
//        BuyersRegion: $('#BuyersRegion').val(),
//        BuyersMobile: $('#BuyersMobile').val(),
//        BuyersEmail: $('#BuyersEmail').val(),
//        IsActive: true,
//        CreatedOn: null
//    };
//    $.ajax({
//        type: 'POST',
//        url: '/Buyer/AddBuyer',
//        data: { buyer: buyer },
//        success: function (resp) {
//            $('#BuyersModal').modal('toggle');
//            toastr.options.positionClass = "toast-bottom-right";
//            toastr.success("Buyer has been added successfully!");
//            LoadBuyers();
//        },
//        error: function (resp) {
//            toastr.options.positionClass = "toast-bottom-right";
//            toastr.error("Oops! Something went wrong!");
//        }
//    });
//}

function PaymentLog(event, id, name) { 
    $("#BuyersId").val(id);
    $(".buyers-name").text(name);
    $("#BuyersPaymentDetails").fadeIn();
    LoadOrdersByBuyers(id);

    $('html, body').animate({
        scrollTop: $("#BuyersPaymentDetails").offset().top
    }, 500);
}

function ClosePaymentDetails() {
    $("#BuyersPaymentDetails").fadeOut(300);
}

function EditBuyer(event) {
    var validation_out = ValidateBuyerCU();
    if (validation_out !== 'VALID') {
        AlertError(validation_out);
        return false;
    }
    else {

        var buyer = {
            Id: $('#BuyersId').val(),
            BuyersName: $('#BuyersName').val(),
            BuyersCode: $('#BuyersCode').val(),
            BuyersRegion: $('#BuyersRegion').val(),
            BuyersMobile: $('#BuyersMobile').val(),
            BuyersEmail: $('#BuyersEmail').val(),
            IsActive: true,
            CreatedOn: null
        };
        $.ajax({
            type: 'POST',
            url: '/Buyer/UpdateBuyer',
            data: { buyer: buyer },
            success: function (resp) {
                $('#BuyersModal').modal('toggle');
                toastr.options.positionClass = "toast-bottom-right";
                toastr.success("Buyer info updated successfully!");

                LoadBuyers();
            },
            error: function (resp) {
                toastr.options.positionClass = "toast-bottom-right";
                toastr.error("Oops! Something went wrong!");
            }
        });
    }
}

function DeleteBuyer(id, event) {
    var result = confirm("Are you sure you want to delete this buyer?");
    if (result === true) {
        $.ajax({
            type: 'POST',
            url: '/Buyer/DeleteBuyer',
            data: { id: id },
            success: function (resp) {
                $(event.target).closest('tr').remove();
                ClosePaymentDetails();
                toastr.options.positionClass = "toast-bottom-right";
                toastr.success("Buyer has been removed successfully!");
            },
            error: function (resp) {
                toastr.options.positionClass = "toast-bottom-right";
                toastr.error("Oops! Something went wrong!");
            }
        });
    }
}

function LoadOrdersByBuyers(buyerId) {
    $.ajax({
        type: 'GET',
        url: '/Order/GetOrdersByBuyer',
        data: {
            buyerId: buyerId
        },
        success: function (record) {
            $("#ddlOrdersPayment").empty();
            var options = '<option value="0">All</option>';
            $.each(record, function (i, data) {
                options += '<option value="' + data.Id + '">' + data.OrderId + '</option>';
            });
            $("#ddlOrdersPayment").append(options);
            $("#ddlOrdersPayment").selectpicker('refresh');
        },
        error: function (resp) {
            AlertError("Oops! Something went wrong!");
        }
    }).done(function () {
        LoadPaymentLog();
    });
}

function LoadPaymentLog() {
    var orderId = $("#ddlOrdersPayment").val();
    if (orderId === null || orderId === '')
        orderId = 0;

    var buyerId = $("#BuyersId").val();
    $.ajax({
        type: 'GET',
        url: '/Order/GetPaymentLogOfBuyer',
        data: {
            pBuyerId: buyerId,
            pOrderId: orderId
        },
        success: function (record) {
            var buyer_log = '';
            $.each(record, function (i, item) {
                var row = '<tr>';
                row += '<td>' + item.OrderId + '</td>';
                row += '<td class="text-center">' + GetDateStr(item.CreatedOn) + '</td>';
                row += '<td class="text-center">' + item.PaidAmount + '</td>';
                row += '<td class="text-center">' + item.AmountPayable + '</td>';
                row += '</tr>';

                buyer_log += row;
            });
            $('#PaymentLog').empty().append(buyer_log);
        },
        error: function (resp) {
            AlertError("Oops! Something went wrong!");
        }
    });
}

function ResetModalInputs() {
    $('#BuyersId').val('');
    $('#BuyersName').val('');
    $('#BuyersCode').val('');
    $('#BuyersRegion').val('');
    $('#BuyersMobile').val('');
    $('#BuyersEmail').val('');
}