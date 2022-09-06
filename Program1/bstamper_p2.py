# Part 2
import numpy as np

#import matrices 1-5
mat1 = np.loadtxt("bstamper_mat1.txt")
mat2 = np.loadtxt("bstamper_mat2.txt")
mat3 = np.loadtxt("bstamper_mat3.txt")
mat4 = np.loadtxt("bstamper_mat4.txt")
mat5 = np.loadtxt("bstamper_mat5.txt")

#not possible due to dimension differences
#mat12
#mat13
#mat14
#mat15
#mat21
#mat24
#mat25
#mat31
#mat34
#mat35
#mat41
#mat42
#mat43
#mat51
#mat52
#mat53

#possible combos

#mat23
mat23 = np.add(mat2, mat3)
np.savetxt('bstamper_p2_out23.txt', mat23)

#mat32
mat32 = np.add(mat3, mat2)
np.savetxt('bstamper_p2_out32.txt', mat32)

#mat45
mat45 = np.add(mat4, mat5)
np.savetxt('bstamper_p2_out45.txt', mat45)

#mat54
mat54 = np.add(mat5, mat4)
np.savetxt('bstamper_p2_out54.txt', mat54)
