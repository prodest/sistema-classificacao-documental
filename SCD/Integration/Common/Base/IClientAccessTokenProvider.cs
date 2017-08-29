using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.Integration.Common.Base
{
    public interface IClientAccessTokenProvider
    {
        string AccessToken { get; }
    }
}
