using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmartStore.Domain.Entities;
using SmartStore.Domain.Intefaces;
using SmartStore.WebApi.Models;
using SmartStore.WebApi.Services;

namespace SmartStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private const string USUARIO_NULO = "Usuário não encontrado.";
        private const string NOTIFICATION_STARTED_CHANGED = "notificationStartedChanged";
        private const string NOTIFICATION_ENDED = "notificationEnded";
        private const string NOTIFICATION_STARTED = "notificationStarted";
        private const string NOTIFICATION_POST_STARTED = "notificationPostStarted";

        public NotificacaoController(IHubContext<NotificationHub> hubContext, IProdutoRepositorio produtoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _hubContext = hubContext;
            _produtoRepositorio = produtoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Notifications()
        {
            await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync(NOTIFICATION_STARTED);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Debug.WriteLine($"notification ={ i + 1 }");
                await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync(NOTIFICATION_STARTED_CHANGED, i + 1);
            }

            await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync(NOTIFICATION_ENDED);

            return Ok();
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> Notifications([FromBody] ProdutoViewModel vmProduto){

            try
            {                
                //var produtoRecuperado = new Produto();
                Dictionary<string, PedidoViewModel> carrinho = new Dictionary<string, PedidoViewModel>();

                var listaProduto = new List<Produto>();
                for (int i = 0; i < vmProduto.ArrayTagProdutoId.Length; i++)
                {
                    var produtoRecuperado = _produtoRepositorio.ObterPorTagId(vmProduto.ArrayTagProdutoId[i]);
                    if (produtoRecuperado != null)
                    {
                        listaProduto.Add(produtoRecuperado);
                    }
                    
                }

                var usuarioRecuperado = _usuarioRepositorio.ObterPorTagId(vmProduto.TagClienteId);
                if (usuarioRecuperado != null)
                {
                    var vmPedido = new PedidoViewModel
                    {
                        CarrinhoId = vmProduto.CarrinhoId,
                        Usuario = usuarioRecuperado,
                        Produtos = listaProduto.ToArray()
                    };

                    carrinho.Add(vmProduto.CarrinhoId, vmPedido);
                    await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync(NOTIFICATION_POST_STARTED, carrinho);
                }
                else
                {
                    return BadRequest(USUARIO_NULO);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}