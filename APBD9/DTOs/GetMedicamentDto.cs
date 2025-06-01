namespace APBD9.DTOs;

public class GetMedicamentDto
{
    public string Name { get; set; } = null!;
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}