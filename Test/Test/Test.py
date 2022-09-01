# Testing making manual Matrices
from array import array


A = [[1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]]

print("A[1][2] =", A[1][2])
print();

# Testing making generated Matrices
n = 5
m = 7
val = [0 + n] * n
for x in range (n):
    val[x] = [0] * m
print(val)

# Trying Mat1
n1 = 5
m1 = 7
mat1 = array([0] * n * m)
for i in range (n * m):
    mat1[i] = [i]
mat1.reshape(n, m)
print(mat1)
