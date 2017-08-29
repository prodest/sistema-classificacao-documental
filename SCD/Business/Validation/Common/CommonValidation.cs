using Prodest.Scd.Infrastructure.Common.Exceptions;
using System;

namespace Prodest.Scd.Business.Validation.Common
{
    public class CommonValidation
    {
        internal void OrganizacaoFilled(string guidOrganizacao)
        {
            if (string.IsNullOrWhiteSpace(guidOrganizacao) || string.IsNullOrWhiteSpace(guidOrganizacao.Trim()))
                throw new ScdExpection("A organização não pode ser vazia ou nula.");
        }

        internal void OrganizacaoValid(string guidOrganizacao)
        {
            Guid result = new Guid();
            bool valid = Guid.TryParse(guidOrganizacao, out result);

            if (!valid)
                throw new ScdExpection("Guid da organização inválido.");
            else if (result.Equals(Guid.Empty))
                throw new ScdExpection("Guid da organização inválido.");
        }
    }
}
