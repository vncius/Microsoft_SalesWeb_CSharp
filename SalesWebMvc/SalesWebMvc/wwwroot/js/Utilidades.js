(function ($) {
    $(document).ready(function () {
        $(".btn-crud").click(function () {
            executeModalCrud(this);
        });

        var executeModalCrud = function (context) {
            var acao = $(context).attr("data-acao");
            var id = $(context).attr("data-value");
            var controller = $(context).attr("data-controller");
            var url_acao = `/${controller}/_ViewAcoes?${id != undefined ? `id=${id}&` : ``}EnumAcao=${acao}`

            $("#modalAcoes").load(url_acao, function () {
                $("#modalAcoes").modal("show");
            })
        }
    });
})(jQuery);