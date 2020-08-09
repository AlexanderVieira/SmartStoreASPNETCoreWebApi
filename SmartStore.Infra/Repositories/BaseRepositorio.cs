using SmartStore.Domain.Intefaces;
using SmartStore.Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace SmartStore.Infra.Repositories
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        protected readonly SmartStoreDbContext _ctx;
               
        public BaseRepositorio(SmartStoreDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Adicionar(TEntity entity)
        {
            _ctx.Set<TEntity>().Add(entity);
            _ctx.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
            _ctx.SaveChanges();
        }
        
        public TEntity ObterPorId(int id)
        {
            return _ctx.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return _ctx.Set<TEntity>().ToList();
        }

        public void Remover(TEntity entity)
        {
            _ctx.Remove(entity);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
