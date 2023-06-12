namespace WebApp.Models
{
    public class TossUp
    {
        public int Id { get; set; } 
        public string Question { get; set; }
        public string Answer { get; set; }


        public TossUp(int id, string question, string answer) 
        {
            this.Id = id;
            this.Question = question;
            this.Answer = answer;
        }

    }
}
