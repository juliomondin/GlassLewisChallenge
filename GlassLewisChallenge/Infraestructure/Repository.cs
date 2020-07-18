using GlassLewisChallange.Domain;
using GlassLewisChallange.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlassLewisChallange.Infraestructure
{
    public class Repository : IRepository
    {
        private readonly CompanyContext _context;

        public Repository(CompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Company Get(long id)
        {
            return _context.Companies.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Companies;
        }

        public Company GetByIsin(string isin)
        {
            return _context.Companies.Where(x => x.Isin == isin).FirstOrDefault();

        }

        public Company Insert(Company entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Company Update(Company entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
