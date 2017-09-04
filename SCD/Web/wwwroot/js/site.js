//function aplicarAcaoBotaoAjax() {
//    $(".BotaoAjax").click(function (e) {

//        var urlDestino = $(this).attr('data-url');
//        $.get(urlDestino).then(function (dados) {
//            $('#DivConteudo').html(dados);
//        });
//    });
//}


$('#DivConteudo').on('click', 'button.BotaoAjax', function (e) {
    //console.log('passou');
    e.stopImmediatePropagation();
    e.preventDefault();
    var urlDestino = $(this).attr('data-url');
    $.get(urlDestino).then(function (dados) {
        $('#DivConteudo').html(dados);
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

