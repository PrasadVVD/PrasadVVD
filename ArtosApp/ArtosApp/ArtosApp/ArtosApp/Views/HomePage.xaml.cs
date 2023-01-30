using ArtosApp.Models;
using ArtosApp.Services;
using ArtosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel _vm;
        public HomePage(DepoVM depo)
        {
            InitializeComponent();
            BindingContext = _vm = new HomeViewModel(Navigation, new AlertService(), depo);
        }
    }
}