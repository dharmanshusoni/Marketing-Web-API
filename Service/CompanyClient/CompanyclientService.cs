using AutoMapper;
using Azure.Core;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.CompanyClient
{
    public class CompanyclientService : ICompanyclientInterface
    {
        SqlConnection con;
        private readonly MarketingToolContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public CompanyclientService(UnitOfWork unitOfWork, IMapper mapper, MarketingToolContext context)
        {
            con = new SqlConnection(Connections.Connect());
            SqlConnection.ClearAllPools();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        public Task<Result> Insert(CompanyClientDTO request)
        {
            ArrayList responseData = new ArrayList();
            var ifExisted = _unitOfWork.CompanyclientRepository.Get(a => a.Email.Equals(request.Email));
            if (ifExisted.Count() == 0)
            {
                var user = _mapper.Map<DataLayer.Models.CompanyClient>(request);
                _unitOfWork.CompanyclientRepository.Insert(user);
                _unitOfWork.Save();
                responseData.Add(request);
                return Task.FromResult(new Result
                {
                    status = 1,
                    success = true,
                    message = "success",
                    count = 1,
                    data_name = "Company-client",
                    generated_on = DateTime.Now.ToString(),
                    data = responseData
                }); ;
            }
            else
            {
                var updatedInfo = ifExisted.FirstOrDefault();
                updatedInfo.ProfileImageUrl = request.ProfileImageUrl;

                var user = _mapper.Map<DataLayer.Models.CompanyClient>(updatedInfo);
                _unitOfWork.CompanyclientRepository.Update(user);
                _unitOfWork.Save();
            }
            responseData.Add(request);
            return Task.FromResult(new Result
            {
                status = 1,
                success = false,
                message = "duplicate data",
                count = 1,
                data_name = "Company-client",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            }) ;
        }

        public async Task<Result>  getCard()
        {
            var newData = (await _unitOfWork.CompanyclientRepository.GetAsync()).OrderBy(o => o.Name);
            ArrayList responseData = new ArrayList(_mapper.Map<List<CompanyClientDTO>>(newData));
            return await Task.FromResult(new Result
            {
                status = 1,
                success = true,
                message = "success",
                count = responseData.Count,
                data_name = "Company-client",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

        public async Task<Result> getById(int companyClientId)
        {
            var newData = (await _unitOfWork.CompanyclientRepository.GetAsync(client => client.CompanyClientId == companyClientId)).OrderBy(o => o.Name);
            var data = _mapper.Map<List<CompanyClientDTO>>(newData);
            ArrayList responseData = new ArrayList();
            responseData.Add(data[0]);
            return await Task.FromResult(new Result
            {
                status = 1,
                success = true,
                message = "success",
                count = responseData.Count,
                data_name = "Company-client",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }
    }
}
