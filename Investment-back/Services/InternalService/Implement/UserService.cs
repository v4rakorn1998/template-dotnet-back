using Dapper;
using Investment_back.Models.Response;
using Investment_back.Services.InternalService.Interface;
using System.Data;

namespace Investment_back.Services.InternalService.Implement
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _db;

        public UserService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserResponse>> GetUsers()
        {
            string sql = "SELECT id, username, password, status, created_by, created_date, updated_by, updated_date FROM authentication";
            return await _db.QueryAsync<UserResponse>(sql);  // ใช้ async method
        }
    }

}
