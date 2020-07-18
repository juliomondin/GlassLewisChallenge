using GlassLewisChallange.Domain;
using GlassLewisChallange.Interfaces;
using GlassLewisChallenge.Interfaces;
using System;

namespace GlassLewisChallenge.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool Validate(Company request, ICompanyService service, bool isUpdate = false)
        {
            var checkExisting = service.GetByIsin(request.Isin);
            if (request.ValidateIsin() && (checkExisting == null || isUpdate) && request.CheckObrigatoryFields())
            {
                return true;
            }
            return false;
        }
    }
}
