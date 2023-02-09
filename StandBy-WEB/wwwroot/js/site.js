//Funcao que verifica se o registro de um cliente esta completo ou nao para preencher coluna de cadastro
function verificaRegistroCompleto(data) {
  // console.log("testinho");
  var colunas = [
    data.cl_data_nascimento,
    data.cl_telefone,
    data.cl_cpf,
    data.cl_cep,
  ];
  var completo = true;
  colunas.forEach((coluna) => {
    if (!coluna || !coluna.trim().length) {
      completo = false;
    }
  });
  return completo;
}

//Funcao para editar o cliente na tabela
function loadEditClientePartialView(id) {
  $.ajax({
    url: "Cliente/EditarCliente/",
    data: { _id: id },
    type: "GET",
    success: function (result) {
      $("#root").html(result);
    },
  });
}

//Funcao para deletar o cliente na tabela
function loadDeleteClientePartialView(id) {
  $.ajax({
    url: "Cliente/Delete/",
    data: { _id: id },
    type: "GET",
    success: function (result) {
      console.log(result);
      Swal.fire({
        title: "Tem certeza que deseja deletar?",
        text: "Você não vai poder reverter isso...",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sim, delete!",
        cancelButtonText: "Não, cancele!",
        confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
        cancelButtonClass: "btn btn-danger w-xs mt-2",
        buttonsStyling: false,
        showCloseButton: true,
      }).then(function (result) {
        if (result.value) {
          $.ajax({
            type: "POST",
            url: "/Cliente/DeleteConfirm",
            data: { _id: id },
            success: function (data) {
              //Codigo sucesso
              console.log("SUCESSO: ");
              console.log(data);
              Swal.fire({
                title: "Deletado!",
                text: data,
                icon: "success",
                confirmButtonClass: "btn btn-primary w-xs mt-2",
                buttonsStyling: false,
              }).then(function () {
                location.reload();
              });
            },
            error: function (data) {
              console.log(data);
              Swal.fire({
                title: "Nao pode ser deletado",
                text: data.responseText,
                icon: "error",
                confirmButtonClass: "btn btn-primary mt-2",
                buttonsStyling: false,
              }).then(function () {
                location.reload();
              });
            },
          });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          Swal.fire({
            title: "Cancelado",
            text: "Fique tranquilo, não removemos o cliente :)",
            icon: "info",
            confirmButtonClass: "btn btn-primary mt-2",
            buttonsStyling: false,
          }).then(function () {
            location.reload();
          });
        }
      });
    },
    error: function (data) {
      console.log("BAD REQUEST");
      console.log(data);
      console.log(data.responseJSON.CustomMessage);
      Swal.fire({
        title: data.responseJSON.CustomMessage,
        text: data.responseJSON.ErrorMessage,
        icon: "error",
        confirmButtonClass: "btn btn-primary mt-2",
        buttonsStyling: false,
      }).then(function () {
        location.reload();
      });
    },
  });
}

//Funcao para deletar varios clientes na tabela
function deleteTodosRegistrosSelecionados() {
  {
    var selectedIds = [];

    $("input[name='selectedIds']:checked").each(function () {
      selectedIds.push($(this).val());
    });
    if (selectedIds.length > 0) {
      if (selectedIds.length == 1) {
        loadDeleteClientePartialView(selectedIds[0]); //Essa funcao foi feita para deletar 1 registro por vez
      } else {
        Swal.fire({
          title:
            "Tem certeza que deseja deletar os " +
            selectedIds.length +
            " registros?",
          text: "Você não vai poder reverter isso...",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Sim, delete!",
          cancelButtonText: "Não, cancele!",
          confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
          cancelButtonClass: "btn btn-danger w-xs mt-2",
          buttonsStyling: false,
          showCloseButton: true,
        }).then(function (result) {
          if (result.value) {
            $.ajax({
              type: "POST",
              url: "/Cliente/DeleteSelected",
              data: { selectedIds: selectedIds },
              success: function (data) {
                //Codigo sucesso
                console.log("SUCESSO: ");
                console.log(data);
                Swal.fire({
                  title: "Deletado!",
                  html: data,
                  icon: "success",
                  confirmButtonClass: "btn btn-primary w-xs mt-2",
                  buttonsStyling: false,
                }).then(function () {
                  location.reload();
                });
              },
              error: function (data) {
                console.log(data);
                Swal.fire({
                  title: "Nao pode ser deletado",
                  text: data.responseText,
                  icon: "error",
                  confirmButtonClass: "btn btn-primary mt-2",
                  buttonsStyling: false,
                }).then(function () {
                  location.reload();
                });
              },
            });
            // $("#deleteRegistrosSelecionadosModal").modal("show");
          }
        });
      }
    }
  }
}
