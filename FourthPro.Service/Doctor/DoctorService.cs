﻿using FourthPro.Dto.Doctor;
using FourthPro.Repository.Doctor;
using FourthPro.Repository.User;
using FourthPro.Service.Base;
using FourthPro.Service.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Doctor;

public class DoctorService : BaseService, IDoctorService
{
    private readonly IDoctorRepo doctorRepo;
    private readonly IUserRepo userRepo;
    private readonly IUserService userService;

    public DoctorService(IDoctorRepo doctorRepo, IUserService userService, IUserRepo userRepo,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
    {
        this.doctorRepo = doctorRepo;
        this.userService = userService;
        this.userRepo = userRepo;
    }
    public async Task<int> AddAsync(DoctorFormDto dto)
    {
        if (CurrentUserId == -1)
            throw new Exception("You do not have Authorize..");

        if (await userRepo.CheckIfStudentById(CurrentUserId))
            throw new Exception("You do not have permission to add doctor..");

        var doctorId = await doctorRepo.AddAsync(dto);
        return doctorId;
    }
    public async Task<List<DoctorDto>> GetAllAsync(string search)
    => await doctorRepo.GetAllAsync(search);

    public async Task<int> GetDoctorsCount(string search)//filter by department name, can be null
        => await doctorRepo.GetDoctorsCount(search);

    public async Task<DoctorDto> GetByIdAsync(int doctorId)
    => await doctorRepo.GetById(doctorId);

    public async Task UpdateAsync(DoctorFormDto dto, int doctorId)
    {
        if (CurrentUserId == -1)
            throw new Exception("You do not have Authorize..");

        if (await userRepo.CheckIfStudentById(CurrentUserId))
            throw new Exception("You do not have permission to add doctor..");

        if (!await doctorRepo.CheckIfExist(doctorId))
            throw new Exception("Doctor not found..");

        await doctorRepo.UpdateAsync(dto, doctorId);
    }
    public async Task RemoveAsync(int doctorId)
    {
        if (CurrentUserId == -1)
            throw new Exception("You do not have Authorize..");

        if (await userRepo.CheckIfStudentById(CurrentUserId))
            throw new Exception("You do not have permission to add doctor..");

        if (!await doctorRepo.CheckIfExist(doctorId))
            throw new Exception("Doctor not found..");

        await doctorRepo.RemoveAsync(doctorId);
    }
}
