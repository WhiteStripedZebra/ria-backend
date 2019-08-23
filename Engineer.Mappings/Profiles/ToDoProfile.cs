using System;
using AutoMapper;
using Engineer.Domain.Entities;
using Engineer.Domain.Models;
using Engineer.Domain.Models.Todo;

namespace Engineer.Mapping.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo, ToDoDTO>().ReverseMap();

            CreateMap<CreateToDoDTO, ToDo>().ReverseMap();
        }
    }
}