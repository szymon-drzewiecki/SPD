import random
import operator
import copy
import re
import numpy as np
import operator
import copy

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
               elif len(_rpqs) < 3:
                   _counter += 1
                   
    global task_matrix
    global heap
    heap = np.empty(len(_matrix), dtype=int)
    task_matrix = np.array(_matrix)
    return _matrix

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

<<<<<<< HEAD
"""
Function for calculating Cmax from sorted array
pi_array - list of tasks ordered in the best sequence
"""
def calculate_cmax_list(pi_list):
    S = pi_list[0][0]
    C = S + pi_list[0][1]
    Cmax = C + pi_list[0][2]
    for i in range(1, len(pi_list)):
        S = max(pi_list[i][0], C)
        C = S + pi_list[i][1]
        Cmax = max(Cmax, C + pi_list[i][2])

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

def schrage(tasks):                                                                                                  
=======
def schrage(tasks):
>>>>>>> main
    pi = []
    G = []
    N = copy.deepcopy(tasks)
    t = min(N)[0]
    while (len(G) != 0 or len(N) != 0):
        while(len(N) != 0 and min(N)[0] <= t):
            j = N.index(min(N))
<<<<<<< HEAD
            G.append(N.pop(j))
        if len(G) != 0:
            j = G.index(max(G, key=operator.itemgetter(2)))
            temporary = G.pop(j)
=======
            G.append(N[j])
            del N[j]
        if len(G) != 0:
            j = G.index(max(G, key=operator.itemgetter(2)))
            temporary = G[j]
            del G[j]
>>>>>>> main
            pi.append(temporary)
            t += temporary[1]
        else:
            t = min(N)[0]
    return pi
<<<<<<< HEAD
    
=======

>>>>>>> main
#MAIN FUNCTION
def main():
    load_data("schrage_data.txt", 4)
    global task_matrix
    print(calculate_cmax_list(schrage(task_matrix.tolist())))
    print("Finished...")

#AVOIDING RUNNING CODE WHILE IMPORTING THIS MODULE
if __name__ == '__main__':
    main()
