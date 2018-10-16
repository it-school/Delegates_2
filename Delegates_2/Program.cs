using System;
using System.Collections;
namespace Delegates_2
{
    class Human
    {
        // Делегат на функцию, которая ничего не возвращает и принимает объект типа h
        public delegate void HumanDelegate(Human h);
        // Перечисление
        public enum Sex { Male, Female };
        private Sex p;
        private string name;
        private string surname;
        private int age;
        // конструктор
        public Human()
        {
            name = surname = "Нет Данных";
            age = 0;
            p = Sex.Male;
        }
        // конструктор с параметрами
        public Human(string name, string surname, int age, Sex p)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.p = p;
        }
        // Задание имени, возврат его
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        // Задание фамилии, возврат её
        public string Surname
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        // Задание возраста, возврат его
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
        // Задание пола,возврат его
        public Sex RealSex
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
            }
        }

    }
    //Класс, содержащий людей
    class Firm
    {
        ArrayList people = new ArrayList();
        public Firm()
        {
            // Добавляем в список 3 - х людей
            people.Add(new Human());
            people.Add(new Human("Вася", "Петров", 80, Human.Sex.Male));
            people.Add(new Human("Катерина", "Сидорова", 25, Human.Sex.Female));
        }
        // Метод, принимающий делегат, четко указывается  название класса, где содержится // делегат, а также название делегата
        public void AnalyzePeople(Human.HumanDelegate ptr)
        {
            Console.WriteLine("Будем выполнять действия над человеком !!!");
            // Вызываются методы, на которые указывает делегат
            foreach (Human obj in people)
                ptr(obj);
        }
    }

    class Sample
    {
        // Проверка пола
        static void AnalyzeSex(Human h)
        {
            if (h.RealSex == Human.Sex.Male)
            {
                Console.WriteLine("Мужчина");
            }
            else
            {
                Console.WriteLine("Женщина");
            }
        }
        // Проверка по возрасту
        static void AnalyzeAge(Human h)
        {
            if (h.Age > 60)
            {
                Console.WriteLine("Больше 60 лет");
            }
            else
            {
                Console.WriteLine("Меньше или равно 60 лет");
            }
        }

        static void Main()
        {
            Console.WriteLine("Пример работы Делегата");
            Firm firm = new Firm(); // В этой строке происходит использование делегатов. Создаётся объект // делегата используя ключевое слово new.
            
            firm.AnalyzePeople(new Human.HumanDelegate(AnalyzeSex)); // Сейчас делегат указывает на метод AnalyzeSex
            firm.AnalyzePeople(new Human.HumanDelegate(AnalyzeAge)); // Сейчас делегат указывает на метод AnalyzeAge
            Console.Read();

            // -----------------
            Console.WriteLine("Пример работы Делегата");
            // Создание делегатов
            Human.HumanDelegate sex = new Human.HumanDelegate(AnalyzeSex);
            Human.HumanDelegate age = new Human.HumanDelegate(AnalyzeAge);

            // Многоадресный Делегат (формируем его через +)
            // Произойдет вызов методов AnalyzeSex и AnalyzeAge
            firm.AnalyzePeople(sex + age);
            Console.WriteLine("\n\n");
            // Многоадресный Делегат (формируем его через +)
            // Произойдет вызов методов AnalyzeSex и AnalyzeAge
            firm.AnalyzePeople(age + sex);

            // Или так тоже Многоадресный Делегат
            Console.WriteLine("\n\n");
            // Многоадресный Делегат (формируем его через Combine)
            // Произойдет вызов методов AnalyzeSex и AnalyzeAge
            firm.AnalyzePeople((Human.HumanDelegate)Delegate.Combine(sex, age));
            // Или так тоже можно
            MulticastDelegate del = age + sex;
            firm.AnalyzePeople((Human.HumanDelegate)del);
            // Удаляем один делегат
            Delegate onlysex = MulticastDelegate.Remove(del, age);
            Console.WriteLine("\n\n************************************\n\n");
            // Уже не многоадресный делегат
            firm.AnalyzePeople((Human.HumanDelegate)onlysex);
            Console.Read();
        }
    }
}