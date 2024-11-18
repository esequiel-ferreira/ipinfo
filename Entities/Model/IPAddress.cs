/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       10/22/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using System.ComponentModel.DataAnnotations.Schema;

namespace IPInfo.Entities.Model
{
    [Table("IPAddresses")]
    public class IPAddress
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string IP { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return IP;
        }
    }
}
