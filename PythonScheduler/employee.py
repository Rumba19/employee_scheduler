class Employee:
    def __init__(self, name):
        self.name = name
        self.preferred_shifts = {}  # e.g., {"Monday": "Morning", "Tuesday": "Evening", ...}
        self.assigned_days = 0
        self.schedule = {}  # final assigned schedule: {day: shift}

    def can_work(self, day):
        return day not in self.schedule and self.assigned_days < 5

    def assign_shift(self, day, shift):
        self.schedule[day] = shift
        self.assigned_days += 1
