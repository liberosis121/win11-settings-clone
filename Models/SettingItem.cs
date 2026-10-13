namespace SettingsClone.Models
{
    public sealed class SettingItem
    {
        public SettingItem(string title, string description, string glyph)
        {
            Title = title;
            Description = description;
            Glyph = glyph;
        }

        public string Title { get; }
        public string Description { get; }
        public string Glyph { get; }
    }
}
