using System;

namespace GlassLewisChallenge.Infraestructure
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
