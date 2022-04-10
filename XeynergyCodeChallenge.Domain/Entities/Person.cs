using XeynergyCodeChallenge.Domain.Common;

namespace XeynergyCodeChallenge.Domain.Entities
{
    public class Person: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
