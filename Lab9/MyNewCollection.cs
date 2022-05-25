namespace Lab9
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    class MyNewCollection : CustomHashTable
    {
        public string? CollectionName { get; set; }
        public event CollectionHandler? CollectionCountChanged;
        public event CollectionHandler? CollectionReferenceChanged;
        CollectionHandler CollectionHandler { get; set; }

        public override Transport this[string key]
        {
            get
            {
                int index = keys.IndexOf(key);
                if (index == -1)
                    throw new IndexOutOfRangeException("Нет такого ключа");

                return transports[keys.IndexOf(key)];
            }
            set
            {
                int index = keys.IndexOf(key);
                if (index == -1)
                    throw new IndexOutOfRangeException("Нет такого ключа");

                transports[keys.IndexOf(key)] = value;
                if (CollectionReferenceChanged != null)
                    CollectionReferenceChanged(
                        this,
                        new CollectionHandlerEventArgs
                        {
                            CollectionName = this.CollectionName,
                            NameOfChange = "Элемент был изменен",
                            ChangedObject = value
                        });
            }
        }

        public void AddDefaults()
        {
            Add("Subaru", new Car()
            {
                Model = "Subaru",
                LicenseNum = "С065МК78",
                LoadCapacity = 700,
                MaxSpeed = 255
            });
            Add("Subway", new Car()
            {
                Model = "Subway",
                LicenseNum = "К891АК159",
                LoadCapacity = 800,
                MaxSpeed = 130
            });
            Add("Suzuki", new Motorbike()
            {
                Model = "Suzuki",
                LicenseNum = "513ВА50",
                LoadCapacity = 250,
                MaxSpeed = 110,
                IsWithSidecar = true
            });
            Add("Yamaha", new Motorbike()
            {
                Model = "Yamaha",
                LicenseNum = "222АА11",
                LoadCapacity = 280,
                MaxSpeed = 120,
                IsWithSidecar = false
            });
            Add("IVECO", new Truck()
            {
                Model = "IVECO",
                LicenseNum = "A123BC48",
                LoadCapacity = 2500,
                MaxSpeed = 100,
                IsWithTrailer = false
            });
            Add("BMW", new Truck()
            {
                Model = "BMW",
                LicenseNum = "B584EA56",
                LoadCapacity = 2700,
                MaxSpeed = 95,
                IsWithTrailer = true
            });
        }

        public override bool Remove(string key)
        {
            int index = base.keys.IndexOf(key);
            if (index > -1)
                if (CollectionCountChanged != null)
                {
                    CollectionCountChanged(this,
                    new CollectionHandlerEventArgs
                    {
                        CollectionName = this.CollectionName,
                        NameOfChange = "Удален элемент",
                        ChangedObject = base.transports[index]
                    });
                }
            return base.Remove(key);
        }

        public override void Add(string key, Transport transport)
        {
            keys.Add(key);
            transports.Add(transport);
            if (CollectionCountChanged != null)
            {
                CollectionCountChanged(this,
                new CollectionHandlerEventArgs
                {
                    CollectionName = this.CollectionName,
                    NameOfChange = "Добавлен объект",
                    ChangedObject = transport
                });
            }
        }

    }

    public class CollectionHandlerEventArgs : EventArgs
    {
        public string? CollectionName { get; set; }
        public string? NameOfChange { get; set; }
        public object? ChangedObject { get; set; }

        public override string ToString()
        {
            return CollectionName + " " + NameOfChange + " " + ChangedObject;
        }
    }
}
