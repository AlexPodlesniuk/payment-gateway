# payment-gateway
checkout.com home challange

# How to run

Using docker compose:
  1. clone branch
  2. cd into root folder
  3. run `docker-compose up`
  
After, you will have next services running:
  1. Payment (http://localhost:15101/api/v1)
  2. Acquisition (http://localhost:15100/api/v1)
  3. Mock BankApi (http://localhost:15102/api/v1)
  4. MongoDb (default)
  5. RabbitMq (default)
  
  
OR manual run (in case docker compose will fail)
  1. Run MongoDb and RabbitMq in container (you can still run them via original docker compose)
  2. Start PaymentGateway.Payments.Api (http://localhost:5119/)
  3. Start PaymentGateway.Acquisition.Api (http://localhost:5102/)
  4. Start Bank.Api
  
# Description

<img width="1164" alt="Screenshot 2023-02-21 at 19 34 16" src="https://user-images.githubusercontent.com/126006374/220441534-ff5a8266-0e24-4ace-ac93-033a492d6574.png">

Solution contains BuildingBlocks and two services - Acquisition and Payments.
  
  BuildingBlocks - provides all required infrastructure and common non-business related abstractions, like database, messaging, APIs, etc.
  
  Acquisition - provides basic functionality for acquiring new Merchants. Created Merchants are replicated to every interested service via messaging.
  
  Payments - provides basic functionality for payment processing.
  
  Each service uses a database cluster for storage purposes. It allows the scaling of services without any need to sync data between each other. As a primary database, MongoDB was selected due to:
  1. it is simple
  2. it is easy to run in a cluster
  3. it has transaction support
  But the solution is database agnostic and any database that meets previous statements can be selected.
  
  Services are using an event bus (RabbitMQ in this case) for messaging needs. RabbitMQ using mostly for demonstration purposes here (it is easier to run locally with default settings). For a real prod system, it is better to use more scalable solutions like SQS + SNS or similar.

# Testing
Acqusition contains 2 endpoints:
1. get merchant
`curl -X 'GET' \
  'http://localhost:5102/api/v1/merchants/eecc2f63-4ba9-47d9-ae79-f79d1c4d8170' \
  -H 'accept: text/plain'`
2. create merchant by id
`curl -X 'POST' \
  'http://localhost:5102/api/v1/merchants' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "VendorName",
  "bankAccount": "42-2701"
}'`

After merchant is created we will use its Id as a Api Key for Payments service auth

Payments contains 2 endpoints:
1. get transaction by id
`curl -X 'GET' \
  'http://localhost:5119/api/v1/card/payments/87c1aa13-0a88-4b5e-9927-e75f7856fd66' \
  -H 'accept: text/plain'`
2. process payment
`curl -X 'POST' \
  'http://localhost:5119/api/v1/card/payments' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -H 'X-Api-Key: a37cca61-6c11-4a3f-9b6a-4dbd362d2d53\'
  -d '{
  "buyerId": "eecc2f63-4ba9-47d9-ae79-f79d1c4d8170",
  "merchantId": "a37cca61-6c11-4a3f-9b6a-4dbd362d2d53",
  "cardPayment": {
    "amount": 10,
    "currency": "USD",
    "buyerCardDetails": {
      "cardNumber": "4916236705392079",
      "cardHolderName": "Its Me",
      "expirationDate": "2023-03-19",
      "cvv": "076"
    }
  }
}'
`
X-Api-Key should be a valid (existed) MerchantId, otherwise, request wont be authorized

# Considerations
Deployment - as the current system is a set of independent services, we can easily run them in a container cluster using AWS Fargate + EKS (just as an example). It is possible to make services images more lightweight and scale out the cluster fast

Messaging - despite RMQ being a great product, there are still some questions regarding network partitions and deduplication handling. Considering also the possibility to run this solution in AWS serverless container services, such solutions as SQS+SNS or EventBridge are preferable

Database - here we can select (almost) any database, relational or nosql. the only requirement is the ability to run in a cluster (without extra effort) and transaction support. To keep everything in AWS, we can consider using DynamoDb (as it has all requirements), but for this option, we definitely will need to calculate its costs.
