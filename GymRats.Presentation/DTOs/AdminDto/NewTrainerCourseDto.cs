using System.ComponentModel.DataAnnotations;
namespace GymRats.Presentation.DTOs.AdminDto
{
    public class NewTrainerCourseDto
    {
        
        public string CourseName { get; set; }

        
        public string Duration { get; set; }

        
        public string Description { get; set; }

        
        public int CoachId { get; set; }
    }
}