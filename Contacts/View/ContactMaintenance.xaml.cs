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
    public partial class ContactMaintenance : ContentPage
    {
        public ContactMaintenance(ContactViewModel vm)
        {
            // Creating new contact
            InitializeComponent();
            vm.Contact = new Models.Contact() { Id = Guid.NewGuid().ToString() };
            vm.Contact.PhonesNumbers = new System.Collections.ObjectModel.ObservableCollection<Models.PhoneNumber>();
            BindingContext = vm;
            Title = "New contact";
        }
        public ContactMaintenance(Models.Contact contact, ContactViewModel vm)
        {
            // Edit a contact
            InitializeComponent();
            vm.Contact = new Models.Contact();
            vm.Contact = contact;
            BindingContext = vm;
            Title = "Edit contact";
        }
    }
}