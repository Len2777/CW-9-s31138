namespace APBD9.DTOs;

public class PrescribedMedicamentDto
{
    public int MedicamentId { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}