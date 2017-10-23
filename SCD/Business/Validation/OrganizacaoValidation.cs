using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation.Common;

namespace Prodest.Scd.Business.Validation
{
    public class OrganizacaoValidation : CommonValidation
    {

        internal void Found(OrganizacaoModel organizacaoModel)
        {
            if (organizacaoModel == null)
                throw new ScdException("Organização não encontrada.");
        }
    }
}
