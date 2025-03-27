using GymRats.Data.Entities;

namespace GymRats.Presentation.DTOs
{
    public class GymPassResponse
    {
        public int UzytkownikIdUzytkownika { get; set; }
    
        public DateOnly startKarnetu { get; set; }

        public DateOnly koniecUmowy { get; set; }

        public int TypKarnetuIdTypKarnetu { get; set; }
    
        public virtual TypKarnetu TypKarnetuIdTypKarnetuNavigation { get; set; } = null!;
    }    
}
