using System;
using System.Threading.Tasks;
using Job_Board;
using Job_Board.Daos;
using Job_Board.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Job_Board.Controllers
{
    [ApiController]
    public class CandidateController : ControllerBase
    {

        private readonly CandidateDao _candidateDao;

        public CandidateController()
        {
        }

        public CandidateController(CandidateDao candidateDao)
        {
            _candidateDao = candidateDao;
        }

        private ICandidateDao candidateDao;

        public CandidateController(ICandidateDao candidateDao)
        {
            this.candidateDao = candidateDao;
        }

        public void CallDao()
        {
            candidateDao.GetCandidate();
            
        [HttpGet]
        [Route("Candidate")]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                var candidates = await _candidateDao.GetCandidates();
                return Ok(candidates);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}