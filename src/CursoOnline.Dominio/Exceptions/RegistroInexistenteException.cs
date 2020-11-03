using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio
{
    [Serializable]
    public class RegistroInexistenteException<T> : Exception
    {
        private readonly string _campo;
        private readonly object _valor;

        public RegistroInexistenteException()
        {
        }

        public RegistroInexistenteException(T obj, Expression<Func<T, int>> exp)
        {
            var memberExp = exp.Body as MemberExpression;

            if (memberExp is null)
            {
                throw new ArgumentException($"Propriedade '{0}' inválida", exp.ToString());
            }
            
            PropertyInfo propInfo = memberExp.Member as PropertyInfo;
            if (propInfo is null)
            {
                throw new ArgumentException($"Propriedade '{0}' inválida.", exp.ToString());
            }

            _campo = memberExp.Member.Name;

            if (obj != null)
            {
                _valor = typeof(T).GetProperty(memberExp.Member.Name).GetValue(obj);
            }
        }

        public RegistroInexistenteException(string message) : base(message)
        {
        }

        public RegistroInexistenteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegistroInexistenteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private string HandleMessage()
        {
            if (String.IsNullOrWhiteSpace(_campo))
            {
                return Message;
            }

            return $"Registro inexistente ({_campo}={_valor})";
        }

        public override string Message => HandleMessage();
    }
}