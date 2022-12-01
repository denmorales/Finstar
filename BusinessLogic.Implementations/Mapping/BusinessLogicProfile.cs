using Application.Contracts.Models;
using AutoMapper;
using Business_Logic.Models;

namespace BusinessLogic.Implementations.Mapping
{
    public class BusinessLogicProfile:Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<TodoBl[], GetAllRowsResponse>()
                .ForMember(x => x.Rows, opt => opt.MapFrom(x => x));
            CreateMap<TodoBl,TodoModel>();
            CreateMap<CommentBl, CommentModel>();
            CreateMap<InsertNewRowRequest, TodoInsertBl>();
        }
    }
}
