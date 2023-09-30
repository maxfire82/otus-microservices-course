# Домашнее задание

## Основы работы с Kubernetes - 2

1. В папке `otus` находится чарт приложения. Чарт зависит от `postgresql` из репозитория `bitnami`.
Для установки чарта в папке `lesson-07` нужно выполнить команду:

```shell
helm install otus-demo otus
```

Во время установки чарта запускается `JOB` для выполнения миграции БД.

2. Тесты лежат в папке `test`

```shell
newman run tests/users.json
```

3. В сервисе доступны url:

- GET "/"

    ```json
    Status 200 OK
    {
        "status": "OK"
    }
    ```

- CRUD Users

## Документация

1. Основные команды

- helm create my_chart - создает шаблон пакета
- helm search repo postgresql - поиск чарта
- helm show values stable/kube-ops-view - показывает переменные для пакета kube-ops-view
- helm install kube-ops-view stable/kube-ops-view --namespace=kube-system -f values.yaml - устанваливает пакет с переменными из файла
- helm ls - показывает список установленных пакетов
- helm delete kube-ops-view - удаляет пакет из дефолтнного неймспейса
- helm dependency build bitnami/postgresql - для скачивания зависимостей
- helm upgrade bitnami/postgresql -f postgresql-values.yaml - обновление чарта
- helm repo add bitnami https://charts.bitnami.com/bitnami - добавление репозитория
- helm install otus-demo otus --dry-run

2. Полезные ссылки

- [Helm Best Practices](https://helm.sh/docs/chart_best_practices/labels/)

## Разработка

1.

- Установка Helm

```shell
choco install kubernetes-helm
```

- [Установка Newman](https://blog.postman.com/installing-newman-on-windows/)

2. Проект .Net core

При создании миграции EF необходимо указать стартовый проект
Запускаем из папки Domain проекта. --startup-project - указывает на место расположение Api проекта.

```shell
dotnet ef --startup-project ..\Otus.Api\ migrations add Init
dotnet ef --startup-project ..\Otus.Api\ database update
```

3. Проблема с чартом `postgresql` с некорректными даными авторизации лечится путем удалнения постоянного хранилища в kubernetes.
[Решение проблемы](https://stackoverflow.com/questions/63917524/helm-postgres-password-authentication-failed)

```shell
kubectl get pvc
kubectl delete pvc mypvc
```


curl --location 'http://arch.homework/otusapp/health' --header 'Host: arch.homework'