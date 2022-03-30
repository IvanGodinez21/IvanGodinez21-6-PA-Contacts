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
        public ICommand cmdContactDetailsModify { get; set; }
        public ICommand cmdContactDetailsCancel { get; set; }
        public ICommand cmdContactDetailsSaveEdit { get; set; }
        public ICommand cmdContactDetailsAdd { get; set; }
        public ICommand cmdContactDetailsAddPhoneNumber { get; set; }
        public ICommand cmdContactDetailsDeletePhoneNumber { get; set; }
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
            cmdContactDetailsModify = new Command<Contact>(async (details) => await PcmdContactDetailsModify(details));
            cmdContactDetailsCancel = new Command(async () => await PcmdContactDetailsCancel());
            cmdContactDetailsSaveEdit = new Command<Contact>(async (details) => await PcmdContactDetailsSaveEdit(details));
            cmdContactDetailsAdd = new Command(async () => await PcmdContactDetailsAdd());
            cmdContactDetailsAddPhoneNumber = new Command(async () => await PcmdContactDetailsAddPhoneNumber());
            cmdContactDetailsDeletePhoneNumber = new Command<PhoneNumber>(async (details) => await PcmdContactDetailsDeletePhoneNumber(details));

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
                Contact tmp = Contacts.FirstOrDefault(item => item.Id == _Contact.Id);
                //foreach(Contact contact in Contacts)
                //{
                //    if (contact.Id == _Contact.Id)
                //    {
                //        index++;
                //    }
                //}
                if (tmp != null)
                {
                    index = Contacts.IndexOf(tmp);
                    Contacts[index] = _Contact;

                }
                else
                {
                    Contacts.Add(_Contact);
                }
                OnPropertyChanged();
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            async Task PcmdContactDetailsAdd()
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.ContactMaintenance(this));
            }
            async Task PcmdContactDetailsAddPhoneNumber()
            {
                if (Contact.PhonesNumbers == null)
                    Contact.PhonesNumbers = new ObservableCollection<PhoneNumber>();
                    Contact.PhonesNumbers.Add(new PhoneNumber { Id = Guid.NewGuid().ToString() });
                await Task.Delay(1000);
            }
            async Task PcmdContactDetailsDeletePhoneNumber(Models.PhoneNumber _PhoneNumber)
            {
                Contact.PhonesNumbers?.Remove(_PhoneNumber);
                await Task.Delay(1000);
            }
        }
    }
}