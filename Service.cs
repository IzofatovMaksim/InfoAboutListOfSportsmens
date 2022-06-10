using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Service
    {
        private Dictionary<string, Container<Sportsmen>> _general = new Dictionary<string, Container<Sportsmen>>();
        private Dictionary<int, Action> _menu = new Dictionary<int, Action>();

        private void Menu()
        {
            _menu.Add(1, () => { _general.Add(CreateNameForContainer(), CreateConatinerWithSportsmens()); });
            _menu.Add(2, () => OutputInfo(ChoiceContainer()));
            _menu.Add(3, AddSportsmen);
            _menu.Add(4, SortContainer);
            _menu.Add(5, ListSportsmensByChoice);
            _menu.Add(6, DictionarySportsmens);
        }

        public  void Application()
        {
            Menu();
            bool isWork = true;
            int number;
            while (isWork)
            {
                
                Console.WriteLine("Выберите действие:\n1 - Создать список спортсменов\n2 - Вывести список спортсменов\n" +
                    "3 - Добвить спортсмена в список\n4 - Сортировка списка спортсменов\n5 - Список спортсменов моложе 20 лет, и которые имеют 1 разряд" +
                    "\n6 - Список с количеством спорстменов по каждому вида спорта\n7 - Выход");
                if(int.TryParse(Console.ReadLine(), out number))
                {
                    if (number != 7)
                        _menu[number]();
                    else
                        isWork = false;
                }
                else
                    Console.WriteLine("Такого пункта нет");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void DictionarySportsmens()
        {
            var listQuery = ChoiceContainer().GroupBy(sp => sp.KindOfSport).Select(sp => new { KindOfSport = sp.Key, Count = sp.Count() });
            Console.WriteLine("Список: ");
            foreach (var item in listQuery)
            {
                Console.WriteLine(item);
            }
        }

        private void ListSportsmensByChoice()
        {
            Container<Sportsmen> c = ChoiceContainer();
            Console.WriteLine("Имя спортсмена\tГод Рождения\tВид спорта\tРазряд");
            c.ListByCondition(sportsmen => sportsmen.GetAge() < 20 && sportsmen.Rank == 1).ForEach(OutputInfo);
        }

        private void SortContainer()
        {
            Container<Sportsmen> sp =  ChoiceContainer();
            Console.WriteLine("Выберите как отсортировать список:\n1 - по году рождения\n2 - по виду спорта");
            switch (Console.ReadLine())
            {
                case "1": sp.Sort(); break;
                case "2": sp.Sort(new SortByKindOfSport()); break;
                default: Console.WriteLine("Такого пункта нет"); break;
            }
        }

        private void AddSportsmen()
        {
            ChoiceContainer().Add(CreateSportsmen());
        }

        private Container<Sportsmen> ChoiceContainer()
        {
            Console.WriteLine("Выберите список спорсменов: ");
            foreach (var item in _general)
            {
                Console.WriteLine(item.Key);
            }
            string choice = Console.ReadLine();
            if (_general.ContainsKey(choice))
                return _general[choice];
            else
                throw new Exception("Такого списка нет");
        }

        private Sportsmen CreateSportsmen()
        {
            Console.WriteLine("Введите данные о спортсмене: ");
            Console.Write("Имя: ");
            Console.InputEncoding = Encoding.Unicode;
            string name = Console.ReadLine();
            Console.Write("Год рождения: ");
            int age = 1900;
            try
            {
                age = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Вы ввели неверный год");
            }
            Console.Write("Вид спорта: ");
            string kind = Console.ReadLine();
            Console.Write("Разряд: ");
            int rank = 0;
            try
            {
                rank = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Вы ввели неверный разряд");
            }
            return new Sportsmen(name, age, kind, rank);
        }

        private void OutputInfo(Container<Sportsmen> container)
        {
            Console.WriteLine("№\tИмя спортсмена\tГод Рождения\tВид спорта\tРазряд");
            int n = 1;
            foreach (var item in container)
            {
                Console.WriteLine($"{n++}\t{item.Name}\t{item.YearOfBirth}\t\t{item.KindOfSport}\t\t{item.Rank}");
            }
        }

        private void OutputInfo(Sportsmen sp)
        {
            Console.WriteLine($"\t{sp.Name}\t{sp.YearOfBirth}\t\t{sp.KindOfSport}\t\t{sp.Rank}");
        }

        private Container<Sportsmen> CreateConatinerWithSportsmens()
        {
            Container<Sportsmen> sportsmens = new Container<Sportsmen>();
            int answer = 2;
            do
            {
                sportsmens.Add(CreateSportsmen());
                Console.WriteLine("Добавить еще спортсмена? 1 - да, 2 - нет");
                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Такого ответа нет");
                }
            } while (answer == 1);
            return sportsmens;
        }

        private string CreateNameForContainer()
        {
            Console.Write("Введите название списка спорсменов: ");
            Console.InputEncoding = Encoding.Unicode;
            return Console.ReadLine();
        }
    }
}
