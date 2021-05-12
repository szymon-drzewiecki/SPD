import copy
import math
import time
import pandas as pd
from matplotlib import pyplot as plt
from classes import *


def load_data(path, i_instance):
    _counter = 0
    _t_counter = 1
    _n_tasks = 0
    tg = TaskGroup()
    with open(path, 'r') as f:
        for n, line in enumerate(f):
            line = line.replace('\n', '')
            _rpqs = [int(i) for i in line.split(' ')
                     if i != '']
            if _rpqs:
                if len(_rpqs) == 3 and _counter == i_instance + 1:
                    tg.add(_t_counter, Task(_rpqs))
                    _t_counter += 1
                elif len(_rpqs) < 3:
                    _counter += 1

    return tg


# SCHRAGE
def schrage(tg):
    start = time.perf_counter()

    pi = TaskGroup()
    Ng = TaskGroup()
    Nn = copy.deepcopy(tg)
    t = min(Nn.get_R().values())
    while len(Ng.group) != 0 or len(Nn.group) != 0:
        while len(Nn.group) != 0 and min(Nn.get_R().values()) <= t:
            j = min(Nn.get_R(), key=Nn.get_R().get)
            Ng.add(j, Nn.pop(j))
        if len(Ng.group) == 0:
            t = min(Nn.get_R().values())
        else:
            j = max(Ng.get_Q(), key=Ng.get_Q().get)
            t = t + Ng.group[j].p
            pi.add(j, Ng.pop(j))

    Cmax = pi.cmax()

    stop = time.perf_counter()
    schrageTime = (stop - start)

    return schrageTime, Cmax, pi


# SCHRAGE PMTN
def schrage_pmtn(tg):
    start = time.perf_counter()

    Cmax = 0
    tgc = copy.deepcopy(tg)
    Ng = TaskGroup()
    Nn = copy.deepcopy(tg)
    t = 0
    l = 1
    while len(Ng.group) != 0 or len(Nn.group) != 0:
        while len(Nn.group) != 0 and min(Nn.get_R().values()) <= t:
            j = min(Nn.get_R(), key=Nn.get_R().get)
            Ng.add(j, Nn.pop(j))
            if Ng.group[j].q > tgc.group[l].q:
                tgc.group[l].p = t - Ng.group[j].r
                t = Ng.group[j].r
                if tgc.group[l].p > 0:
                    Ng.add(l, tgc.group[l])
        if len(Ng.group) == 0:
            t = min(Nn.get_R().values())
        else:
            j = max(Ng.get_Q(), key=Ng.get_Q().get)
            l = j
            t = t + Ng.group[j].p
            Cmax = max(Cmax, t + Ng.group[j].q)
            Ng.pop(j)

    stop = time.perf_counter()
    schragePtmnTime = (stop - start)

    return schragePtmnTime, Cmax


# SCHRAGE [HEAP]
def hschrage(tg):
    start = time.perf_counter()

    pi = TaskGroup()
    Ng = Heap_MaxQ(len(tg.group))
    Nn = Heap_MinR(len(tg.group))
    Nn.tg_import(tg)
    t = Nn.root().r
    x = 0
    while Ng.n != 0 or Nn.n != 0:
        while Nn.n != 0 and Nn.root().r <= t:
            j = Nn.root().nr
            Ng.push(Nn.pop())
        if Ng.n == 0:
            t = Nn.root().r
        else:
            j = Ng.root().nr
            t = t + Ng.data[0].p
            pi.add(j, Task(Ng.pop().rpq()))
    HCmax = pi.cmax()

    stop = time.perf_counter()
    hschrageTime = (stop - start)

    return hschrageTime, HCmax, pi


# SCHRAGE PMTN [HEAP]
def hschrage_pmtn(tg):
    start = time.perf_counter()

    Cmax = 0
    tgc = copy.deepcopy(tg)
    Ng = Heap_MaxQ(len(tg.group))
    Nn = Heap_MinR(len(tg.group))
    Nn.tg_import(tg)
    t = 0
    l = 1
    while Ng.n != 0 or Nn.n != 0:
        while Nn.n != 0 and Nn.root().r <= t:
            j = Nn.root().nr
            Ng.push(Nn.pop())
            if Ng.root().q > tgc.group[l].q:
                tgc.group[l].p = t - Ng.root().r
                t = Ng.root().r
                if tgc.group[l].p > 0:
                    Ng.push(HTask(l, tgc.group[l].rpq()))
        if Ng.n == 0:
            t = Nn.root().r
        else:
            j = Ng.root().nr
            l = j
            t = t + Ng.root().p
            Cmax = max(Cmax, t + Ng.root().q)
            Ng.pop()
    stop = time.perf_counter()
    hschragePtmnTime = (stop - start)

    return hschragePtmnTime, Cmax


# MAIN FUNCTION
def main():
    tg50 = load_data("in50.txt", 0)
    tg100 = load_data("in100.txt", 0)
    tg200 = load_data("in200.txt", 0)

    schrageTime50, Cmax50, pi50 = schrage(tg50)
    schragePtmnTime50, cmax_pmtn50 = schrage_pmtn(tg50)
    hschrageTime50, HCmax50, hpi50 = hschrage(tg50)
    hschragePtmnTime50, hcmax_pmtn50 = hschrage_pmtn(tg50)
    schrageTime100, Cmax100, pi100 = schrage(tg100)
    schragePtmnTime100, cmax_pmtn100 = schrage_pmtn(tg100)
    hschrageTime100, HCmax100, hpi100 = hschrage(tg100)
    hschragePtmnTime100, hcmax_pmtn100 = hschrage_pmtn(tg100)
    schrageTime200, Cmax200, pi200 = schrage(tg200)
    schragePtmnTime200, cmax_pmtn200 = schrage_pmtn(tg200)
    hschrageTime200, HCmax200, hpi200 = hschrage(tg200)
    hschragePtmnTime200, hcmax_pmtn200 = hschrage_pmtn(tg200)
    print("\n50 zadan:\n")
    print("Cmax:", Cmax50, "\ttime execution:", schrageTime50)
    print("Cmax pmtn:", cmax_pmtn50, "\ttime execution:", schragePtmnTime50)
    print("H Cmax:", HCmax50, "\ttime execution:", hschrageTime50)
    print("H Cmax pmtn:", hcmax_pmtn50, "\ttime execution:", hschragePtmnTime50)
    print("-" * 50)
    #-------------------------------------------------
    print("\n100 zadan:\n")
    print("Cmax:", Cmax100, "\ttime execution:", schrageTime100)
    print("Cmax pmtn:", cmax_pmtn100, "\ttime execution:", schragePtmnTime100)
    print("H Cmax:", HCmax100, "\ttime execution:", hschrageTime100)
    print("H Cmax pmtn:", hcmax_pmtn100, "\ttime execution:", hschragePtmnTime100)
    print("-" * 50)
    # -------------------------------------------------
    print("\n200 zadan:\n")
    print("Cmax:", Cmax200, "\ttime execution:", schrageTime200)
    print("Cmax pmtn:", cmax_pmtn200, "\ttime execution:", schragePtmnTime200)
    print("H Cmax:", HCmax200, "\ttime execution:", hschrageTime200)
    print("H Cmax pmtn:", hcmax_pmtn200, "\ttime execution:", hschragePtmnTime200)
    print("-" * 50)
    # -------------------------------------------------
    plot1 = plt.figure(1)
    x = ["50","100","200"]
    y = [schrageTime50,schrageTime100,schrageTime200]
    z = [hschrageTime50, hschrageTime100, hschrageTime200]
    plt.plot(x, y)
    plt.plot(x, z)
    plt.title("Czasy wykonywania się algorytmu Schrage")
    plt.xlabel("Liczba zadań")
    plt.ylabel("Czas")
    plt.legend(["Zwykły Schrage","Schrage z kopcem"])
    # -------------------------------------------------
    plot2 = plt.figure(2)
    x1 = ["50", "100", "200"]
    y1 = [schragePtmnTime50, schragePtmnTime100, schragePtmnTime200]
    z1 = [hschragePtmnTime50, hschragePtmnTime100, hschragePtmnTime200]
    plt.plot(x1, y1)
    plt.plot(x1, z1)
    plt.title("Czasy wykonywania się algorytmu Schrage z przerwaniami")
    plt.xlabel("Liczba zadań")
    plt.ylabel("Czas")
    plt.legend(["Schrage z przewaniami", "Schrage z przerwaniami z kopcem"])
    plt.show()
    plt.show()
    print("\nScript finished...")

# AVOIDING RUNNING CODE WHILE IMPORTING THIS MODULE
if __name__ == '__main__':
    main()