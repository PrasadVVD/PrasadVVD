using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.Common
{
    public class CustomCodeEntry : Entry
    {
        public CustomCodeEntry()
        {
            this.Unfocused += UnFocussed;
        }

        public void UnFocussed(object sender, FocusEventArgs e)
        {
            OnUnFocussed?.Execute(Text);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: "BorderColor",
            returnType: typeof(Color),
            declaringType: typeof(CustomCodeEntry),
            defaultValue: Color.FromHex("#0197FF")
        );

        public ICommand OnUnFocussed
        {
            get => (ICommand)GetValue(OnUnFocussedProperty);
            set => SetValue(OnUnFocussedProperty, value);
        }

        public static BindableProperty OnUnFocussedProperty = BindableProperty.Create(
            propertyName: "OnUnFocussed",
            returnType: typeof(ICommand),
            declaringType: typeof(CustomCodeEntry)
        );
    }
}
