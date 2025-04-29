using GymRats.Data.Entities;

namespace GymRats.Presentation.DTOs
{
    public class GymPassResponse
    {
        public int IdUser { get; set; }
    
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int IdTypePass { get; set; }
    
        public virtual TypePass TypePass { get; set; } = null!;
    }    
}
