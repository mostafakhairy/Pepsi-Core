using AutoMapper;
using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Integrations.MailService;
using Coupons.PepsiKSA.Api.Presistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Presistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AuthRepository(ApplicationDbContext context, IMapper mapper, IConfiguration configuration, IMailService mailService)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _mailService = mailService;
        }
        public async Task<bool> EmailExist(string userEmail)
        {
            return await _context.Users.AnyAsync(c => c.Email.ToLower() == userEmail.ToLower());
        }

        public async Task<User> Login(LoginDto userResource)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.MobileNumber == userResource.MobileNumber);
            if (user == null) return null;

            if (!VerifyPassword(userResource.Password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;

        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            if (computedHash.Length != passwordHash.Length) return false;

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }

        public async Task<User> Register(UserRegisterDto user)
        {
            var registerUser = _mapper.Map<User>(user);
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            registerUser.PasswordHash = passwordHash;
            registerUser.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            return registerUser;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public async Task<bool> UserExist(string mobileNumber)
        {
            return await _context.Users.AnyAsync(c => c.MobileNumber == mobileNumber);
        }

        public AuthDto GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.FirstName),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim ("mobileNumber", user.MobileNumber),
                new Claim ("points", user.Points.ToString()),
                new Claim ("isSubscribedMail", user.IsSubscribedMail.ToString() ),
                new Claim ("isSubscribedSms", user.IsSubscribedSms.ToString())
            };
            var appKey = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value);

            var key = new SymmetricSecurityKey(appKey);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);
            return new AuthDto() { Token = token };
        }

        public async Task<bool> GenerateOTP(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
            if (user == null) return false;
            if (user.ResetPasswordOTPDate > DateTime.Now) return false;

            user.ResetPasswordOTP = new Random().Next(1000, 9999);
            user.ResetPasswordOTPDate = DateTime.Now.AddMinutes(10);
            var bodyText = _configuration.GetSection("MailService:EmailBody").Value;
            _mailService.Send(new EmailProperties { Subject = "Pepsi KSA Promo Password Reset", Body = bodyText + user.ResetPasswordOTP.ToString(), To = email });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateOTP(string email, int otp)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
            if (user == null) return false;

            if (user.ResetPasswordOTPDate < DateTime.Now) return false;
            if (user.ResetPasswordOTP != otp) return false;

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == req.Email);
            if (user == null) return false;

            if (user.ResetPasswordOTPDate < DateTime.Now) return false;
            if (user.ResetPasswordOTP != req.OTP) return false;

            CreatePasswordHash(req.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ResetPasswordOTP = null;
            user.ResetPasswordOTPDate = null;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
