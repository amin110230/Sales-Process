'use strict';

LoadOrders();

function LoadOrders() {
    $.ajax({
        type: 'GET',
        url: '/Order/GetAllOrders',
        data: {
            pIsPaid: null
        },
        success: function (data) {
            console.log(data);
            var order_table = '';
            $.each(data, function (i, item) {
                var row = '<tr class="order_' + item.Id + '">';
                row += '<td>' + item.OrderId + '</td>';
                row += '<td class="text-center" id="OrderDate_' + item.Id + '">' + GetDateStr(item.OrderDate) + '</td>';
                row += '<td class="text-center">' + item.OrderTotalAmount + '</td>'
                row += '<td class="text-center">' + OrderStatus(item.IsPaid) + '</td>';

                row += '<td class="text-center">';
                row += '<button class="btn btn-outline-primary btn-sm" style="margin: 5px;" onclick="OpenInvoice(event, ' + item.Id + ')">';
                row += '<i class="far fa-eye"></i></button>';
                row += '</td></tr>';

                order_table += row;
            });
            $('.orders-list').empty().append(order_table);
        },
        error: function (resp) {

        }
    });
}

function OrderStatus(value) {
    if (value)
        return '<b class="bg-success order-status-lbl">FULLY PAID</b>';
    else
        return '<b class="bg-warning order-status-lbl">PENDING</b>';
}

function OpenInvoice(event, id) {
    var productRows = '',
        buyerRows = '',
        totalProductPrice = 0,
        totalBuyersAmount = 0,
        totalDue = 0;

    $.ajax({
        type: 'POST',
        url: '/Product/ProductsOrderedByOrder',
        data: {
            pId: id
        },
        success: function (record) {
            $.each(record, function (i, data) {
                totalProductPrice += parseFloat(data.Price);
                var row = '<tr>';
                row += '<td>' + ++i + '</td>';
                row += '<td>' + data.ProductName + '</td>';
                row += '<td class="text-center">' + data.Quantity + '</td>';
                row += '<td class="text-center">' + data.Price + '</td>';
                row += '</tr>';
                productRows += row;
            });
            productRows += '<tr><td colspan="3" class="text-right">Grand Total</td><td class="text-center">' + totalProductPrice + '</td></tr>';
            $('#invoiceOrderScreenProducts').empty().append(productRows);
        },
        error: function (resp) {
            console.log(resp);
        }
    });

    $.ajax({
        type: 'POST',
        url: '/Buyer/BuyersOrderedByOrder',
        data: {
            pId: id
        },
        success: function (record) {
            $.each(record, function (i, data) {
                totalBuyersAmount += parseFloat(data.TotalAmount);
                totalDue += parseFloat(data.DueAmount);
                var row = '<tr>';
                row += '<td>' + ++i + '</td>';
                row += '<td>' + data.BuyersName + '</td>';
                row += '<td class="text-center">' + data.PayablePCT + '</td>';
                row += '<td class="text-center">' + data.TotalAmount + '</td>';
                row += '<td class="text-center">' + data.DueAmount + '</td>';
                row += '</tr>';
                buyerRows += row;
            });
            buyerRows += '<tr><td colspan="3" class="text-right">Grand Total</td><td class="text-center">' + totalBuyersAmount + '</td><td class="text-center">' + totalDue + '</td></tr>';
            $('#invoiceOrderScreenBuyers').empty().append(buyerRows);
            if (totalDue === 0)
                $("#PaymentStatus").removeClass('bg-danger').addClass('bg-success').text("FULLY PAID");
            else
                $("#PaymentStatus").removeClass('bg-success').addClass('bg-danger').text(totalDue + '/= Due');
        },
        error: function (resp) {
            console.log(resp);
        }
    });

    $("#today").text($("#OrderDate_" + id).text());
    $("#InvoiceModal").modal('toggle');
}
