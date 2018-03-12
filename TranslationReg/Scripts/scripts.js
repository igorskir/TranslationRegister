(function () {
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

    $(".ajaxToggleCard").one('click', function (e) {
        updatedBlock = $(this).attr("data-ajax-update");
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                $(updatedBlock).html(result).hide();
                $(updatedBlock).toggle();
                $(e.target).on("click", function () {
                    var updatedBlock = $(this).attr("data-ajax-update");
                    $(updatedBlock).toggle();
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
        $("<div></div>")
            .addClass("dialog")
            .appendTo("body")
            .dialog({
                height: 300,
                width: 500,
                title: $(this).attr("data-dialog-title"),
                close: function () {
                    $(this).remove();
                },
                modal: true
            })
            .load(this.href);
    });

})(jQuery);