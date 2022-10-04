import numpy as np
import matplotlib.pyplot as plt

N = int(input('Введите чётное число N > 3: '))
while N % 2 != 0 or N < 4:
    N = int(input('Введите четное число N > 3: '))

K = int(input('Введите K: '))

A = np.random.randint(-10, 10, (N, N))

print(f'\nA = \n{A}\n')

B = A[:int(N/2), :int(N/2)]
print(f'B = \n{B}\n')
C = A[:int(N/2), int(N/2):]
print(f'C = \n{C}\n')
D = A[int(N/2):, :int(N/2)]
print(f'D = \n{D}\n')
E = A[int(N/2):, int(N/2):]
print(f'E = \n{E}\n')

F = A.copy()

transpose_A = np.transpose(A);

if (A == transpose_A).all():
    print("Матрица А симметрична относительно главной диагонали, поэтому меняем С и В симметрично")
    B1 = np.flip(B, axis=1)
    C1 = np.flip(C, axis=1)
    F = np.vstack([np.hstack([C1, B1]), np.hstack([D, E])])
    print(f'F = \n{F}\n')
else:
    print("Матрица А несимметрична относительно главной диагонали, поэтому меняем С и Е несимметрично")
    F = np.hstack([np.vstack([B, D]), np.vstack([E, C])])
    print(f'F = \n{F}\n')

det_A = np.linalg.det(A)
print(f'Определитель матрицы А: {det_A}')

diagonals_A = sum(np.diagonal(F)) + sum(np.diagonal(np.flip(F, axis=1)))
print(f'Сумма диагоналей матрицы F: {diagonals_A}')

if det_A > diagonals_A:
    print("\nОпределитель матрицы А больше суммы диагоналей матрицы F:")
    result_expression = np.linalg.inv(A) * np.transpose(A) - K * np.linalg.inv(F)
else:
    print("\nОпределитель А меньше суммы диагоналей F:")
    G = np.tril(A)
    result_expression = (np.transpose(A) + G - np.transpose(F)) * K

print("Результат выражения:")
print(result_expression)

plt.subplot(3, 2, 1)
plt.plot(F,':2')

plt.subplot(3, 2, 2)
plt.boxplot(F)

plt.subplot(3, 2, 3)
plt.pcolormesh(F[:int(N/2), :int(N/2)], cmap='plasma', edgecolors="k", shading='flat')

plt.subplot(3, 2, 4)
plt.pcolormesh(F[:int(N/2), int(N/2):], cmap='plasma', edgecolors="k", shading='flat')

plt.subplot(3, 2, 5)
plt.pcolormesh(F[int(N/2):, :int(N/2)], cmap='plasma', edgecolors="k", shading='flat')

plt.subplot(3, 2, 6)
plt.pcolormesh(F[:int(N/2):, :int(N/2):], cmap='plasma', edgecolors="k", shading='flat')
plt.show()