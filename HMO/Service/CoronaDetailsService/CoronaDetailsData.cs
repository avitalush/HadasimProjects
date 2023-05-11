using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoronaDetailsService
{
    public class CoronaDetailsData : ICoronaDetailsData
    {
        private readonly MyDBContext _context;
        public CoronaDetailsData(MyDBContext context)
        {
            _context = context;
        }
        public async Task<bool> addCoronaDetail(CoronaDetail coronaDetail)
        {
            var result = ValidateDateRange((DateTime)coronaDetail.PositiveAnswerDate, (DateTime)coronaDetail.RecoveryDate);
            if (!result)
            {
                return false;
            }
            var isOk = false;
            await _context.AddAsync(coronaDetail);
            isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk) return true;
            return false;
        }
        public static bool ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return false;

           
            if (startDate > DateTime.Now)
                return false;

        

            return true;
        }

        public async Task<string> listOfCovid()
        {
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-30);

            var patients = await _context.CoronaDetails.ToListAsync();
                

            var activePatientsByDate = new List<object>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var activePatients = patients
                    .Count(c => c.PositiveAnswerDate <= date && (!c.RecoveryDate.HasValue || c.RecoveryDate.Value >= date));

                activePatientsByDate.Add(new { Date = date.Date, ActivePatients = activePatients });
            }

            var json = JsonConvert.SerializeObject(activePatientsByDate);

            return json;
        }



    }
}
 