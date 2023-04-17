﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Board.Daos;
using Job_Board.Models;
using Job_Board.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;

namespace Job_Board.Controllers
{
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private ICandidateDao _candidateDao;

        public CandidateController(ICandidateDao candidatedao)
        {
            _candidateDao = candidatedao;
        }

        /// <summary>Get All Candidate's Information</summary>
        /// <returns>Candidate Information</returns>
        /// <response code="200">Returns the Information of all Candidates</response>
        [HttpGet]
        [Route("Candidate")]
        public async Task<IActionResult> GetCandidates()
        {
            try
            {
                IEnumerable<Candidate> candidates = await _candidateDao.GetCandidates();
                if (!candidates.Any())
                {
                    return ErrorResponses.CustomError("There are currently no Candidate entries");
                }
                return SuccessResponses.GetAllSuccessful(candidates);
                
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Get Candidate Information by Job ID</summary>
        /// <returns>Candidate Information</returns>
        /// <response code="200">Returns the Information of selected Candidates</response>
        [HttpGet]
        [Route("Candidate/{id:Guid}")]
        public async Task<IActionResult> GetCandidateByID([FromRoute] Guid id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }
                return SuccessResponses.GetObjectSuccessful(candidate);
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Create a new Candidate</summary>
        /// <returns></returns>
        /// <response code="201">Candidate has been successfully created</response>
        [HttpPost]
        [Route("Candidate")]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateRequest createRequest)
        {
            try
            {
                await _candidateDao.CreateCandidate(createRequest);
                return SuccessResponses.CreateSuccessful("Candidate");
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Delete a Candidate By ID</summary>
        /// <returns></returns>
        /// <response code="200">Candidate has been successfully deleted</response>
        [HttpDelete]
        [Route("Candidate/{id}")]
        public async Task<IActionResult> DeleteCandidateById([FromRoute] Guid id)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorInputNotFound(id.ToString());
                }

                await _candidateDao.DeleteCandidateById(id);
                return SuccessResponses.DeleteSuccessful(id.ToString());
            }
            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

        /// <summary>Update a Candidate by Candidate ID</summary>
        /// <returns></returns>
        /// <response code="200">Candidate has been successfully Updated</response>
        /// 
        [HttpPatch]
        [Route("Candidate")]
        public async Task<IActionResult> UpdateCandidateByID([FromBody] Candidate candidateReq)
        {
            try
            {
                var candidate = await _candidateDao.GetCandidateByID(candidateReq.Id);
                if (candidate == null)
                {
                    return ErrorResponses.ErrorUpdating(candidateReq.Id.ToString());
                }

                var updatedCandidate = await _candidateDao.UpdateCandidateById(candidateReq);

                return SuccessResponses.UpdateObjectSuccessful(candidateReq.Id.ToString());
            }

            catch (Exception)
            {
                return ErrorResponses.Error500();
            }
        }

    }
}