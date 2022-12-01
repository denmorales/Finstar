using AutoMapper;
using Business_Logic.Models;
using Data_Access.Entities;

namespace Data_Access.Mapping
{
    public class DataAccessProfile:Profile
    {
        public DataAccessProfile()
        {
            CreateMap<TodoInsertBl, TodoEntity>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => DateTimeOffset.Now));
            CreateMap<TodoEntity, TodoBl>();
            CreateMap<CommentEntity, CommentBl>();
        }
    }
}
