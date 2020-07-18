using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewisChallange.Domain
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name
        {
            get; set;
        }

        public string Exchange
        {
            get; set;
        }

        public string Ticker
        {
            get; set;
        }

        public string Isin
        {
            get; set;
        }

        public string WebSite
        {
            get; set;
        }

        public bool ValidateIsin()
        {
            if (string.IsNullOrEmpty(this.Isin) || this.Isin.Count() < 2)
                return false;

            if (!char.IsLetter(this.Isin[0]) || !char.IsLetter(this.Isin[1]))
                return false;

            return true;

        }
    }
}
