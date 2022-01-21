using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioEmSala2.Entidade
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Int64 Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
