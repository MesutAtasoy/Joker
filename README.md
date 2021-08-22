# Joker

Microservices Example!  ![main workflow](https://github.com/MesutAtasoy/Joker/actions/workflows/main.yml/badge.svg)


![alt text](https://github.com/MesutAtasoy/Joker/blob/main/src/WebApplications/Joker.WebApp/wwwroot/images/joker-web-ui-image.png)

Joker platform is location based campaign marketplace. An example of microservices container based application which implemented different approaches within each microservice (DDD, CQRS, Simple CRUD)

## Architecture Diagram
![alt text](https://github.com/MesutAtasoy/Joker/blob/main/src/WebApplications/Joker.WebApp/wwwroot/images/Diagram.jpg)

## Tech Stack
 All service projects are written with .Net 5 framework. gRPC, Consul, CAP, Rabbit MQ, Swagger, Ocelot, EF Core, Mongo, Elasticsearch, IdentityServer4, Automapper, FluentValidation.
 All infrastructure codes are separated each class library. It can be accessed below link.
 
 https://github.com/MesutAtasoy/Joker.Packages
 
 ## Project Setup 
 
1. Clone the repository 

`git clone https://github.com/MesutAtasoy/Joker.git` 

2. Create Docker Network


`docker network create joker-network` 

3. Build the containers

`docker-compose build` 

4. Buid ELK Stack 

`docker-compose -f docker-compose-elk.yml build` 

5. Run ELK stack

`docker-compose -f docker-compose-elk.yml up -d` 

6. Run the containers

`docker-compose up -d` 

 ## License
 
This project is licensed under the MIT License - see the LICENSE.md file for details.


 ## Template
Joker Web Application Template -> https://github.com/creativetimofficial/soft-ui-design-system

 ## Contributing
This project welcomes contributions and suggestions. When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

