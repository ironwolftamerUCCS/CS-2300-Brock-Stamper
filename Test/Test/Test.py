# Testing making manual Matrices
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