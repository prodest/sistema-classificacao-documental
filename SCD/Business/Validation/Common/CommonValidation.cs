using Prodest.Scd.Business.Common.Exceptions;
using System;

namespace Prodest.Scd.Business.Validation.Common
{
    public class CommonValidation
    {
        internal void OrganizacaoFilled(string guidOrganizacao)
        {
            if (string.IsNullOrWhiteSpace(guidOrganizacao) || string.IsNullOrWhiteSpace(guidOrganizacao.Trim()))
                throw new ScdException("A organização não pode ser vazia ou nula.");
        }

        internal void OrganizacaoValid(string guidOrganizacao)
        {
            Guid result = new Guid();
            bool valid = Guid.TryParse(guidOrganizacao, out result);

            if (!valid)
                throw new ScdException("Guid da organização inválido.");
            else if (result.Equals(Guid.Empty))
                throw new ScdException("Guid da organização inválido.");
        }

        internal void OrganizacaoFilled(Guid guidOrganizacao)
        {
            if (guidOrganizacao == null)
                throw new ScdException("O guid da organização não pode ser nulo.");
        }

        internal void OrganizacaoValid(Guid guidOrganizacao)
        {
            if (guidOrganizacao.Equals(Guid.Empty))
                throw new ScdException("Guid da organização inválido.");
        }
    }
}