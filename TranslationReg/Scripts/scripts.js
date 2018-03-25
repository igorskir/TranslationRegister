(function ()
{
    $(".ajaxToggleCard").on('click', function (e) {
        updatedBlock = $(this).attr("data-ajax-update");
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                $(updatedBlock).html(result).hide();
                $(updatedBlock).toggle();
            }
        });
    });

    $(".ajaxToggleBtn").one('click', function (e) {
        updatedBlock = $(this).attr("data-ajax-update");
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                $(updatedBlock).html(result).hide();
                $(updatedBlock).toggle("fast");
                $(e.target).on("click", function () {
                    var updatedBlock = $(this).attr("data-ajax-update");
                    $(updatedBlock).toggle("fast");
                });
            }
        });
    });

    $(".filter").click(function () {
        $(".filter").removeClass("filter-active");
        $(this).addClass("filter-active");
    });

    $(".deleteBtn").on("click", function (e) {
        e.preventDefault();
        $.ajax({
            type: 'GET',
            url: this.href,
            success: function (result) {
                if (!$('modal#myModal').length) {
                    $("<div class=\"modal fade\" id=\"myModal\" role=\"dialog\"></div>").appendTo("body")
                }
                $("#myModal").html(result);
                $('#myModal').modal({ show: true });
            }
        });
    });
})(jQuery);