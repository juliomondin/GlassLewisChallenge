using GlassLewisChallenge.Domain;
using GlassLewisChallenge.Interfaces;


namespace GlassLewisChallenge.Interfaces
{
    public interface IValidatorService
    {
        bool Validate(Company request, ICompanyService service, bool isUpdate = false);
    }
}
