namespace API.models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; } 
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public bool IsDoctorApproved { get; set; } 
        public String AdminId { get; set; }


    }
}
