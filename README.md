# SYS Assignment - Twitter clone

## Introduction

This assignment was made to practice and display skills regarding the material of the System Integration class.
The repository contains a Twitter clone, where the functionality is scoped only to users creating tweets and accessing eachother's profile.

## Bounded Contexts

We identified 3 bounded contexts in our scope. Namely
- user management
- tweet management
- accessing profiles (accessing user data and a user's tweets)
  Based on these, we have created 3 services. These correspond to each context, as it is assumed that these services encapsulate business logic well.

## Architecture
The diagram of the project's architecture can be found [here](https://drive.google.com/file/d/1iESJ07-CODezmV6Ky1pACcmP-Izng_yF/view?usp=sharing)

## Docker
Each component is independently Dockerized. Docker Compose is also set up and configured. The project can be started with Docker Compose via issuing the command below, from the root of the repository.
```
docker-compose up
```
Before running the command, make sure that the Docker daemon is running and that you have intranet and extranet networks set up in Docker.
Set them up via:
```
docker network create intranet
docker network create extranet
```
## Deployment Strategy
**RabbitMQ**
- **Health Check**: RabbitMQ is checked for health via 
    ```
    rabbitmqctl status
    ```
- **Ports**: RabbitMQ is exposed on port 5672 and 15672, same as the default internal ports.
- **Networks**: RabbitMQ is connected to the intranet network. (A non-exposed network for internal communication.)

**Gateway**
- **Ports**: Ocelot Gateway is exposed on port 80, same as the default internal port.
- **Dependencies**: Ocelot Gateway depends on the ProfileService, UserService and TweetService.
- **Networks**: Ocelot Gateway is connected to the intranet and the extranet network. (A network for external communication.)

**ProfileService**
- **Ports**: ProfileService is exposed on port 81, it's internal port is 80.
- **Dependencies**: ProfileService depends on RabbitMQ.
- **Networks**: ProfileService is connected to the intranet network.

**UserService**
- **Ports**: UserService is exposed on port 82, it's internal port is 80.
- **Dependencies**: UserService depends on RabbitMQ.
- **Networks**: UserService is connected to the intranet network.

**TweetService**
- **Ports**: TweetService is exposed on port 83, it's internal port is 80.
- **Dependencies**: TweetService depends on RabbitMQ.
- **Networks**: TweetService is connected to the intranet network.

### Network Configuration
- **Intranet Network**: This network is used for internal communication between the services. It is not exposed to the outside world.
- **Extranet Network**: This network is used for external communication. It is exposed to the outside world.

### Environment Variables

The environment variables are set in the docker-compose.yml file. The environment variables are set for the services to communicate with each other.
These may be changed to suit the needs of the deployment environment, but may be done with caution, so that the services can communicate with each other.
  

## Communication Protocols
- **HTTP** is used between most services.
- **Messaging** (via RabbitMQ) is utilised between the Profile, User and Tweet services, to update the ProfileService's cache in a "push" manner.
