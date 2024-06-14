using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CompanyClient
{
    public interface ICompanyclientInterface
    {
        Task<Result> Insert(CompanyClientDTO data);
        Task<Result> getCard();
        Task<Result> getById(int companyClientId);
    }
}
