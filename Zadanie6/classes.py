import math

class Task:
    def __init__(self, rpq):
        self.r = rpq[0]
        self.p = rpq[1]
        self.q = rpq[2]

    def rpq(self):
        return [self.r, self.p, self.q]

    def __str__(self):
        return f"{self.r} {self.p} {self.q}"


class TaskGroup:
    def __init__(self):
        self.group = {}

    def add(self, nr, task):
        self.group[nr] = task

    def pop(self, key):
        return self.group.pop(key)

    def get_R(self):
        return {nr: t.r for nr, t in self.group.items()}

    def get_P(self):
        return {nr: t.p for nr, t in self.group.items()}

    def get_Q(self):
        return {nr: t.q for nr, t in self.group.items()}

    def cmax(self):
        t_list = list(self.group.values())
        S = t_list[0].r
        C = S + t_list[0].p
        Cmax = C + t_list[0].q
        for i in range(1, len(self.group)):
            S = max(t_list[i].r, C)
            C = S + t_list[i].p
            Cmax = max(Cmax, C + t_list[i].q)

        return Cmax

    def t_matrix(self):
        return [[t.r, t.p, t.q] for nr, t in self.group.items()]

    def __str__(self):
        s = ""
        for nr, t in self.group.items():
            s += str(nr) + ". " + str(t) + "\n"
        return s

    def __getitem__(self, task_number):
        return self.group[task_number]

    def find_b(self):
        C = 0
        for k, v in self.group.items():
            if v.r > C:
                C = v.r
            C += v.p
            if C + v.q == self.cmax():
                return k
        return None

    def find_c(self, a, b):
        pi_slice = {i: self.group[i].q for i in list(self.group.keys())[
            list(self.group.keys()).index(a)
            :
            list(self.group.keys()).index(b)+1]}
        _cs = {}
        for k, v in pi_slice.items():
            if v < pi_slice[b]:
                _cs[k] = v

        if len(_cs) == 0:
            return None
        else:
            return list(_cs.keys())[-1]

    def find_a(self, b): #do lookniecia
        for k, v in self.group.items():
            _p = [i.p for i in list(self.group.values())[
                list(self.group.keys()).index(k)
                :
                list(self.group.keys()).index(b)+1]]
            
            if v.r + sum(_p) + self.group[b].q == self.cmax():
                return k
        return None

    def get_K(self, c, b, with_c=False):
        K = {i: self.group[i] for i in list(self.group.keys())[
            list(self.group.keys()).index(c)+int(not with_c)
            :
            list(self.group.keys()).index(b)+1]}
        rK = qK = math.inf
        pK = 0
        for k, v in K.items():
            if v.r < rK:
                rK = v.r
            if v.q < qK:
                qK = v.q
            pK += v.p

        return [rK, pK, qK]
    
class HTask:
    def __init__(self, nr, rpq):
        self.nr = nr
        self.r = rpq[0]
        self.p = rpq[1]
        self.q = rpq[2]

    def rpq(self):
        return [self.r, self.p, self.q]

    def __str__(self):
        return f"{self.nr}. {self.r} {self.p} {self.q}"

class Heap:
    def __init__(self, size, heap_type, w_rpq):
        self.n = 0
        self.data = [None] * size
        self.heap_type = heap_type
        self.w_rpq = w_rpq

    def comp(self, dt, ht, nr):
        comp_ht = None
        comp_dt = None
        if self.w_rpq == 'r':
            comp_ht = ht.r
            comp_dt = dt.r
        elif self.w_rpq == 'p':
            comp_ht = ht.p
            comp_dt = dt.p
        else:
            comp_ht = ht.q
            comp_dt = dt.q

        if nr == 0:
            if self.heap_type is min:
                return comp_dt > comp_ht
            else:
                return comp_dt < comp_ht
        else:
            if self.heap_type is min:
                return comp_dt >= comp_ht
            else:
                return comp_dt <= comp_ht

    def push(self, ht):
        i = self.n
        self.n += 1
        j = (i -1) // 2
        while i > 0 and self.comp(self.data[j], ht, 0):
            self.data[i] = self.data[j]
            i = j
            j = (i - 1) // 2
        self.data[i] = ht

    def pop(self):
        if self.n == 0:
            return None
        r = self.data[0]
        self.n -= 1
        ht = self.data[self.n]
        i = 0
        j = 1
        while j < self.n:
            if (j + 1 < self.n) and self.comp(self.data[j], self.data[j + 1], 0):
                j += 1
            if self.comp(self.data[j], ht, 1):
                break
            self.data[i] = self.data[j]
            i = j
            j = 2 * j + 1
        self.data[i] = ht
        self.data[self.n] = None

        return r

    def root(self):
        return self.data[0]

    def tg_import(self, tg):
        for nr, t in tg.group.items():
            self.push(HTask(nr, t.rpq()))

    def __str__(self):
        s = ""
        for t in self.data:
            if t != None:
                if self.w_rpq == 'r':
                    s += str(t.r) + ", "
                elif self.w_rpq == 'p':
                    s += str(t.p) + ", "
                else:
                    s += str(t.q) + ", "
        return s
