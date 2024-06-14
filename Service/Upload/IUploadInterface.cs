using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Upload
{
    public interface IUploadInterface
    {
        Task<Result> SaveDocument(CandidateDocumentDTO request);
        Task<Result> SaveDocument(ClientDocumentDTO request);
    }
}
