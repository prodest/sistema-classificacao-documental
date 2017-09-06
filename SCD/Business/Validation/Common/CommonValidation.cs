using Prodest.Scd.Business.Common.Exceptions;
using System;

namespace Prodest.Scd.Business.Validation.Common
{
    public class CommonValidation
    {
        internal void IdValid(int id)
        {
            if (id == default(int))
                throw new ScdException("O id não pode ser nulo ou vazio.");
        }

        internal void OrganizacaoValid(Guid guidOrganizacao)
        {
            if (guidOrganizacao.Equals(Guid.Empty))
                throw new ScdException("Guid da organização inválido.");
        }
    }
}