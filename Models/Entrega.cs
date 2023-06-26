

namespace App_Entrega.Models
{
    public class Entrega
    {
       
        public int Id { get; set; }

        public string Remetente { get; set; }

        public DateTime DataEntrega { get; set; }

        public int Hora { get; set; }
            
        public int IdMorador { get; set; }
    }
}
