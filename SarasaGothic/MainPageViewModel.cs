using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SarasaGothic
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly Frame _frame;
        private string _previewText = "“abc” => Il1 — 0O";

        public MainPageViewModel(Frame frame)
        {
            FontPreviews = new[]
            {
                new FontPreviewItem("更纱黑体 SC", "Sarasa Gothic SC"),
                new FontPreviewItem("更纱黑体 UI SC", "Sarasa UI SC"),
                new FontPreviewItem("等距更纱黑体 SC", "Sarasa Mono SC"),
                new FontPreviewItem("等距更纱黑体 Slab SC", "Sarasa Mono Slab SC"),
                new FontPreviewItem("Sarasa Term SC", "Sarasa Term SC"),
                new FontPreviewItem("Sarasa Term Slab SC", "Sarasa Term Slab SC"),
                new FontPreviewItem("Sarasa Fixed SC", "Sarasa Fixed SC"),
                new FontPreviewItem("Sarasa Fixed Slab SC", "Sarasa Fixed Slab SC"),
            };

            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<FontPreviewItem> FontPreviews { get; }

        public string PreviewText
        {
            get => _previewText;
            set
            {
                if (_previewText != value)
                {
                    _previewText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreviewText)));
                }
            }
        }

        public async void ViewInSettings()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:fonts"));
        }

        public void ShowAboutPage()
        {
            _frame.Navigate(typeof(AboutPage));
        }
    }
}
