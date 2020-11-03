using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio.Builders
{
    public class ValidadorRegra
    {
        private readonly List<Exception> _exceptions;

        private ValidadorRegra()
        {
            _exceptions = new List<Exception>();
        }

        public static ValidadorRegra Novo()
        {
            return new ValidadorRegra();
        }

        public ValidadorRegra ComRegra(bool invalid, Func<Exception> func)
        {
            if (invalid)
            {
                try
                {
                    func.Invoke();
                }
                catch (Exception ex)
                {
                    _exceptions.Add(ex);
                }
            }

            return this;
        }

        public void Validar()
        {
            if (_exceptions.Any())
            {
                throw new RegraDominioException(_exceptions);
            }
        }
    }
}
