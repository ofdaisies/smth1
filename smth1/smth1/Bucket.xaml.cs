using System;
using System.Collections.Generic;


using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smth1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bucket : ContentPage
    {
        public Dictionary<string, string> im = new Dictionary<string, string>()
        {
            { "voda", "voda.jpg" },
            { "coca", "coca.jpg" },
            { "juice", "juice.png" }
        };
        public enum D : int
        {
            [Description("voda")]
            One = 1,
            [Description("cola")]
            Two = 2,
            [Description("juice 01")]
            Three = 3
        }
       public D[] result = (D[])Enum.GetValues(typeof(D));
        public Bucket()
        {
            InitializeComponent();
            for (int i = 1; i <= result.Length; i++)
            {
                string s = GetEnumDescription((D)i);
                p.Items.Add(s);
            }
        }
        public string src = null;
        public string cnt = null;
        public bool isConfirmed = false;
        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
        private void val_change(object sender, ValueChangedEventArgs e)
        {
            if (e.NewValue >= 0 && product.Source != null)
            {
                double value = e.NewValue;
                count.Text = value.ToString();
                cnt = count.Text;
            }
        }
        private void name_img(object sender, EventArgs e)
        {
            src = $"{(sender as Picker)?.SelectedItem}";
            product.Source = im[src];
            count.Text = "0";
            cnt = null;
            _stepper.Value = 0; 

        }
        private void confirm(object sender, EventArgs e)
        {
            if (cnt != null && cnt != "0")
            {
                isConfirmed = true;
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Oops", "pls, choose smth", "ok.");
            }
        }
    }
}