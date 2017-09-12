$(document).ajaxStart(function () {
    $("#DivConteudo").hide();
    $("#loading").show();
    //$(".mdl-layout__drawer").removeClass("is-visible");
    //$(".mdl-layout__obfuscator").addClass("is-visible");
});
$(document).ajaxStop(function () {
    $("#loading").hide();
    $("#DivConteudo").show();
    console.log('stop');
    //$(".mdl-layout__obfuscator").removeClass("is-visible");
});
$(document).ajaxComplete(function (evento, request, ajaxOptions) {
    console.log('complete');
    componentHandler.upgradeDom();
    //$.validator.unobtrusive.parse(ajaxOptions.currentTarget);
    //$.validator.unobtrusive.parse($('form'));
    _messages.forEach(ShowMessage);
    //limpa as mensagens já notificadas
    _messages = [];
});


$('#DivConteudo').on('click', 'button.BotaoAjax', function (e) {
    //console.log('passou');
    e.stopImmediatePropagation();
    e.preventDefault();
    var urlDestino = $(this).attr('data-url');
    $.get(urlDestino).then(function (dados) {
        $('#DivConteudo').html(dados);
        $.validator.unobtrusive.parse($('form'));
    });
});

function ShowMessage(item, index) {
    message = item.message;
    tipo = item.type; //para uso futuro
    if (message !== null && message !== '') {
        var snackbarContainer = document.querySelector('#notificacao');
        var data = {
            message: message,
            timeout: 4000
        };
        snackbarContainer.MaterialSnackbar.showSnackbar(data);
    }
}

