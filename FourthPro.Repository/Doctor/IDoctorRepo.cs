﻿using FourthPro.Dto.Doctor;

namespace FourthPro.Repository.Doctor;

public interface IDoctorRepo
{
    Task<int> AddAsync(DoctorFormDto dto);
    Task<bool> CheckIfExistAsync(int doctorId);
    Task<List<DoctorDto>> GetAllAsync(string departmentName, string doctorName);
    Task<DoctorDto> GetById(int doctorId);
    Task<int> GetDoctorProjectCountAsync(int doctorId);
    Task<int> GetDoctorsCountAsync(string search);
    Task RemoveAsync(int doctorId);
    Task UpdateAsync(DoctorFormDto dto, int id);
}
