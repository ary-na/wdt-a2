// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Code sourced and adapted from:
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Template_literals
// https://stackoverflow.com/questions/22940464/how-do-i-display-a-value-from-an-input-field-in-a-bootstrap-modal
// https://stackoverflow.com/questions/5515310/is-there-a-standard-function-to-check-for-null-undefined-or-blank-variables-in

// Confirm Modal
$("#confirmModal").on("shown.bs.modal", function () {
    
    let amount = $("#Amount").val();
    let comment = $("#Comment").val();
    let destinationAccountNumber = $("#DestinationAccountNumber").val();

    $(".amount").html(`<li><span class="text-muted">Amount:</span> $${amount}</li>`);
    if (destinationAccountNumber)
        $(".destination-account-number").html(`<li><span class="text-muted">Destination Account Number:</span> ${destinationAccountNumber}</li>`);
    if (comment)
        $(".comment").html(`<li><span class="text-muted">Comment:</span> ${comment}</li>`);
});