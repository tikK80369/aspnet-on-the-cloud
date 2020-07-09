using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Models
{
    //エンティティクラス
    public class UserListModel
    {
    
        public int Id { get; set; } //主キー
        public int Userid { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime D_CRT {get; set;}
        
    }

    //データベースコンテキストクラス
    public class MvcCRUDContext : DbContext
    {
        public MvcCRUDContext (DbContextOptions<MvcCRUDContext> options)
            : base(options)
        {
        }
        public DbSet<UserListModel> CRUDTest { get; set; }
    }
}