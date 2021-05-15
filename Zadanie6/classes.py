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


class Heap_MaxQ:
    def __init__(self, size):
        self.n = 0
        self.data = [None] * size

    def push(self, ht):
        i = self.n
        self.n += 1
        j = (i - 1) // 2
        while i > 0 and self.data[j].q < ht.q:
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
            if (j + 1 < self.n) and (self.data[j + 1].q > self.data[j].q):
                j += 1
            if ht.q >= self.data[j].q:
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
                s += str(t.q) + ", "
        return s


class Heap_MinR:
    def __init__(self, size):
        self.n = 0
        self.data = [None] * size

    def push(self, ht):
        i = self.n
        self.n += 1
        j = (i - 1) // 2
        while i > 0 and self.data[j].r > ht.r:
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
            if (j + 1 < self.n) and (self.data[j + 1].r < self.data[j].r):
                j += 1
            if ht.r <= self.data[j].r:
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
                s += str(t.r) + ", "
        return s