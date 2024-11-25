using GestaoPedidos.Models;

namespace GestaoPedidos.Request
{
    public class ClientCreateRequest
    {
        public Client Client { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
