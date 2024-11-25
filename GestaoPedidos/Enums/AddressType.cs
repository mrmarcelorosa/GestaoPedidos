using System.Runtime.Serialization;

namespace GestaoPedidos.Enums
{
    public enum AddressType
    {
        [EnumMember(Value = "Entrega")]
        Delivery,

        [EnumMember(Value = "Cobrança")]
        Billing,

        [EnumMember(Value = "Ambos")]
        Both
    }
}
