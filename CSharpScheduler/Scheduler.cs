using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiftScheduler
{
    public class Scheduler
    {
        private static readonly string[] DAYS = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private static readonly string[] SHIFTS = { "Morning", "Afternoon", "Evening" };

        private List<Employee> Employees = new();
        private Dictionary<string, Dictionary<string, List<string>>> Schedule = new();

        public Scheduler()
        {
            foreach (var day in DAYS)
            {
                Schedule[day] = new Dictionary<string, List<string>>();
                foreach (var shift in SHIFTS)
                {
                    Schedule[day][shift] = new List<string>();
                }
            }
        }

        public void CollectPreferences()
        {
            Console.Write("Enter number of employees: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.Write("Enter employee name: ");
                var name = Console.ReadLine();
                var emp = new Employee(name);

                foreach (var day in DAYS)
                {
                    Console.Write($"Preferred shift for {day} (Morning/Afternoon/Evening): ");
                    var pref = Console.ReadLine();
                    emp.PreferredShifts[day] = SHIFTS.Contains(pref, StringComparer.OrdinalIgnoreCase) ? pref : "Morning";
                }

                Employees.Add(emp);
            }
        }

        public void AssignShifts()
        {
            var rand = new Random();

            foreach (var day in DAYS)
            {
                foreach (var shift in SHIFTS)
                {
                    var preferred = Employees
                        .Where(e => e.CanWork(day) && string.Equals(e.PreferredShifts[day], shift, StringComparison.OrdinalIgnoreCase))
                        .Take(2)
                        .ToList();

                    foreach (var emp in preferred)
                    {
                        emp.AssignShift(day, shift);
                        Schedule[day][shift].Add(emp.Name);
                    }

                    while (Schedule[day][shift].Count < 2)
                    {
                        var available = Employees
                            .Where(e => e.CanWork(day) && !Schedule[day][shift].Contains(e.Name))
                            .ToList();

                        if (available.Count == 0) break;

                        var fallback = available[rand.Next(available.Count)];
                        fallback.AssignShift(day, shift);
                        Schedule[day][shift].Add(fallback.Name);
                    }
                }
            }
        }

        public void PrintSchedule()
        {
            Console.WriteLine("\nFinal Weekly Schedule:");
            foreach (var day in DAYS)
            {
                Console.WriteLine($"\n{day}:");
                foreach (var shift in SHIFTS)
                {
                    var names = Schedule[day][shift];
                    Console.WriteLine($"  {shift}: {(names.Count > 0 ? string.Join(", ", names) : "No one assigned")}");
                }
            }
        }
    }
}
