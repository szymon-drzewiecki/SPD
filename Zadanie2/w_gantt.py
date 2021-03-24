import plotly.express as px
import pandas as pd
from numpy import transpose

class Task:
    def __init__(self, name, times, s_time):
        self.name = name
        self.times = times
        self.m_counter = 0
        self.start_time = s_time
        self.end_time = s_time + times[0]

    #Dobranie czasow to kolejnej maszyny
    def advance_machine(self):
        self.m_counter += 1
        if self.m_counter < len(self.times):
            self.start_time = self.end_time
            self.end_time = self.start_time + self.times[self.m_counter]

        s = self.start_time
        e = self.end_time

        return (s, e)

    #Aktualizacja startowego czasu, gdy nie jest mozliwy ten wyliczony
    def update_s_time(self, ns_time):
        self.start_time = ns_time
        self.end_time = self.start_time + self.times[self.m_counter]

    def print_task(self):
        print(self.name + ":", self.start_time, self.end_time)


#Funkcja zmieniajaca podany czas w sekundach na odpowiedni do biblioteki plotly
def calc_ts(seconds):
    minutes = seconds // 60
    hours = minutes // 60
    if hours > 1:
        minutes = minutes % 60
    r_seconds = seconds % 60

    return "1970-01-01 {:02d}:{:02d}:{:02d}".format(hours, minutes, r_seconds)


#Stworzenie zbioru danych o polozeniu kazdego zadania w wykresie Gantta
def add_task_data(df, tasks):
    _tasks = []
    for i,t in enumerate(tasks):
        if i == 0:
            _tasks.append(Task("Zad. "+str(i+1), t, 0))
        else:
            _tasks.append(Task("Zad. "+str(i+1), t, _tasks[i-1].end_time))

    _dr = 0
    for i in range(len(tasks[0])):
        for j, t in enumerate(_tasks):
            if i != 0:
                t.advance_machine()
                if j != 0:
                    if t.start_time < _tasks[j-1].end_time:
                        t.update_s_time(_tasks[j-1].end_time)

            df.loc[_dr] = dict(Start=calc_ts(t.start_time),
                             End=calc_ts(t.end_time),
                             Res="Maszyna "+str(i+1),Name="Zad. "+str(j+1))
            _dr += 1
    
    return df

if __name__ == "__main__":
    tasks = []
    tasks_order = []
    makespan_C = 0
    with open("output.txt", 'r') as file:
        sequence_index = 0;
        for i, line in enumerate(file.readlines()):
            if i == 0:
                sequence_index = int((line.strip().split(' '))[0]) + 3
                continue
            elif line == '\n':
                continue
            elif i == sequence_index - 1:
                makespan_C = int(line.strip())
            elif i == sequence_index:
                tasks_order = [int(x) for x in line.strip().split()]
                tasks_order = [x-1 for x in tasks_order]
            else:
                tasks.append([int(x) for x in line.strip().split(' ')])

    tasks_o = [tasks[i] for i in tasks_order]

    df = pd.DataFrame(columns=['Start', 'End', 'Res', 'Name'])
    df = add_task_data(df, tasks_o)
    
    fig = px.timeline(df, x_start="Start", x_end="End", y="Res", color="Name")
    fig.update_layout(xaxis=dict(title='Czas', tickformat = '%M:%S'),
                      yaxis=dict(title='Maszyny'))
    fig.show()
