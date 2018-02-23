(function () {
    $(".editBtn").click(function () {
        updatedBlock = $(this).attr("data-ajax-update");
        $(updatedBlock).toggle("fast");
    });

    $(".createBtn").on('click', function () {
        updatedBlock = $(this).attr("data-ajax-update");
        $(updatedBlock).toggle("fast");
    });

    $(".filter").click(function () {
        $(".filter").removeClass("filter-active");
        $(this).addClass("filter-active");
    });

    $(".detailsBtn").click(function () {
        updatedBlock = $(this).attr("data-ajax-update");
        $(updatedBlock).toggle("fast");
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
                close: function () { $(this).remove() },
                modal: true
            })
            .load(this.href);
    });

    $(document).on('show.bs.tab', '.nav-tabs-responsive [data-toggle="tab"]', function (e) {
        var $target = $(e.target);
        var $tabs = $target.closest('.nav-tabs-responsive');
        var $current = $target.closest('li');
        var $parent = $current.closest('li.dropdown');
        $current = $parent.length > 0 ? $parent : $current;
        var $next = $current.next();
        var $prev = $current.prev();
        var updateDropdownMenu = function ($el, position) {
            $el
                .find('.dropdown-menu')
                .removeClass('pull-xs-left pull-xs-center pull-xs-right')
                .addClass('pull-xs-' + position);
        };

        $tabs.find('>li').removeClass('next prev');
        $prev.addClass('prev');
        $next.addClass('next');

        updateDropdownMenu($prev, 'left');
        updateDropdownMenu($current, 'center');
        updateDropdownMenu($next, 'right');
    });

})(jQuery);