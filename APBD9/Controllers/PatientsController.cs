using APBD9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD9.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatienService _patientService;

    public PatientsController(IPatienService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _patientService.GetPatientAsync(id);
        if (patient == null)
            return NotFound($"Patient with id {id} not found.");

        return Ok(patient);
    }
}
 