using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPersonApplication.Models
{
    class ListOfPeople
    {
        private List<Person> ListofPeople = new List<Person>();

        public void AddPerson(Person p)
        {
            ListofPeople.Add(p);
        }

        public void RemovePerson(int index)
        {
            ListofPeople.RemoveAt(index);
        }

        public void InsertPerson(Person p, int index)
        {
            ListofPeople.Insert(index, p);
        }

        public int LookUpPersonIndex(Person p)
        {
            return ListofPeople.IndexOf(p);
        }

        public Person LookUpPerson(int index)
        {
            return ListofPeople[index];
        }
    }
}
