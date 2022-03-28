using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        #region Propiedades
        public ObservableCollection<Contact> Contacts { get; set; }
        #endregion
        private Contact contact;
        public Contact Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged(); }
        }
        public ICommand cmdContactDetails { get; set; }
        public ICommand cmdContactDetailsDelete { get; set; }
        public ContactViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            Contacts.Add(new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Jesús Iván",
                FLastName = "Godínez",
                MLastName = "Martínez",
                Organization = "@Universidad-de-Colima",
                PhonesNumbers = new ObservableCollection<PhoneNumber>() {
                    new PhoneNumber{ Id = Guid.NewGuid().ToString(), Number = "3121205900" },
                    new PhoneNumber{ Id = Guid.NewGuid().ToString(), Number = "3123308456"}
                }
            });
            Contacts.Add(new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cristian Alejandro",
                FLastName = "Godínez",
                MLastName = "Martínez",
                Organization = "@Universidad-de-Colima",
                PhonesNumbers = new ObservableCollection<PhoneNumber>() {
                    new PhoneNumber{ Id = Guid.NewGuid().ToString(), Number = "3121001234" }
                }
            });
            cmdContactDetails = new Command<Contact>(async (details) => await PcmdContactDetails(details));
            cmdContactDetailsDelete = new Command<Contact>(async (details) => await PcmdContactDetailsDelete(details));

            async Task PcmdContactDetails(Models.Contact _Contact)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.ContactDetails(_Contact, this));
            }
            async Task PcmdContactDetailsDelete(Models.Contact _Contact)
            {
                int index = Contacts.IndexOf(_Contact);
                if (index >= 0)
                {
                    Contacts.Remove(_Contact);
                    OnPropertyChanged();
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }
    }
}