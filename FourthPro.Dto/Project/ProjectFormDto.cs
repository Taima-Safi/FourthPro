﻿using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Project;

public class ProjectFormDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tools { get; set; }
    public SectionType Type { get; set; }
    public DateTime Date { get; set; }
    public int DoctorId { get; set; }
    public List<int> UserIds { get; set; }
}
