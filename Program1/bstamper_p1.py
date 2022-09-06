# Part 1
import numpy as np

#set variables
firstName = 5
lastName = 7
counter = 1 #For the for loop, used as the value for (0,0)
mat1 = np.empty([firstName,lastName], int) #Initializing the matrix
for row in range(mat1.shape[0]): #Creates the rows
    for col in range(mat1.shape[1]): #Creates and the columns
        mat1[row][col] = counter #Fills the columns
        counter += 1
np.savetxt('bstamper_mat1.txt', mat1) #Save the matrix in a .txt file

#These are all copy paste and use the same format as the one above
counter2 = 4
mat2 = np.empty([lastName, firstName], int)
for row in range(mat2.shape[0]):
    for col in range(mat2.shape[1]):
        mat2[row][col] = counter2
        counter2 += 2
np.savetxt('bstamper_mat2.txt', mat2)

counter3 = 0.3
mat3 = np.empty([lastName, firstName], float)
for col in range(mat3.shape[1]):
    for row in range(mat3.shape[0]):
        mat3[row][col] = counter3
        counter3 += 0.1
np.savetxt('bstamper_mat3.txt', mat3)

counter4 = 3
mat4 = np.empty([9, 11], int)
for row in range(mat4.shape[0]):
    for col in range(mat4.shape[1]):
        mat4[row][col] = counter4
        counter4 += 3
np.savetxt('bstamper_mat4.txt', mat4)

counter5 = -5
mat5 = np.empty([9, 11], float)
for col in range(mat5.shape[1]):
    for row in range(mat5.shape[0]):
        mat5[row][col] = counter5
        counter5 += 1.5
np.savetxt('bstamper_mat5.txt', mat5)

print("Part 1 successfully run")
#^ got help from my friend Alex
#He helped me figure out the for loop


