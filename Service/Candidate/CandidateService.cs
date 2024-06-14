using AutoMapper;
using Azure.Core;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Models;
using Service.Upload;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Candidate
{
    public class CandidateService : ICandidateInterface
    {
        SqlConnection con;
        private readonly MarketingToolContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IUploadInterface _uploadService;
        private readonly IMapper _mapper;
        public CandidateService(UnitOfWork unitOfWork, IMapper mapper, MarketingToolContext context, IUploadInterface uploadService)
        {
            con = new SqlConnection(Connections.Connect());
            SqlConnection.ClearAllPools();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
            _uploadService = uploadService;
        }
        
        public Task<Result> Insert(CandidateDTO request)
        {
            ArrayList responseData = new ArrayList();
            var ifExisted = _unitOfWork.CandidateRepository.Get(a => a.Email.Equals(request.Email));
            if (ifExisted.Count() == 0)
            {
                var user = _mapper.Map<DataLayer.Models.Candidate>(request);
                //var document = _mapper.Map<CandidateDocument>(documentRequest);
                _unitOfWork.CandidateRepository.Insert(user);
                //_unitOfWork.CandidateDocumentRepository.Insert(document);

                _unitOfWork.Save();
                responseData.Add(request);
                
                return Task.FromResult(new Result
                {
                    status = 1,
                    success = true,
                    message = "success",
                    count = 1,
                    data_name = "candidate",
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
                data_name = "candidate",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            }) ;
        }

        public async Task<Result>  getCard()
        {
            
            var newData = (await _unitOfWork.CandidateRepository.GetAsync()).OrderBy(o => o.Name);
            ArrayList responseData = new ArrayList(_mapper.Map<List<CandidateDTO>>(newData));
            return await Task.FromResult(new Result
            {
                status = 1,
                success = true,
                message = "success",
                count = responseData.Count,
                data_name = "candidate",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

        public async Task<Result> getById(int candidateId)
        {
            var newData = (await _unitOfWork.CandidateRepository.GetAsync(client => client.CandidateId == candidateId)).OrderBy(o => o.Name);
            var data = _mapper.Map<List<CandidateDTO>>(newData);
            ArrayList responseData = new ArrayList();
            responseData.Add(data[0]);
            return await Task.FromResult(new Result
            {
                status = 1,
                success = true,
                message = "success",
                count = responseData.Count,
                data_name = "candidate",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

    }
}
