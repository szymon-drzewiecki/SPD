from classes import *
from schrage import *

def carlier(tg, UB):
    pi_g = 0
    U, pi = hschrage(tg)
    if U < UB:
        UB = U
        pi_g = pi

    b = pi.find_b()
    a = pi.find_a(b)
    c = pi.find_c(a, b)
    if c == None:
        return pi_g
    K = pi.get_K(c,b)
    _tmp = pi.group[c].r
    pi.group[c].r = max(pi.group[c].r, K[0] + K[1])
    LB = hschrage_pmtn(pi)
    Kc = pi.get_K(c, b, with_c=True)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        pi_g = carlier(pi, UB)
    pi.group[c].r = _tmp
    _tmp = pi.group[c].q
    pi.group[c].q = max(pi.group[c].q, K[1] + K[2])
    LB = hschrage_pmtn(pi)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        pi_g = carlier(pi, UB)
    pi.group[c].q = _tmp


    return pi_g

def main():
    tg = load_data('schrage_data.txt', 10)
    pi = carlier(tg, math.inf)
    print("Cmax:", pi.cmax())
    
if __name__ == '__main__':
    main()
