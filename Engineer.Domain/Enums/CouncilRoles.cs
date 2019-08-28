using System.Runtime.Serialization;

namespace Engineer.Domain.Enums
{
    public enum CouncilRole
    {
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "BoardMember")]
        BoardMember,
        [EnumMember(Value = "Volunteer")]
        Volunteer
    }
}