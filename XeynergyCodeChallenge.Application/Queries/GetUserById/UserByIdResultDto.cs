using System;

namespace XeynergyCodeChallenge.Application.Queries.GetUserById
{
    public class UserByIdResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid AttachedCustomerId { get; set; }
        public string UserGroupName { get; set; }
        public string AccessRuleName { get; set; }
    }
}
