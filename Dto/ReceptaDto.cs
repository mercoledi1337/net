﻿namespace Przychodnia.Dto
{
    public class ReceptaDto
    {
        public int Id { get; set; }
        public DateTime DataWystawienia { get; set; }
        public string KodRecepty { get; set; }
        public string Wystawiajacy { get; set; }
        public string Zalecenia { get; set; }
        public string Dawkowanie { get; set; }
    }
}
