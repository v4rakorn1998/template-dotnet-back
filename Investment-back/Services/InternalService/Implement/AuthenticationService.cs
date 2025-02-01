using Dapper;
using Investment_back.Helpers;
using Investment_back.Models.Request;
using Investment_back.Models.Response;
using Investment_back.Services.InternalService.Interface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Investment_back.Services.InternalService.Implement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDbConnection _db;

        public AuthenticationService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();
            string sql = "SELECT username, password FROM authentication WHERE status = 1 AND username = @Username";

            // ดึงข้อมูลผู้ใช้จากฐานข้อมูล
            var user = await _db.QueryFirstOrDefaultAsync<UserResponse>(sql, new { Username = request.username });

            if (user == null)
            {
                return null;
            }
            else {
                var verify = AuthenticationHelper.VerifyPassword(request.password, user.password);

                if (verify)
                {
                    response.token = AuthenticationHelper.GenerateJwtToken(user.username);
                }
                else {
                    return null;
                }
            }
            return response;
        }
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            
            string checkUserSql = "SELECT COUNT(1) FROM authentication WHERE username = @Username";
            var userExists = await _db.ExecuteScalarAsync<int>(checkUserSql, new { Username = request.username });
            
            if (userExists > 0)
            {
                return new RegisterResponse
                {
                    success = false,
                    message = "Username already exists."
                };
            }
          
            string hashedPassword = AuthenticationHelper.HashPassword(request.password);

            string insertUserSql = "INSERT INTO authentication (username, password) VALUES (@Username, @Password)";
            var rowsAffected = await _db.ExecuteAsync(insertUserSql, new { Username = request.username, Password = hashedPassword });

            if (rowsAffected > 0)
            {
                return new RegisterResponse
                {
                    success = true,
                    message = "Registration successful."
                };
            }

            return new RegisterResponse
            {
                success = false,
                message = "Registration failed."
            };
        }

        

    }
}
