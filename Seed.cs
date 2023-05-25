
using Przychodnia.Data;
using Przychodnia.Models;
using System.Diagnostics.Metrics;

namespace Przychodnia
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public Lekarz Lekarz { get; private set; }

        public void SeedDataContext()
        {
            Lekarz = new Lekarz()
            {
                KosztWizyty = 3,
                Specjalizacja = "perwert"

            };
                dataContext.Lekarze.AddRange();
                dataContext.SaveChanges();
            
        }
    }
}