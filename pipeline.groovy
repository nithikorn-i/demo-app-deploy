pipeline {
    agent any

    parameters {
        choice(name: 'ENV', choices: ['dev', 'ss', 'prod'], description: 'เลือก environment สำหรับ deploy')
    }

    environment {
        REGISTRY = "docker.io"
        IMAGE_NAME = "username/demo-app-deploy"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/nithikorn-i/demo-app-deploy.git'
            }
        }

        stage('Build & Push Docker Image') {
            steps {
                script {
                    docker.image('docker:24.0.5-cli').inside('-v /var/run/docker.sock:/var/run/docker.sock') {
                        dir("${env.WORKSPACE}") {
                            withCredentials([usernamePassword(
                                credentialsId: 'docker-hub',
                                usernameVariable: 'DOCKER_CRED_USR',
                                passwordVariable: 'DOCKER_CRED_PSW'
                            )]) {
                                // Login Docker Hub
                                sh """
                                    echo \$DOCKER_CRED_PSW | docker login -u \$DOCKER_CRED_USR --password-stdin \$REGISTRY
                                """

                                // Build Docker image
                                sh """
                                    docker build \
                                        --build-arg ANGULAR_ENV=${params.ENV} \
                                        -t \$REGISTRY/\$IMAGE_NAME:${params.ENV}-\$BUILD_NUMBER \
                                        .
                                """

                                // Push Docker image
                                sh """
                                    docker push \$REGISTRY/\$IMAGE_NAME:${params.ENV}-\$BUILD_NUMBER
                                """
                            }
                        }
                    }
                }
            }
        }
    }
}
