using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Candidate
{
    public interface ICandidateInterface
    {
        Task<Result> Insert(CandidateDTO data);
        Task<Result> getCard();
        Task<Result> getById(int candidateId);
    }
}
