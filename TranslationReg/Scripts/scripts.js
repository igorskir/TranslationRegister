﻿(function () {
    //$(".ajaxToggleBtn").one('click', function (e) {
    //    updatedBlock = $(this).attr("data-ajax-update");
    //    myurl = $(this).attr("data-href");
    //    $.ajax({
    //        type: 'GET',
    //        url: myurl,
    //        success: function (result) {
    //            $(updatedBlock).html(result).hide();
    //            $(updatedBlock).toggle("fast");
    //            $(e.target).on("click", function () {
    //                var updatedBlock = $(this).attr("data-ajax-update");
    //                $(updatedBlock).toggle("fast");
    //            });
    //        }
    //    });
    //});

    $(".ajaxToggleBtn").on('click', function (e) {
        updatedBlock = $(this).attr("data-ajax-update");
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                $(updatedBlock).html(result);
                $(updatedBlock).toggle("fast");
            }
        });
    });

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

    $(".filter").click(function () {
        $(".filter").removeClass("filter-active");
        $(this).addClass("filter-active");
    });

    $(".deleteBtn").on("click", function (e) {
        e.preventDefault();
        myurl = $(this).attr("data-href");
        $.ajax({
            type: 'GET',
            url: myurl,
            success: function (result) {
                if (!$('modal#delModal').length) {
                    $("<div class=\"modal fade delModal\" id=\"delModal\" role=\"dialog\"></div>").appendTo("body");
                }
                $("#delModal").html(result);
                $('#delModal').modal({ show: true });
            }
        });
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

    $("#searchBtn").on("click", function (e) {
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