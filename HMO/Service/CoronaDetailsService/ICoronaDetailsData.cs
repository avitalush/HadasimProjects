using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoronaDetailsService
{
    public interface ICoronaDetailsData
    {

        Task<bool> addCoronaDetail(CoronaDetail coronaDetail);
        Task<string> listOfCovid();


    }
}
