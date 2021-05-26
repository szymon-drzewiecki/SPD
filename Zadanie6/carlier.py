from classes import *
from schrage import *

_counter = 0

def carlier(tg, UB):
    global _counter
    _counter += 1
    UB, pi = hschrage(tg)

    print(_counter,". węzeł... Cmax=",pi.cmax(),sep='')

    b = pi.find_b()
    a = pi.find_a(b)
    c = pi.find_c(a, b)
    if c == None:
        return pi
    K = pi.get_K(c, b)
    _tmp = pi.group[c].r
    pi.group[c].r = max(pi.group[c].r, K[0] + K[1])
    LB = hschrage_pmtn(pi)
    Kc = pi.get_K(c, b, with_c=True)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        x = carlier(copy.deepcopy(pi), UB)
        if x.cmax() < UB:
            pi = x
            UB = x.cmax()
    pi.group[c].r = _tmp
    _tmp = pi.group[c].q
    pi.group[c].q = max(pi.group[c].q, K[1] + K[2])
    LB = hschrage_pmtn(pi)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        x = carlier(copy.deepcopy(pi), UB)
        if x.cmax() < UB:
            pi = x
            UB = x.cmax()
    pi.group[c].q = _tmp

    return pi

def carlier_without_recurrency(tg):
    global _counter
    tg_list = []
    UB, pi = hschrage(tg)
    b = pi.find_b()
    a = pi.find_a(b)
    c = pi.find_c(a, b)
    while (c != None):
        _counter += 1
        print(_counter,". węzeł... Cmax=",pi.cmax(),sep='')
        b = pi.find_b()
        a = pi.find_a(b)
        c = pi.find_c(a, b)
        tg_list.append(copy.deepcopy(pi))
        if (c == None):
            break
        K = pi.get_K(c, b)
        _tmp = pi.group[c].r
        pi.group[c].r = max(pi.group[c].r, K[0] + K[1])
        LB = hschrage_pmtn(pi)
        Kc = pi.get_K(c, b, with_c=True)
        LB = max(sum(K), sum(Kc), LB)
        if LB < UB:
            tg_list.append(copy.deepcopy(pi))
            UB = pi.cmax()
        else:
            pi.group[c].r = _tmp
            _tmp = pi.group[c].q
            pi.group[c].q = max(pi.group[c].q, K[1] + K[2])
            LB = hschrage_pmtn(pi)
            LB = max(sum(K), sum(Kc), LB)
            if LB < UB:
                tg_list.append(copy.deepcopy(pi))
                UB = pi.cmax()
        pi.group[c].q = _tmp



    the_best_cmax = 10000000
    best_pi = {}
    for task_group in tg_list:
        print("Cmax=", task_group.cmax())
        if task_group.cmax() <= the_best_cmax:
            the_best_cmax = task_group.cmax()
            best_pi = task_group
    return best_pi

def main():
    tg = load_data('schrage_data.txt', 7)
    pi = carlier_without_recurrency(tg)
    print("final cmax:", pi.cmax(), "final pi:", pi)


if __name__ == '__main__':
    main()