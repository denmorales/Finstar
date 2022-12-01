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
            CreateMap<TodoEntity, TodoBl>()
                .ForMember(x=>x.Hash, opt =>opt.MapFrom(x=>CreateHash(x.Header)));
            CreateMap<CommentEntity, CommentBl>();
        }

        private string CreateHash(string header)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(header);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
