# Паттерны поддержания консистентности данных (Stream processing)

## Теоретическая часть

Спроектировать взаимодействие сервисов при создании заказов. Предоставить варианты взаимодействий в следующих стилях в виде sequence диаграммы с описанием API на IDL:

1. Только HTTP взаимодействие

![HTTP взаимодействие](https://www.plantuml.com/plantuml/proxy?fmt=svg&src=https://raw.githubusercontent.com/maxfire82/otus-microservices-course/main/Lesson38/architecture/http_schema.puml)

2. Cобытийное взаимодействие с использование брокера сообщений для нотификаций (уведомлений)

![Комбинированное взаимодействие](https://www.plantuml.com/plantuml/proxy?fmt=svg&src=https://raw.githubusercontent.com/maxfire82/otus-microservices-course/main/Lesson38/architecture/http_event_schema.puml)

3. Event Collaboration cтиль взаимодействия с использованием брокера сообщений

![Событийное взаимодействие](https://www.plantuml.com/plantuml/proxy?fmt=svg&src=https://raw.githubusercontent.com/maxfire82/otus-microservices-course/main/Lesson38/architecture/event_schema.puml)

