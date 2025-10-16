pipeline {
    agent any

    parameters {
        choice(name: 'ENV', choices: ['dev', 'ss', 'prod'], description: 'เลือก environment สำหรับ deploy')
    }

    environment {
        IMAGE_NAME      = "demo-app-deploy"
        IMAGE_TAG       = "latest"
        GHCR_REPO       = "ghcr.io/nithikorn-i/demo-app-deploy"
        K8S_NAMESPACE   = "${params.ENV}"
        DEPLOYMENT_FILE = "/data/deploy-test/deployment.yaml"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'deploy', url: 'https://github.com/nithikorn-i/demo-app-deploy.git'
            }
        }

        stage('Clean old images safely') {
            steps {
                sh '''
                    echo "🧹 Cleaning old Docker images..."
                    docker images | grep ${IMAGE_NAME} | grep latest | awk '{print $3}' | xargs -r docker rmi -f
                '''
            }
        }

        stage('Build & Push Docker Image') {
            steps {
                withCredentials([string(credentialsId: 'ghcr-token', variable: 'GH_TOKEN')]) {
                    sh """
                        echo $GH_TOKEN | docker login ghcr.io -u nithikorn-i --password-stdin
                        docker build \
                            --build-arg ANGULAR_ENV=${params.ENV} \
                            --build-arg DOTNET_ENV=${params.ENV} \
                            -t $GHCR_REPO:latest .
                        docker push $GHCR_REPO:latest
                        docker rmi -f $GHCR_REPO:latest || true
                    """
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                withKubeConfig([credentialsId: 'kubeconfig-cred']) {
                    sh """
                        if kubectl get deployment ${IMAGE_NAME} -n ${K8S_NAMESPACE} >/dev/null 2>&1; then
                            echo "⚡ Deployment exists, updating image..."
                            kubectl set image deployment/${IMAGE_NAME} ${IMAGE_NAME}=$GHCR_REPO:latest -n ${K8S_NAMESPACE}
                        else
                            echo "✨ Deployment not found, creating..."
                            kubectl apply -f ${DEPLOYMENT_FILE} -n ${K8S_NAMESPACE} --validate=false
                        fi

                        kubectl rollout status deployment/${IMAGE_NAME} -n ${K8S_NAMESPACE}
                    """
                }
            }
        }
    }
}
