using DataLayer.GenericRepository;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MarketingToolContext context = new MarketingToolContext();

        #region User Repository
        private GenericRepository<User> userRepository;
        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }
        #endregion

        #region Country Repository
        private GenericRepository<CountryMaster> countryRepository;
        public GenericRepository<CountryMaster> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                {
                    this.countryRepository = new GenericRepository<CountryMaster>(context);
                }
                return countryRepository;
            }
        }
        #endregion

        #region State Repository
        private GenericRepository<StateMaster> stateRepository;
        public GenericRepository<StateMaster> StateRepository
        {
            get
            {
                if (this.stateRepository == null)
                {
                    this.stateRepository = new GenericRepository<StateMaster>(context);
                }
                return stateRepository;
            }
        }
        #endregion

        #region City Repository
        private GenericRepository<CityMaster> cityRepository;
        public GenericRepository<CityMaster> CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                {
                    this.cityRepository = new GenericRepository<CityMaster>(context);
                }
                return cityRepository;
            }
        }
        #endregion

        #region Company Client Repository
        private GenericRepository<CompanyClient> companyclientRepository;
        public GenericRepository<CompanyClient> CompanyclientRepository
        {
            get
            {
                if (this.companyclientRepository == null)
                {
                    this.companyclientRepository = new GenericRepository<CompanyClient>(context);
                }
                return companyclientRepository;
            }
        }
        #endregion

        #region Language Repository
        private GenericRepository<LanguageMaster> languageRepository;
        public GenericRepository<LanguageMaster> LanguageRepository
        {
            get
            {
                if (this.languageRepository == null)
                {
                    this.languageRepository = new GenericRepository<LanguageMaster>(context);
                }
                return languageRepository;
            }
        }

        #endregion

        #region Gender Repository
        private GenericRepository<GenderMaster> genderRepository;
        public GenericRepository<GenderMaster> GenderRepository
        {
            get
            {
                if (this.genderRepository == null)
                {
                    this.genderRepository = new GenericRepository<GenderMaster>(context);
                }
                return genderRepository;
            }
        }

        #endregion

        #region Qualification Repository
        private GenericRepository<QualificationMaster> qualificationRepository;
        public GenericRepository<QualificationMaster> QualificationRepository
        {
            get
            {
                if (this.qualificationRepository == null)
                {
                    this.qualificationRepository = new GenericRepository<QualificationMaster>(context);
                }
                return qualificationRepository;
            }
        }

        #endregion

        #region Skill Repository
        private GenericRepository<SkillMaster> skillRepository;
        public GenericRepository<SkillMaster> SkillRepository
        {
            get
            {
                if (this.skillRepository == null)
                {
                    this.skillRepository = new GenericRepository<SkillMaster>(context);
                }
                return skillRepository;
            }
        }

        #endregion

        #region Candidate Repository
        private GenericRepository<Candidate> candidateRepository;
        public GenericRepository<Candidate> CandidateRepository
        {
            get
            {
                if (this.candidateRepository == null)
                {
                    this.candidateRepository = new GenericRepository<Candidate>(context);
                }
                return candidateRepository;
            }
        }
        #endregion

        #region Agent Repository
        private GenericRepository<Agent> agentRepository;
        public GenericRepository<Agent> AgentRepository
        {
            get
            {
                if (this.agentRepository == null)
                {
                    this.agentRepository = new GenericRepository<Agent>(context);
                }
                return agentRepository;
            }
        }
        #endregion

        #region ClientDocument Repository
        private GenericRepository<ClientDocument> clientDocumentRepository;
        public GenericRepository<ClientDocument> ClientDocumentRepository
        {
            get
            {
                if (this.clientDocumentRepository == null)
                {
                    this.clientDocumentRepository = new GenericRepository<ClientDocument>(context);
                }
                return clientDocumentRepository;
            }
        }
        #endregion

        #region CandidateDocument Repository
        private GenericRepository<CandidateDocument> candidateDocumentRepository;
        public GenericRepository<CandidateDocument> CandidateDocumentRepository
        {
            get
            {
                if (this.candidateDocumentRepository == null)
                {
                    this.candidateDocumentRepository = new GenericRepository<CandidateDocument>(context);
                }
                return candidateDocumentRepository;
            }
        }
        #endregion

        #region Generic
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
