using CursoOnline.Data.Contexts;
using CursoOnline.Dominio.Base;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Data.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        internal readonly AppDbContext Context;

        public RepositorioBase(AppDbContext appDbContext)
        {
            Context = appDbContext;
        }

        public void Salvar(TEntidade entidade)
        {
            Context.Set<TEntidade>().Add(entidade);
        }

        public virtual IEnumerable<TEntidade> Consultar()
        {
            var lista = Context.Set<TEntidade>();

            return lista;
        }

        public TEntidade ObterPorId(int id)
        {
            return Context.Set<TEntidade>().FirstOrDefault(e => e.Id == id);
        }
    }
}
