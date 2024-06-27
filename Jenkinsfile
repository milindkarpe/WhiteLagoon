pipeline {
    agent any
    
   // tools {
        // Install the .NET Core SDK
   //     dotnet 'dotnet-sdk'
   // }

    stages {
        stage('Checkout') {
            steps {
                // Checkout code from the repository
                git url: 'https://github.com/your-repo/your-project.git', branch: 'main'
            }
        }
        stage('Restore') {
            steps {
                // Restore NuGet packages
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                // Build the project
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Test') {
            steps {
                // Run unit tests
                sh 'dotnet test --configuration Release'
            }
        }
        stage('Publish') {
            steps {
                // Publish the project
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }
    }
}
