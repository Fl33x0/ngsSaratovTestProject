using System;

namespace ContactList.Entities
{
    public class Contact
    {
        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public int ID { get; private set; }

        public Contact(string name, string phoneNumber, int id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            ID = id;
        }
    }
}
