'use strict';

function LoadOrders() {
    $.ajax({
        type: 'GET',
        url: '/Order/GetAllOrders',
        data: {
            pIsPaid: false
        },
        success: function (record) {
            $("#ddlOrders").empty();
            var options = '';
            $.each(record, function (i, data) {
                options += '<option value="' + data.Id + '">' + data.OrderId + '</option>';
            });
            $("#ddlOrders").append(options);
            $("#ddlOrders").selectpicker('refresh');
        },
        error: function (resp) {
            AlertError("Oops! Something went wrong!");
        }
    }).done(function () {
        ChangeOrder();
    });
}

LoadOrders();

function ChangeOrder() {
    var id = $("#ddlOrders").val(),
            orderId = $("#ddlOrders option:selected").text();
    if (id === null || id === '') {
        id = 0;
        orderId = '';
    }

    LoadOrderDetails(id, orderId);
}

function LoadOrderDetails(id, orderId) {
    $.ajax({
        type: 'GET',
        url: '/Order/GetBuyersOfOrder',
        data: {
            id: id
        },
        success: function (record) {
            var grandTotal = 0,
                paidTotal = 0,
                dueTotal = 0;
            var tablerows = '';
            $.each(record, function (i, data) {
                var row = '<tr id="PaymentBuyer_' + data.Id + '">';
                row += '<td>' + data.BuyersName + '</td>';
                row += '<td class="text-center">' + data.TotalAmount + '</td>';
                row += '<td class="text-center">' + data.PaidAmount + '</td>';
                row += '<td class="text-center" id="DueAmount_' + data.Id + '">' + data.DueAmount + '</td>';
                row += '<td class="text-center">';
                row += '<input type="number" class="form-control payment-grid-input text-center" id="Payment_' + data.Id + '" onblur="CalculatePaymentTotal(event)"/>';
                row += '</td></tr>';

                tablerows += row;
                grandTotal += parseFloat(data.TotalAmount);
                paidTotal += parseFloat(data.PaidAmount);
                dueTotal += parseFloat(data.DueAmount);
            });

            tablerows += '<tr><td class="text-right">Grand Total</td>';
            tablerows += '<td class="text-center">' + grandTotal + '</td>';
            tablerows += '<td class="text-center text-success">' + paidTotal + '</td>';
            tablerows += '<td class="text-center text-danger">' + dueTotal + '</td>';
            tablerows += '<td class="text-center" id="PaymentTotal_' + id + '"></td></tr>';

            $('#tblPayment').empty().append(tablerows);
            if (record.length > 0)
                $('#payment-entry').fadeIn();
            else
                $('#payment-entry').fadeOut();
        },
        error: function (resp) {
            console.log(resp);
        }
    });
}

function CalculatePaymentTotal(event) {
    var payment = $(event.target).val();
    if (isNaN(payment)) {
        $(event.target).val(0);
        return false;
    }

    if (parseFloat(payment) < 0) {
        AlertError("Payment cannot be negative");
        $(event.target).val(0);
        return false;
    }

    var buyer_order_id = $(event.target).closest('tr').attr('id').split('PaymentBuyer_')[1];
    var due_amount = parseFloat($("#DueAmount_" + buyer_order_id).text());
    if (parseFloat(payment) > due_amount)
        $(event.target).val(due_amount);

    var total_payment = 0;

    $('.payment-grid-input').each(function () {
        var value = $(this).val();
        value = value === '' ? 0 : parseFloat(value);

        total_payment += value;
    });

    $("#PaymentTotal_" + $("#ddlOrders").val()).text(total_payment);
}

function MakePayment(event) {
    var total_payment = 0,
        buyersOrderDetailIds = [],
        buyersPayment = [],
        id = $("#ddlOrders").val(),
        orderId = $("#ddlOrders option:selected").text();

    $('.payment-grid-input').each(function () {
        var buyerOrderDetailsId = $(this).closest('tr').attr('id').split('PaymentBuyer_')[1];
        var value = $(this).val();
        value = value === '' ? 0 : parseFloat(value);

        total_payment += value;
        buyersOrderDetailIds.push(buyerOrderDetailsId);
        buyersPayment.push(value);
    });

    total_payment = total_payment === '' ? 0 : parseFloat(total_payment);

    if(total_payment === 0 ){
        AlertError("No payment amount provided! Please provide payment amount");
        return false;
    }

    $.ajax({
        type: 'POST',
        url: '/Order/MakePayment',
        data: {
            pId: id,
            pBuyerOrderIds: buyersOrderDetailIds,
            pBuyersPayments: buyersPayment
        },
        success: function (resp) {
            AlertSuccess("Payment Successfull! Thank You");
            LoadOrders();
        },
        error: function (resp) {
            console.log(resp);
        }
    });
}