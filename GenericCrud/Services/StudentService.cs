﻿using AutoMapper;
using GenericCrud.Db.Entities;
using GenericCrud.Dto;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Services.Base;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Services
{
    public class StudentService : Service<StudentDTO, Student>, IStudentService
    {
        public StudentService(IRepository<Student> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

    }
}