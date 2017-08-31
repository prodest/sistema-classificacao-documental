using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Validation.Common;
using Prodest.Scd.Persistence.Model;

namespace Prodest.Scd.Business.Validation
{
    public class OrganizacaoValidation : CommonValidation
    {

        internal void Found(Organizacao organizacao)
        {
            if (organizacao == null)
                throw new ScdException("Organização não encontrada.");
        }
    }
}
