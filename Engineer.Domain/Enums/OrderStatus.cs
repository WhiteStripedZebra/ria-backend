using System.Runtime.Serialization;

namespace Engineer.Domain.Enums
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Appending")]
        Appending,
        [EnumMember(Value = "Approved")]
        Approved,
        [EnumMember(Value = "Delivered")]
        Delivered,
        [EnumMember(Value = "Returned")]
        Returned
    }
}