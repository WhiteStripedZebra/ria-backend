namespace Engineer.Api.Authorization.Policies
{
    public static class CouncilPolicies
    {
        public const string IsBoardMember = "IsBoardMember";
        public const string IsBoardMemberOrVolunteer = "IsBoardMemberOrVolunter";
    }
}