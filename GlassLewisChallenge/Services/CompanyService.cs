using GlassLewisChallange.Domain;
using GlassLewisChallange.Interfaces;
using System;
using System.Collections.Generic;


namespace GlassLewisChallenge.Services
{
    public class CompanyService : ICompanyService
    {
        IRepository _repository;

        public CompanyService(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Company Get(long id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Company> GetAll()
        {
            return _repository.GetAll();
        }

        public Company GetByIsin(string isin)
        {
            return _repository.GetByIsin(isin);
        }

        public Company Insert(Company entity)
        {
            return _repository.Insert(entity);
        }

        public Company Update(Company entity)
        {
            return _repository.Update(entity);
        }
    }
}
