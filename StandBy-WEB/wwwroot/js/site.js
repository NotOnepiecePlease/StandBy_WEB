﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificaRegistroCompleto(data) {
    var colunas = [data.cl_data_nascimento, data.cl_telefone, data.cl_cpf, data.cl_cep];
    var completo = true;
    colunas.forEach(coluna => {
        if (!coluna || !coluna.trim().length) {
            completo = false;
        }
    });
    return completo;
}


$(document).ready(function () {
    $.fn.dataTable.moment('DD/MM/YYYY');
    $('#tabelaClientes').DataTable({

        language: { url: "https://cdn.datatables.net/plug-ins/1.13.2/i18n/pt-BR.json" },
        ajax: {
            url: 'Cliente/ListaClientes',
            dataSrc: ''
            //dataSrc: '',
            //beforeSend: function() {
            //    $('#loading').show();
            //},
            //complete: function(data) {
            //    console.log(data);
            //    $('#loading').hide();
            //}
        },
        columns: [
            {
                data: null,
                render: function(data) {
                    //console.log(data);
                    return '<input class="form-check-input fs-15" type="checkbox" name="selectedIds" value="' +
                        data.cl_id +
                        '">';
                }
            },
            { data: "cl_id" },
            { data: "cl_nome" },
            //{
            //    data: "cl_data_nascimento",
            //},


        {
                data: "cl_data_nascimento",
                orderDataType: "date",
                render: function (data, type, row) {
                    if (data) {
                        //var date = new Date(data);
                        //var day = date.getDate();
                        //var month = date.toLocaleString('pt-BR', { month: 'short' });
                        //var year = date.getFullYear();
                        //return day + " " + month + " " + year;
                        var date = new Date(data);
                        return date.toLocaleDateString('pt-BR', { day: 'numeric', month: 'numeric', year: 'numeric' });
                    } else {
                        return "";
                    }
                }

            },
            
            { data: "cl_telefone" },
            { data: "cl_cpf" },
            { data: "cl_cep" },
            { 
                data: null,//Coluna de Status
                render: function (data) {
                    //if (!data.cl_cep || !data.cl_cep.trim().length) {
                    //    return '<td><span class="badge badge-soft-info">Completo</span></td>';
                    //} else {
                    //    return '<td><span class="badge badge-soft-warning">Incompleto</span></td>';
                    //}

                    var estado = verificaRegistroCompleto(data) ? "Completo" : "Incompleto";
                    return '<td><span class="badge badge-soft-' + (estado === "Completo" ? "info" : "warning") + '">' + estado + '</span></td>';


                }
            },
            {
                data: null,
                render: function (data) {//Coluna de Action
                    return '<div class="d-flex justify-center text-center"><a href="javascript:void(0);" class="link-success fs-15">' +
                        '<a class="link-success fs-15 me-2 ms-2"><i class="ri-edit-2-line" data-bs-toggle="modal" id="create-btn" data-bs-target="#showModal-' +
                        data.cl_id +
                        '" onclick="loadEditClientePartialView(' +
                        data.cl_id +
                        ')"></i></a>' +
                        '<a  class="link-danger fs-15"><i class="ri-delete-bin-line" data-bs-toggle="modal" id="delete-btn" data-bs-target="#deleteRegistro-' +
                        data.cl_id +
                        '" onclick="loadDeleteClientePartialView(' +
                        data.cl_id +
                        ')"></i></a></div>';
                }
            }
        ],
        pageLength: 10

    });
});

function loadEditClientePartialView(id) {
    $.ajax({
        url: "Cliente/Edit/",
        data: { _id: id },
        type: 'GET',
        success: function(result) {
            console.log("AEEE: " + id);
            $("#tes").html(result);
            $('#showModal-' + id).modal("show");
        }
    });
}

function loadDeleteClientePartialView(id) {
    $.ajax({
        url: "Cliente/Delete/",
        data: { _id: id },
        type: 'GET',
        success: function(result) {
            console.log("AEEE: " + id);
            $("#tes").html(result);
            $('#deleteRegistro-' + id).modal("show");
        }
    });
}


$(document).ready(function() {
    var selectedIds = [];
    $("#deleteSelecionadosButton").click(function() {
        $("input[name='selectedIds']:checked").each(function() {
            selectedIds.push($(this).val());
        });
        if (selectedIds.length > 0) {
            $("#deleteRegistrosSelecionadosModal").modal("show");
        };
    });
    $("#confirmDeleteRegistrosSelecionadosButton").click(function() {
        $("#deleteRegistrosSelecionadosModal").modal("hide");
        //console.log("LAU");
        $.ajax({
            type: "POST",
            url: "/Cliente/DeleteSelected",
            data: { selectedIds: selectedIds },
            success: function(data) {
                //console.log("CHORISBRESA: " + data);
                $("#clientesNaoPodemSerDeletadosDIV").html(data);
                $("#clientesNaoPodemSerDeletadosModal").modal("show");
            },
            error: function(data) {
                console.log(data);
                window.alert(`Erro: ${data.status} \nMensagem: ${data.responseText}`);
            }
        });
    });
});