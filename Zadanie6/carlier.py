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


def main():
    tg = load_data('schrage_data.txt', 4)
    pi = carlier(tg, math.inf)
    print("Cmax:", pi.cmax())


if __name__ == '__main__':
    main()