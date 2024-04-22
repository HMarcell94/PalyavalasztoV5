namespace Palyavalaszto.Dto
{
    public class ApplicationsDto
    {
        public int ApplicationID { get; set; }

        public int? JobID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string ApplicationStatus { get; set; }
    }
}