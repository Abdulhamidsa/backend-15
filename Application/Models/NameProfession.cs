using System;

namespace Application.Models;

public class NameProfession
{
    public required string Nconst { get; set; }       // Foreign key to NameBasics
    public required int ProfessionId { get; set; }    // Foreign key to Profession
    public NameBasics? Name { get; set; }
    public Profession? Profession { get; set; }
}

