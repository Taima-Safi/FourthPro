﻿using FourthPro.Dto.Doctor;

namespace FourthPro.Dto.Department;

public class DepartmentDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<DoctorDto> Doctors { get; set; } = new();
}
