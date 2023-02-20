// Path: StandBy-WEB\wwwroot\js\Add-Edit-Servico.js

var lock = new PatternLock("#lock", {
  onPattern: function (pattern) {
    // Context is the pattern lock instance
    console.log(pattern);
  },
});

// Inicializa o datepicker
var teste = flatpickr("#flatpickr", {
  locale: {
    firstDayOfWeek: 1,
    weekAbbreviation: "Sem",
    weekdays: {
      shorthand: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
      longhand: [
        "Domingo",
        "Segunda-feira",
        "Terça-feira",
        "Quarta-feira",
        "Quinta-feira",
        "Sexta-feira",
        "Sábado",
      ],
    },
    rangeSeparator: " até ",
    scrollTitle: "Clique para rolar",
    toggleTitle: "Clique para alternar",
    time_24hr: true,
    months: {
      shorthand: [
        "Jan",
        "Fev",
        "Mar",
        "Abr",
        "Mai",
        "Jun",
        "Jul",
        "Ago",
        "Set",
        "Out",
        "Nov",
        "Dez",
      ],
      longhand: [
        "Janeiro",
        "Fevereiro",
        "Março",
        "Abril",
        "Maio",
        "Junho",
        "Julho",
        "Agosto",
        "Setembro",
        "Outubro",
        "Novembro",
        "Dezembro",
      ],
    },
  },
  enableTime: true,
  altInput: true,
  weekNumbers: true,
  altFormat: "d M, Y - H:i:S",
  dateFormat: "d/m/Y H:i:S",
  onChange: function (selectedDates, dateStr, instance) {
    //console.log(instance);
    document.querySelector("#PrevisaoEntrega").value =
      selectedDates[0].toLocaleString();
  },
});

function ReceberSenhaPatternDaPartial(inputValue) {
  // Aqui você pode usar o valor digitado pelo usuário
  console.log("Senha recebida:", inputValue);
}

function EnviarSenhaPatternParaViewPai() {
  console.log("Enviando senha");
  var inputValue = document.getElementById("inputPassPattern").value;
  console.log("senha enviada: ", inputValue);
  // Envia o valor digitado pelo usuário para a View principal
  window.parent.ReceberSenhaPatternDaPartial(inputValue);
  VerCodigoImagem();
}

function VerCodigoImagem() {
  //document.querySelector("#containerPasswordLock").style.backgroundColor = "red";

  html2canvas(document.querySelector("#containerPasswordLock")).then(
    (canvas) => {
      console.log("Enviou o formulario");
      var base64Image = canvas.toDataURL();
      console.log("Base64 image: " + base64Image);
      document.querySelector("#ImageData").value = base64Image;
    }
  );
}

function loadModal() {
  TituloModalSenhaAtual();
  $("#exibirPatternPassword").modal("show");
}

function loadModalNovaSenha() {
  TituloModalCriandoNovaSenha();
  $("#exibirPatternPassword").modal("show");
}

function MudarSenha() {
  var root = document.getElementById("containerPasswordLock");
  console.log("root:", root);
  root.classList.remove("d-none");

  TituloModalEditandoSenha();

  var senhaAparelho = document.getElementById("senhaAparelho-container");
  senhaAparelho.classList.add("d-none");
  var btnMudarSenha = document.getElementById("btnMudarSenha");
  btnMudarSenha.classList.add("d-none");
}

$("#exibirPatternPassword").on("hidden.bs.modal", function (e) {
  LimparPattern();
});

function Cancelar() {
  console.log("Cancelando");
  LimparPattern();
  var root = document.getElementById("containerPasswordLock");
  root.classList.add("d-none");

  var senhaAparelho = document.getElementById("senhaAparelho-container");
  senhaAparelho.classList.remove("d-none");
  var btnMudarSenha = document.getElementById("btnMudarSenha");
  btnMudarSenha.classList.remove("d-none");
}

function LimparPattern() {
  lock.clear();
  var inputValue = document.getElementById("inputPassPattern");
  inputValue.value = "";
}

function TituloModalSenhaAtual() {
  var root = document.getElementById("tituloModal");
  root.classList.remove("text-warning");
  root.classList.add("text-primary");
  root.innerHTML = "Senha atual do aparelho";
}

function TituloModalEditandoSenha() {
  var root = document.getElementById("tituloModal");
  root.classList.remove("text-primary");
  root.classList.add("text-warning");
  root.innerHTML = "Desenhe a nova senha do aparelho";
}

function TituloModalCriandoNovaSenha() {
  var root = document.getElementById("tituloModal");
  root.classList.remove("text-info");
  root.classList.add("text-dark");
  root.innerHTML = "Cadastre uma nova senha para o aparelho";
}
