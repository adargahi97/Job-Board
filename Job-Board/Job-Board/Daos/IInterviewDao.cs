using Job_Board.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Board.Daos
{
    public interface IInterviewDao
    {
        //public void GetInterview();

        
        Task DeleteInterviewById(int id);
        

    }
}
