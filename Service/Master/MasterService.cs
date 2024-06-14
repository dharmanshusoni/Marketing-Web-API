using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Models;
using Service.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Master
{
    public class MasterService : IMasterInterface
    {
        SqlConnection con;
        private readonly MarketingToolContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public MasterService(UnitOfWork unitOfWork, IMapper mapper, MarketingToolContext context)
        {
            con = new SqlConnection(Connections.Connect());
            SqlConnection.ClearAllPools();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        public async Task<List<CountryDTO>> getCountry()
        {
            var records = await _unitOfWork.CountryRepository.GetAsync();
            if (records != null)
            {
                return _mapper.Map<List<CountryMaster>, List<CountryDTO>>(records.ToList());
            }
            return new List<CountryDTO>();
        }
        
        public async Task<List<CityDTO>> getCity(int stateId)
        {
            if (stateId>0)
            {
                var data = await _context.CityMasters.Where(c => c.StateId == stateId).ProjectTo<CityDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return data;
            }
            else
            {
                var data = await _unitOfWork.CityRepository.GetAsync();
                return _mapper.Map<List<CityMaster>, List<CityDTO>>(data.ToList());
            }
        }
        
        public async Task<List<StateDTO>> getState(int countryId)
        {
            if (countryId > 0)
            {
                var data = await _context.StateMasters.Where(c => c.CountryId == countryId).ProjectTo<StateDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return data;
            }
            else
            {
                var data = await _unitOfWork.StateRepository.GetAsync();
                return _mapper.Map<List<StateMaster>, List<StateDTO>>(data.ToList());
            }
            //var records = await _unitOfWork.StateRepository.GetAsync();
            //if (records != null)
            //{
            //    return _mapper.Map<List<StateMaster>, List<StateDTO>>(records.ToList());
            //}
            //return new List<StateDTO>();
        }

        public async Task<List<GenderDTO>> getGender(int genderId)
        {
            if (genderId > 0)
            {
                var data = await _context.GenderMasters.Where(c => c.Id == genderId).ProjectTo<GenderDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return data;
            }
            else
            {
                var data = await _unitOfWork.GenderRepository.GetAsync();
                return _mapper.Map<List<GenderMaster>, List<GenderDTO>>(data.ToList());
            }
        }

        public async Task<List<LanguageDTO>> getLanguage(int languageId)
        {
            if (languageId > 0)
            {
                var data = await _context.LanguageMasters.Where(c => c.Id == languageId).ProjectTo<LanguageDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return data;
            }
            else
            {
                var data = await _unitOfWork.LanguageRepository.GetAsync();
                return _mapper.Map<List<LanguageMaster>, List<LanguageDTO>>(data.ToList());
            }
            
        }

        public async Task<List<QualificationDTO>> getQualification(int qualificationId)
        {
            if (qualificationId > 0)
            {
                var data = await _context.QualificationMasters.Where(c => c.Id == qualificationId).ProjectTo<QualificationDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return data;
            }
            else
            {
                var data = await _unitOfWork.QualificationRepository.GetAsync();
                return _mapper.Map<List<QualificationMaster>, List<QualificationDTO>>(data.ToList());
            }
        }

        public async Task<List<SkillDTO>> getSkill(int skillId)
        {
            if (skillId > 0)
            {
                var data = await _context.SkillMasters.Where(c => c.Id == skillId).ProjectTo<SkillDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return data;
            }
            else
            {
                var data = await _unitOfWork.SkillRepository.GetAsync();
                return _mapper.Map<List<SkillMaster>, List<SkillDTO>>(data.ToList());
            }
        }
    }
}
