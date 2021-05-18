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
        b_key = 0
        C = 0
        dict = {}
        Cmax = self.cmax()
        for key, value in self.group.items():
            if C < value.r:
                C = value.r
            C = C + value.p
            if C + value.q == Cmax:
                b_key = key
        dict[b_key] = self.group[b_key]
        return dict

    def find_c(self, a, b):
        key_a = list(b.key())[0]
        key_b = list(b.key())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_a = key_list.index(key_a)
        index_b = key_list.index(key_b)
        index_c = 0
        dict = {}
        for i in range(index_a, index_b):
            if value_list[i].q < value_list[index_b].q:
                index_c = i
        dict[key_list[index_c]] = value_list[index_c]
        return dict

    def find_a(self, b):
        sigma = 0
        Cmax = self.cmax()
        key_a = 0
        key_b = list(b.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_b = key_list.index(key_b)
        dict = {}

        for key, value in self.group.items():
            for i in range(key_list.index(key), index_b):
                sigma += value_list[i].p
            if value.r + sigma + value_list[index_b].q:
                key_a = key
                dict[key_a] = self.group[key]
                return dict
            if key == key_b:
                key_a = key
                dict[key_a] = self.group[key]
                return dict


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