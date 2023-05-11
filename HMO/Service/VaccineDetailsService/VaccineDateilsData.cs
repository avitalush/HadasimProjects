using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.VaccineDetailsService
{
    public class VaccineDateilsData:IVaccineetailsData
    {
        private readonly MyDBContext _context;
        public VaccineDateilsData(MyDBContext context)
        {
            _context = context;
        }
        public async Task<string> addVaccination(VaccineDetail vaccineDetail)
        {
            var isOk = true;
            int numVaccinations = _context.VaccineDetails.Count(v => v.UserId == vaccineDetail.UserId);
            if (numVaccinations < 4)
            {
                if (vaccineDetail.ReceivingVaccine > DateTime.Now) 
                    return "Invalid Date";
                await _context.AddAsync(vaccineDetail);
                isOk = await _context.SaveChangesAsync() >= 0;
                if (isOk) return "Added successfully";
            }

            return "deviation";
        }
    }
}
