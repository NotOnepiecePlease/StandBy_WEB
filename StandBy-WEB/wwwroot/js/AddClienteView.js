//Conexao com a API de cep e preenche os campos
$(document).ready(function () {
  $("#cleave-cep").change(function () {
    var cep = $(this).val().replace(/\D/g, "");
    // console.log(cep);
    // console.log("Length: " + cep.length);
    if (cep.length == 8) {
      // console.log("Deu 9");
      $.ajax({
        url: "https://viacep.com.br/ws/" + cep + "/json/",
        type: "GET",
        success: function (dados) {
          // console.log("success:", dados);
          if (!("erro" in dados)) {
            // console.log("if:", dados);
            $("#badge-badRequest").addClass("d-none");
            $("#badge-avisoErro").addClass("d-none"); //Adiciona a classe d-none e faz a badge sumir
            $("#badge-avisoSucess").removeClass("d-none"); //Adiciona a classe d-none e faz a badge sumir

            $("input[id='cleave-rua']").val(dados.logradouro);
            $("input[id='cleave-comlemento']").val(dados.complemento);
            $("input[id='cleave-bairro']").val(dados.bairro);
            $("input[id='cleave-cidade']").val(dados.localidade);
            //$("input[id='cleave-estado']").val(dados.uf);
            $("#cleave-estado").val(dados.uf);
          } else {
            console.log("else:", dados);
            $("#badge-badRequest").addClass("d-none");
            $("#badge-avisoSucess").addClass("d-none");
            $("#badge-avisoErro").removeClass("d-none");

            $("input[id='cleave-rua']").val("");
            $("input[id='cleave-comlemento']").val("");
            $("input[id='cleave-bairro']").val("");
            $("input[id='cleave-cidade']").val("");
            $("#cleave-estado").val("");
            //alert("CEP não encontrado");
          }
        },
        error: function (xhr, status, error) {
          console.log("error:", error);
          $("#badge-badRequest").removeClass("d-none");
        },
      });
    }
  });
});


