using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MessageCodePin.Models;
using MessageCodePin.Views;
using MessageCodePin.ViewModels;
using MessageCodePin.Services;
using System.Windows.Input;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace MessageCodePin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
      
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
  
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
            //var item = args.SelectedItem as Item;
            //if (item == null)
            //    return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        //}

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
            isLoading(false);
            isVisible(false);
        }

        void isVisible(bool value)
        {
            res.IsVisible = value;
            btn_Clear.IsVisible = value;
            btn_Share.IsVisible = value;
        }

        void isLoading(bool value)
        {
            defaultActivityIndicator.IsRunning = value;
            defaultActivityIndicator.IsVisible = value;

        }
                
        private void Button_Clicked(object sender, EventArgs e)
        {
            
            var REQUEST = req.Text.ToString().Trim();

            isLoading(true);

            if (Convert.ToBoolean(isEncrypting.IsToggled))
            {
                res.Text = Security.Encripting(REQUEST);
            }
            else
            {
                res.Text = Security.Decrypting(REQUEST);
            }
            res.IsVisible = res.Text !=""?true:false;
            
            isLoading(false);
            isVisible(true);
        }

        private void IsEncrypting_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.optionLabel.Text = isEncrypting.IsToggled ? "Encrypt" : "Desencrypt";
        }

        private void Button_Clicked_Share(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new ShareMessage
            {
                Title = "Message",
                Text = res.Text
            });
            isLoading(false);
            isVisible(true);
        }

        private void Button_Clicked_Clear(object sender, EventArgs e)
        {
            this.req.Text = string.Empty;
            isVisible(false);
        }

        private async void Button_Clicked_Close(object sender, EventArgs e)
        {
            isVisible(false);
            isLoading(false);
            await DisplayAlert("See you later!", "Come back soon.", "OK");
            System.Environment.Exit(0);
        }
    }
}