using ArtosApp.Services;
using ArtosApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDepoPage : ContentPage
    {
        private readonly AddDepoViewModel _vm;
        public AddDepoPage()
        {
            InitializeComponent();
            BindingContext = _vm = new AddDepoViewModel(Navigation, new AlertService());
            Title = "Add new Depo";
            BtnSave.Text = "Save";
            _vm.IsEditMode = false;
        }
    }
}