using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModel
{
    public class ContactViewModel : BaseViewModel
    {
        #region Propiedades
        public ObservableCollection<Contact> ContactsCollection { get; set; }
        public ObservableCollection<Contact> ContactsFavoriteCollection { get; set; }
        #endregion
        private Contact contact;
        public Contact Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged(); }
        }
        public ICommand cmdContactDetails { get; set; }
        public ICommand cmdContactDetailsDelete { get; set; }
        public ICommand cmdContactDetailsModify { get; set; }
        public ICommand cmdContactDetailsCancel { get; set; }
        public ICommand cmdContactDetailsSaveEdit { get; set; }
        public ICommand cmdContactAdd { get; set; }
        public ICommand cmdContactDetailsAddPhoneNumber { get; set; }
        public ICommand cmdContactDetailsDeletePhoneNumber { get; set; }
        public ICommand cmdContactDetailsFavoriteToogle { get; set; }
        public ICommand cmdContactFavoriteList { get; set; }
        public ContactViewModel()
        {
            ContactsCollection = new ObservableCollection<Contact>();
            ContactsCollection.Add(new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Jesús Iván",
                FLastName = "Godínez",
                MLastName = "Martínez",
                Organization = "@Universidad-de-Colima",
                PhonesNumbers = new ObservableCollection<PhoneNumber>() {
                    new PhoneNumber{ Id = Guid.NewGuid().ToString(), Number = "3121205900" },
                    new PhoneNumber{ Id = Guid.NewGuid().ToString(), Number = "3123308456" }
                },
                Favorite = true
            }); ;
            ContactsCollection.Add(new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cristian Alejandro",
                FLastName = "Godínez",
                MLastName = "Martínez",
                Organization = "@Universidad-de-Colima",
                PhonesNumbers = new ObservableCollection<PhoneNumber>() {
                    new PhoneNumber{ Id = Guid.NewGuid().ToString(), Number = "3121001234" }
                },
                Favorite = false
            });
            ContactsFavoriteCollection = new ObservableCollection<Contact>(ContactsCollection.Where((Contact) => Contact.Favorite.Equals(true)));
            cmdContactDetails = new Command<Contact>(async (details) => await PcmdContactDetails(details));
            cmdContactDetailsDelete = new Command<Contact>(async (details) => await PcmdContactDetailsDelete(details));
            cmdContactDetailsModify = new Command<Contact>(async (details) => await PcmdContactDetailsModify(details));
            cmdContactDetailsCancel = new Command(async () => await PcmdContactDetailsCancel());
            cmdContactDetailsSaveEdit = new Command<Contact>(async (details) => await PcmdContactDetailsSaveEdit(details));
            cmdContactAdd = new Command(async () => await PcmdContactAdd());
            cmdContactDetailsAddPhoneNumber = new Command(async () => await PcmdContactDetailsAddPhoneNumber());
            cmdContactDetailsDeletePhoneNumber = new Command<PhoneNumber>(async (details) => await PcmdContactDetailsDeletePhoneNumber(details));
            cmdContactDetailsFavoriteToogle = new Command<Contact>(async (details) => await PcmdContactDetailsFavoriteToogle(details));
            cmdContactFavoriteList = new Command(async () => await PcmdContactFavoriteList());

            async Task PcmdContactDetails(Models.Contact _Contact)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.ContactDetails(_Contact, this));
            }
            async Task PcmdContactDetailsDelete(Models.Contact _Contact)
            {
                int index = ContactsCollection.IndexOf(_Contact);
                if (index >= 0)
                {
                    ContactsCollection.Remove(_Contact);
                    ContactsFavoriteCollection.Remove(_Contact);
                    OnPropertyChanged();
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            async Task PcmdContactDetailsModify(Models.Contact _Contact)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.ContactMaintenance(Contact, this));
            }
            async Task PcmdContactDetailsCancel()
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            async Task PcmdContactDetailsSaveEdit(Models.Contact _Contact)
            {
                int index = -1;
                Contact tmp = ContactsCollection.FirstOrDefault(item => item.Id == _Contact.Id);
                if (tmp != null)
                {
                    index = ContactsCollection.IndexOf(tmp);
                    ContactsCollection[index] = _Contact;
                }
                else
                {
                    ContactsCollection.Add(_Contact);
                }
                OnPropertyChanged();
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            async Task PcmdContactAdd()
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.ContactMaintenance(this));
            }
            async Task PcmdContactDetailsAddPhoneNumber()
            {
                if (Contact.PhonesNumbers == null)
                    Contact.PhonesNumbers = new ObservableCollection<PhoneNumber>();
                    Contact.PhonesNumbers.Add(new PhoneNumber { Id = Guid.NewGuid().ToString() });
                OnPropertyChanged();
                await Task.Delay(1000);
            }
            async Task PcmdContactDetailsDeletePhoneNumber(Models.PhoneNumber _PhoneNumber)
            {
                Contact.PhonesNumbers?.Remove(_PhoneNumber);
                OnPropertyChanged();
                await Task.Delay(1000);
            }
            async Task PcmdContactDetailsFavoriteToogle(Models.Contact _Contact)
            {
                if (!_Contact.Favorite)
                {
                    ContactsFavoriteCollection.Add(_Contact);
                }
                else
                {
                    ContactsFavoriteCollection.Remove(_Contact);
                }
                _Contact.Favorite = _Contact.Favorite ? false : true;
                OnPropertyChanged();
                await PcmdContactDetailsSaveEdit(_Contact);
            }
            async Task PcmdContactFavoriteList()
            {
                //ContactsFavoriteCollection = new ObservableCollection<Contact>(ContactsCollection.Where((Contact) => Contact.Favorite.Equals(true)));
                OnPropertyChanged();
                await Application.Current.MainPage.Navigation.PushAsync(new View.ContactFavorite(this));
            }
        }
    }
}
