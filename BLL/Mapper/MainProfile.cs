using AutoMapper;
using BLL.DTOs.Coment;
using BLL.DTOs.Post;
using BLL.DTOs.User;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            //User main data
            CreateMap<User, UserMainDataDTO>();
            CreateMap<UserMainDataDTO, User>();

            //User register
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserRegisterDTO>();

            //Post
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostDTO>();

            CreateMap<PostCreateDTO,Post>();
            CreateMap<Post, PostCreateDTO>();

            //Coment
            CreateMap<ComentDTO,Coment>();
            CreateMap<Coment,ComentDTO>();

           



        }
    }
}
