using System;

namespace SettingsClone.Models
{
    public sealed class NavigationItem
    {
        public NavigationItem(string label, string icon, string automationId, Type? pageType = null)
        {
            Label = label;
            IconPath = $"ms-appx:///Assets/Navigation/{icon}.svg";
            AutomationId = automationId;
            PageType = pageType;
        }

        public string Label { get; }
        public string IconPath { get; }
        public string AutomationId { get; }
        public Type? PageType { get; }
    }
}
