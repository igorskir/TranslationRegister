function setfilter(obj) {
    var path = obj.context.pathname.split('/')[1];
    sessionStorage.setItem(path, obj.context.id);
    
}
$(function () {
    var selectedID = sessionStorage.getItem(window.location.pathname.split('/')[1]);
    //console.log(window.location.pathname);
    if (selectedID != null) {
        $("#" + selectedID).click();
    }
});
(function () {
    $(document).on('click', '.ajaxToggleBtn', function (e) {
        var updatedBlockId = $(this).attr("data-ajax-update");
        var updatedBlock = $(updatedBlockId);
        var myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                updatedBlock.html(result);
                updatedBlock.toggle("fast");
            }
        });
    });

    $(document).on('click', '.ajaxBtn', function (e) {
        updatedBlock = $(this).attr("data-ajax-update");
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            
            success: function (result) {
                $(updatedBlock).html(result);
            }
        });
    });

    $(document).on('click', '.ajaxToggleCard', function (e) {
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

    $(document).ajaxStart(function () {
        $('#loader').show();
    }).ajaxStop(function () {
        $('#loader').hide();
    });

    $(document).on('click', '.filter', function () {
        setfilter($(this));
        $(".filter").removeClass("filter-active");
        $(this).addClass("filter-active");
    });

    $(document).on('click', '.main-menu-button', function () {
        var selection = $(this).find(".menuSelection");
        $(".menuSelection").removeClass("currentMenuSelection");
        selection.addClass("currentMenuSelection");
        localStorage['currentMenuItem'] = selection.attr("id");
    });

    $(document).ready(function () {
        var currentMenuItem = localStorage['currentMenuItem'];
        if (!currentMenuItem) {
            localStorage['currentMenuItem'] = "Projects";
        }
        else {
            var selector = "#" + localStorage['currentMenuItem'];
            var selection = $(selector);
            $(".menuSelection").removeClass("currentMenuSelection");
            selection.addClass("currentMenuSelection");
        }
    });

    $(document).on('click', '.loadModal', function (e) {
        e.preventDefault();
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                if (!$('modal#delModal').length) {
                    $("<div class=\"modal fade\" id=\"modal\" role=\"dialog\"></div>").appendTo("body");
                }
                $("#modal").html(result);
                $('#modal').modal({ show: true });
            }
        });
    });

    $(document).on("click", ".hide-modal", function (e) {
        $("#modal").modal("hide");
    });

    $(".addBtn").on("click", function (e) {
        e.preventDefault();
        $.ajax({
            type: 'GET',
            url: this.href,
            success: function (result) {
                if (!$('modal#addModal').length) {
                    $("<div class=\"modal fade addModal\" id=\"addModal\" role=\"dialog\"></div>").appendTo("body");
                }
                $("#addModal").html(result);
                $('#addModal').modal({ show: true });
            }
        });
    });

    $(document).on('click', '#searchBtn', function (e) {
        e.preventDefault();
        updatedBlock = $(this).attr("data-ajax-update");
        var token = $("#searchToken").val();
        if (token) {
            $.ajax({
                type: 'GET',
                url: $(this).attr("data-href"),
                data: { searchToken: token },
                success: function (result) {
                    $(updatedBlock).html(result);
                    $(".filter").removeClass("filter-active");
                }
            });
        }
        else {
            alert("Сначала введите поисковой запрос!");
            $("#searchToken").focus();
        }
    });
})(jQuery);