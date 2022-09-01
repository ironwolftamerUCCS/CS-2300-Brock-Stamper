# Testing making manual Matrices
A = [[1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]]

print("A[1][2] =", A[1][2])

# Testing making generated Matrices
n = 2
m = 5
increment = 1
val = [5] * n
for x in range (n):
    val[x] = [0 + increment] * m
    increment = increment + 1
print(val)