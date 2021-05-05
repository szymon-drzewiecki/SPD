import random
import re
import numpy as np

#GLOBAL variables
task_matrix = 0
#heap
heap = 0
n = 0

"""
Function used to load data from file.
path - path to the file
i_instance - number of wanted instance (starting from 0)
"""
def load_data(path, i_instance):
    _counter = 0
    _n_tasks = 0
    _matrix = []
    with open(path, 'r') as f:
        for n, line in enumerate(f):
            line = line.replace('\n', '')
            _rpqs = [int(i) for i in line.split(' ')
                        if i != '']
            if _rpqs:
               if len(_rpqs) == 3 and _counter == i_instance+1:
                   _matrix.append(_rpqs)
               elif len(_rpqs) == 1:
                   _counter += 1
                   
    global task_matrix
    global heap
    heap = np.empty(30, dtype=int)#np.empty(len(_matrix), dtype=int)
    task_matrix = np.array(_matrix)

"""
Function calculating the Cmax.
pi - sequence in which the tasks are being executed
"""
def calculate_cmax(pi):
    global task_matrix
    S = task_matrix[pi[0]-1][0]
    C = S + task_matrix[pi[0]-1][1]
    Cmax = C + task_matrix[pi[0]-1][2]
    for i in range(1, task_matrix.shape[0]):
        S = max(task_matrix[pi[i]-1][0], C)
        C = S + task_matrix[pi[i]-1][1]
        Cmax = max(Cmax, C + task_matrix[pi[i]-1][2])

    return Cmax

"""
Function used for pushing element into heap
"""
def heap_push(d):
    global n
    global heap
    i = n
    n += 1
    j = (i - 1) // 2

    while (i > 0 and heap[j] < d):
        heap[i] = heap[j]
        i = j
        j = (i - 1) // 2

    heap[i] = d

"""
Function used to remove the root of the heap
"""
def heap_pop():
    global n
    global heap

    n -= 1
    if n == 0:
        v = heap[n]
        i = 0
        j = 0

        while (j < n):
            if (j+1 < n) and (heap[j+1] > heap[j]):
                j += 1
            if (v >= heap[j]):
                break
            heap[i] = heap[j]
            i = j
            j = 2 * j + 1

        heap[i] = v

#MAIN FUNCTION
def main():
    load_data("schrage_data.txt", 0)

    global n
    global heap
    for i in range(30):
        r = random.randint(0, 50)
        heap_push(r)
        
    print("Finished...")

#AVOIDING RUNNING CODE WHILE IMPORTING THIS MODULE
if __name__ == '__main__':
    main()
