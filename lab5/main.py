import numpy as np
import pandas as pd
from sklearn.metrics import classification_report, accuracy_score, confusion_matrix
from sklearn.preprocessing import StandardScaler, MinMaxScaler
from sklearn.naive_bayes import GaussianNB
from sklearn.svm import SVC
from sklearn.linear_model import SGDClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split, GridSearchCV
from matplotlib import pyplot as pl, pyplot as plt
import seaborn as sns


def vizualization_pred(x_train, x_test, y_train, y_test):
    nb_pred = native_bayes(x_train, x_test, y_train, y_test)
    svm_pred = support_vector_machines(x_train, x_test, y_train, y_test)
    sgd_pred = stochastic_gradient_descent(x_train, x_test, y_train, y_test)
    rf_pred = random_forest(x_train, x_test, y_train, y_test)

    print("\nОтчет о наивной Байесовской классификации:")
    print(classification_report(y_test, nb_pred))

    nb_accuracy = accuracy_score(y_test, nb_pred)
    print(f"\nТочность Наивного Байесовского классификатора: {nb_accuracy}")

    plt.figure()
    sns.heatmap(confusion_matrix(y_test, nb_pred), annot=True)
    plt.xlabel("Predicted Risk of heart attack")
    plt.ylabel("True Risk of heart attack")
    plt.title("Naive Bayes Classifier Confusion Matrix")
    plt.show()

    print("\nОтчет о классификации опорных векторов:")
    print(classification_report(y_test, svm_pred))

    svm_accuracy = accuracy_score(y_test, svm_pred)
    print(f"\nТочность классификатора Опорные вектора: {svm_accuracy}")

    plt.figure()
    sns.heatmap(confusion_matrix(y_test, svm_pred), annot=True)
    plt.xlabel("Predicted Risk of heart attack")
    plt.ylabel("True Risk of heart attack")
    plt.title("Support Vector Machines Confusion Matrix")
    plt.show()

    print("\nОтчет о классификации стохастического градиентного спуска:")
    print(classification_report(y_test, sgd_pred, zero_division=0))

    sgd_accuracy = accuracy_score(y_test, sgd_pred)
    print(f"\nТочность классификатора Стохастический Градиентный Спуск: {sgd_accuracy}")

    plt.figure()
    sns.heatmap(confusion_matrix(y_test, sgd_pred), annot=True)
    plt.xlabel("Predicted Risk of heart attack")
    plt.ylabel("True Risk of heart attack")
    plt.title("Stochastic Gradient Descent Classifier Confusion Matrix")
    plt.show()

    print("\nОтчет о классификации случайного леса:")
    print(classification_report(y_test, rf_pred))

    rf_accuracy = accuracy_score(y_test, rf_pred)
    print(f"\nТочность классификатора Случайный Лес: {rf_accuracy}")

    plt.figure()
    sns.heatmap(confusion_matrix(y_test, rf_pred), annot=True)
    plt.xlabel("Predicted Risk of heart attack")
    plt.ylabel("True Risk of heart attack")
    plt.title("Random Forest Classifier Confusion Matrix")
    plt.show()


def native_bayes(x_train, x_test, y_train, y_test):
    nb = GaussianNB()
    nb.fit(x_train, y_train)
    pred = nb.predict(x_test)
    return pred


def support_vector_machines(x_train, x_test, y_train, y_test):
    grid = {"C": np.arange(1, 10, 1),
            'gamma': [0.00001, 0.00005, 0.0001, 0.0005, 0.001, 0.005, 0.01, 0.05, 0.1, 0.5, 1, 5]}
    svm0 = SVC(random_state=42)
    svm_cv = GridSearchCV(svm0, grid, cv=10)
    svm_cv.fit(x, y)
    svm = SVC(C=svm_cv.best_params_["C"], gamma=svm_cv.best_params_["gamma"], random_state=42)
    svm.fit(x_train, y_train)
    pred = svm.predict(x_test)
    return pred


def stochastic_gradient_descent(x_train, x_test, y_train, y_test):
    sgd = SGDClassifier()
    sgd.fit(x_train, y_train)
    pred = sgd.predict(x_test)
    return pred


def random_forest(x_train, x_test, y_train, y_test):
    rf = RandomForestClassifier()
    rf.fit(x_train, y_train)
    pred = rf.predict(x_test)
    return pred


def selection_features(df):
    label = df["Risk of heart attack"]
    df.drop("Risk of heart attack", axis=1, inplace=True)

    categorical_features = [
        "Sex",
        "Chest pain type",
        "Fasting blood sugar",
        "Resting electrocardiographic results",
        "Exercise induced angina",
        "Slope of the peak exercise ST segment",
        "Thall rate"
    ]
    df[categorical_features] = df[categorical_features].astype("category")

    continuous_features = set(df.columns) - set(categorical_features)
    scaler = MinMaxScaler()
    df_norm = df.copy()
    df_norm[list(continuous_features)] = scaler.fit_transform(df[list(continuous_features)])

    clf = RandomForestClassifier()
    clf.fit(df_norm, label)

    plt.figure(figsize=(12, 12))
    plt.barh(df_norm.columns, clf.feature_importances_, color='#ffa420')
    plt.title("Отбор признаков с использованием случайного леса (Random Forest)")
    plt.yticks(fontsize=6)
    pl.show()


train = pd.read_csv("heart.csv", delimiter=',')

train.rename(columns=
    {
        'age': 'Age',
        'sex': 'Sex',
        'cp': 'Chest pain type',
        'trtbps': 'Resting blood pressure',
        'chol': 'Serum cholestoral',
        'fbs': 'Fasting blood sugar',
        'restecg': 'Resting electrocardiographic results',
        'thalachh': 'Maximum heart rate',
        'exng': 'Exercise induced angina',
        'oldpeak': 'Previous peak',
        'slp': 'Slope of the peak exercise ST segment',
        'caa': 'Major vessels colored by flourosopy',
        'thall': 'Thall rate',
        'output': 'Risk of heart attack'
    }, inplace=True)

datafream = train.copy()

# отбор признаков по их важности
selection_features(datafream)

train.drop(['Age'], axis=1, inplace=True)
train.drop(['Sex'], axis=1, inplace=True)
train.drop(['Resting blood pressure'], axis=1, inplace=True)
train.drop(['Serum cholestoral'], axis=1, inplace=True)
train.drop(['Fasting blood sugar'], axis=1, inplace=True)
train.drop(['Resting electrocardiographic results'], axis=1, inplace=True)
train.drop(['Exercise induced angina'], axis=1, inplace=True)
train.drop(['Slope of the peak exercise ST segment'], axis=1, inplace=True)

print("\nОбобщённая информация:")
print(train.info())

# проверка наличия дубликатов и их удаление
duplicated_rows = train[train.duplicated()]
print(f"\nКоличество повторяющихся записей: {len(duplicated_rows)}")
train = train.drop_duplicates()

categories = [
    "Chest pain type",
    "Thall rate"
]
train[categories] = train[categories].astype("category")
print("\nОбобщённая информация:")
print(train.info())

# разделение на набор для обучения и тестирования
y = train['Risk of heart attack']
train.drop(['Risk of heart attack'], axis=1, inplace=True)

x = train.iloc[::].values
x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=0.2, random_state=2)

scaler = StandardScaler()
scaler.fit_transform(x_train)
scaler.fit_transform(x_test)

# запуск работы классификаторов и их визуализация
vizualization_pred(x_train, x_test, y_train, y_test)
