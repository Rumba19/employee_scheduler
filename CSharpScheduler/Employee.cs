using System.Collections.Generic;

namespace ShiftScheduler
{
    public class Employee
    {
        public string Name { get; set; }
        public Dictionary<string, string> PreferredShifts { get; set; } = new();
        public Dictionary<string, string> Schedule { get; set; } = new();
        public int AssignedDays => Schedule.Count;

        public Employee(string name)
        {
            Name = name;
        }

        public bool CanWork(string day)
        {
            return !Schedule.ContainsKey(day) && AssignedDays < 5;
        }

        public void AssignShift(string day, string shift)
        {
            Schedule[day] = shift;
        }
    }
}
