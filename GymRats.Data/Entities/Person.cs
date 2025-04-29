using System;
using System.Collections.Generic;

namespace GymRats.Data.Entities;

public partial class Person
{
    public int IdPerson { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public decimal Weight { get; set; }

    public int Height { get; set; }

    public string? Address { get; set; }

    public string? FlatNumber { get; set; }

    public string? ZipCode { get; set; }

    public string? Place { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual User? User { get; set; }
}
