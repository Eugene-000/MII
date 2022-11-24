import numpy as np
import pandas as pnd
import csv
import random as rand
import matplotlib.pyplot as plt

titles = [
    'Табельный номер',
    'Фамилия И.О.',
    'Пол',
    'Год рождения',
    'Год начала работы в компании',
    'Подразделение',
    'Должность',
    'Оклад',
    'Количество выполненных проектов'
]

divisions = [
    'Отдел информационной безопасности',
    'Отдел поддержки ИТ',
    'Отдел развития ИТ',
    'Отдел разработки ПО',
    'Отдел контроля и качества ИТ'
]

positions = [
    'Программист JavaScript',
    'Web-дизайнер',
    'Программист PHP',
    'Программист Swift',
    'Программист Android',
    'Сетевой инженер',
    'Программист Python',
    'Руководитель отдела IT',
    'Системный аналитик',
    'Тестировщик',
    'DevOps'
]

gender = [
    'Мужской',
    'Женский'
]

male_first_name = [
    'Евгений', 'Андрей', 'Илья', 'Игорь', 'Артём', 'Павел', 'Артур',
    'Алексей', 'Дмитрий', 'Денис', 'Даниил', 'Тимур', 'Владимир',
    'Николай', 'Сергей', 'Всеволод', 'Рафаэль', 'Егор', 'Руслан',
    'Салават', 'Иван', 'Максим', 'Роман', 'Олег', 'Никита', 'Александр'
]

male_last_name = [
    'Грязнов', 'Абрамов', 'Гаврилин', 'Глухов', 'Родионов', 'Захаров',
    'Колчанов', 'Будилин', 'Ефремов', 'Винаев', 'Ивлев', 'Афанасьев',
    'Чекмарёв', 'Ульянин', 'Кириллов', 'Карташов', 'Фокин', 'Равдин',
    'Комаренко', 'Пугатов', 'Миничкин', 'Валиев', 'Азмуханов', 'Цыпарев'
]

male_patronymic = [
    'Борисович', 'Вадимович', 'Васильевич', 'Григорьевич', 'Геннадиевич', 'Глебович',
    'Даниилович', 'Денисович', 'Егорович', 'Евгениевич', 'Захарович', 'Иванович',
    'Леонидович', 'Матвеевич', 'Миронович', 'Моисеевич', 'Никитич', 'Николаевич',
    'Олегович', 'Павлович', 'Петрович', 'Викторович', 'Робертович', 'Виталиевич'
]

female_first_name = [
    'Альбина', 'Аврора', 'Анастасия', 'Ангелина', 'Анжелика', 'Анна',
    'Антонина', 'Белла', 'Валентина', 'Виктория', 'Варвара', 'Вера',
    'Галина', 'Дарья', 'Диана', 'Ева', 'Екатерина', 'Елизавета',
    'Жанна', 'Ирина', 'Кристина', 'Ксения', 'Мария', 'Наталья'
]

female_last_name = [
    'Иванова', 'Петрова', 'Смирнова', 'Кузнецова', 'Васильева', 'Попова',
    'Новикова', 'Волкова', 'Романова', 'Козлова', 'Соколова', 'Андреева',
    'Морозова', 'Николаева', 'Михайлова', 'Павлова', 'Алексеева', 'Макарова',
    'Сергеева', 'Александрова', 'Степанова', 'Никитина', 'Лебедева', 'Зайцева'
]

female_patronymic = [
    'Александровна', 'Алексеевна', 'Анатольевна', 'Андреевна', 'Антоновна', 'Богдановна',
    'Борисовна', 'Валентиновна', 'Валерьевна', 'Васильевна', 'Викторовна', 'Витальевна',
    'Владимировна', 'Вячеславовна', 'Георгиевна', 'Денисовна', 'Ивановна', 'Игоревна',
    'Михайловна', 'Николаевна', 'Олеговна', 'Павловна', 'Романовна', 'Сергеевна'
]


def data_writing():
    amount_of_rows = rand.randint(1000, 1500)
    with open('data.csv', 'w', encoding='utf-16', newline='') as file:
        file_writer = csv.DictWriter(file, fieldnames=titles)
        file_writer.writeheader()
        for number_of_row in range(1, amount_of_rows + 1):
            gen = rand.choice(gender)
            if gen == 'Женский':
                full_name = rand.choice(female_last_name) + ' ' + rand.choice(female_first_name) + ' ' + rand.choice(
                    female_patronymic)
            elif gen == 'Мужской':
                full_name = rand.choice(male_last_name) + ' ' + rand.choice(male_first_name) + ' ' + rand.choice(
                    male_patronymic)
            year_of_birth = rand.randint(1960, 2000)
            year_of_start_work = year_of_birth + rand.randint(18, 22)
            division = rand.choice(divisions)
            position = rand.choice(positions)
            salary = rand.randint(15000, 100000)
            amount_of_project: int = rand.randint(1, 50)
            file_writer.writerow({
                'Табельный номер': number_of_row,
                'Фамилия И.О.': full_name,
                'Пол': gen,
                'Год рождения': year_of_birth,
                'Год начала работы в компании': year_of_start_work,
                'Подразделение': division,
                'Должность': position,
                'Оклад': salary,
                'Количество выполненных проектов': amount_of_project
            })


def create_numpy_statistics():
    with open('data.csv', 'r', encoding='utf-16') as file:
        amounts_of_projects = []
        salaries = []
        years_of_birth = []
        years_of_start_work = []
        post = []
        full_names_of_men = []
        full_names_of_women = []

        file_reader = csv.DictReader(file)
        for row in file_reader:
            amounts_of_projects.append(int(row['Количество выполненных проектов']))
            salaries.append(int(row['Оклад']))
            years_of_birth.append(int(row['Год рождения']))
            years_of_start_work.append(int(row['Год начала работы в компании']))
            post.append(row['Должность'])

            if row['Пол'] == 'Мужской':
                full_names_of_men.append(row['Фамилия И.О.'])
            else:
                full_names_of_women.append(row['Фамилия И.О.'])

        amount_of_project, counts = np.unique(amounts_of_projects, return_counts=True)
        ind_mode_amount_of_project = np.argmax(counts)

        print('')
        print('--------------Основные статистические характеристики с использованием Numpy--------------')
        print(f'Количество сотрудников: {np.count_nonzero(salaries)} чел.')
        print(f'Количество мужчин: {np.count_nonzero(full_names_of_men)} чел.')
        print(f'Количество женщин: {np.count_nonzero(full_names_of_women)} чел.')
        print('')
        print(f'Год рождения самого возрастного сотрудника: {np.max(years_of_birth)} год')
        print(f'Год рождения самого молодого сотрудника: {np.min(years_of_birth)} год')
        print('')
        print(f'Количество должностей: {len(np.unique(post))}')
        print('')
        print(f'Среднее арифметическое значение оклада: {round(np.mean(salaries), 2)} руб.')
        print(f'Размах оклада: {np.ptp(salaries)} руб.')
        print(f'Медианное значение оклада: {round(np.median(salaries))} руб.')
        print(f'Стандартное отклонение оклада: {round(np.std(salaries), 2)} руб.')
        print(f'Дисперсия оклада: {round(np.var(salaries), 2)} руб.')
        print('')
        print(f'Суммарное количество выполненных проектов: {np.sum(amounts_of_projects)} шт.')
        print(f'Среднее количество выполненных проектов сотрудника: {round(np.average(amounts_of_projects))} шт.')
        print(f'Мода количества выполненных проектов: {amount_of_project[ind_mode_amount_of_project]} шт.')
        print('')


def create_pandas_statistics():
    dataframe = pnd.read_csv('data.csv', encoding='utf-16')

    sum_amounts_of_project = dataframe["Количество выполненных проектов"].sum()
    avg_amount_of_project = round(sum_amounts_of_project / dataframe["Количество выполненных проектов"].count())

    amounts_of_project = dataframe['Количество выполненных проектов'].value_counts()
    mode_amount_of_project = amounts_of_project.idxmax()

    print('')
    print('--------------Основные статистические характеристики с использованием Pandas--------------')
    print(f'Количество сотрудников: {dataframe["Табельный номер"].count()} чел.')
    print(f'Количество мужчин: {len(dataframe[dataframe.Пол == "Мужской"])} чел.')
    print(f'Количество женщин: {len(dataframe[dataframe.Пол == "Женский"])} чел.')
    print('')
    print(f'Год рождения самого возрастного сотрудника: {dataframe["Год рождения"].max()} год')
    print(f'Год рождения самого молодого сотрудника: {dataframe["Год рождения"].min()} год')
    print('')
    print(f'Количество должностей: {dataframe["Должность"].nunique()}')
    print('')
    print(f'Среднее арифметическое значение оклада: {round(dataframe["Оклад"].mean(), 2)} руб.')
    print(f'Размах оклада: {dataframe["Оклад"].max() - dataframe["Оклад"].min()} руб.')
    print(f'Медианное значение оклада: {round(dataframe["Оклад"].median())} руб.')
    print(f'Стандартное отклонение оклада: {round(dataframe["Оклад"].std(), 2)} руб.')
    print(f'Дисперсия оклада: {round(dataframe["Оклад"].var(), 2)} руб.')
    print('')
    print(f'Суммарное выполненных количество проектов: {sum_amounts_of_project} шт.')
    print(f'Среднее количество выполненных проектов на одного сотрудника: {avg_amount_of_project} шт.')
    print(f'Мода количества выполненных проектов: {mode_amount_of_project} шт.')


def create_charts():
    dataframe = pnd.read_csv('data.csv', encoding='utf-16')

    with open('data.csv', 'r', encoding='utf-16') as file:
        salary_information_security_department = []
        salary_support_department = []
        salary_development_department = []
        salary_software_development_department = []
        salary_control_quality_department = []
        salary_avg_all_department = []

        file_reader = csv.DictReader(file)
        for row in file_reader:
            if row['Подразделение'] == 'Отдел информационной безопасности':
                salary_information_security_department.append(int(row['Оклад']))
            elif row['Подразделение'] == 'Отдел поддержки ИТ':
                salary_support_department.append(int(row['Оклад']))
            elif row['Подразделение'] == 'Отдел развития ИТ':
                salary_development_department.append(int(row['Оклад']))
            elif row['Подразделение'] == 'Отдел разработки ПО':
                salary_software_development_department.append(int(row['Оклад']))
            elif row['Подразделение'] == 'Отдел контроля и качества ИТ':
                salary_control_quality_department.append(int(row['Оклад']))

    salary_avg_all_department.append(round(np.mean(salary_information_security_department)))
    salary_avg_all_department.append(round(np.mean(salary_support_department)))
    salary_avg_all_department.append(round(np.mean(salary_development_department)))
    salary_avg_all_department.append(round(np.mean(salary_software_development_department)))
    salary_avg_all_department.append(round(np.mean(salary_control_quality_department)))

    plt.plot(dataframe['Количество выполненных проектов'], 'g')
    plt.title('Количество выполненных проектов сотрудников', loc='center')
    plt.xlabel('Cотрудники', color='gray')
    plt.ylabel('Количество выполненных проектов', color='gray')
    plt.xticks(rotation=25)
    plt.yticks(rotation=25)
    plt.grid(True)
    plt.show()

    plt.hist(dataframe['Должность'], bins=21, orientation="horizontal", alpha=0.5)
    plt.title('Количество сотрудников каждой должности', loc='center')
    plt.xlabel('Количество', color='gray')
    plt.ylabel('Должности', color='gray')
    plt.show()

    plt.barh(dataframe['Подразделение'].unique(), salary_avg_all_department, color='y', align='center')
    plt.title('Средний оклад сотрудников по подразделениям')
    plt.xlabel('Оклад', color='gray')
    plt.ylabel('Подразделения', color='gray')
    plt.yticks(fontsize=5, color='r')
    plt.xticks(fontsize=10, color='r')
    plt.show()


data_writing()
create_numpy_statistics()
create_pandas_statistics()
create_charts()

