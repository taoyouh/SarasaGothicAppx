using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SarasaGothic
{
    public class FontPreviewItem
    {
        public FontPreviewItem(string displayName, string familyName)
        {
            Name = displayName ?? throw new ArgumentNullException(nameof(displayName));
            Font = new FontFamily(familyName ?? throw new ArgumentNullException(nameof(familyName)));
        }

        public FontFamily Font { get; }

        public string Name { get; }
    }
}
