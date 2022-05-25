using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class CustomHashTable : IEnumerable
    {
        protected List<string> keys = new List<string>();
        protected List<Transport> transports = new List<Transport>();
        
        //public IEnumerable<string> Keys { get { return keys; } } 
        //public IEnumerable<Transport> Transports { get { return transports; } }

        public int Count { get { return keys.Count; } }

        public CustomHashTable()
        {

        }

        public CustomHashTable(int capacity)
        {
            for(int i = 0; i < capacity; i++)
            {
                keys.Add(String.Empty);
                transports.Add(null);
            }    
        }

        public CustomHashTable(CustomHashTable h)
        {
            foreach(var entry in h)
            {
                transports.Add((Transport)entry);

                if(entry is Car)
                    keys.Add(((Car)entry).Model.ToString());
                if(entry is Motorbike)
                    keys.Add(((Motorbike)entry).Model.ToString());
                if (entry is Truck)
                    keys.Add(((Truck)entry).Model.ToString());
            }
        }


        public virtual Transport this[string key]
        { 
            get 
            {
                int index = keys.IndexOf(key);
                if (index == -1)
                    throw new IndexOutOfRangeException("Нет такого ключа");

                return transports[keys.IndexOf(key)]; 
            }
            set { transports[keys.IndexOf(key)] = value; }
        }

        public virtual void Add(string key, Transport transport)
        {
            keys.Add(key);
            transports.Add(transport);
        }

        public void AddRange(CustomHashTable items)
        {
            keys.AddRange(items.keys);
            transports.AddRange(items.transports);
        }

        public virtual bool Remove(string key)
        {
            transports.Remove(this[key]);
            return keys.Remove(key);
        }

        public void RemoveAll(CustomHashTable items)
        {
            foreach (Transport item in items)
            {
                Remove(Find(item));
            }
        }

        public string Find(Transport item)
        {
            if (item != null && transports.IndexOf(item) > -1)
                return keys[transports.IndexOf(item)];
            throw new NullReferenceException("Не удалось найти");
        }

        public CustomHashTable Clone()
        {
            var temp = new CustomHashTable() 
            { 
                keys = this.keys,
                transports = this.transports
            };
            return temp;
        }

        public void Copy(ref CustomHashTable item)
        {
            item = this;
        }

        public void Clear()
        {
            keys.Clear();
            transports.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return transports[i];
        }
    }

    class CustomHashTableHandler : CustomHashTable
    {

    }
}
