using System.ComponentModel.DataAnnotations;

namespace WebAPIFootball.Model
{
    public class Player
    {
        public int PlayerID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Range(1,99), Required]
        public int ShirtNumber { get; set; }
        public decimal Salary { get; set; }
        public int GoalsThisSeason { get; set; }
        [Required]
        public string Position { get; set; }
        
        private Team teamname { get; set; }

        public override string ToString()
        {
            return $"{PlayerID} , {Name},{ShirtNumber},{Salary},{GoalsThisSeason},{Position}  ";
        }
    }
}