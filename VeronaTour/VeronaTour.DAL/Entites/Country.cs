using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeronaTour.DAL.Entites
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
