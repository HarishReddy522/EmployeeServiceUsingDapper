using EmployeeBusinessLayer;
using EmployeeModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
namespace EmployeeDataAccessLayer
{
    public class BookRepository: IBookRepository
    {
        IOptions<ReadConfig> _Connectionstring;
        public BookRepository(IOptions<ReadConfig> Connectionstring)
        {
            _Connectionstring = Connectionstring;
        }

        public List<Book> GetAllBooksWithDetails()
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                var BookDict = new Dictionary<int,Book>();
                string sql = "select * from Books B Left Join[Library] L on B.BId = L.BId Left join Users U on U.BId = B.BId";
             var Bookslst=   con.Query<Book, Library, User, Book>(sql,
                    map: (B, L, U) => {
                        Book BookEntry;
                        if (!BookDict.TryGetValue(B.BId, out BookEntry))
                        {
                            BookEntry = B;
                            BookEntry.Libraries = new List<Library>();
                            BookEntry.Users = new List<User>();
                            BookDict.Add(B.BId, B);
                        }
                        if(!BookEntry.Users.Any(u=>u.UId==U.UId))
                           BookEntry.Users.Add(U);
                        if (!BookEntry.Libraries.Any(u => u.LId == L.LId))
                            BookEntry.Libraries.Add(L);

                        return BookEntry;
                    },
                    splitOn:"LId,UId"
                    ).Distinct().ToList();
                return Bookslst;
            }
         }

        public void GetAllUserLibraryDetails()
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {

                string sql = "select count(*) from Library;";
                var v = con.ExecuteScalar(sql);
                //string sql = "select * from Library; select * from Users";
                //using (var multiresult= con.QueryMultiple(sql))
                //{
                //    List <Library> librarylist= multiresult.Read<Library>().ToList();
                //    List<User> Userlist = multiresult.Read<User>().ToList();
                //}
            }
        }
    }
}
