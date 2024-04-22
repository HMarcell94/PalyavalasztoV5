namespace Palyavalaszto.Dto
{
    public class JobDto
    {
        public int JobID { get; set; }

        public int? EmployerID { get; set; }

        public string? JobTitle { get; set; }

        public string? JobDescription { get; set; }

        public string? JobRequirements { get; set; }

        public string? JobLocation { get; set; }

        public string MinSalary { get; set; }
        public string MaxSalary { get; set; }
        public string? Short_Summary { get; set;}
        public string picture { get; set; }
        public string Extras { get; set; }
    }
}
