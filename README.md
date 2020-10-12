# FPSO Tools Manager API

This API manages vessel and equipment registrations of an FPSO.\
The following functionalities are available:

* Register new vessel
* Register new equipment for a Vessel
* Deactivate equipments
* Retrieve a list of all active equipments of a vessel

## Requirements

To build this project, you will need to install the DotNet SDK 3.1+ \
[Click here to download it](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Building

To build this project, you just need to run "dotnet build" inside the main project folder

```
cd FPSOManagerApi_CS
dotnet build
```

In dotnet doesn't automatically download the required packages, you can run theses commands to fix it.

```
dotnet clean
dotnet restore
```

## Running

To run the program, just use the following command inside the main project folder

```
dotnet run
```

The REST API was configured to listen requests on http://localhost:5000 \
Below are the RESTful endpoints of the API\
\
Alternatively, you can use the SWAGGER UI by typing http://localhost:5000 on a browser.

### Post Vessel

* **Verb**: POST
* **Endpoint**: /FPSO/vessel
* **Arguments**: vesselCode
* **Returns**: 201 (Vessel Registered Successfully), 409 (Vessel Already Registered)

Registers a vessel using with the code *"vesselCode"*

### Post Equipment

* **Verb**: POST
* **Endpoint**: /FPSO/equipment
* **Arguments**: vesselCode
* **Returns**: 201 (Equipment Registered Successfully), 409 (Equipment Already Registered), 404 (Vessel Not Registered)

Registers an active equipment to the vessel *"vesselCode"*, equipment info is given using the following Request Body

**JSON Body**
```
{
    "name": "name_example",
    "code": "code_example",
    "location": "location_example"
}
```

### Put Deactivate Equipment

* **Verb**: PUT
* **Endpoint**: /FPSO/equipment
* **Returns**: 204 (Equipments deactivated successfully), 404 (Equipment Not Registered)

Deactivates equipments with the list of codes from the Request Body

**JSON Body**
```
[
    "equipment_code_example_1",
    "equipment_code_example_2",
    "equipment_code_example_3"
]
```

### Get Equipments From Vessel

* **Verb**: GET
* **Endpoint**: /FPSO/vessel
* **Arguments**: vesselCode
* **Returns**: 200 (Equipments retrieved successfully), 404 (Vessel Not Registered)

Retrieves a list of active equipments from the vessel with code *"vesselCode"*

## Running Unit Tests

To execute the Unit Tests, you just need to run "dotnet test" inside the test project folder

```
cd ProjectTests
dotnet test
```
