(function ($) {
    $(document).ready(function () {
        var requestAjax = function (action, typeRequest, jsonData, dataType, jsonHeades, contentType) {

            if (typeof quantidade !== 'undefined') {

            }

            $.ajax({
                url: action,
                // POST - PUT - GET - DELETE
                type: typeRequest,
                headers: JSON.stringify(jsonHeades),
                //'application/json' // DEFINE TIPO DE ARQUIVO ESPERADO DO SERVIDOR
                dataType: dataType,
                //'application/json' // DEFINE TIPO DE ARQUIVO ENVIADO AO SERVIDOR
                contentType: contentType,
                data: JSON.stringify(jsonData),
                beforeSend: function () {
                    // EXECUTA ENQUANTO ENVIA A REQUISICAO
                }
            }).done(function (msg) {
                // EXECUTA ENQUANTO AO FINAL SE DER TUDO OK
            }).fail(function (jqXHR, textStatus, msg) {
                
            });// EXECUTA ENQUANTO AO FINAL SE DER FALHA
        }
    });
})(jQuery);