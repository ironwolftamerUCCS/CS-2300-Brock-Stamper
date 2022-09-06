#This is so you can actually read the matrices (the txt files format real weird)
import numpy as np

#read in the matrices and print each one
mat1 = np.loadtxt("bstamper_mat1.txt")
print("mat1:", mat1)

mat2 = np.loadtxt("bstamper_mat2.txt")
print("mat2:", mat2)

mat3 = np.loadtxt("bstamper_mat3.txt")
print("mat3:", mat3)

mat4 = np.loadtxt("bstamper_mat4.txt")
print("mat4:", mat4)

mat5 = np.loadtxt("bstamper_mat5.txt")
print("mat5:", mat5)

p2_out23 = np.loadtxt("bstamper_p2_out23.txt")
print("p2_out23:", p2_out23)

p2_out32 = np.loadtxt("bstamper_p2_out32.txt")
print("p2_out32:", p2_out32)

p2_out45 = np.loadtxt("bstamper_p2_out45.txt")
print("p2_out45:", p2_out45)

p2_out54 = np.loadtxt("bstamper_p2_out54.txt")
print("p2_out54:", p2_out54)

p3_out12 = np.loadtxt("bstamper_p3_out12.txt")
print("p3_out12:", p3_out12)

p3_out13 = np.loadtxt("bstamper_p3_out13.txt")
print("p3_out13:", p3_out13)

p3_out21 = np.loadtxt("bstamper_p3_out21.txt")
print("p3_out21:", p3_out21)

p3_out31 = np.loadtxt("bstamper_p3_out31.txt")
print("p3_out31:", p3_out31)
