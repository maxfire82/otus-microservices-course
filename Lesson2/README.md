# Домашнее задание
## Основы работы с Docker

1. Для запуска из католога Lesson2 выполнить команду 
```
docker build --platform linux/amd64 -t maximognev/otus-lesson:docker -f .\docker\Dockerfile .
```

2. В сервисе доступны запросы:
```
- "/health" - адрес запроса готовности сервиса. Возвращает:
```json
Status 200 OK
{
    "status": "OK"
}
```

3. Основные команды
```
docker images - показывает список образов
docker docker push maximognev/otus-lesson:docker  - отправляет образ на docker hub
docker ps - показывает список контейнеров
docker rm - удаляет образ Docker из системы
docker build - собирает образ с нуля
docker create - создание контейнера из образа
docker start 49920 - запуск контейнера через ID 
docker run - создает и запускает контейнер
docker stop - останавливает работу контейнера
```