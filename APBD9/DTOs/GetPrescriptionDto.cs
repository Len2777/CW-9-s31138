namespace APBD9.DTOs;

public class GetPrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public string DoctorName { get; set; } = null!;
    public List<GetMedicamentDto> Medicaments { get; set; } = new();
}