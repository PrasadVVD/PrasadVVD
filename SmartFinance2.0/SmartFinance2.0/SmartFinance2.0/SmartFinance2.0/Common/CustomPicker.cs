using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.Common
{
    public class CustomPicker : StackLayout, INotifyPropertyChanged
    {

        #region "Private Variables"
        private readonly CustomCodeEntry _entryPicker;
        private readonly Picker _picker;
        //private readonly List<string> _salutationSelectOptions;


        #endregion

        #region "Private Events"

        private void OnFocus(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                _entryPicker.Unfocus();
                _picker.Focus();
            });

        }

        #endregion

        public CustomPicker()
        {
            _entryPicker = new CustomCodeEntry();
            _entryPicker.Focused += OnFocus;


            //_entryPicker.SetBinding(Entry.StyleProperty, "EntryStyleProperty");
            _picker = new Picker
            {
                IsVisible = false
            };

            _picker.SelectedIndexChanged += (o, se) =>
            {
                if (_picker.SelectedIndex != -1)
                {
                    SelectedIndex = _picker.SelectedIndex;
                    if (FilterOptions == "Child")
                    {
                        _entryPicker.SetBinding(Entry.TextProperty, new Binding("SelectedItem.Key", source: _picker));
                        PickerIndexChanged?.Execute(_entryPicker.Text);
                    }
                    else if (FilterOptions == "Type")
                    {
                        PickerIndexChanged?.Execute(0);
                    }
                    else
                    {
                        _entryPicker.Text = (string)_picker.ItemsSource[_picker.SelectedIndex];
                        PickerIndexChanged?.Execute(_entryPicker.Text);
                    }
                    Text = _entryPicker.Text;
                }
            };
            Children.Add(_entryPicker);
            Children.Add(_picker);
        }

        public Style EntryStyle
        {
            get { return (Style)GetValue(EntryStyleProperty); }
            set { SetValue(EntryStyleProperty, value); }
        }

        public static readonly BindableProperty EntryStyleProperty = BindableProperty.Create(
            propertyName: "EntryStyle",
            returnType: typeof(Style),
            declaringType: typeof(CustomPicker),
            defaultValue: default(Style)
        );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
         propertyName: "Text",
         returnType: typeof(string),
         declaringType: typeof(CustomPicker),
         defaultValue: default(string),
        defaultBindingMode: BindingMode.TwoWay
         );
        //public new bool IsVisible
        //{
        //    get { return (bool)GetValue(IsVisibleProperty); }
        //    set { SetValue(IsVisibleProperty, value); }
        //}

        //public static readonly new BindableProperty IsVisibleProperty = BindableProperty.Create(
        //    propertyName: "IsVisible",
        //    returnType: typeof(bool),
        //    declaringType: typeof(CustomPicker),
        //    defaultValue: default(bool)
        //    );

        public string FilterOptions
        {
            get { return (string)GetValue(FilterOptionsProperty); }
            set { SetValue(FilterOptionsProperty, value); }
        }
        public static readonly BindableProperty FilterOptionsProperty = BindableProperty.Create(
            propertyName: "FilterOptions",
            returnType: typeof(string),
            declaringType: typeof(CustomPicker),
            defaultValue: default(string)
        );

        public List<KeyValuePair<string, Guid>> ChildOptions
        {
            get { return (List<KeyValuePair<string, Guid>>)GetValue(ChildOptionsProperty); }
            set { SetValue(ChildOptionsProperty, value); }
        }

        public static readonly BindableProperty ChildOptionsProperty = BindableProperty.Create(
            propertyName: "ChildOptions",
            returnType: typeof(List<KeyValuePair<string, Guid>>),
            declaringType: typeof(CustomPicker),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay
        );

        public BindingBase ItemDisplayBinding
        {
            get { return (BindingBase)GetValue(ItemDisplayBindingPeoProperty); }
            set { SetValue(ItemDisplayBindingPeoProperty, value); }
        }

        public static readonly BindableProperty ItemDisplayBindingPeoProperty = BindableProperty.Create(
            propertyName: "ItemDisplayBinding",
            returnType: typeof(BindingBase),
            declaringType: typeof(CustomPicker),
            defaultValue: default(BindingBase),
            defaultBindingMode: BindingMode.TwoWay
        );
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
         propertyName: "SelectedIndex",
         returnType: typeof(int),
         declaringType: typeof(CustomPicker),
         defaultValue: default(int),
         defaultBindingMode: BindingMode.TwoWay
         );

        public ICommand PickerIndexChanged
        {
            get { return (ICommand)GetValue(PickerIndexChangedProperty); }
            set { SetValue(PickerIndexChangedProperty, value); }
        }

        public static readonly BindableProperty PickerIndexChangedProperty = BindableProperty.Create(
            propertyName: "PickerIndexChanged",
            returnType: typeof(ICommand),
            declaringType: typeof(CustomPicker),
            defaultValue: default(ICommand)
        );
        public ObservableCollection<string> SelectOptions
        {
            get { return (ObservableCollection<string>)GetValue(SelectOptionsProperty); }
            set { SetValue(SelectOptionsProperty, value); }
        }

        public static readonly BindableProperty SelectOptionsProperty = BindableProperty.Create(
            propertyName: "SelectOptions",
            returnType: typeof(ObservableCollection<string>),
            declaringType: typeof(CustomPicker),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay
        );

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            propertyName: "BorderColor",
            returnType: typeof(Color),
            declaringType: typeof(CustomPicker),
            defaultValue: default(Color)
        );

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: "TextColor",
            returnType: typeof(Color),
            declaringType: typeof(CustomPicker),
            defaultValue: default(Color)
        );

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            propertyName: "PlaceholderColor",
            returnType: typeof(Color),
            declaringType: typeof(CustomPicker),
            defaultValue: default(Color)
        );

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: "FontSize",
            returnType: typeof(double),
            declaringType: typeof(CustomPicker),
            defaultValue: default(double)
        );

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: "Placeholder",
            returnType: typeof(string),
            declaringType: typeof(CustomPicker),
            defaultValue: default(string)
        );

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(SelectOptions))
            {
                _picker.ItemsSource = SelectOptions;
            }
            if (propertyName == nameof(Text))
            {
                _entryPicker.Text = Text;
            }
            if (propertyName == nameof(BorderColor))
            {
                _entryPicker.BorderColor = BorderColor;
            }
            if (propertyName == nameof(TextColor))
            {
                _entryPicker.TextColor = TextColor;
            }
            if (propertyName == nameof(FontSize))
            {
                _entryPicker.FontSize = FontSize;
            }
            if (propertyName == nameof(Placeholder))
            {
                _entryPicker.Placeholder = Placeholder;
            }
            if (propertyName == nameof(PlaceholderColor))
            {
                _entryPicker.PlaceholderColor = PlaceholderColor;
            }
            if (propertyName == nameof(ChildOptions))
            {
                _picker.ItemsSource = ChildOptions;
            }
            if (propertyName == nameof(ItemDisplayBinding))
            {
                _picker.ItemDisplayBinding = new Binding("Key");
            }
        }
    }
}
