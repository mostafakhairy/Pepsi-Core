using AutoMapper;
using AutoMapper.QueryableExtensions;
using Coupons.PepsiKSA.Api.Core.Enums;
using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Core.Models;
using Coupons.PepsiKSA.Api.Presistence.Models;
using Coupons.PepsiKSA.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Presistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IFileExporter _fileExporter;
        private readonly IMapper _mapper;
        public TransactionRepository(ApplicationDbContext context, IConfiguration configuration, IFileExporter fileExporter, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _fileExporter = fileExporter;
            _mapper = mapper;
        }
        public async Task<bool> AddTransaction(User user, TransactionType type, int oldPoints, string promoCode, string catgoryId, string rewardRedeemed)
        {
            var transaction = new UserTransaction()
            {
                UserEmail = user.Email,
                Type = type.ToString(),
                UserId = user.Id,
                NewPointsBalance = user.Points,
                OldPointsBalance = oldPoints,
                Points = Math.Abs(user.Points - oldPoints),
                PromoCode = promoCode,
                CategoryId = catgoryId,
                RewardRedeemed = rewardRedeemed
            };
            try
            {
                _context.Add(transaction);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExportRewards(string type)
        {

            var inputPath = _configuration.GetSection("FileExport:inputFilePath").Value;
            var outputPath = _configuration.GetSection("FileExport:outputFilePath").Value;
            var publicKeyPath = _configuration.GetSection("FileExport:publicKeyFilePath").Value;
            var sftpHost = _configuration.GetSection("Sftp:Host").Value;
            var sftpPort = int.Parse(_configuration.GetSection("Sftp:Port").Value);
            var sftpUsername = _configuration.GetSection("Sftp:Username").Value;
            var sftpKeyPath = _configuration.GetSection("Sftp:KeyPath").Value;
            var exportfileName = _configuration.GetSection("FileExport:" + type + "EncryptedFileName").Value;


            var startDate = DateTime.Now.AddDays(-1);
            var query =  _context.UserTransactions.Where(x => x.Type == type && x.Date >= startDate && x.Date <= DateTime.Now);
            var dataList = new Object();

            if (type == "burn")
                dataList = await query.ProjectTo<BurnReport>(_mapper.ConfigurationProvider).ToListAsync();
            else
                dataList = await query.ProjectTo<SubscribeReport>(_mapper.ConfigurationProvider).ToListAsync();

            var dataJson = JsonSerializer.Serialize(dataList);

            var txtFile = _fileExporter.JsonStringToTXT(dataJson, exportfileName + "Unencrypted_", inputPath);
            var encryptedFile = _fileExporter.PgpEncryptedFile(txtFile, exportfileName + DateTime.Now.ToString("yyyyMMdd"), outputPath, Environment.CurrentDirectory + publicKeyPath);
           // _sFTPUploader.UploadSFTPFile(sftpHost, sftpUsername, sftpPort, sftpKeyPath, exportfileName + DateTime.Now.ToString("yyyyMMdd"), outputPath);
            return true;
        }
    }
}
