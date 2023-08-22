# Домашнее задание
## Основы работы с Kubernetes

1. В папке `kubernetes` располагаются манифесты Deployment, Service, Ingress

Для их запуска в директории `kubernetes` нужно выполнить команду:

```shell
kubectl apply -f .
```

2. Запрос можно открыть по ссылке:
[ http://arch.homework/otusapp/max-ognev/health](http://arch.homework/otusapp/max-ognev/health])


3. В сервисе доступны запросы:

```
- "/health" - адрес запроса готовности сервиса. Возвращает:
```json
Status 200 OK
{
    "status": "OK"
}
```
## Документация 

1. Устанваливанием Minikube

```shell
minikube status
minikube --help
minikube ip
minikube dashboard
```

-- Стартуем миникуб (используем драйвер vmware/hyperv - через него получается адекватно пробрасывать IP)
Если использовать драйвер докер то могут возникнуть проблемы при использовании minikube IP в /etc/hosts

```shell
minikube start --cpus=6 --memory=6g --vm-driver=hyperv
```


2. Основные команды 

- kubectl create -f pod.yml  - создает сущность
- kubectl apply -f - создает или обновляет все сущности из файла
- kubectl get pods --all-namespaces - получает список всех подов
- kubectl get services --all-namespaces - получает списко всех сервисов
- kubectl api-resources - получает список всех ресурсов
- kubectl delete -f pod.yml - удаляет из файла
- kubectl delete pods --all - удаляет все поды
- kubectl scale --replicas=2 replicaset myrs - устанваливает кол-во реплик
- kubectl describe rs myrs - выводит информацию по сущности 
- kubectl rollout undo deployment my-dep - откатываем изменения
- kubectl port-forward mypod 8000:80 & - перенаправляет порт 8000 хоста на 80 порт пода
- kubectl exec -it mypod -- bash - войти в bash на поде

[Шпаргалка по командам Kubernetes](https://kubernetes.io/ru/docs/reference/kubectl/cheatsheet/)

3. Если Ingress не поднимается проверяем по инуструкции

```
Here’s what worked for me:

minikube start

minikube addons enable ingress

minikube addons enable ingress-dns

Wait until you see the ingress-nginx-controller-XXXX is up and running using Kubectl get pods -n ingress-nginx

Create an ingress using the K8s example yaml file

Update the service section to point to the NodePort Service that you already created

Append 127.0.0.1 hello-world.info to your /etc/hosts file on MacOS (NOTE: Do NOT use the Minikube IP)

Run minikube tunnel ( Keep the window open. After you entered the password there will be no more messages, and the cursor just blinks)

Hit the hello-world.info ( or whatever host you configured in the yaml file) in a browser and it should work
```

Важный момент 

Append 127.0.0.1 hello-world.info to your /etc/hosts file on MacOS **(NOTE: Do NOT use the Minikube IP)**

4. Полезные советы 
```
kubeclt complition - создает полезные alias в разлиных оболочках
``` 