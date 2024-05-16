pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building the application...'
                sh "docker-compose build"
                echo 'application build image completed.'
            }
        }
        stage('Deploy') {
            steps {
                sh 'docker-compose up -d'
                echo 'application deployment completed.'
            }
        }
    }
}
