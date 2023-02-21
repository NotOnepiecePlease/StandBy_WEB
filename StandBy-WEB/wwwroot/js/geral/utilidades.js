//Funcao que mostra e esconde o card que exibe os erros
$(document).ready(function () {
  if ($(".validation-summary-errors li").length > 0) {
    console.log("Tem erros");
    $("#errorCard").removeClass("d-none");
    $("#errorCard").show();
  } else {
    console.log("Nao tem erros");
    $("#errorCard").addClass("d-none");
    $("#errorCard").hide();
  }
});
