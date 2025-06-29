import random
from employee import Employee

DAYS = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
SHIFTS = ["Morning", "Afternoon", "Evening"]

class Scheduler:
    def __init__(self):
        self.employees = []
        self.schedule = {day: {shift: [] for shift in SHIFTS} for day in DAYS}

    def add_employee(self, employee):
        self.employees.append(employee)

    def collect_preferences(self):
        n = int(input("Enter number of employees: "))
        for _ in range(n):
            name = input("Enter employee name: ")
            emp = Employee(name)
            for day in DAYS:
                pref = input(f"Preferred shift for {day} (Morning/Afternoon/Evening): ").capitalize()
                emp.preferred_shifts[day] = pref if pref in SHIFTS else "Morning"
            self.add_employee(emp)

    def assign_shifts(self):
        for day in DAYS:
            for shift in SHIFTS:
                available = [e for e in self.employees if e.can_work(day) and e.preferred_shifts.get(day) == shift]
                assigned = available[:2]  # take up to 2 preferred
                for emp in assigned:
                    emp.assign_shift(day, shift)
                    self.schedule[day][shift].append(emp.name)

                # If fewer than 2, randomly assign others
                while len(self.schedule[day][shift]) < 2:
                    others = [e for e in self.employees if e.can_work(day) and e.name not in self.schedule[day][shift]]
                    if not others:
                        break
                    chosen = random.choice(others)
                    chosen.assign_shift(day, shift)
                    self.schedule[day][shift].append(chosen.name)

    def print_schedule(self):
        print("\nFinal Weekly Schedule:")
        for day in DAYS:
            print(f"\n{day}")
            for shift in SHIFTS:
                assigned = ", ".join(self.schedule[day][shift])
                print(f"  {shift}: {assigned if assigned else 'No one assigned'}")

if __name__ == "__main__":
    s = Scheduler()
    s.collect_preferences()
    s.assign_shifts()
    s.print_schedule()
