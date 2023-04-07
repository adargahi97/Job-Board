using Job_Board.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ICandidateDao
    {
        Task CreateCandidate(CandidateRequest candidate);
        Task<IEnumerable<Candidate>> GetCandidates();
        Task<Candidate> GetCandidateByID(Guid id);
        Task DeleteCandidateById(Guid id);
        Task<Candidate> UpdateCandidateById(Candidate candidate);

    }
}