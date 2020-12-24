using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace MessageCodePin.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://www.linkedin.com/in/manuel-emilio-mendez/"/*"https://xamarin.com/platform"*/)));
        }

        public ICommand OpenWebCommand { get; }
    }
}