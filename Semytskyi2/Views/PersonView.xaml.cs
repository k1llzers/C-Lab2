using System.Windows.Controls;
using Semytskyi2.ViewModels;

namespace Semytskyi2.Views
{
    public partial class PersonView : UserControl
    {
        private PersonViewModel _personViewModel = new PersonViewModel();
        
        public PersonView()
        {
            InitializeComponent();
            DataContext = _personViewModel;
        }
    }
}
