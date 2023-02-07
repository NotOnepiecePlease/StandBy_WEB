$(document).ready(function () {
    $("#cleave-cep").change(function () {
        var cep = $(this).val().replace(/\D/g, '');
        console.log(cep);
        console.log("Length: " + cep.length);
        if (cep.length == 8) {
            console.log("Deu 9");
            $.ajax({
                url: "https://viacep.com.br/ws/" + cep + "/json/",
                type: "GET",
                success: function (dados) {
                    console.log(dados);
                    if (!("erro" in dados)) {
                        $("#badge-avisoErro").addClass("d-none"); //Adiciona a classe d-none e faz a badge sumir
                        $("#badge-avisoSucess").removeClass("d-none"); //Adiciona a classe d-none e faz a badge sumir
                        $("input[id='cleave-rua']").val(dados.logradouro);
                        $("input[id='cleave-comlemento']").val(dados.complemento);
                        $("input[id='cleave-bairro']").val(dados.bairro);
                        $("input[id='cleave-cidade']").val(dados.localidade);
                        //$("input[id='cleave-estado']").val(dados.uf);
                        $("#cleave-estado").val(dados.uf);
                    } else {
                        $("#badge-avisoSucess").addClass("d-none");
                        $("#badge-avisoErro").removeClass("d-none");
                        //alert("CEP não encontrado");
                    }
                },
                error: function (xhr, status, error) {
                    $("#badge-avisoSucess").addClass("d-none");
                    $("#badge-avisoErro").removeClass("d-none");
                }
            });
        }
    });
});

$(document).ready(function () {
    if ($(".validation-summary-errors li").length > 0) {
        $("#errorCard").removeClass("d-none");
        $("#errorCard").show();
    } else {
        $("#errorCard").addClass("d-none");
        $("#errorCard").hide();
    }
});