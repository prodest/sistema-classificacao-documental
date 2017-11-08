using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface ITemporalidadeCore
    {
        Task<TemporalidadeModel> InsertAsync(TemporalidadeModel temporalidadeModel);

        Task<TemporalidadeModel> SearchAsync(int id);

        Task UpdateAsync(TemporalidadeModel temporalidadeModel);

        Task DeleteAsync(int id);
    }
}