using Prodest.Scd.Business.Validation.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Prodest.Scd.Persistence.Model;
using Prodest.Scd.Infrastructure.Common.Exceptions;

namespace Prodest.Scd.Business.Validation
{
    public class OrganizacaoValidation : CommonValidation
    {

        internal void Found(Organizacao organizacao)
        {
            if (organizacao == null)
                throw new ScdExpection("Organização não encontrada.");
        }
    }
}
