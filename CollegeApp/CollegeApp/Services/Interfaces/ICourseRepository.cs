﻿using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeApp.Services.Interfaces
{
    public interface ICourseRepository
    {
        public void RegisterCourse(CourseRegisterDto courseDto);
    }
}