from classes import *
from schrage import *
import time
import pandas as pd
import numpy as np
from matplotlib import pyplot as plt

_counter = 0

def carlier_naive(tg, UB):
    global _counter
    _counter += 1
    UB, pi = schrage(tg)

    print(_counter,". węzeł... Cmax=",pi.cmax(),sep='')

    b = pi.find_b()
    a = pi.find_a(b)
    c = pi.find_c(a, b)
    if c == None:
        return pi
    K = pi.get_K(c, b)
    _tmp = pi.group[c].r
    pi.group[c].r = max(pi.group[c].r, K[0] + K[1])
    LB = schrage_pmtn(pi)
    Kc = pi.get_K(c, b, with_c=True)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        x = carlier_naive(copy.deepcopy(pi), UB)
        if x.cmax() < UB:
            pi = x
            UB = x.cmax()
    pi.group[c].r = _tmp
    _tmp = pi.group[c].q
    pi.group[c].q = max(pi.group[c].q, K[1] + K[2])
    LB = hschrage_pmtn(pi)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        x = carlier_naive(copy.deepcopy(pi), UB)
        if x.cmax() < UB:
            pi = x
            UB = x.cmax()
    pi.group[c].q = _tmp

    return pi

def carlier_heap(tg, UB):
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
        x = carlier_heap(copy.deepcopy(pi), UB)
        if x.cmax() < UB:
            pi = x
            UB = x.cmax()
    pi.group[c].r = _tmp
    _tmp = pi.group[c].q
    pi.group[c].q = max(pi.group[c].q, K[1] + K[2])
    LB = hschrage_pmtn(pi)
    LB = max(sum(K), sum(Kc), LB)
    if LB < UB:
        x = carlier_heap(copy.deepcopy(pi), UB)
        if x.cmax() < UB:
            pi = x
            UB = x.cmax()
    pi.group[c].q = _tmp

    return pi

def main():

    carlier_naive_list = []
    carlier_heap_list = []

    for i in range(20):
        tg = load_data('wlasne_instancje.txt', i)
        start = time.perf_counter()
        pi = carlier_naive(tg, math.inf)
        stop = time.perf_counter()
        time_of_execution = stop - start
        carlier_naive_list.append(time_of_execution)
    avg_5_naive = sum(carlier_naive_list[:5])/len(carlier_naive_list[:5])
    avg_10_naive = sum(carlier_naive_list[5:10])/len(carlier_naive_list[5:10])
    avg_15_naive = sum(carlier_naive_list[10:15]) / len(carlier_naive_list[10:15])
    avg_20_naive = sum(carlier_naive_list[15:20]) / len(carlier_naive_list[15:20])

    for i in range(20):
        tg = load_data('wlasne_instancje.txt', i)
        start = time.perf_counter()
        pi = carlier_heap(tg, math.inf)
        stop = time.perf_counter()
        time_of_execution = stop - start
        carlier_heap_list.append(time_of_execution)
    avg_5_heap = sum(carlier_heap_list[:5]) / len(carlier_heap_list[:5])
    avg_10_heap = sum(carlier_heap_list[5:10]) / len(carlier_heap_list[5:10])
    avg_15_heap = sum(carlier_heap_list[10:15]) / len(carlier_heap_list[10:15])
    avg_20_heap = sum(carlier_heap_list[15:20]) / len(carlier_heap_list[15:20])

    labels = ['5', '10', '15', '20']
    naive = [avg_5_naive, avg_10_naive, avg_15_naive, avg_20_naive]
    heap = [avg_5_heap, avg_10_heap, avg_15_heap, avg_20_heap]

    x = np.arange(len(labels))
    width = 0.35

    fig, ax = plt.subplots()
    rects1 = ax.bar(x - width / 2, naive, width, label='naiwna implementacja Schrage')
    rects2 = ax.bar(x + width / 2, heap, width, label='implementacja Schrage z kopcem')

    ax.set_xlabel('Liczba zadań')
    ax.set_ylabel('Czas')
    ax.set_title('Porównanie czasów algorytmu Carlier z wykorzystaniem Schrage')
    ax.set_xticks(x)
    ax.set_xticklabels(labels)
    ax.legend()

    ax.bar_label(rects1, padding=3)
    ax.bar_label(rects2, padding=3)

    fig.tight_layout()

    plt.show()

if __name__ == '__main__':
    main()