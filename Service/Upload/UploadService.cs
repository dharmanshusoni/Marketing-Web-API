using AutoMapper;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Upload
{
    public class UploadService : IUploadInterface
    {
        SqlConnection con;
        private readonly MarketingToolContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UploadService(UnitOfWork unitOfWork, IMapper mapper, MarketingToolContext context)
        {
            con = new SqlConnection(Connections.Connect());
            SqlConnection.ClearAllPools();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
            _context = context;
        }

        public Task<Result> SaveDocument(CandidateDocumentDTO request)
        {
            ArrayList responseData = new ArrayList();
            var ifExisted = _unitOfWork.CandidateDocumentRepository.Get(a => a.Name.Equals(request.Name));
            if (ifExisted.Count() == 0)
            {
                var doc = _mapper.Map<CandidateDocument>(request);
                _unitOfWork.CandidateDocumentRepository.Insert(doc);
                _unitOfWork.Save();
                responseData.Add(request);
                return Task.FromResult(new Result
                {
                    status = 1,
                    success = true,
                    message = "success",
                    count = 1,
                    data_name = "document",
                    generated_on = DateTime.Now.ToString(),
                    data = responseData
                }); ;
            }
            responseData.Add(request);
            return Task.FromResult(new Result
            {
                status = 1,
                success = false,
                message = "duplicate data",
                count = 1,
                data_name = "document",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

        public Task<Result> SaveDocument(ClientDocumentDTO request)
        {
            ArrayList responseData = new ArrayList();
            var ifExisted = _unitOfWork.ClientDocumentRepository.Get(a => a.Name.Equals(request.Name));
            if (ifExisted.Count() == 0)
            {
                var doc = _mapper.Map<ClientDocument>(request);
                _unitOfWork.ClientDocumentRepository.Insert(doc);
                _unitOfWork.Save();
                responseData.Add(request);
                return Task.FromResult(new Result
                {
                    status = 1,
                    success = true,
                    message = "success",
                    count = 1,
                    data_name = "document",
                    generated_on = DateTime.Now.ToString(),
                    data = responseData
                }); ;
            }
            responseData.Add(request);
            return Task.FromResult(new Result
            {
                status = 1,
                success = false,
                message = "duplicate data",
                count = 1,
                data_name = "document",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

    }
}
