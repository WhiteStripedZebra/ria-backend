using System.Collections.Generic;

namespace Engineer.Domain.Authorization
{
    public static class Permissions
    {
        public static string BoardMember { get; } = "BoardMember";
        public static string Volunteer { get; } = "Volunteer";

        public static IEnumerable<string> All { get; } = new List<string>
        {
            BoardMember,
            Volunteer
        };
    }
}