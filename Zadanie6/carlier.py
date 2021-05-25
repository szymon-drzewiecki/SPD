from classes import *
from schrage import *
import math
import sys
import threading

_counter = 0

def make_tg_from_lists(kl, vl):
    new_tg = TaskGroup()
    for n, k in enumerate(kl):
        new_tg.add(k, vl[n])

    return new_tg

def carlier(tg, UB):
    global _counter
    _counter += 1
    print(_counter)
    piZG = 0

    UCmax, Upi = schrage(tg) #U i pi ze schrage
    if UCmax < UB: #if U < UB
        UB = UCmax #UB = U
        piZG = Upi #pi* = pi

    b = tg.find_b()
    a = tg.find_a(b)
    c = tg.find_c(a, b)

    if len(c) == 0:
        return piZG

    key_c = list(c.keys())[0]
    key_list = list(tg.group.keys())
    value_list = list(tg.group.values())
    index_c = key_list.index(key_c)

    r_prim, p_prim, q_prim = tg.find_K_params(b, c)
    value_list[index_c].r = max(value_list[index_c].r, r_prim + p_prim)

    new_tg = make_tg_from_lists(key_list, value_list) #experimental

    LB = schrage_pmtn(new_tg)
    r_prim_with_c, p_prim_with_c, q_prim_with_c = new_tg.find_K_params_with_C(b, c)
    LB = max(max(r_prim + q_prim + p_prim, r_prim_with_c+p_prim_with_c+q_prim_with_c), LB)

    if LB < UB:
        carlier(Upi, UB)

    value_list.clear()
    value_list = list(tg.group.values())
    value_list[index_c].q = max(value_list[index_c].q, q_prim + p_prim)
    new_tg = make_tg_from_lists(key_list, value_list)

    LB = schrage_pmtn(new_tg)
    LB = max(max(r_prim + q_prim + p_prim, r_prim_with_c + p_prim_with_c + q_prim_with_c), LB)

    if LB < UB:
        carlier(Upi, UB)

    value_list.clear()
    value_list = list(tg.group.values())

# def carlier(lst):
#     lst, Cmax, b = schrage(copy.deepcopy(lst)) # lst - permutacja zadań, C-max - górne oszacowanie, b - pozycja ostatniego w ścieżce krytycznej
#     a = findA(lst, b, Cmax) # pozycja pierwszego zadania w ścieżce krytycznej
#     c = findC(lst, a, b) # zadanie o jak najwyższej pozycji ale z mniejszym q_pi_j < q_pi_b
#     if not c: # jeśli nie znaleziono takiego to c_max jest roziwązaniem optymalnym
#         return Cmax

#     rprim, pprim, qprim = findRPQprim(lst, b, c) # min r, max q, suma czasów wykonania zadań - w bloku (c+1, b)
#     r_saved = lst[c][0] # zapisz r
#     lst[c][0] = max(lst[c][0], rprim + pprim) # modyfikacja terminu dostępności zadania c, wymusi to jego późniejszą realizację, za wszystkimi zadaniami w bloku(c+1, b)
#     LB = schrage_div(copy.deepcopy(lst)) # sprawdzamy dolne ograniczenie schrage z podziałem  - dla wszystkich permutacji spełniających to wymaganie

#     if LB < Cmax: # sprawdź czy rozwiązanie jest obiecujące
#         Cmax = min(Cmax, carlier(lst)) # wywołaj carliera jeszcze raz dla nowego problemu

#     lst[c][0] = r_saved # odtwórz r

#     q_saved = lst[c][2]
#     lst[c][2] = max(lst[c][2], pprim + qprim) # wymuszenie aby zadanie c było wykonywane przed wszystkimi zadaniami w bloku (c+1, b)
#     LB = schrage_div(copy.deepcopy(lst)) # sprawdź czy taki problem jest obiecujący

#     if LB < Cmax:
#         Cmax = min(Cmax, carlier(lst))

#     lst[c][2] = q_saved # przywróc q

def carlier2(tg, UB):
    tg_copy = copy.deepcopy(tg)
    U, pi = schrage(tg)
    pi_g = 0
    if U < UB:
        UB = U
        pi_g = pi

    b = pi.find_b(pi.cmax())
    a = pi.find_a2(b,pi.cmax())
    c = pi.find_c(a, b)

    if len(c) == 0:
        return pi_g

    key_c = list(c.keys())[0]

    rK, pK, qK = pi.find_K_params(b, c)
    _tmp = pi[key_c].r
    pi[key_c].r = max(pi[key_c].r, rK+pK)
    LB = schrage_pmtn(tg) #później do testów
    hK = rK + pK + qK
    rK_c, pK_c, qK_c = pi.find_K_params_with_C(b, c)
    hK_c = rK_c + pK_c + qK_c
    LB = max(hK, hK_c, LB)
    if LB < UB:
      pi1 = carlier2(pi, UB)
    pi[key_c].r = _tmp
    _tmp = pi[key_c].q
    pi[key_c].q = max(pi[key_c].q, qK + pK)
    LB = schrage_pmtn(tg)
    rK_c, pK_c, qK_c = pi.find_K_params_with_C(b, c)
    hK_c = rK_c + pK_c + qK_c
    LB = max(hK, hK_c, LB)
    if LB < UB:
      pi1 = carlier2(pi, UB)
    pi[key_c].q = _tmp

    return pi1

# MAIN FUNCTION
def main():
    tg = load_data("schrage_data.txt", 0)
    #Cmax, pi = schrage(tg)
    UB=math.inf
    #car = carlier(pi,UB)
    pi = carlier2(tg, UB)
    print(pi.cmax())
    #print(car)

if __name__ == '__main__':
    main()
    # sys.setrecursionlimit(2147483647)
    # threading.stack_size(200000000)
    # thread = threading.Thread(target=main)
    # thread.start()
