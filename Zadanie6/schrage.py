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

    return Cmax, pi


# SCHRAGE PMTN
def schrage_pmtn(tg):

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

    return Cmax


# SCHRAGE [HEAP]
def hschrage(tg):

    pi = TaskGroup()
    Ng = Heap(len(tg.group), max, 'q')
    Nn = Heap(len(tg.group), min, 'r')
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

    return HCmax, pi


# SCHRAGE PMTN [HEAP]
def hschrage_pmtn(tg):

    Cmax = 0
    tgc = copy.deepcopy(tg)
    Ng = Heap(len(tg.group), max, 'q')
    Nn = Heap(len(tg.group), min, 'r')
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

    return Cmax
