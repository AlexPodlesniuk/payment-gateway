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
Solution contains from BuildingBlocks and two services - Acquisition and Payments.
  BuildingBlocks - provides all required infrastructure and common non business related abstractions, like: database, messaging, apis, etc.
  Acquisition - provides basic functionality for acquiring new Merchants. Created Merchants are replicated to every interested services via messaging.
  Payments - provides basic functionality for payments processing.
