namespace Job_Board.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int LocationsId { get; set; }
        public int CandidateId { get; set; }

    }
}
