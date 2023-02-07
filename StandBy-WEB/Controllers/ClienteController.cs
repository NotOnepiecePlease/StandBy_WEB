using Microsoft.AspNetCore.Mvc;
using standby_data.Enums;
using standby_data.Models;
using standby_data.Models.UtilModels;
using standby_data.Services;

namespace StandBy_WEB.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteService clienteService = new ClienteService();

        //public IActionResult Index(int? page)
        //{
        //    var pageNumber = page ?? 1;
        //    int pageSize = 15;
        //    var onePageOfProducts = clienteService.repositoryCliente.BuscarTodos().ToPagedList(pageNumber, pageSize);
        //    return View(onePageOfProducts);
        //}

        public IActionResult AddCliente()
        {
            return View("AddClienteView");
        }

        [HttpPost]
        public IActionResult AddCliente(tb_clientes _cliente)
        {
            MessageAlertModel message = new MessageAlertModel();

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }


                message.Type = MessageAlertEnum.Error;
                message.Message = $"Erro ao cadastrar o cliente: {_cliente.cl_nome}";
                message.Desc = "Verifique os campos e tente novamente";

                ViewData["Message"] = message;

                return View("AddClienteView");
                //View = Retorna uma view sem recarregar ela
                //RedirectToAction = Retorna a view recarregando novamente os dados.
            }

            clienteService.repositoryCliente.Adicionar(_cliente);
            clienteService.repositoryCliente.SalvarModificacoes();

            message.Type = MessageAlertEnum.Success;
            message.Message = $"{_cliente.cl_nome} foi inserido com sucesso!";
            message.Desc = "";

            ViewData["Message"] = message;

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        //Usado para popular o grid mas usando ajax
        public JsonResult ListaClientes()
        {
            var clientes = GetClientesAsync();
            return Json(clientes);
        }

        private List<tb_clientes> GetClientesAsync()
        {
            List<tb_clientes> listCliente = clienteService.repositoryCliente.BuscarTodos();
            return listCliente;
        }

        #region Editar cliente

        public IActionResult Edit(int _id)
        {
            tb_clientes cliente = clienteService.repositoryCliente.BuscarPorID(_id);
            return PartialView("_EditClientePartialView", cliente);
        }

        [HttpPost]
        public IActionResult Edit(tb_clientes _cliente)
        {
            if (_cliente.cl_telefone_recado == "------------" || string.IsNullOrWhiteSpace(_cliente.cl_telefone_recado))
            {
                _cliente.cl_telefone_recado = "------------";
            }

            clienteService.repositoryCliente.Atualizar(_cliente);
            clienteService.repositoryCliente.SalvarModificacoes();
            return RedirectToAction("Index");
        }

        #endregion

        #region Deletar registro especifico

        public async Task<IActionResult> Delete(int _id)
        {
            try
            {
                var _cliente = clienteService.repositoryCliente.BuscarPorID(_id);
                return Ok($"Tem certeza que deseja deletar {_cliente.cl_nome}?");
            }
            catch (Exception e)
            {
                var erroTraduzido = await TraduzirTexto(e.Message);
                return BadRequest(new BadRequestErrorModel
                {
                    CustomMessage = $"Algo inesperado aconteceu",
                    ErrorMessage = $"{erroTraduzido}"
                });
            }
            //return PartialView("_DeleteUnidadeModalPartialView", _cliente);
        }

        public static async Task<string> TraduzirTexto(string text)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=pt-br&dt=t&q={Uri.EscapeUriString(text)}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                var startIndex = result.IndexOf("\"") + 1;
                var endIndex = result.IndexOf("\"", startIndex);
                return result.Substring(startIndex, endIndex - startIndex);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int _id)
        {
            List<tb_clientes> listClienteNaoPodeSerDeletado = new List<tb_clientes>();
            bool isExisteServicoVinculado = clienteService.repositoryCliente.SeExisteServicoVinculado(_id);
            string clienteDeletado = "";
            if (isExisteServicoVinculado == false)
            {
                var _cliente = clienteService.repositoryCliente.BuscarPorID(_id);
                clienteDeletado = _cliente.cl_nome;
                clienteService.repositoryCliente.Deletar(_cliente);
                clienteService.repositoryCliente.SalvarModificacoes();
            }
            else
            {
                var _cliente = clienteService.repositoryCliente.BuscarPorID(_id);
                listClienteNaoPodeSerDeletado.Add(_cliente);
                //return PartialView("_ClienteNaoPodeSerDeletadoModalPartialView", listClienteNaoPodeSerDeletado);
                return BadRequest($"{_cliente.cl_nome} possui serviços vinculados =( !");
            }


            return Ok($"{clienteDeletado} foi deletado com sucesso =D !");
            //return PartialView("_DeletadoSucessoModalPartialView");
        }

        #endregion

        #region Deletar registros selecionados

        public IActionResult DeleteSelected(int[] selectedIds)
        {
            try
            {
                if (selectedIds == null || !selectedIds.Any())
                {
                    return BadRequest();
                }

                List<tb_clientes> listClienteNaoPodeSerDeletado = new List<tb_clientes>();
                foreach (var id in selectedIds)
                {
                    bool isExisteServicoVinculado = clienteService.repositoryCliente.SeExisteServicoVinculado(id);
                    if (isExisteServicoVinculado == false)
                    {
                        var item = clienteService.repositoryCliente.BuscarPorID(id);
                        clienteService.repositoryCliente.Deletar(item);
                        clienteService.repositoryCliente.SalvarModificacoes();
                    }
                    else
                    {
                        listClienteNaoPodeSerDeletado.Add(clienteService.repositoryCliente.BuscarPorID(id));
                    }
                }

                if (listClienteNaoPodeSerDeletado.Count > 0)
                {
                    //return StatusCode(500, "Ocorreu um erro interno no servidor.");
                    return PartialView("_ClienteNaoPodeSerDeletadoModalPartialView", listClienteNaoPodeSerDeletado);
                }
                else
                {
                    List<tb_clientes> listCliente = clienteService.repositoryCliente.BuscarTodos();
                    return PartialView("Index", listCliente);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        #endregion
    }
}