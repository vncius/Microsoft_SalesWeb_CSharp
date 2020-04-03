(function ($) {
    $(document).ready(function () {
        $(".details").click(function () {
            var id = $(this).attr("data-value");
            var acao = $(this).attr("data-acao");
            $("#modal").load("/Departments/_ViewAcoes?id=" + id + "&EnumAcao=" + acao, function () {
                $("#modal").modal("show");
            })
        });

        $(".edit").click(function () {
            var id = $(this).attr("data-value");
            var acao = $(this).attr("data-acao");
            $("#modal").load("/Departments/_ViewAcoes?id=" + id + "&EnumAcao=" + acao, function () {
                $("#modal").modal("show");
            })
        });

        $(".delete").click(function () {
            var id = $(this).attr("data-value");
            var acao = $(this).attr("data-acao");
            $("#modal").load("/Departments/_ViewAcoes?id=" + id + "&EnumAcao=" + acao, function () {
                $("#modal").modal("show");
            })
        });

        $(".create").click(function () {
            $("#modal").load("/Departments/_ViewAcoes?EnumAcao=" + acao, function () {
                $("#modal").modal("show");
            })

            $("#modal").load("Create", function () {
                $("#modal").modal();
            })
        });
    });
})(jQuery);