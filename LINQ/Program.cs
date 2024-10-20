using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace LINQ
{
    internal class Program
    {
        public static DataClasses2DataContext dataClasses;

        public static void ShowAllCountries()
        {
            var query = from country in dataClasses.Country
                        join capital in dataClasses.CapitalsOfCountries on country.ID equals capital.CountryId
                        join region in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals region.ID
                        join city in dataClasses.BigSities on country.ID equals city.CountryId
                        select new { Name = country.Name, Region = region.Name, City = city.Name, Population = country.TotalCountOfPersons, Area = country.SquareOfCountry };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} {item.Region} {item.City} {item.Population} {item.Area}");
            }
        }

        public static void ShowCountryNames()
        {
            var query = from country in dataClasses.Country select country.Name;
            Console.WriteLine("Имена стран:");
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowCapitalNames()
        {
            var query = from capital in dataClasses.CapitalsOfCountries
                        join city in dataClasses.BigSities on capital.SityId equals city.ID
                        select new { Name = city.Name };
            Console.WriteLine("Имена столиц:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowCitiesInCountry(string country)
        {
            var query = from city in dataClasses.BigSities
                        join c in dataClasses.Country on city.CountryId equals c.ID
                        where c.Name == country
                        select new { Name = city.Name };
            Console.WriteLine($"Имена больших городов страны {country}:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowCapitalsWithPopulationOver(int threshold)
        {
            var query = from city in dataClasses.BigSities
                        join capital in dataClasses.CapitalsOfCountries on city.ID equals capital.SityId
                        where city.CountOfPersons > threshold
                        select new { Name = city.Name, Population = city.CountOfPersons };
            Console.WriteLine($"Столицы с населением больше {threshold}:");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} {item.Population}");
            }
        }

        public static void ShowAsianCountries()
        {
            var query = from country in dataClasses.Country
                        join region in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals region.ID
                        where region.Name.ToLower() == "asia"
                        select new { Name = country.Name };
            Console.WriteLine("Государства из Азии:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowCountriesByArea(int minArea)
        {
            var query = dataClasses.Country.Where(c => c.SquareOfCountry > minArea).Select(c => c.Name);
            Console.WriteLine($"Государства где площадь больше {minArea}:");
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowCapitalsContaining(string letter1, string letter2)
        {
            var query = from city in dataClasses.BigSities
                        join capital in dataClasses.CapitalsOfCountries on city.ID equals capital.SityId
                        where city.Name.Contains(letter1) && city.Name.Contains(letter2)
                        select new { Name = city.Name };

            Console.WriteLine($"Столицы, в названиях которых есть буквы '{letter1}' и '{letter2}':");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowCapitalsStartingWith(string startingLetter)
        {
            var query = from city in dataClasses.BigSities
                        join capital in dataClasses.CapitalsOfCountries on city.ID equals capital.SityId
                        where city.Name.StartsWith(startingLetter)
                        select new { Name = city.Name };

            Console.WriteLine($"Столицы, названия которых начинаются с '{startingLetter}':");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowCountriesByAreaRange(int minArea, int maxArea)
        {
            var query = dataClasses.Country.Where(c => c.SquareOfCountry > minArea && c.SquareOfCountry < maxArea).Select(c => c.Name);
            Console.WriteLine($"Государства в диапазоне по площади {minArea} - {maxArea}:");
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowCountriesByPopulation(int minPopulation)
        {
            var query = dataClasses.Country.Where(c => c.TotalCountOfPersons > minPopulation).Select(c => c.Name);
            Console.WriteLine($"Государства где количество людей больше {minPopulation}:");
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowTopFiveCountriesByArea()
        {
            var query = dataClasses.Country.OrderByDescending(c => c.SquareOfCountry).Take(5).Select(c => c.Name);
            Console.WriteLine("Топ 5 стран по площади:");
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowTopFiveCountriesByPopulation()
        {
            var query = dataClasses.Country.OrderByDescending(c => c.TotalCountOfPersons).Take(5).Select(c => c.Name);
            Console.WriteLine("Топ 5 стран по количеству людей:");
            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowCountryWithMaxArea()
        {
            int maxArea = dataClasses.Country.Max(c => c.SquareOfCountry);
            var countryWithMaxArea = dataClasses.Country.Where(c => c.SquareOfCountry == maxArea).Select(c => c.Name);

            Console.WriteLine("Страна с самой большой площадью:");
            foreach (var name in countryWithMaxArea)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowCapitalWithMaxPopulation()
        {
            var capitalsQuery = from city in dataClasses.BigSities
                                join capital in dataClasses.CapitalsOfCountries on city.ID equals capital.SityId
                                select new { Name = city.Name, Population = city.CountOfPersons };

            int maxPopulation = capitalsQuery.Max(c => c.Population);
            var capitalWithMaxPopulation = capitalsQuery.Where(c => c.Population == maxPopulation).Select(c => c.Name);

            Console.WriteLine("Столица с наибольшим количеством людей:");
            foreach (var name in capitalWithMaxPopulation)
            {
                Console.WriteLine(name);
            }
        }

        public static void ShowSmallestCountryInEurope()
        {
            int minArea = dataClasses.Country.Min(c => c.SquareOfCountry);
            var smallestEuropeanCountry = from country in dataClasses.Country
                                          join region in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals region.ID
                                          where country.SquareOfCountry == minArea && region.Name == "Europe"
                                          select new { Name = country.Name };

            Console.WriteLine("Страны с самой маленькой площадью в Европе:");
            foreach (var item in smallestEuropeanCountry)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowAveragePopulationInEurope()
        {
            var countriesInEurope = from country in dataClasses.Country
                                    join region in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals region.ID
                                    where region.Name == "Europe"
                                    select new { Population = country.TotalCountOfPersons };

            double averagePopulation = countriesInEurope.Average(c => c.Population);
            Console.WriteLine($"Средняя площадь стран Европы: {averagePopulation}");
        }

        public static void ShowTopThreeCitiesInCountry(string userCountry)
        {
            var citiesQuery = from country in dataClasses.Country
                              join city in dataClasses.BigSities on country.ID equals city.CountryId
                              where country.Name == userCountry
                              orderby city.CountOfPersons descending // Changed to descending for top cities.
                              select new { Name = city.Name, Population = city.CountOfPersons };

            var topThreeCities = citiesQuery.Take(3); // Changed to take 3 for top three.

            Console.WriteLine($"Топ 3 городов по количеству жителей в стране {userCountry}:");
            foreach (var item in topThreeCities)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ShowTotalNumberofCountries()
        {
            int totalCount = dataClasses.Country.Count();
            Console.WriteLine($"Суммарное количество стран: {totalCount}");
        }

        public static void GroupCountriesByRegion()
        {
            var groupedData = from country in dataClasses.Country
                              join region in dataClasses.PartsOfTheWorld on country.PartOfTheWorldId equals region.ID
                              select new { RegionName = region.Name, CountryName = country.Name };

            var groupedRegions = groupedData.GroupBy(x => x.RegionName).ToList();

            Console.WriteLine("Группировка стран по регионам:");
            foreach (var group in groupedRegions)
            {
                Console.WriteLine($"{group.Key}:");
                foreach (var item in group)
                {
                    Console.WriteLine($" - {item.CountryName}");
                }
            }
        }

        static void Main(string[] args)
        {
            dataClasses = new DataClasses2DataContext();
            ShowTotalNumberofCountries();
            GroupCountriesByRegion();
        }
    }
}