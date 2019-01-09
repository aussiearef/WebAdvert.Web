// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(".boxClick").click(function () {
        var advertId = this.getAttribute("data-id");

        $.ajax({
            type: 'GET',
            url: 'api/' + advertId,
            dataType: "json",
            success: function (data) {
                var baseImageUrl = $("#hidImagesBaseUrl").val();
                $("#detailImage").attr("src", baseImageUrl + '/' + data.filePath);
                $("#detailTitle").text(data.title);
                $("#detailDesc").text(data.description);
                $("#detailPrice").text('Price $' + data.price);

                $("#detailsModal").modal();
            }
        });
    });
});