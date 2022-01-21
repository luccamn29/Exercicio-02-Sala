using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioEmSala2.Entidade
{
    public class Faturamento
    {
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal Despesa { get; set; }

    }
}
