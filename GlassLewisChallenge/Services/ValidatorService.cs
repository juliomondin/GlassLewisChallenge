using GlassLewisChallange.Domain;
using GlassLewisChallange.Interfaces;
using GlassLewisChallenge.Interfaces;
using System;

namespace GlassLewisChallenge.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool Validate(Company request, ICompanyService service)
        {
            var checkExisting = service.GetByIsin(request.Isin);
            if (request.ValidateIsin() && checkExisting == null && !string.IsNullOrEmpty(request.Ticker) && !string.IsNullOrEmpty(request.Exchange) && !string.IsNullOrEmpty(request.Name))
            {
                return true;
            }
            return false;
        }
    }
}
