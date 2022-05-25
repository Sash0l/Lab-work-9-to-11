using System.Text;

namespace Lab9;

abstract class Transport
{
    public virtual int LoadCapacity { get; set; } // Грузоподъемость
    
    public override string ToString() // Вывести информацию о транспортном средстве
    {
        return LoadCapacity.ToString();
    }
    
    public int CompareTo(object Obj)
    {
        Transport temp = (Transport)Obj;
        if (this.LoadCapacity > temp.LoadCapacity) return 1;
        if (this.LoadCapacity < temp.LoadCapacity) return -1;
        return 0;
    }
}

class Car : Transport
{
    public string? Model { get; set; }
    public string? LicenseNum { get; set; }
    public int MaxSpeed { get; set; }
    public override string ToString()
    {
        return "Load capacity = " + LoadCapacity + ", Model = " + Model + ", Licence = " + LicenseNum + ", Max speed = " + MaxSpeed; 
    }
}

class Motorbike : Transport
{
    public string? Model { get; set; }
    public string? LicenseNum { get; set; }
    public int MaxSpeed { get; set; }
    public bool IsWithSidecar { get; set; }
    public override int LoadCapacity
    {
        get
        {
            if (IsWithSidecar == true) // Проверяем наличие коляски
                return loadCapacity;

            return 0;
        }
        set => loadCapacity = value;
    }
    private int loadCapacity;

    public override string ToString()
    {
        return "Load capacity = " + LoadCapacity + ", Model = " + Model + ", Licence = " + LicenseNum + ", Max speed = " + MaxSpeed + ", Is with sidecar - " + IsWithSidecar;
    }
}

class Truck : Transport
{
    public string? Model { get; set; }
    public string? LicenseNum { get; set; }
    public int MaxSpeed { get; set; }
    public bool IsWithTrailer { get; set; }
    public override int LoadCapacity
    {
        get 
        {
            if (IsWithTrailer == true) // Проверяем наличие прицепа
                return loadCapacity * 2;
            
            return loadCapacity;
        }
        set => loadCapacity = value;
    }
    private int loadCapacity;
    public override string ToString()
    {
        return "Load capacity = " + LoadCapacity + ", Model = " + Model + ", Licence = " + LicenseNum + ", Max speed = " + MaxSpeed + ", Is with trailer - " + IsWithTrailer;
    }
}

static class Program
{
    static void PrintTransport(List<Transport> transports)
    {
        foreach (var item in transports)
        {
            if (item is Car)
                Console.WriteLine(((Car)item).ToString());
            if (item is Motorbike)
                Console.WriteLine(((Motorbike)item).ToString());
            if (item is Truck)
                Console.WriteLine(((Truck)item).ToString());
        }
    }

    static void PrintTransport(CustomHashTable transports)
    {
        foreach (var item in transports)
        {
            if (item is Car)
                Console.WriteLine(((Car)item).ToString());
            if (item is Motorbike)
                Console.WriteLine(((Motorbike)item).ToString());
            if (item is Truck)
                Console.WriteLine(((Truck)item).ToString());
        }
    }

    static void AddTransports(List<Transport> transports)
    {
        transports.Add(new Car()
        {
            Model = "Subaru",
            LicenseNum = "С065МК78",
            LoadCapacity = 700,
            MaxSpeed = 255
        });
        transports.Add(new Car()
        {
            Model = "Subway",
            LicenseNum = "К891АК159",
            LoadCapacity = 800,
            MaxSpeed = 130
        });
        transports.Add(new Motorbike()
        {
            Model = "Suzuki",
            LicenseNum = "513ВА50",
            LoadCapacity = 250,
            MaxSpeed = 110,
            IsWithSidecar = true
        });
        transports.Add(new Motorbike()
        {
            Model = "Yamaha",
            LicenseNum = "222АА11",
            LoadCapacity = 280,
            MaxSpeed = 120,
            IsWithSidecar = false
        });
        transports.Add(new Truck()
        {
            Model = "IVECO",
            LicenseNum = "A123BC48",
            LoadCapacity = 2500,
            MaxSpeed = 100,
            IsWithTrailer = false
        });
        transports.Add(new Truck()
        {
            Model = "BMW",
            LicenseNum = "B584EA56",
            LoadCapacity = 2700,
            MaxSpeed = 95,
            IsWithTrailer = true
        });
    }

    static List<Transport> FilterByWeightDialog(List<Transport> transport)
    {
        bool isNumEntered = false;
        int loadCap = 0;
        while (!isNumEntered)
        {
            try
            {
                Console.Write("Введите минимальную грузоподъемность для поиска: ");
                loadCap = Convert.ToInt32(Console.ReadLine());
                isNumEntered = true;
            }
            catch
            {
                Console.WriteLine("Проверьте введенные данные!");
            }
        }

        List<Transport> transportFiltered = transport.FindAll(
        delegate (Transport t)
        {
            return t.LoadCapacity >= loadCap;
        });
        return transportFiltered;
    }

    static void Menu(CustomHashTable mockHashTable)
    {
        bool isInMenu = true;
        while (isInMenu)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить объект в коллекцию");
            Console.WriteLine("2. Удалить объект из коллекции");
            Console.WriteLine("3. Покинуть меню");

            ConsoleKeyInfo keyboardKey = Console.ReadKey();
            Console.WriteLine();
            char keyChar = keyboardKey.KeyChar;
            switch (keyChar)
            {
                case '1':
                    MenuAddElem(mockHashTable);
                    break;
                case '2':
                    MenuRemoveElem(mockHashTable);
                    break;
                case '3':
                    isInMenu = false;
                    break;
                default:
                    Console.WriteLine("Такого пункта меню нет!");
                    break;
            }
        }
    }

    static void MenuRemoveElem(CustomHashTable mockHashTable)
    {
        bool isInMenu = true;
        while (isInMenu)
        {
            Console.WriteLine("Удаление элемента из коллекции по ключу");
            Console.WriteLine("1. Удалить один элемент");
            Console.WriteLine("2. Удалить все элементы");
            Console.WriteLine("3. Покинуть меню удаления элементов");
            ConsoleKeyInfo keyboardKey = Console.ReadKey();
            Console.WriteLine();
            char keyChar = keyboardKey.KeyChar;

            switch (keyChar)
            {
                case '1':
                    Console.WriteLine("Доступные ключи для удаления: ");
                    foreach (Transport entry in mockHashTable)
                    {
                        Console.Write(mockHashTable.Find(entry) + ", ");
                    }
                    Console.WriteLine();
                    Console.Write("Ключ: ");
                    string key = Console.ReadLine();
                    try
                    {
                        mockHashTable.Remove(key);
                        Console.WriteLine("Удалось!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case '2':
                    mockHashTable.Clear();
                    Console.WriteLine("Элементы были удалены");
                    break;
                case '3':
                    isInMenu = false;
                    break;
                default:
                    break;
            }
        }
    }

    static void MenuAddElem(CustomHashTable mockHashTable)
    {
        bool isInMenu = true;
        while (isInMenu)
        {
            Console.WriteLine("Добавление элемента в коллекцию");
            Console.WriteLine("Выберите тип транспортного средства:");
            Console.WriteLine("1. Машина");
            Console.WriteLine("2. Мотоцикл");
            Console.WriteLine("3. Грузовик");
            Console.WriteLine("4. Покинуть меню добавления ТС");
            ConsoleKeyInfo keyboardKey = Console.ReadKey();
            Console.WriteLine();
            char keyChar = keyboardKey.KeyChar;

            switch (keyChar)
            {
                case '1':
                    Console.Write("Ключ: ");
                    string dictKey = Console.ReadLine();

                    Console.Write("Грузоподъемность: ");
                    int loadCap = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Модель: ");
                    string model = Console.ReadLine();

                    Console.Write("Номерной знак: ");
                    string licenceNum = Console.ReadLine();

                    Console.Write("Максимальная скорость: ");
                    int maxSpeed = Convert.ToInt32(Console.ReadLine());

                    mockHashTable.Add(dictKey, new Car
                    {
                        LoadCapacity = loadCap,
                        Model = model,
                        LicenseNum = licenceNum,
                        MaxSpeed = maxSpeed
                    });
                    break;
                case '2':
                    Console.Write("Ключ: ");
                    dictKey = Console.ReadLine();

                    Console.Write("Грузоподъемность: ");
                    loadCap = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Модель: ");
                    model = Console.ReadLine();

                    Console.Write("Номерной знак: ");
                    licenceNum = Console.ReadLine();

                    Console.Write("Максимальная скорость: ");
                    maxSpeed = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Есть коляска? (true/false): ");
                    bool isWithSidecar = Convert.ToBoolean(Console.ReadLine());

                    mockHashTable.Add(dictKey, new Motorbike
                    {
                        LoadCapacity = loadCap,
                        Model = model,
                        LicenseNum = licenceNum,
                        MaxSpeed = maxSpeed,
                        IsWithSidecar = isWithSidecar
                    });
                    break;
                case '3':
                    Console.Write("Ключ: ");
                    dictKey = Console.ReadLine();

                    Console.Write("Грузоподъемность: ");
                    loadCap = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Модель: ");
                    model = Console.ReadLine();

                    Console.Write("Номерной знак: ");
                    licenceNum = Console.ReadLine();

                    Console.Write("Максимальная скорость: ");
                    maxSpeed = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Есть прицеп? (true/false): ");
                    bool isWithTrailer = Convert.ToBoolean(Console.ReadLine());

                    mockHashTable.Add(dictKey, new Truck
                    {
                        LoadCapacity = loadCap,
                        Model = model,
                        LicenseNum = licenceNum,
                        MaxSpeed = maxSpeed,
                        IsWithTrailer = isWithTrailer
                    });
                    break;
                case '4':
                    isInMenu = false;
                    break;
                default:
                    Console.WriteLine("Такого пункта меню нет!");
                    break;
            }
        }
    }

    static void CloneFromListToHashTable(List<Transport> transport, CustomHashTable mockHashTable)
    {
        foreach (var t in transport) // Внесение транспорта из списка в хэш-таблицу
        {
            if (t is Car)
                mockHashTable.Add(((Car)t).Model, t); // ключ - модель транспорта, значение - сам экземпляр транспорта
            if (t is Motorbike)
                mockHashTable.Add(((Motorbike)t).Model, t);
            if (t is Truck)
                mockHashTable.Add(((Truck)t).Model, t);
        }
    }

    static void Main()
    {
        Console.InputEncoding = System.Text.Encoding.UTF8; ;

        List<Transport> transport = new List<Transport>();
        AddTransports(transport); // Заполняет список предопределенными примерами

        transport = transport.OrderBy(o => o.LoadCapacity).ToList(); // Сортирует список по грузоподъемности
        PrintTransport(transport);

        List<Transport> transportFiltered = FilterByWeightDialog(transport); // Инициализирует диалог ввода минимальной грузоподъемности и фильтрует
        PrintTransport(transportFiltered);

        CustomHashTable mockHashTable = new CustomHashTable();
        CloneFromListToHashTable(transport, mockHashTable); // Переносим данные из списка в хэш-таблицу

        Menu(mockHashTable); // Инициализирует меню с опциями добавления и удаления элементов из хэш-таблицы

        
        PrintTransport(mockHashTable);

        int countCars = 0;
        int countMotorbikesWithSidecar = 0;

        Console.WriteLine("Запросы:");
        foreach(Transport t in mockHashTable) // Три запроса: Вывести инф. о грузовиках, посчитать сколько машин и посчитать мотоциклы с коляской
        {
            if(t is Car)
                countCars++;
            if(t is Motorbike && ((Motorbike)t).IsWithSidecar)
                countMotorbikesWithSidecar++;
            if(t is Truck)
                Console.WriteLine(((Truck)t).ToString());
        }
        Console.WriteLine("Количество машин: " + countCars);
        Console.WriteLine("Количество мотоциклов с колясками: " + countMotorbikesWithSidecar);

        Console.WriteLine("Клонирование:");
        CustomHashTable clonedTransport = mockHashTable.Clone();
        PrintTransport(clonedTransport);

        
        Console.WriteLine("Поиск: ");
        Car toFind = (Car)mockHashTable["Subaru"];
        Console.WriteLine(((Car)mockHashTable[mockHashTable.Find(toFind)]).ToString());



        Console.WriteLine("Тест MyNewCollection:");
        MyNewCollection collectionOne = new MyNewCollection { CollectionName = "Первая коллекция" };
        MyNewCollection collectionTwo = new MyNewCollection { CollectionName = "Вторая коллекция" };
        Journal journalOne = new Journal();
        Journal journalTwo = new Journal();

        collectionOne.CollectionCountChanged += journalOne.OnColectionEvent;
        collectionOne.CollectionReferenceChanged += journalOne.OnColectionEvent;

        collectionOne.CollectionReferenceChanged += journalTwo.OnColectionEvent;
        collectionTwo.CollectionReferenceChanged += journalTwo.OnColectionEvent;

        collectionOne.AddDefaults();
        collectionTwo.AddDefaults();

        collectionOne.Remove("Subaru");
        collectionTwo.Remove("Yamaha");
        collectionOne["Subway"] = new Car 
        { 
            Model = "Subway 2",
            LicenseNum = "А290ВУ147",
            LoadCapacity = 980,
            MaxSpeed = 198
        };
        collectionOne.Add("Subaru", new Car
        {
            Model = "Subaru 2",
            LicenseNum = "В216ВО148",
            LoadCapacity = 750,
            MaxSpeed = 240
        });
        collectionTwo["BMW"] = new Truck
        {
            Model = "BMW 2",
            LicenseNum = "Х548ЕУ58",
            LoadCapacity = 500,
            MaxSpeed = 249
        };

        Console.WriteLine("Journal 1:");
        Console.WriteLine(journalOne.ToString());
        Console.WriteLine("Journal 2:");
        Console.WriteLine(journalTwo.ToString());
    }
}