# disable standerd ingress controller
minikube addons disable ingress

# add ingress-nginx repository for installing
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo add traefik https://helm.traefik.io/traefik
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update

# install ingress-controller and postgres exporter like helm application
helm install nginx ingress-nginx/ingress-nginx -f ingress/nginx-ingress.yaml --namespace ingress-nginx --create-namespace --skip-crds

# install kafka CRD
kubectl create namespace kafka
kubectl create -f 'https://strimzi.io/install/latest?namespace=kafka' -n kafka
kubectl apply -f .\kafka\kafka.yaml -n kafka
kubectl wait kafka/otus-project --for=condition=Ready --timeout=300s -n kafka 

sleep 5s

# setting otus-services namespace
kubectl create namespace otus-services
kubectl config set-context --current --namespace=otus-services

helm install otus-project otus

