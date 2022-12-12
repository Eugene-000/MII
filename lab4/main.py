from collections import Counter
import csv
import pandas as pd
from matplotlib import pyplot as pl, pyplot as plt
from sklearn.metrics import accuracy_score
from sklearn.neighbors import KNeighborsClassifier
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler


fields = ["продукт", "сладость", "хруст", "класс"]

# Классы: 0 - Овощи, 1 - Фрукты, 2 - Протеин
data1 = [
    ['яблоко', 7, 6, 1],
    ['салат', 2, 5, 0],
    ['бекон', 1, 2, 2],
    ['банан', 9, 1, 1],
    ['рыба', 1, 1, 2],
    ['сыр', 1, 1, 2],
    ['виноград', 8, 1, 1],
    ['морковь', 2, 7, 0],
    ['апельсин', 6, 1, 1],
    ['груша', 8, 4, 1],
    ['картофель', 1, 7, 0],
    ['творог', 2, 1, 2],
    ['киви', 7, 2, 1],
    ['фасоль', 2, 2, 2],
    ['кукуруза', 4, 4, 0],
    ['манго', 7, 5, 1],
    ['хлеб', 1, 3, 2],
    ['лук', 1, 6, 0],
    ['дыня', 9, 4, 1],
    ['нектарин', 6, 4, 1]
]


# Классы: 0 - Овощи, 1 - Фрукты, 2 - Протеин, 3 - Орехи
data2 = [
    ['яблоко', 7, 6, 1],
    ['салат', 2, 5, 0],
    ['бекон', 1, 2, 2],
    ['банан', 9, 1, 1],
    ['кедровый орех', 3, 7, 3],
    ['рыба', 1, 1, 2],
    ['сыр', 1, 1, 2],
    ['виноград', 8, 1, 1],
    ['морковь', 2, 6, 0],
    ['фундук', 2, 9, 3],
    ['груша', 8, 4, 1],
    ['картофель', 1, 6, 0],
    ['творог', 2, 1, 2],
    ['киви', 7, 2, 1],
    ['фасоль', 2, 2, 2],
    ['грецкий орех', 1, 8, 3],
    ['кукуруза', 4, 4, 0],
    ['манго', 7, 4, 1],
    ['дыня', 9, 4, 1],
    ['арахис', 3, 8, 3],
]


def minkowski_distance(a, b, p=1):
    dim = len(a)
    distance = 0
    for d in range(dim):
        distance += abs(a[d] - b[d]) ** p
    distance = distance ** (1 / p)
    return distance


def knn_predict(x_train, x_test, y_train, y_test, count_classes, k, p):
    y_hat_test = []

    for test_point in x_test:
        distances = []

        for train_point in x_train:
            distance = minkowski_distance(test_point, train_point, p=p)
            distances.append(distance)

        df_dists = pd.DataFrame(data=distances, columns=['dist'], index=y_train.index)
        df_nn = df_dists.sort_values(by=['dist'], axis=0)[:k]
        counter = Counter(y_train[df_nn.index])
        prediction = counter.most_common()[0][0]
        y_hat_test.append(prediction)

    accuracy = accuracy_score(y_test, y_hat_test)
    print(f"\nТочность метрического классификатора c {count_classes} классами: {accuracy}, при тестовой выборке 20% "
          f"от всех данных и k = 3")
    return y_hat_test


def knn_sklearn(x_train, x_test, y_train, y_test, count_classes, k, p):
    clf = KNeighborsClassifier(n_neighbors=k, p=p)
    clf.fit(x_train, y_train)
    y_pred_test = clf.predict(x_test)
    accuracy = accuracy_score(y_test, y_pred_test)
    print(f"\nТочность классификатора sklearn c {count_classes} классами: {accuracy}, при тестовой выборке 20% от "
          f"всех данных и k = 3")
    return y_pred_test


def visualization(dataset, classifier):
    x = dataset.drop('класс', axis=1)
    x = dataset.drop('продукт', axis=1)
    y = dataset['класс']

    count_classes = len(Counter(y).keys())
    x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=0.20)
    scaler = StandardScaler()
    train_x = scaler.fit_transform(x_train)
    test_x = scaler.transform(x_test)

    classifier_predict = classifier(train_x, test_x, y_train, y_test, count_classes, k=3, p=1)

    fig, ax = pl.subplots(figsize=(6.5, 5))

    scatter = ax.scatter(x_train['сладость'][:], x_train['хруст'][:], c=x_train['класс'][:],
                         marker="o",
                         alpha=0.5)

    ax.scatter(x_test['сладость'][:], x_test['хруст'][:], c=classifier_predict,
               marker="x",
               alpha=0.5)

    legend = ax.legend(*scatter.legend_elements(), title="Классы")
    ax.add_artist(legend)
    ax.title.set_text(f"Классификация продуктов с {count_classes} классами")
    ax.set_xlabel("Сладость")
    ax.set_ylabel("Хруст")
    if count_classes == 3:
        ax.text(8.5, 7.5, "0 - Овощи\n1 - Фрукты\n2 - Протеин")
        ax.text(1, 7.7, "o - обучающая выборка\nх - тестовая выборка")
    elif count_classes == 4:
        ax.text(8.5, 9.5, "0 - Овощи\n1 - Фрукты\n2 - Протеин\n3 - Орехи")
        ax.text(1, 10, "o - обучающая выборка\nх - тестовая выборка")
    plt.show()


def preparation_datasets(file, data, fields_name):
    with open(file, 'w', newline='', encoding='utf-16') as w_f:
        writer = csv.writer(w_f)
        writer.writerow(fields_name)
        writer.writerows(data)

    dataset = pd.read_csv(file, encoding='utf-16')

    return dataset


dataset1 = preparation_datasets('dataset1.csv', data1, fields)
dataset2 = preparation_datasets('dataset2.csv', data2, fields)

visualization(dataset1, knn_predict)
visualization(dataset1, knn_sklearn)

visualization(dataset2, knn_predict)
visualization(dataset2, knn_sklearn)
