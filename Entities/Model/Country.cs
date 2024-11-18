/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       10/22/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using System.ComponentModel.DataAnnotations.Schema;

namespace IPInfo.Entities.Model
{
    [Table("Countries")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TwoLetterCode { get; set; } = string.Empty;
        public string ThreeLetterCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Country)
            {
                Country other = obj as Country;
                return Name == other.Name
                    && TwoLetterCode == other.TwoLetterCode
                    && ThreeLetterCode == other.ThreeLetterCode;
            }
            else { return false; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
