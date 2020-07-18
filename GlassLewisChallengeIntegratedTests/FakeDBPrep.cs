﻿using GlassLewisChallenge.Domain;
using GlassLewisChallenge.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassLewisChallengeIntegratedTests
{
    public static class FakeDBPrep
    {
        public static void PopulateTestData(CompanyContext dbContext)
        {
            dbContext.Companies.AddRange(
                new Company() { Isin = "AR123", Exchange = "AAA", Name = "GlassLewis" },
                new Company() { Isin = "OI123", Exchange = "BBB", Name = "JulioCompany" }
            );
            dbContext.SaveChanges();
        }
    }
}
