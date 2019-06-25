'use strict';

function LoadProducts() {
    $.ajax({
        type: 'GET',
        url: '/Product/GetAllProducts',
        data: {
            pIsAvailable: true
        },
        success: function (resp) {
            $("#ddlProducts").empty();
            var options = "";
            $.each(resp, function (i, data) {
                options += '<option value="' + data.Id + '">' + data.ProductName + '</option>';
            });
            $("#ddlProducts").append(options);
            $("#ddlProducts").selectpicker('refresh');
        },
        error: function (resp) {
            console.log(resp);
        }
    });
}

function LoadBuyers() {
    $.ajax({
        type: 'GET',
        url: '/Buyer/GetAllBuyers',
        data: {
            pIsActive: true
        },
        success: function (resp) {
            $("ddlBuyers").empty();
            var options = "";
            $.each(resp, function (i, data) {
                options += '<option value="' + data.Id + '">' + data.BuyersName + '</option>';
            });
            $("#ddlBuyers").empty().append(options);
            $("#ddlBuyers").selectpicker('refresh');
        },
        error: function (resp) {
            console.log(resp);
        }
    });
}

function ProceedToNext() {
    var selected_products = $("#ddlProducts").val();
    var selected_buyers = $("#ddlBuyers").val();

    if (selected_buyers.length <= 0 || selected_products.length <= 0) {
        AlertError("Please select both buyers and products to proceed further!");
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Product/ProductsOfNewOrder',
            data: {
                pSelectedProducts: selected_products
            },
            success: function (records) {
                var rows = '';
                $.each(records, function (i, data) {
                    var row = '<tr id="Product_' + data.Id + '">';
                    row += '<td>' + ++i + '</td>';
                    row += '<td rel="ProductName" id="ProductName_' + data.Id + '">' + data.ProductName + '</td>';
                    row += '<td class="text-center">' + data.ProductCode + '</td>';
                    row += '<td class="text-center" id="UpdatedSTQ_' + data.Id + '" rel="' + data.StockQuantity + '">' + data.StockQuantity + '</td>';
                    row += '<td class="text-center" id="PerUnitPrice_' + data.Id + '">' + data.ProductPrice + '</td>';
                    row += '<td class="text-center order-quantity"><input type="number" onblur="CalculateProductPrice(event,' + data.Id + ')" class="form-control text-center mr-auto ml-auto ordered-product" /></td>';
                    row += '<td class="text-center product-total" id="TotalPrice_' + data.Id + '"></td>';
                    row += '<td><button class="btn btn-outline-danger btn-sm grid-action-button" onclick="RemoveProduct(event)"><i class="fas fa-times"></i></td>';

                    rows += row;
                });

                $("#tblProducts").empty().append(rows);
            },
            error: function (resp) {
                console.log(resp);
            }
        });

        $.ajax({
            type: 'POST',
            url: '/Buyer/BuyersOfNewOrder',
            data: {
                pSelectedBuyers: selected_buyers
            },
            success: function (records) {
                var rows = '';
                $.each(records, function (i, data) {
                    var row = '<tr id="Buyer_' + data.Id + '">';
                    row += '<td>' + ++i + '</td>';
                    row += '<td rel="BuyersName" id="BuyerName_' + data.Id + '">' + data.BuyersName + '</td>';
                    row += '<td rel="BuyersCode">' + data.BuyersCode + '</td>';
                    row += '<td class="text-center" rel="BuyersRegion">' + data.BuyersRegion + '</td>';
                    row += '<td class="text-center order-quantity"><input type="number" onblur="CalculateBuyersIndividualTotal(event,' + data.Id + ')" id="PayablePCT_' + data.Id + '" class="form-control text-center mr-auto ml-auto payable-pct" /></td>';
                    row += '<td class="text-right" rel="PayableAmount" id="AmountPayable_' + data.Id + '"></td>';
                    row += '<td><button class="btn btn-outline-danger btn-sm grid-action-button" onclick="RemoveBuyer(event)"><i class="fas fa-times"></i></td>';

                    rows += row;
                });

                $("#tblBuyers").empty().append(rows);
            },
            error: function (resp) {
                console.log(resp);
            }
        });
        $('.order-details').fadeIn();
        $('html, body').animate({
            scrollTop: $("#OrderDetails").offset().top
        }, 500);
    }
}

function CalculateProductPrice(event, id) {
    var old_amount = $(event.target).attr('rel');
    if (old_amount === undefined || old_amount === '')
        old_amount = 0;

    var ordered_quantity = $(event.target).val() === '' ? 0 : parseInt($(event.target).val());
    var stock_quantity = parseInt($("#UpdatedSTQ_" + id).attr('rel'));
    var per_unit_price = parseFloat($('#PerUnitPrice_' + id).text());

    if (ordered_quantity > stock_quantity || ordered_quantity < 0) {
        AlertWarning(ordered_quantity < 0 ? "Order cannot be less than 0" : "Cannot order more than Stock Quantity");
        ordered_quantity = old_amount;
        $(event.target).val(old_amount);
    }

    var ordered_total = per_unit_price * ordered_quantity;
    var available_stock_after_order = stock_quantity - ordered_quantity;

    $("#UpdatedSTQ_" + id).text(available_stock_after_order);
    $('#TotalPrice_' + id).text(parseFloat(ordered_total));
    $(event.target).attr('rel', ordered_quantity);

    $('#GrandTotal').text(parseFloat(GrandTotalPrice()));
    RecalculateBuyersPayableTotal();
}

function GrandTotalPrice() {
    var total = 0;
    $('.product-total').each(function (e) {
        total += parseInt($(this).text() === '' ? 0 : $(this).text());
    });
    return total;
}

function RemoveProduct(event) {
    $(event.target).closest('tr').remove();
    $('#GrandTotal').text(parseFloat(GrandTotalPrice()));
    RecalculateBuyersPayableTotal();
}

function CalculateBuyersIndividualTotal(event, id) {
    var old_pct = $(event.target).attr('rel');
    if (old_pct === undefined || old_pct === '')
        old_pct = 0;

    var buyersPCT = $(event.target).val() === '' ? 0 : parseFloat($(event.target).val());
    if (buyersPCT < 0) {
        AlertError("Invalid Percentage Value");
        return false;
    }
    if (!IsValidPCT()) {
        AlertError('Total percentage cannot be greater than 100');
        $(event.target).val(old_pct);
        return false;
    }
    var total_price = GrandTotalPrice();
    var amount_payable = total_price * (buyersPCT / 100);

    $('#AmountPayable_' + id).text(amount_payable.toFixed(2));
    $(event.target).attr('rel', buyersPCT);
}

function IsValidPCT() {
    var total = 0;
    $('.payable-pct').each(function (e) {
        total += parseFloat($(this).val() === '' ? 0 : $(this).val());
    });

    if (total > 100)
        return false;
    return true;
}

function RecalculateBuyersPayableTotal() {
    var grand_total = GrandTotalPrice();
    $('.payable-pct').each(function (e) {
        var Id = $(this).attr('id').split('PayablePCT_')[1];
        var pct = parseFloat($(this).val() === '' ? 0 : $(this).val());
        var amount_payable = grand_total * (pct / 100);

        $('#AmountPayable_' + Id).text(amount_payable.toFixed(2));
    });
}

function RemoveBuyer(event) {
    $(event.target).closest('tr').remove();
}

function ConfirmOrderModal(event) {
    var totalPCT = 0;
    var flag = true;
    $('.ordered-product').each(function () {
        var value = $(this).val() === '' ? 0 : parseInt($(this).val());
        var id = $(this).closest('tr').attr('id').split('Product_')[1];
        if (value === 0) {
            AlertError("Provide product quantity in all selected product.");
            flag = false;
            return false;
        }
    });

    $('.payable-pct').each(function () {
        var value = $(this).val() === '' ? 0 : parseFloat($(this).val());
        var id = $(this).closest('tr').attr('id').split('Buyer_')[1];
        if (value === 0) {
            AlertError("Provide percentage for all selected buyers.");
            flag = false;
            return false;
        }
        totalPCT += value;
    });

    if (totalPCT < 100) {
        AlertError("Sum of total percentage cannot be Less than 100");
        flag = false;
    }
    if (flag) {
        $("#ConfirmOrderModal").modal('toggle');
    }
    else {
        return false;
    }

    var today = new Date();
    var y = today.getFullYear();
    var m = today.getMonth() + 1;
        m = m < 10 ? '0' + m : m;
    var d = today.getDate();
    $("#today").text(d + "/" + m + "/" + y);
    var productRows = '';
    var buyerRows = '';
    var countProduct = 0,
        countBuyer = 0,
        totalProductPrice = 0,
        totalBuyersAmount = 0;

    $('.ordered-product').each(function () {
        ++countProduct;
        var id = $(this).closest('tr').attr('id').split('Product_')[1];
        var quantity = $(this).val() === '' ? 0 : parseInt($(this).val());
        var price = $("#TotalPrice_" + id).text();
        totalProductPrice += parseFloat(price);
        var productName = $("#ProductName_" + id).text();
        var row = '<tr>';
        row += '<td>' + countProduct + '</td>';
        row += '<td>' + productName + '</td>';
        row += '<td class="text-center">' + quantity + '</td>';
        row += '<td class="text-center">' + price + '</td>';
        row += '</tr>';
        productRows += row;
    });
    productRows += '<tr><td colspan="3" class="text-right">Grand Total</td><td class="text-center">' + totalProductPrice + '</td></tr>';

    $('.payable-pct').each(function () {
        ++countBuyer;
        var id = $(this).closest('tr').attr('id').split('Buyer_')[1];
        var amount = $("#AmountPayable_" + id).text();
        totalBuyersAmount += parseFloat(amount);
        var buyerName = $("#BuyerName_" + id).text();
        var row = '<tr>';
        row += '<td>' + countBuyer + '</td>';
        row += '<td>' + buyerName + '</td>';
        row += '<td class="text-center">' + amount + '</td>';
        row += '</tr>';
        buyerRows += row;
    });
    buyerRows += '<tr><td colspan="2" class="text-right">Grand Total</td><td class="text-center">' + totalBuyersAmount + '</td></tr>';

    countBuyer = 0;
    countProduct = 0;
    $('#orderScreenProducts').empty().append(productRows);
    $('#orderScreenBuyers').empty().append(buyerRows);
}

function Confirmation(event) {
    $('#ConfirmOrder').prop('disabled', true);
    var totalPCT = 0;
    var productIds = [];
    var productQuantity = [];
    var buyerIds = [];
    var buyerPCTs = [];
    var totalPrice = parseFloat($("#GrandTotal").text());
    //Generating Products
    $('.ordered-product').each(function () {
        var value = $(this).val() === '' ? 0 : parseInt($(this).val());
        var id = $(this).closest('tr').attr('id').split('Product_')[1];
        if (value === 0) {
            AlertError("Provide product quantity in all selected product.");
            return false;
        }

        productIds.push(id);
        productQuantity.push(value);
    });

    $('.payable-pct').each(function () {
        var value = $(this).val() === '' ? 0 : parseFloat($(this).val());
        var id = $(this).closest('tr').attr('id').split('Buyer_')[1];
        if (value === 0) {
            AlertError("Provide percentage for all selected buyers.");
            return false;
        }
        totalPCT += value;
        buyerIds.push(id);
        buyerPCTs.push(value);
    });

    if (totalPCT < 100) {
        AlertError("Sum of total percentage cannot be Less than 100");
        return false;
    }

    var result = confirm("Are you sure to place this order?");
    if (result) {
        $.ajax({
            type: 'POST',
            url: '/Order/PlaceOrder',
            data: {
                pProductIds: productIds,
                pProductQuantity: productQuantity,
                pBuyerIds: buyerIds,
                pBuyersPCT: buyerPCTs,
                pTotalPrice: totalPrice
            },
            success: function (resp) {
                AlertSuccess("Order Confirmed!");
                $(".order-details").fadeOut(100);
                $('#ConfirmOrder').prop('disabled', false);
                $("#ConfirmOrderModal").modal('toggle');
                window.location.href = '/Order/Order';
            },
            error: function (resp) {
                AlertError("Oops! Something went wrong!");
                $('#ConfirmOrder').prop('disabled', false);
            }
        });
    }
    else {
        $('#ConfirmOrder').prop('disabled', false);
    }
}

LoadProducts();
LoadBuyers();


