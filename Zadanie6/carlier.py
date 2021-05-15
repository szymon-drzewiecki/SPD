from classes import *
from schrage import *

def carlier(tg, UBCmax, UBpi):
    UCmax, Upi = schrage(tg)
    if UCmax < UBCmax:
        UBCmax = UCmax
        UBpi = Upi
    b = tg.find_b()
    a = tg.find_a()
    c = tg.find_c()
    
    if len(c) == 0:
        return UBpi
    
    