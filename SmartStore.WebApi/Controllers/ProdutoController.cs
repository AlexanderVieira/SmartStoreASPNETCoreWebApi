using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using SmartStore.Domain.Intefaces;
using SmartStore.Domain.Entities;
using Microsoft.AspNetCore.Cors;

namespace SmartStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostEnvironment;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, 
                                 IHttpContextAccessor httpContextAccessor, 
                                 IHostingEnvironment hostEnvironment)
        {
            _produtoRepositorio = produtoRepositorio;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        //[EnableCors("AllowOrigin")]
        public IActionResult Get()
        {
            try
            {
                return new JsonResult(_produtoRepositorio.ObterTodos())
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                produto.Validate();
                if (!produto.EhValido)
                {
                    return BadRequest(produto.ObterMensagensValidacao());
                }
                if (produto.Id > 0)
                {
                    _produtoRepositorio.Atualizar(produto);
                }
                else
                {
                    _produtoRepositorio.Adicionar(produto);
                }

                return Created("/api/produto", produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Deletar([FromBody] Produto produto)
        {
            try
            {
                if (produto.Id > 0)
                {
                    _produtoRepositorio.Remover(produto);                    
                }

                return new JsonResult(_produtoRepositorio.ObterTodos())
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Upload")]
        public IActionResult Upload()
        {
            try
            {
                var formFile = _httpContextAccessor.HttpContext.Request.Form.Files["arquivoEnviado"];
                var nomeArquivo = formFile.FileName;
                var extensao = nomeArquivo.Split(".").Last();
                var novoNomeArquivo = GerarNovoNomeArquivo(nomeArquivo, extensao);
                var pastaArquivo = _hostEnvironment.WebRootPath + "\\arquivos\\";
                var nomeCompleto = pastaArquivo + novoNomeArquivo;

                using (var streamArquivo = new FileStream(nomeCompleto, FileMode.Create))
                {
                    formFile.CopyTo(streamArquivo);
                }

                return new JsonResult(novoNomeArquivo)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string GerarNovoNomeArquivo(string nomeArquivo, string extensao)
        {
            var arrayNome = Path.GetFileNameWithoutExtension(nomeArquivo).Take(10).ToArray();
            var novoNomeArquivo = new string(arrayNome).Replace(" ", "-");
            novoNomeArquivo = $"{novoNomeArquivo}" +
                                $"_{DateTime.Now.Year}" +
                                $"{DateTime.Now.Month}" +
                                $"{DateTime.Now.Day}" +
                                $"{DateTime.Now.Hour}" +
                                $"{DateTime.Now.Minute}" +
                                $"{DateTime.Now.Second}.{extensao}";

            return novoNomeArquivo;
        }
    }
}