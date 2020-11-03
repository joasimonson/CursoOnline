using System.Collections.Generic;

namespace CursoOnline.Dominio.Base
{
    public interface IRepositorioBase<TEntidade>
    {
        TEntidade ObterPorId(int id);
        IEnumerable<TEntidade> Consultar();
        void Salvar(TEntidade entidade);
    }
}
