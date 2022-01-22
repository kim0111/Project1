using System;

namespace Project.Models.Entities
{
    public class UserLanguage
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string Level { get; set; }

}
}
