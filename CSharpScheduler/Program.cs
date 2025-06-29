using System;

namespace ShiftScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new Scheduler();
            scheduler.CollectPreferences();
            scheduler.AssignShifts();
            scheduler.PrintSchedule();
        }
    }
}
