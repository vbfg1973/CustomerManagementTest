# Customer Management API

## Plan

- Vertical slices (DTOs, validation, messages, handlers, etc all together with the feature they support)
- "Production" ready. 
  - Swagger
  - Versioning support
  - Dockerised
  - Supporting DB
  - health check route
  - docker-compose for ease of local running
  - Basic correlation-id support
  - logging locally to console and SEQ in a parallel running container
   
## Implemented

- Routes for creation of customers
- Contact details (on customer creation, DB supports more but routes and functionality not implemented)
- Address management
  - Can add additional addresses for the customer
  - Can set an address to be the default
  
## Issues

- Obviously not a full app
- I am opinionated on the matter but not a UI guy, and so no UI.
- I did go over the time by some way (probably 12 hours total effort)
- No validation except that provided by WebAPI naturally via interpretation of nullable properties.
- The data model for the address <-> customer relationship is wrong. It should be *one to many* customer to addresses. It is *many to many*. I've left this in place.
- There are creds in the docker-compose.yml for connecting to the DB. This is for your convenience of running it. This ought to be a shooting offence in a real repository.
- Testing is limited to ensuring that controllers and DTOs have documentation comments attached. This is to make sure swagger documentation is available.

## To run it

On a docker capable machine simply run:

```
docker-compose up
```

This should pull all necessary images. 

The API itself is on port 51770.

It is running in Development mode inside the container so that Swagger is available. You can get at the swagger docs here:

http://localhost:51770/swagger/index.html

All logging occurs to console.

It is also browsable via SEQ, which is available here:

http://localhost/#/events

Note that docker networks and volumes created have "chrisrussell" or "CHRISRUSSELL" in their names. This should assist with cleaning up resources that kick around later.

## Usage

The concepts are:

customers
contact details
addresses

When running, 

http://localhost:51770/api/v1/customer/

You should see a customer called "Mr Darcy Carter" with multiple contact details and multiple addresses.

There are routes supporting:

- Adding customers
- Adding addresses to existing customers
- Setting an address to be the customer's default (thereby unsetting any previous default addresses)
- Searching for customers
- Retrieving a specific customer
- Deleting a specific customer
