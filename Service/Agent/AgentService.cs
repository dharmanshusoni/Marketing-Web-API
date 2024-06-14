using AutoMapper;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Models;
using Service.CompanyClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Agent
{
    public class AgentService : IAgentInterface
    {
        SqlConnection con;
        private readonly MarketingToolContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AgentService(UnitOfWork unitOfWork, IMapper mapper, MarketingToolContext context)
        {
            con = new SqlConnection(Connections.Connect());
            SqlConnection.ClearAllPools();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public Task<Result> Insert(AgentDTO request)
        {
            ArrayList responseData = new ArrayList();
            var ifExisted = _unitOfWork.AgentRepository.Get(a => a.Email.Equals(request.Email));
            if (ifExisted.Count() == 0)
            {
                var user = _mapper.Map<DataLayer.Models.Agent>(request);
                _unitOfWork.AgentRepository.Insert(user);
                _unitOfWork.Save();
                responseData.Add(request);
                return Task.FromResult(new Result
                {
                    status = 1,
                    success = true,
                    message = "success",
                    count = 1,
                    data_name = "agent",
                    generated_on = DateTime.Now.ToString(),
                    data = responseData
                }); ;
            }
            else
            {
                var updatedInfo = ifExisted.SingleOrDefault();
                updatedInfo.ProfileImageUrl = request.ProfileImageUrl;

                var user = _mapper.Map<DataLayer.Models.Agent>(updatedInfo);
                _unitOfWork.AgentRepository.Update(user);
                _unitOfWork.Save();
            }
            responseData.Add(request);
            return Task.FromResult(new Result
            {
                status = 1,
                success = false,
                message = "duplicate data",
                count = 1,
                data_name = "agent",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

        public async Task<Result> getCard()
        {
            var newData = (await _unitOfWork.AgentRepository.GetAsync()).OrderBy(o => o.Name);
            ArrayList responseData = new ArrayList(_mapper.Map<List<AgentDTO>>(newData));
            return await Task.FromResult(new Result
            {
                status = 1,
                success = true,
                message = "success",
                count = responseData.Count,
                data_name = "agentt",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }

        public async Task<Result> getById(int agentId)
        {
            var newData = (await _unitOfWork.AgentRepository.GetAsync(client => client.AgentId == agentId)).OrderBy(o => o.Name);
            var data = _mapper.Map<List<AgentDTO>>(newData);
            ArrayList responseData = new ArrayList();
            responseData.Add(data[0]);
            return await Task.FromResult(new Result
            {
                status = 1,
                success = true,
                message = "success",
                count = responseData.Count,
                data_name = "agent",
                generated_on = DateTime.Now.ToString(),
                data = responseData
            });
        }
    }
}
