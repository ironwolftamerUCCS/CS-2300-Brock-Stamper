
# Part 3
import numpy as np

#import matrices 1-5
mat1 = np.loadtxt("bstamper_mat1.txt")
mat2 = np.loadtxt("bstamper_mat2.txt")
mat3 = np.loadtxt("bstamper_mat3.txt")
mat4 = np.loadtxt("bstamper_mat4.txt")
mat5 = np.loadtxt("bstamper_mat5.txt")

#not possible due to dimension incompatibility
#mat14
#mat15
#mat23
#mat24
#mat25
#mat32
#mat34
#mat35
#mat41
#mat42
#mat43
#mat45
#mat51
#mat52
#mat53
#mat54

#possible combos

#mat12
mat12 = np.matmul(mat1, mat2)
np.savetxt('bstamper_p3_out12.txt', mat12, fmt='%1i')

#mat13
mat13 = np.matmul(mat1, mat3)
np.savetxt('bstamper_p3_out13.txt', mat13, fmt='%1.1f')

#mat21
mat21 = np.matmul(mat2, mat1)
np.savetxt('bstamper_p3_out21.txt', mat21, fmt='%1i')

#mat31
mat31 = np.matmul(mat3, mat1)
np.savetxt('bstamper_p3_out31.txt', mat31, fmt='%1.1f')
