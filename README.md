# Joker

Microservices Example!

![alt text](https://github.com/MesutAtasoy/Joker/blob/main/src/WebApplications/Joker.WebApp/wwwroot/images/joker-web-ui-image.png)

Joker is location based campaign marketplace. Merchants publish campaign, the users search campaign which they like.

## Architecture Diagram
![alt text](https://github.com/MesutAtasoy/Joker/blob/main/src/WebApplications/Joker.WebApp/wwwroot/images/Diagram.jpg)

## Tech Stack
 All service projects are written with .Net 5 framework. gRPC, Consul, CAP, Rabbit MQ, Swagger, Ocelot, EF Core, Mongo, Elasticsearch. 
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



