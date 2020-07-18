using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GlassLewisChallenge.Domain
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

        public bool CheckObrigatoryFields()
        {
            if (string.IsNullOrEmpty(this.Ticker) || string.IsNullOrEmpty(this.Exchange) || string.IsNullOrEmpty(this.Name))
                return false;
            return true;
        }
    }
}
