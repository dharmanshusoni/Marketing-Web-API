using AutoMapper;
using DataLayer.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utility
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            #region Master
            CreateMap<CountryMaster, CountryDTO>();
            CreateMap<StateMaster, StateDTO>();
            CreateMap<CityMaster, CityDTO>();
            CreateMap<LanguageMaster, LanguageDTO>();
            CreateMap<GenderMaster, GenderDTO>();
            CreateMap<SkillMaster, SkillDTO>();
            CreateMap<QualificationMaster, QualificationDTO>();
            
            CreateMap<CountryDTO, CountryMaster>();
            CreateMap<StateDTO, StateMaster>();
            CreateMap<CityDTO, CityMaster>();
            #endregion

            #region Main
            CreateMap<CompanyClientDTO,DataLayer.Models.CompanyClient>();
            CreateMap<DataLayer.Models.CompanyClient, CompanyClientDTO>();
            CreateMap<DataLayer.Models.User, UserDTO>();
            CreateMap<CandidateDTO, DataLayer.Models.Candidate>();
            CreateMap<DataLayer.Models.Candidate,CandidateDTO>();
            CreateMap<AgentDTO, DataLayer.Models.Agent>();
            CreateMap<DataLayer.Models.Agent, AgentDTO>();
            CreateMap<ClientDocumentDTO, ClientDocument>();
            CreateMap<ClientDocument, ClientDocumentDTO>();
            CreateMap<CandidateDocumentDTO, CandidateDocument>();
            CreateMap<CandidateDocument, CandidateDocumentDTO>();
            #endregion

            //CreateMap<CityDTO, CityMaster>();
            //CreateMap<StateDTO, StateMaster>();
            //CreateMap<CountryDTO, CountryMaster>();
            //CreateMap<MemberUpdateDto, AppUser>();
            //CreateMap<RegisterDto, AppUser>();
            //CreateMap<Message, MessageDto>()
            //    .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
            //        src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
            //    .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
            //        src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            //CreateMap<MessageDto, Message>();
        }
    }
}
