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
- [docker images [OPTIONS] [REPOSITORY[:TAG]]](https://docs.docker.com/engine/reference/commandline/ps/) - показывает список образов
- [docker images [OPTIONS] [REPOSITORY[:TAG]]](https://docs.docker.com/engine/reference/commandline/images/) - показывает список образов
- [docker push maximognev/otus-lesson:docker](https://docs.docker.com/engine/reference/commandline/push/)  - отправляет образ на docker hub
- [docker ps [OPTIONS]](https://docs.docker.com/engine/reference/commandline/ps/) - показывает список контейнеров
- [docker rm [OPTIONS] CONTAINER [CONTAINER...]](https://docs.docker.com/engine/reference/commandline/rm/) - удаляет образ Docker из системы
- [ddocker build [OPTIONS] PATH | URL | -](https://docs.docker.com/engine/reference/commandline/build/) - собирает образ с нуля
- [docker create [OPTIONS] IMAGE [COMMAND] [ARG...]](https://docs.docker.com/engine/reference/commandline/create/) - создание контейнера из образа
- [docker start [OPTIONS] CONTAINER [CONTAINER...]](https://docs.docker.com/engine/reference/commandline/start/) - запуск контейнера через ID 
- [docker run [OPTIONS] CONTAINER [CONTAINER...]](https://docs.docker.com/engine/reference/commandline/run/) - создает и запускает контейнер
- [docker stop [OPTIONS] CONTAINER [CONTAINER...]](https://docs.docker.com/engine/reference/commandline/stop/) - останавливает работу контейнера
- [docker logs [OPTIONS] CONTAINER](https://docs.docker.com/engine/reference/commandline/logs/) - извлекает логи из контейнера 
- [docker inspect [OPTIONS] NAME|ID [NAME|ID...]](https://docs.docker.com/engine/reference/commandline/logs/)  - возвращает низкоуровневую информацию об объектах Docker
