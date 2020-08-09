using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStore.Domain.Intefaces
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        Usuario Obter(string email, string senha);
        Usuario Obter(string email);
    }
}
