import math

class Witi_task:
    def __init__(self, i, p, w, d):
        self.i = i
        self.p = p
        self.w = w
        self.d = d

    def __repr__(self):
        return '{}. {} {} {}'.format(self.i, self.p, self.w, self.d)



def calc_cmax(pi):
    cmax = 0
    for t in pi:
        cmax += t.p

    return cmax

def calc_T(C, task):
    if C <= task.d:
        return 0
    else:
        return C - task.d

def calc_delay_sum(pi):
    _time = 0
    _sum = 0
    for t in pi:
        _time += t.p
        T = calc_T(_time, t)
        _sum += t.w * T
    return _sum

def load_witi_data(path, instance):
    _counter = 0
    _t_counter = 1
    tasks = []
    with open(path, 'r') as f:
        for n, line in enumerate(f):
            line = line.replace('\n', '')
            _pwds = [int(i) for i in line.split(' ') if i != '']
            if _pwds:
                if len(_pwds) == 3 and _counter == instance + 1:
                    tasks.append(Witi_task(_t_counter, _pwds[0], _pwds[1], _pwds[2]))
                    _t_counter += 1
                elif len(_pwds) < 3:
                    _counter += 1

    return tasks

def solve_witi_with_solver(tasks):
    from ortools.sat.python import cp_model
    
    model = cp_model.CpModel()

    #variables: start_time, end_time, delay_sum
    
    #max value of variables
    vmax_val = sum([t.p for t in tasks]) + 1
    #min value of variables
    vmin_val = 0

    #initialization of model variables
    model_start_vars = []
    model_end_vars = []
    model_penalty_vars = []
    model_interval_vars = []

    #single variable for storing sum of delays
    delay_sum_objective = model.NewIntVar(vmin_val, 2147483647, 'delays_sum')

    #each variable for each task
    for n, t in enumerate(tasks):
        suffix = 't:{}'.format(n+1)
        start_var = model.NewIntVar(vmin_val, vmax_val, 'start_'+suffix)
        end_var = model.NewIntVar(vmin_val, vmax_val, 'end_'+suffix)
        penalty_var = model.NewIntVar(vmin_val, 2147483647, 'penalty_'+suffix)
        interval_var = model.NewIntervalVar(start_var, t.p, end_var, 'interval_'+suffix)

        model_start_vars.append(start_var)
        model_end_vars.append(end_var)
        model_penalty_vars.append(penalty_var)
        model_interval_vars.append(interval_var)

    #CONSTRAINTS
    # 1. no overlap
    model.AddNoOverlap(model_interval_vars)
    # 2. penalty constraint
    for n, t in enumerate(tasks):
        model.Add(t.w * (model_end_vars[n] - t.d) <= model_penalty_vars[n])
    # 3. delay_sum_constraint
    model.Add(sum(model_penalty_vars) <= delay_sum_objective)

    #initialize solver and run it
    model.Minimize(delay_sum_objective)
    solver = cp_model.CpSolver()
    solver.parameters.max_time_in_seconds = 300.0

    status = solver.Solve(model)
    if (status is not cp_model.OPTIMAL):
        status_readable = 'not optimal'
    else:
        status_readable = 'optimum found!'

    pi = []
    for n, t in enumerate(tasks):
        pi.append((t, solver.Value(model_start_vars[n])))
    pi.sort(key=lambda x: x[1])
    pi = [x[0] for x in pi]

    return solver.ObjectiveValue(), status_readable, pi
    

def main():
    data = load_witi_data('c_witi.txt', 0)
    del_sum, status, pi = solve_witi_with_solver(data)
    print(del_sum,'-', status)
    for p in pi:
        print(p)

if __name__ == '__main__':
    main()
    
