using APBD9.Data;
using APBD9.DTOs;
using APBD9.Exeptions;
using APBD9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD9.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(CreatePrescriptionDto dto);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _context;

    public PrescriptionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task AddPrescriptionAsync(CreatePrescriptionDto dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("A prescription can contain at most 10 medicaments.");

        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate must be on or after Date.");

        var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
        if (doctor == null)
            throw new NotFoundException("Doctor not found.");

        var patient = await _context.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == dto.Patient.FirstName &&
            p.LastName == dto.Patient.LastName &&
            p.BirthDate == dto.Patient.BirthDate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                BirthDate = dto.Patient.BirthDate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            Doctor = doctor,
            Patient = patient
        };

        foreach (var medDto in dto.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(medDto.MedicamentId);
            if (medicament == null)
                throw new NotFoundException($"Medicament with ID {medDto.MedicamentId} not found.");

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                Medicament = medicament,
                Dose = medDto.Dose,
                Details = medDto.Description
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }
}