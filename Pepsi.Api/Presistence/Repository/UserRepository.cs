using AutoMapper;
using AutoMapper.QueryableExtensions;
using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Presistence.Models;
using Coupons.PepsiKSA.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Presistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IFileExporter _fileExporter;
        public UserRepository(ApplicationDbContext context, IMapper mapper,
            IConfiguration configuration, IFileExporter fileExporter)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _fileExporter = fileExporter;

        }

        public async Task<User> GetUser(string mobileNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.MobileNumber == mobileNumber);
        }

        public async Task GetUsersReport()
        {
            var inputPath = _configuration.GetSection("FileExport:inputFilePath").Value;
            var outputPath = _configuration.GetSection("FileExport:outputFilePath").Value;
            var publicKeyPath = _configuration.GetSection("FileExport:publicKeyFilePath").Value;
            var exportfileName = _configuration.GetSection("FileExport:UserReportFileName").Value;
            var sftpHost = _configuration.GetSection("Sftp:Host").Value;
            var sftpPort = int.Parse(_configuration.GetSection("Sftp:Port").Value);
            var sftpUsername = _configuration.GetSection("Sftp:Username").Value;
            var sftpKeyPath = _configuration.GetSection("Sftp:KeyPath").Value;

            var startDate = DateTime.Now.AddDays(-1);
            var users = await _context.Users
                .Where(c => c.LastModificationDate >= startDate && c.LastModificationDate <= DateTime.Now)
                .ProjectTo<UserReportDto>(_mapper.ConfigurationProvider).ToListAsync();
            var usersJson = JsonConvert.SerializeObject(users);

            var txtFile = _fileExporter.JsonStringToTXT(usersJson, "Profile_Daily_Unencrypted_", inputPath);
            var encryptedFile = _fileExporter.PgpEncryptedFile(txtFile, exportfileName + DateTime.Now.ToString("yyyyMMdd"), outputPath, Environment.CurrentDirectory + publicKeyPath);
            //_sFTPUploader.UploadSFTPFile(sftpHost, sftpUsername, sftpPort, sftpKeyPath, exportfileName + DateTime.Now.ToString("yyyyMMdd"), outputPath);
        }

        public async Task<User> UpdatePoints(string mobileNumber, int points, bool isRedeem = false)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.MobileNumber == mobileNumber);
            user.Points = !isRedeem ? user.Points + points : user.Points - points;
            user.LastModificationDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateSubsciptions(string mobileNumber, UserSubscriptionsDto userSubscriptions)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.MobileNumber == mobileNumber);
            user.IsSubscribedMail = userSubscriptions.IsSubscribedMail;
            user.IsSubscribedSms = userSubscriptions.IsSubscribedSms;
            user.LastModificationDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserStatus(string mobileNummber, bool isVerified)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.MobileNumber == mobileNummber);
            user.IsEcouponsVerified = isVerified;
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
