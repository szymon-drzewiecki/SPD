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


    def find_b(self, Cmax):
        C = 0
        b_key = None
        for i in list(self.group.keys()):
            if C < self.group[i].r:
                C = self.group[i].r
            C = C + self.group[i].p
            if C + self.group[i].q == Cmax:
                return {i: self.group[i]}


    def find_kurwA(self, b, Cmax):

        return dict
    def find_c(self, a, b):
        key_a = list(a.keys())[0]
        key_b = list(b.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_a = key_list.index(key_a)
        index_b = key_list.index(key_b)
        index_c = None
        dict = {}
        for i in range(index_a, index_b+1):
            if value_list[i].q < value_list[index_b].q:
                index_c = i
        if index_c != None:
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
            for i in range(key_list.index(key), index_b+1):
                sigma += value_list[i].p
            if value.r + sigma + value_list[index_b].q:
                key_a = key
                dict[key_a] = self.group[key]
                return dict
            if key == key_b:
                key_a = key
                dict[key_a] = self.group[key]
                return dict

    def find_a2(self, b, Cmax):
        sigma = 0
        key_b = list(b.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_b = key_list.index(key_b)
        a=0
        dict = {}
        for i in range(0,index_b+1):
            sigma += value_list[i].p
        while a < index_b and not Cmax == value_list[a].r + sigma + value_list[index_b].q:
            sigma -= (value_list[a].p)
            a += 1
        dict[a] = self.group[a]
        return dict

    def find_K_params(self, b, c):
        key_c = list(c.keys())[0]
        key_b = list(b.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_c = key_list.index(key_c)
        index_b = key_list.index(key_b)
        p_prim = 0
        r_prim = value_list[index_c+1].r
        q_prim = value_list[index_c+1].q
        for i in range(index_c+1, index_b+1):
            p_prim += value_list[i].p
            r_prim = min(value_list[i].r, r_prim)
            q_prim = min(value_list[i].q, q_prim)

        return r_prim, p_prim, q_prim

    def find_K_params_with_C(self, b, c):
        key_c = list(c.keys())[0]
        key_b = list(b.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_c = key_list.index(key_c)
        index_b = key_list.index(key_b)
        p_prim = 0
        r_prim = value_list[index_c].r
        q_prim = value_list[index_c].q
        for i in range(index_c, index_b+1):
            p_prim += value_list[i].p
            r_prim = min(value_list[i].r, r_prim)
            q_prim = min(value_list[i].q, q_prim)
        return r_prim, p_prim, q_prim

    def recreate_pi_r_c(self, c):
        key_c = list(c.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_c = key_list.index(key_c)
        origin_pi_r_c = value_list[index_c].r
        return origin_pi_r_c

    def recreate_pi_q_c(self, c):
        key_c = list(c.keys())[0]
        key_list = list(self.group.keys())
        value_list = list(self.group.values())
        index_c = key_list.index(key_c)
        origin_pi_q_c = value_list[index_c].q
        return origin_pi_q_c

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