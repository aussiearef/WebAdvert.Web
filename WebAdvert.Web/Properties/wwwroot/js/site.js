
$(function() {
	$(".boxClick").click(function() {
		var advertId = this.getAttribute("data-id");

		$.ajax({
			type: "GET",
			url: "/api/" + advertId,
			dataType: "json",
			success: function(data) {
				var baseImageUrl = $("#hidImagesBaseUrl").val();
				$("#detailImage").attr("src", baseImageUrl + "/" + data.filePath);
				$("#detailTitle").text(data.title);
				$("#detailDesc").text(data.description);
				$("#detailPrice").text("Price $" + data.price);

				$("#detailsModal").modal();
			}
		});
	});
});