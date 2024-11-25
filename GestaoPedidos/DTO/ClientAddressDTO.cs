namespace GestaoPedidos.DTO
{
    public class ClientAddressDTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public List<ClientAddressDetailDTO> Addresses { get; set; } = new List<ClientAddressDetailDTO>();
    }
}
