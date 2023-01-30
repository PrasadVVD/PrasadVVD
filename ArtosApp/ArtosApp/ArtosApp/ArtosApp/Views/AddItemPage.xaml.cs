using ArtosApp.Services;
using ArtosApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        private readonly AddItemViewModel _vm;
        public AddItemPage()
        {
            InitializeComponent();
            BindingContext = _vm = new AddItemViewModel(Navigation, new AlertService());
        }
    }
}