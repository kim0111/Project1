using System.Collections.Generic;

namespace Project.Models.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<UserLanguage> UserLanguages { get; set; }
    }
}
