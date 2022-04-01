using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contacts.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FLastName { get; set; }
        public string MLastName { get; set; }
        public string Organization { get; set; }
        public ObservableCollection<PhoneNumber> PhonesNumbers { get; set; }
        public bool Favorite { get; set; }
    }

    public class PhoneNumber
    {
        public string Id { get; set; }
        public string Number { get; set; }
    }
}
