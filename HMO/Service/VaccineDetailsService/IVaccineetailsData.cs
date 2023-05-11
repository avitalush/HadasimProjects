using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.VaccineDetailsService
{
    public interface IVaccineetailsData
    {
        Task<string> addVaccination(VaccineDetail vaccineDetail);
    }
}
