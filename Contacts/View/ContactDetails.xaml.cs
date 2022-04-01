using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetails : ContentPage
    {
        public ContactDetails(Models.Contact contact, ContactViewModel vm)
        {
            InitializeComponent();
            vm.Contact = contact;
            this.BindingContext = vm;
            if (contact.Favorite)
            {
                btnFavorite.BackgroundColor = Color.Gray;
                btnFavorite.Text = "Unfavorite";
                OnPropertyChanged();
            } 
            else
            {
                btnFavorite.BackgroundColor = Color.Gold;
                btnFavorite.Text = "Favorite";
                OnPropertyChanged();
            }
        }
    }
}