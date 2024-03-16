using System.Windows;
using System.Windows.Controls;

namespace Semytskyi2.Tools.Controls
{
    /// <summary>
    /// Interaction logic for TextBlockWithCaption.xaml
    /// </summary>
    public partial class TextBlockWithCaption : UserControl
    {
        public string Caption
        {
            get
            {
                return TbCaption.Text;
            }
            set
            {
                TbCaption.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(TextBlockWithCaption),
            new PropertyMetadata(null)
        );

        public TextBlockWithCaption()
        {
            InitializeComponent();
        }
    }
}
