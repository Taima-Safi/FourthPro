using FourthPro.Dto.Doctor;
using FourthPro.Repository.Doctor;
using FourthPro.Repository.User;
using FourthPro.Service.Base;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Doctor;

public class DoctorService : BaseService, IDoctorService
{
    private readonly IDoctorRepo doctorRepo;
    private readonly IUserRepo userRepo;

    public DoctorService(IDoctorRepo doctorRepo, IUserRepo userRepo,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
    {
        this.doctorRepo = doctorRepo;
        this.userRepo = userRepo;
    }
    public async Task<int> AddAsync(DoctorFormDto dto)
    {
        if (CurrentUserId == -1)
            throw new AccessViolationException("You do not have Authorize..");

        if (await userRepo.CheckIfStudentByIdentifierAsync(CurrentUserId))
            throw new UnauthorizedAccessException("You do not have permission to add doctor..");

        var doctorId = await doctorRepo.AddAsync(dto);
        return doctorId;
    }
    public async Task<List<DoctorDto>> GetAllAsync(string departmentName, string doctorName)//filter by department name Or/and doctor name, can be null
    => await doctorRepo.GetAllAsync(departmentName, doctorName);

    public async Task<int> GetDoctorsCountAsync(string search)//filter by department name, can be null
        => await doctorRepo.GetDoctorsCountAsync(search);

    public async Task<DoctorDto> GetByIdAsync(int doctorId)
    => await doctorRepo.GetById(doctorId);

    public async Task UpdateAsync(DoctorFormDto dto, int doctorId)
    {
        if (!userRepo.checkIfAdmin(CurrentUserId))
            throw new UnauthorizedAccessException("You do not have permission to update doctor..");

        if (!await doctorRepo.CheckIfExistAsync(doctorId))
            throw new NotFoundException("Doctor not found..");

        await doctorRepo.UpdateAsync(dto, doctorId);
    }
    public async Task RemoveAsync(int doctorId)
    {
        if (CurrentUserId == -1)
            throw new AccessViolationException("You do not have Authorize..");

        if (!userRepo.checkIfAdmin(CurrentUserId))
            throw new UnauthorizedAccessException("You do not have permission to delete doctor..");

        if (!await doctorRepo.CheckIfExistAsync(doctorId))
            throw new NotFoundException("Doctor not found..");

        await doctorRepo.RemoveAsync(doctorId);
    }
}
