import time
import random

def MergeSort(list):
    n = len(list)
    if n == 1:
        return list
    if n == 2:
        if list[0] > list[1]:
            return [list[1], list[0]]
        return list
    return Merge(MergeSort(list[:n//2]), MergeSort(list[n//2:]))
        
def Merge(list1, list2):
    result = []
    i = 0
    j = 0
    while i < len(list1) and j < len(list2):
        if list1[i] <= list2[j]:
            result.append(list1[i])
            i += 1
        else:
            result.append(list2[j])
            j += 1

    result.extend(list1[i:])
    result.extend(list2[j:])
    return result

list = [random.randint(0, 1000) for _ in range(1000000)]

startTwo = time.time()
Two = MergeSort(list)
endTwo = time.time()
print(f"Při rozdělování na poloviny běžel algoritmus {(endTwo-startTwo)*1000} ms.")

#print(Two)