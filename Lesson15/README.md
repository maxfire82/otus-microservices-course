# Api Gateway

## Архитектура приложения

![architecture/schema.puml](http://www.plantuml.com/plantuml/proxy?fmt=svg&src=https://github.com/maxfire82/otus-microservices-course/blob/main/Lesson15/architecture/schema.puml)

В качестве APIGateway используется Traefik.
Аутентификации реализована на основе JWT токена.

Сервис `Auth` осуществляет за регистрацию и авторизацию аккаунта пользователя.
Сервис `Users` осуществляет управление профилем пользователя. Запросы в сервис осуществляются только с JWT токеном.

## Установка

Необходимо запустить скрипт `install.sh`

## Тестирование

Тесты лежат в папке `test`

```shell
newman run tests/users.json
```

## Полезное

Могут быть проблемы со сторонним Ingress котроллером. Необходимо переключится на контроллер от миникуба

```shell
minikube addons enable ingress
```

Helm

```shell
helm upgrade otus-demo otus
helm install otus-demo otus -n otus --create-namespace - создание неймспейса одновременно с установкой пакета
```

Кубернетес

```shell
-A эквивалентно --all-namespaces
kubectl logs [pods] -f - просмотр текущих логов
kubectl describe svc treafik - просмотр спецификации траефика
```

Minikube

```shell
minikube delete - удаление все профилей
minikube delete --profile minikube - удаление определенного профайла
minikube profile list - списко профилей миникуба
```
