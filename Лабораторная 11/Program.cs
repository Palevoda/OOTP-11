using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Dynamic;

namespace Лабораторная_11
{
    class Program
    {
        static void Main(string[] args)
        {

            //Задание 1
            List<string> mounth = new List<string>();
            mounth.Add("June"); mounth.Add("July"); mounth.Add("August"); mounth.Add("September"); 
            mounth.Add("October"); mounth.Add("November"); mounth.Add("December"); mounth.Add("January");
            mounth.Add("March"); mounth.Add("April"); mounth.Add("May"); mounth.Add("February");

            int mounth_length = 4;
            IEnumerable<string> result1 = from element in mounth where element.Length == mounth_length select element;
            IEnumerable<string> summer = mounth.Where(p => p == "June" || p == "July" || p == "August");
            IEnumerable<string> winter = from element in mounth where element == "December" || element == "January" || element == "February" select element;
            IEnumerable<string> alfabet = from element in mounth orderby element select element;
            IEnumerable<string> count = from element in mounth where element.Contains("u") == true && element.Length >= 4  select element;
            
            foreach (string element in count)
                Console.WriteLine(element);
            // Задание 2-3

            String[] City = new String[] { "Минск", "Москва", "Стамбул", "Гамбург", "Вена", "Монако", "Сан-Марино", "Варшава", "Дубай", "Гаага" };
            String[] WeekDay = new String[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            String[] DepTime = new String[] { " 00:19 ", "17:38", "19:09", "02:12", "23:59", "22:17", "03:15", "09:00", "13:42", "05:55" };
            String[] PlaneType = new string[] { "Пассажирский", "Грузовой" };
            Random rnd = new Random();      

            List<Airline> Airlines = new List<Airline>();

           

            for (int i = 0; i < 40; i++)
            {
                Airlines.Add( new Airline(City[rnd.Next(0, 9)], Convert.ToUInt16(0), PlaneType[rnd.Next(0, 1)], DepTime[rnd.Next(0, 9)], WeekDay[rnd.Next(0, 6)], rnd.Next(400, 12000)));
            }

            IEnumerable<Airline> link1 = from element in Airlines where element.punct == "Минск" select element;
            IEnumerable<Airline> link2 = from element in Airlines where element.weekDay == "Понедельник" select element;
            var link3 = Airlines.OrderBy(el => el.Dist).Where(el => el.weekDay == "Понедельник").LastOrDefault();          
            IEnumerable<Airline> link4 = Airlines.Where(n => n.weekDay == "Понедельник").Where(elem => elem.Date == Airlines.Max(el => el.Date));
            IEnumerable<Airline> link5 = Airlines.Where(el => el.weekDay == "Суббота").Where(el => el.Date.Hour > 14).Select(n => n);
            int link6 = Airlines.Where(el => el.PlaneType == "Пассажирский").Count(); 



            foreach (Airline element in link5)
                Console.WriteLine(element.ToString());
            Console.WriteLine("\n\n");
            foreach (Airline element in Airlines)
                element.showObjectInfo();

            Console.WriteLine("\n\n");
            Console.WriteLine(link6);

            // Задание 4

            List<Airline> PassAirlines = new List<Airline>();
            List<Airline> Heavylines = new List<Airline>();



            for (int i = 0; i < 40; i++)
            {
                PassAirlines.Add(new Airline(City[rnd.Next(0, 9)], Convert.ToUInt16(0), PlaneType[0], DepTime[rnd.Next(0, 9)], WeekDay[rnd.Next(0, 6)], rnd.Next(400, 15000)));
            }



            for (int i = 0; i < 40; i++)
            {
                Heavylines.Add(new Airline(City[rnd.Next(0, 9)], Convert.ToUInt16(0), PlaneType[1], DepTime[rnd.Next(0, 9)], WeekDay[rnd.Next(0, 6)], rnd.Next(400, 12000)));
            }

            Console.WriteLine("\n\n Task4 \n\n");
            string MyDay = "Понедельник";
            var sortAirlines = PassAirlines.Concat(Heavylines).Where(n => n.Date.Hour <= 24 && n.Date.Hour >= 20).OrderBy(el => el.Dist).Distinct().SkipLast(5);
            foreach (Airline element in sortAirlines)
            {
                element.showObjectInfo();
            }

            // Task 5
           
            List<Airoport> airoports = new List<Airoport>();
            for (int i = 0; i < 10; i++)
            {
                airoports.Add(new Airoport(
                    City[rnd.Next(0,10)]
                    ));
            }

            var ArriveInfo = from pas in PassAirlines
                         join port in airoports on pas.punct equals port.city
                         select new { Port = pas.punct, arrive_time = pas.Date, type = pas.PlaneType };

            
            Console.WriteLine("\n\n\n Join \n\n\n");
            foreach (var el in ArriveInfo)
                Console.WriteLine($"Время прибытия: {el.arrive_time}\nГород {el.Port}\nТип борта{el.type}\n");
        }
    }

    public class Airoport
    {
        string City;

        public string city
        {
            get { return City; }
        }

        public Airoport(string city) { City = city; }
    }

    public partial class Airline
    {
        private static int CurrentSize = 0;
        private const int ArchiveMaxSize = 100;
        private DateTime date = new DateTime(2020, 12, 04, 0, 0, 0);

        public DateTime Date
        {
            get
            {
                return date;
            }
        }


        private int dist;
        public int Dist
        {
            set
            {
                dist = value;
            }
            get
            {
                return dist;
            }
        }


        private String Punct;
        public String punct
        {
            set
            {
                Punct = value;
            }
            get
            {
                return Punct;
            }
        }

        private int flightNumber;
        public int FlightNumber
        {
            private set
            {

                flightNumber = value;

            }
            get { return flightNumber; }
        }
        private String planeType;

        public String PlaneType
        {
            set
            {
                planeType = value;
            }
            get
            {
                return planeType;
            }
        }

        private String depatureTime;

        public String DepartureTime
        {
            set
            {
                depatureTime = value;
            }
            get
            {
                return depatureTime;
            }
        }

        private String WeekDay;

        public String weekDay
        {
            set
            {
                WeekDay = value;
            }
            get
            {
                return WeekDay;
            }
        }


        private const String developer = "Palevoda POIT-4";


        Airline[] Archive = new Airline[ArchiveMaxSize];

        static string status = "Стркутура структура не использовалась";            //обычный конструктор
        static Airline()
        {
            status = "Был использован приватный конструктор";
        }

        public override bool Equals(object obj)
        {
            Airline flight = (Airline)obj;

            if (this.flightNumber == flight.flightNumber)
                return true;
            else return false;
        }

        public override string ToString()
        {
            return "Номер рейса: " + this.flightNumber + ", Пункт назначения: " + this.Punct + ", Время вылета: " + this.Date + " День вылета: " + this.weekDay + " дальность полета " + this.dist;
        }


        private int upgradeHashCodeRef(ref int hash)
        {
            hash /= 1000;
            return hash;
        }

        private void upgradeHashCodeOut(out int hash)
        {

            hash = 123456;
        }


        public Airline() { punct = "Неизвестно"; flightNumber = 0; planeType = "Неизвестно"; depatureTime = "Неизвестно"; weekDay = "Неизвестно"; CurrentSize++; }
        public Airline(String Punct, String DepatureTime) { punct = Punct; flightNumber = 0; planeType = "Неизвестно"; depatureTime = DepatureTime; weekDay = "Неизвестно"; CurrentSize++; }
        public Airline(String newPunct, ushort newFlightNumber, String newPlaneType, String newdepatureTime, String newWeekDay, int newdist)
        {
            punct = newPunct;
            //flightNumber = newFlightNumber; 
            this.flightNumber = GetHashCode();
            planeType = newPlaneType;
            depatureTime = newdepatureTime;
            weekDay = newWeekDay;
            //upgradeHashCodeOut(out this.flightNumber);
            upgradeHashCodeRef(ref this.flightNumber);
            dist = newdist;
            CurrentSize++;
            Random rand = new Random();
            date = date.AddHours(Convert.ToDouble(rand.Next(0,24)));
            date = date.AddMinutes(Convert.ToDouble(rand.Next(0, 60)));
        }

        public void showObjectInfo()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("::" + Airline.status);
            Console.WriteLine($"\nПункт назначения: {punct}  \nНомер рейса: {flightNumber} \nТип рейса: { planeType} \nВремя отправления: {date} \nДень отправления: {weekDay} \nДальность полета: {dist} ");
            Console.WriteLine($"=============={CurrentSize}==================");
        }




    }

}
