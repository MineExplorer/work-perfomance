using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class ObjectNotFoundException : Exception
    {
        /// <inheritdoc />
        public ObjectNotFoundException()
        {
        }

        /// <inheritdoc />
        public ObjectNotFoundException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
