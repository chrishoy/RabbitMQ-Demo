## Sample Consumer-Publisher example using RabbitMQ
This is a simple example of a consumer-publisher model using RabbitMQ. 
The consumer is a simple C# console app that listens to a queue and prints messages to the console.
The publisher is a simple C# console app that sends messages to a queue.
- docker-compose.yml: This file contains the RabbitMQ configuration.
- src/consumer.csproj: This is the consumer project.
- src/publisher.csproj: This is the publisher project.

### How to run
1. Clone the repository
1. Modify `docker-compose.yml` to set the RabbitMQ persistent volume data location
1. Run `docker-compose up -d` to start RabbitMQ
1. Run the consumer project (Debug > Start new instance)
1. Run the publisher project (Debug > Start new instance)