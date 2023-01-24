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
    public class JobPostingController : ControllerBase
    {

        private readonly JobPostingDao _jobPostingDao;
        public JobPostingController(JobPostingDao jobPostingDao)
        {
            _jobPostingDao = jobPostingDao;
        }
    }
}