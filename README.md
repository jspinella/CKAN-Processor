# Central Executive
Central Executive listens for AMQP messages from web scrapers, processes those messages, and adds/updates data to CKAN via the CKAN API.

[![pipeline status](https://gitlab.com/urban-sdk/CentralExecutive/badges/master/pipeline.svg)](https://gitlab.com/urban-sdk/CentralExecutive/commits/master)

## Overview
Central Executive ("the Executive") listens for messages in the CentralExecutive queue in RabbitMQ and processes messages concurrently but not in parallel.

The Executive checks local CKAN for existing items and creates them if necessary:
- Organizations
- Packages
- Resources
- Groups 

## Organizations
Organization names are derived from their titles by the Executive
- Title: "U.S. Department of Agriculture" (sent over in AMQP message)
- Name: "us_department_of_agriculture" (names must conform to CKAN validation, e.g. cannot contain special characters)

In some cases, scraped data is bad in the sense that there are multiple organizations with slightly different names. The Executive compensates for this by comparing the organization name derived from the AMQP message with a list of existing organizations in local CKAN. Using a variant of the Levenshtein Distance algorithm, similarity is determined and an existing organization may be used in lieu of the "new" organization provided by the AMQP message.
For example, a new organization with the name "department_of_agriculture" will not be created when an organization with the name "us_department_of_agriculture" already exists in local CKAN.

## Packages
Packages are created regardless of whether any resources are ultimately added by the Executive to the package. It's probably better to validate resource URLs prior to creating/finding existing packages. I'll make this change when adding the enhanced resource validation logic (whereby individual resource files are downloaded and parsed within the Executive).

## Resources
CKAN DataPusher only supports certain file types (CSV, XLS(X), and possibly some others). Additionally, we only want data pertinent to the CKAN groups we support (e.g. Jacksonville, Palm Beach County, etc.). To do this, The Executive should download and parse files where possible, literally reading through each file to find keywords to suggest relevance to the groups we support (package and resource titles may also be used to hint at pertinence).

## Groups
In CKAN, groups are a way to _group_ packages. CKAN also provides group-level permissions. We can use groups to organize packages by geolocation, rather than creating a new CKAN instance and website for each geographical area (e.g. one for Duval County, one for Palm Beach County, etc.).

As of this writing The Executive is unaware of CKAN groups. 

## FAQs and Other Info
- In cases where there are multiple, very similar organization names, CE will use the one it encounters first. If that is not the desired permutation of the organization's name, you can simply change the organization name within the CKAN website and the new name will be used by CE automatically.


## Code Notes
//while loops - we could add code later to try adding orgs/packages/resources multiple times (e.g. appending "(1)" to the end of the name) as these get more complex
//but they're mainly here because messages are not processed one at a time, as each request awaits CKAN api responses, processing gets out of order
// for example, thread A is going to add a new organization "org5" but thread B has just added "org5" in between thread A checking for existance of the 
// org and thread A going to add the new org... this would throw an exception. So this has to be caught, but the loop allows
// thread A to re-evaluate the situation and see that the organization now exists.