using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Investment_back.Models;
using Microsoft.Extensions.Options;

namespace Investment_back.Helpers
{
    public class AuthenticationHelper
    {
        private static string _secretKey;
        private static string _issuer;
        private static string _audience;
        // ต้องใช้ IOptions<JwtSettings> ในการดึงค่าจาก appsettings
        public static void Initialize(IOptions<JwtSettings> jwtSettings)
        {
            _secretKey = jwtSettings.Value.SecretKey; // ดึงค่า SecretKey จาก config
            _issuer = jwtSettings.Value.Issuer; // ดึงค่า SecretKey จาก config
            _audience = jwtSettings.Value.Audience; // ดึงค่า SecretKey จาก config
        }

        // ฟังก์ชันสำหรับการสร้าง JWT Token
        public static string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username), // สามารถเพิ่มข้อมูลอื่นๆ ที่ต้องการใน claims
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer, // สามารถใช้ชื่อ domain หรือชื่อของแอปพลิเคชันของคุณ
                audience: _audience, // ใช้ชื่อของผู้ใช้หรือโปรแกรมที่สามารถรับ token ได้
                claims: claims,
                expires: DateTime.Now.AddHours(1), // กำหนดอายุของ token เช่น 1 ชั่วโมง
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ฟังก์ชันสำหรับเข้ารหัสรหัสผ่าน
        public static string HashPassword(string plainPassword)
        {
            // การใช้ Salt เพื่อความปลอดภัยในการเข้ารหัส
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return hashedPassword;
        }

        // ฟังก์ชันสำหรับตรวจสอบรหัสผ่าน
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            // ตรวจสอบรหัสผ่านที่ผู้ใช้กรอกกับรหัสผ่านที่เข้ารหัสแล้ว
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
            return isPasswordValid;
        }

    }


}


