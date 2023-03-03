using Job_Board.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface ICandidateDao
    {
        Task CreateCandidate(CandidateRequest candidate);
        Task<IEnumerable<Candidate>> GetCandidates();
        Task<Candidate> GetCandidateByID(int id);
        Task DeleteCandidateById(int id);
        Task<Candidate> UpdateCandidateById(Candidate candidate);
        Task<Candidate> GetCandidateByFirstName(string firstName);

    }
}