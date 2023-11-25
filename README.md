# ChallengeN5

WebApi for registering user permissions, 

Created with Clean Architecture using Domain Driven Design, uses patterns such as UnitOfWork, CQRS, Mediator;

Persist data on SQLServer via EntityFramework Core(code first approach), for logging 

Uses Serilog with elasticSearch, every operation is persisted in a apache kafka queue

Has unit tests for business logic and entry points by users 

Has integration tests for database interaction, end to end focused
Its prepared to be conteinerized, set it up to run just using docker-compose, or so I hope hahaha

## Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Integration Tests Notes](#integration)

## Getting Started

Download repo
execute docker-compose up 

### Prerequisites

-dotnet 7
-docker

### Integration
The integration tests will fail if the elasticsearch and kafka components are not being executed, the scope of the integration test is to execute a request emulating as much as possible the final usage, however the persistence is completely in memory so as not to compromise the normal usage information. 

###### Disclaimer
In case of finding bad practices and possible problems please do not hesitate to post a comment and raise the issue, the goal is always to learn and no project is perfect. 
