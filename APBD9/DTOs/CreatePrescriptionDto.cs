namespace APBD9.DTOs;

public class CreatePrescriptionDto
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public CreatePatientDto Patient { get; set; } = null!;

    public int DoctorId { get; set; }

    public List<PrescribedMedicamentDto> Medicaments { get; set; } = new();
}