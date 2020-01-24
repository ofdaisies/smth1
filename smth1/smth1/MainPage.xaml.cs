using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace smth1
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Dictionary<string, int> products_check = new Dictionary<string, int>();
        public MainPage()
        {
            InitializeComponent();
        }
        private void add(string src, int sum)
        {
            var bucket = new Bucket();
            Frame frame = new Frame()
            {
                BackgroundColor = Color.LightPink,
                Padding = 5
            };

            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };

            Image product = new Image()
            {
                Source = bucket.im[src],
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40,
                HeightRequest = 40
            };

            Label name = new Label()
            {
                TextColor = Color.Black,
                Text = src,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };

            Label count = new Label()
            {
                TextColor = Color.Black,
                Text = Convert.ToString(sum),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            Button delete = new Button()
            {
                Text = "x",
                TextColor = Color.Black,
                BackgroundColor = Color.AliceBlue,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 40,
                HeightRequest = 40
            };

            delete.Clicked += (s, w) =>
            {
                products.Children.Remove(frame);
                products_check.Remove(src);
            };
            stack.Children.Add(product);
            stack.Children.Add(name);
            stack.Children.Add(count);
            
            stack.Children.Add(delete);
            frame.Content = stack;
            products.Children.Add(frame);
        }

        private void Add_(object sender, EventArgs e)
        {
            var bucket = new Bucket();
            bucket.Disappearing += (a, b) =>
            {
                if (!bucket.isConfirmed)
                {
                    return;
                }
                if (products_check.ContainsKey(bucket.src))
                {
                    products_check[bucket.src] += Convert.ToInt32(bucket.cnt);
                    products.Children.Clear();
                    foreach (KeyValuePair<string, int> k in products_check)
                    {
                        add(k.Key, k.Value);
                    }
                }
                else
                {
                    products_check.Add(bucket.src, Convert.ToInt32(bucket.cnt));
                    add(bucket.src, Convert.ToInt32(bucket.cnt));
                }
                
            };
            Navigation.PushAsync(bucket);
        }
        private async void order(object sender, EventArgs e)
        {
            if (products_check.Count == 0)
            {
                await DisplayAlert("Oops", "pls, choose smth", "ok.");
            }
            else
            {
                string new_order = null;
                foreach (KeyValuePair<string, int> k in products_check)
                {
                    new_order += $"{k.Key} ({k.Value})\n";
                }

                bool isConfirmed = await DisplayAlert("r u sure?", $"{new_order}", "yep", "nope");
                if (isConfirmed)
                {
                    await DisplayAlert("thanks", "your order will be arrived soon", "ok.");
                    products.Children.Clear();
                    products_check.Clear();
                }
            }
        }
    }
}