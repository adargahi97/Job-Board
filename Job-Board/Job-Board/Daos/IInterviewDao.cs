using Job_Board.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IInterviewDao
    {
        //public void GetInterview();

        
        Task DeleteInterviewById(int id);

        Task<InterviewRequest> GetInterviewByID(int id);


        Task<InterviewRequest> GetInterviewByCandidateId(int candidateId);


        Task<InterviewRequest> GetInterviewByJob_Id(int job_Id);

        Task<Interview> UpdateInterviewById(Interview interview);



    }
}
