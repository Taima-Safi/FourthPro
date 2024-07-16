using FourthPro.Dto.Doctor;

namespace FourthPro.Service.Doctor;

public interface IDoctorService
{
    Task<int> AddAsync(DoctorFormDto dto);
    Task<List<DoctorDto>> GetAllAsync(string search);
    Task<DoctorDto> GetByIdAsync(int doctorId);
    Task<int> GetDoctorsCountAsync(string search);
    Task RemoveAsync(int doctorId);
    Task UpdateAsync(DoctorFormDto dto, int doctorId);
}
