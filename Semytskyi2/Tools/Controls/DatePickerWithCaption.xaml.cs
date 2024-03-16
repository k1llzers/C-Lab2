using System.Windows;
using System.Windows.Controls;

namespace Semytskyi2.Tools.Controls
{
    public partial class DatePickerWithCaption : UserControl
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
    
        public DateTime SelectedDate
        {
            get
            {
                return (DateTime)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register
        (
            "SelectedDate",
            typeof(DateTime),
            typeof(DatePickerWithCaption),
            new PropertyMetadata(null)
        );
    
        public DatePickerWithCaption()
        {
            InitializeComponent();
        }
    }   
}