﻿using GlassLewisChallenge.Domain;
using System.Collections.Generic;

namespace GlassLewisChallenge.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Company> GetAll();

        Company Get(long id);

        Company GetByIsin(string isin);

        Company Insert(Company entity);

        Company Update(Company entity);

    }
}
