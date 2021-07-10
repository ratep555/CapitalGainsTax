namespace Core.Entities
{
    public class Surtax : BaseEntity
    {
        public string Residence { get; set; }
        // dobro da je nullable jer može biti i nula
        public int? Amount { get; set; }
    }
}