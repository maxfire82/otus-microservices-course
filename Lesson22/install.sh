# disable standerd ingress controller
minikube addons disable ingress

# setting otus-services namespace
kubectl create namespace otus-services
kubectl config set-context --current --namespace=otus-services

# add ingress-nginx repository for installing
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo add traefik https://helm.traefik.io/traefik
helm repo update

# install ingress-controller and postgres exporter like helm application
helm install nginx ingress-nginx/ingress-nginx -f ingress/nginx-ingress.yaml --namespace ingress-nginx --create-namespace --skip-crds

sleep 15s

helm install otus-demo otus

