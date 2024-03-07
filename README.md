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
## Deployment Strategy
TODO

## Communication Protocols
- **HTTP** is used between most services.
- **Messaging** (via RabbitMQ) is utilised between the Profile, User and Tweet services, to update the ProfileService's cache in a "push" manner.
