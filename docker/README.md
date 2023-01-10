# Introduction

Docker directory is folder for local debugging purpose.

# Pre-Requirements

# CosmosDb
1. Set up your local ip in AZURE_COSMOS_EMULATOR_IP_ADDRESS_OVERRIDE
2. Download simulator self-signed certificate and install it to you machine trusted store. To download certificate just open in the browser following https://localhost:8081/_explorer/emulator.pem  link and save the certificate file, but make sure you gave it .crt extension.

On Windows

Double click on certificate.
Click Install certificate….
Select Current User and click Next.
IMPORTANT: Select Place all certificates in the following store and click Browse. Because by default it installs into Personal stores. And most likely certificate validation in you application will not work out of the box.
Select Trusted Root Certification Authority and click Ok, then Next and Finish.

link:
https://libertus.dev/posts/connect-to-cosmosbd-emulator-on-docker/part1/#inst-cert

# Azure function
Test function with Postman:
Url:http://localhost:7127/runtime/webhooks/EventGrid?functionName=UpdateExpensesOnHouseUpdate
Header: {aeg-event-type:Notification}

# Event Grid
All subscribers have to be set up before event grid container is started.
Test publisher with Postman:
Utl:https://localhost:60102/api/events?api-version=2018-01-01
Header: {aeg-event-type:Notification},{aeg-sas-key:TheLocal+DevelopmentKey=}