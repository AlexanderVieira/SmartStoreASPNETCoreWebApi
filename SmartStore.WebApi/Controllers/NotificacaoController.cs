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

        public NotificacaoController(IHubContext<NotificationHub> hubContext, IProdutoRepositorio produtoRepositorio)
        {
            _hubContext = hubContext;
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Notifications()
        {
            await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync("notificationStarted");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Debug.WriteLine($"notification ={ i + 1 }");
                await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync("notificationStartedChanged", i + 1);
            }

            await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync("notificationEnded");

            return Ok();
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> Notifications([FromBody] ProdutoViewModel vmProduto){

            try
            {                
                var produtoRecuperado = new Produto();
                Dictionary<string, PedidoViewModel> carrinho = new Dictionary<string, PedidoViewModel>();

                var listaProduto = new List<Produto>();
                for (int i = 0; i < vmProduto.ArrayTagProdutoId.Length; i++)
                {
                    produtoRecuperado = _produtoRepositorio.ObterPorTagId(vmProduto.ArrayTagProdutoId[i]);
                    if (produtoRecuperado != null)
                    {
                        listaProduto.Add(produtoRecuperado);
                    }
                    
                }

                var vmPedido = new PedidoViewModel
                {
                    CarrinhoId = vmProduto.CarrinhoId,
                    Usuario = new Usuario { Id = 1, Nome = "Alexander", SobreNome = "Silva" },
                    Produtos = listaProduto.ToArray()
                };

                carrinho.Add(vmProduto.CarrinhoId, vmPedido);

                await _hubContext.Clients.Group(NotificationHub.GROUP_NAME).SendAsync("notificationStartedChanged", carrinho);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}