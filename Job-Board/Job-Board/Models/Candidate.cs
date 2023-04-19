using Job_Board.Models;
using Microsoft.VisualBasic;
using System;

namespace Job_Board.Models
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid JobId { get; set; }

    }
    public class CandidateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid JobId { get; set; }
    }

    public class CandidateByLastName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }

    public class CandidateByJobId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}

//
//

//
//Get Candidate by Job ID: Retrieve Candidate information by the Job ID they are applying for.

//Delete Candidate: Remove an existing Candidate from the system by their Candidate ID.

//Search Interview by Date: Find all Interviews scheduled for a specific date.

//Get Today's Interview: Retrieve all Interviews scheduled for the current day.

//Get Interview by Position: Retrieve all Interviews scheduled for a specific position.

//Get All Interviews: Retrieve all Interviews stored in the system.

//Create Interview: Schedule a new Interview.

//Update Interview: Modify an existing Interview's information by the Interview ID.

//Get Interview by Job ID: Retrieve all Interviews scheduled for a specific Job ID.

//Delete Interview: Remove an existing Interview from the system by their Interview ID.

//Get All Job Postings: Retrieve all Job Postings stored in the system.

//Create Job Posting: Add a new Job Posting to the system.

//Update Job Posting: Modify an existing Job Posting's information by its Job ID.

//Get Job Posting by ID: Retrieve Job Posting information by its Job ID.

//Delete Job Posting: Remove an existing Job Posting from the system by its Job ID.

//Search Job Posting by Location: Retrieve all Job Postings based on a specific location.-------------

//Search Job Posting by Position: Retrieve all Job Postings based on a specific Position.

//Get All Locations: Retrieve all Locations stored in the system.

//Delete Location: Remove an existing Location from the system by its unique ID.

//Update Location: Modify an existing Location's information by the Location ID.

//Get Location: Retrieve Location information by its Location ID.

//Get Location by Building: Retrieve all Locations within a specific building.

//Search Location by State: Retrieve all Locations based on a specific state.

//Get Candidates by Last Name: Retrieve all Candidates searching by last name.

//Search Candidates by Position: Retrieve all Candidates applying for a specific Position.

//Search Interview by Job ID: Retrieve all Interviews scheduled for a specific Job ID.

//Search Interview by Last Name: Retrieve all Interviews scheduled for a Candidate with a specific last name.