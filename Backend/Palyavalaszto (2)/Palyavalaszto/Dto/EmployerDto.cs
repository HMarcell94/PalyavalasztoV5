﻿namespace Palyavalaszto.Dto
{
    public class EmployerDto
    {
        public int EmployerID { get; set; }

        public int? UserID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }
        
        public string? CompanyLogo { get; set; }
        public string CompanyLocation { get; set; }
        public int size { get; set; }

        public string enterprisetype { get; set; }
    }
}