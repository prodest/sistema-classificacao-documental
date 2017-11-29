window.mdc.autoInit();
let menu = new mdc.menu.MDCSimpleMenu(document.querySelector('.mdc-simple-menu'));
document.querySelector('.toggle-avatar').addEventListener('click', () => menu.open = !menu.open);

$(document).ajaxStart(function () {
    $("#DivConteudo").hide();
    $("#loading").show();
});
$(document).ajaxStop(function () {
    $("#loading").hide();
    $("#DivConteudo").show();
});
$(document).ajaxComplete(function (evento, request, ajaxOptions) {
    componentHandler.upgradeDom();
    _messages.forEach(ShowMessage);
    //limpa as mensagens já notificadas
    _messages = [];
});


$('#DivConteudo').on('click', 'button.BotaoAjax, li.ItemAjax', function (e) {
    //console.log('passou');
    e.stopImmediatePropagation();
    e.preventDefault();
    var urlDestino = $(this).attr('data-url');
    $.get(urlDestino).then(function (dados) {
        $('#DivConteudo').html(dados);
        $.validator.unobtrusive.parse($('form'));
    });
});

$('#DivConteudo').on('change', '#entidade_CriterioRestricao_Id', function (e) {
    e.stopImmediatePropagation();
    e.preventDefault();
    var id = $('#entidade_CriterioRestricao_Id').val();
    $.get('/TermoClassificacaoInformacao/DocumentosByCriterio?idCriterioRestricao='+id).then(function (dados) {
        $('#DivCriterioRestricaoDocumentos').html(dados);
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



function AdicionarMDCTextField(item, index) {
    mdc.textfield.MDCTextfield.attachTo(item)
}


function AdicionarAcaoMenu(item, index) {
    let id = $(item).attr('data-id');
    let menuAcoes = new mdc.menu.MDCSimpleMenu(item);
    document.querySelector('.toggle[data-id="'+id+'"]').addEventListener('click', () => menuAcoes.open = !menuAcoes.open);
}
