function SelecionarTodasCheckBoxes() {
  //console.log("entrou");
  var checkAll = document.getElementById("checkAll");
  var checkboxes = document.querySelectorAll("input[name='selectedIds']");

  for (var i = 0; i < checkboxes.length; i++) {
    //console.log(checkAll.checked);
    if (checkAll.checked == true) {
      //console.log("if", checkboxes[i]);
      checkboxes[i].checked = true;
    } else {
      //console.log("else", checkboxes[i]);
      checkboxes[i].checked = false;
    }
  }
}

function CalcularPrazo(_dataServico, _previsaoEntrega) {
  var dataServico = new Date(_dataServico);
  var previsaoEntrega = new Date(_previsaoEntrega);
  var diferencaDias = Math.round(
    (previsaoEntrega - dataServico) / (1000 * 60 * 60 * 24)
  );
  var diferencaDiasSemEspaco = diferencaDias.toString().replace(/\s/g, "");
  var diferencaDiasSemEspacoInteiro = parseInt(diferencaDiasSemEspaco);
  if (_previsaoEntrega == null) {
    return "";
  } else if (diferencaDiasSemEspacoInteiro < 0) {
    return diferencaDiasSemEspacoInteiro;
  }
  return diferencaDiasSemEspacoInteiro;
}

function ConverterData(data) {
  if (data == null) {
    return "";
  }
  var date = new Date(data);
  return date.toLocaleDateString("pt-BR", {
    day: "numeric",
    month: "numeric",
    year: "numeric",
  });
}
