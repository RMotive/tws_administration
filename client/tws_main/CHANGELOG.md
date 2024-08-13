# CHANGELOG

## CURRENT
- Notes: 

    1. Changed the Create method in [BMigrationDepot] to allow the use of set navigation properties.
    2. Incluided a new [TWSListTile] component. This component is intended for implementations in [ListView] componentents 
    due to it's low performance impact and simplicity.
    3. [TWSAutoComplete] & [TWSFutureAutoComplete] Incluided some performance improvements, bug fixes and state management changes to 
    work propertly with [TWSArticleCreation] Component.
    4. [TrucksCreateWhisper] Implementation.
    5. Removed unnecesary services and articles from Bussines module. The entities data removed are the following:
        - [Plates] (Removed only in articles section)
        - [Insurnace]
        - [SCT]
        - [Maintenance]

- Dependencies upgrade: 

    1. tws_administration_service ([2.0.0] -> [2.1.0])
    2. web ([0.5.1] -> [1.0.0])
    3. http ([1.2.1] -> [1.2.2])
    

## 1.0.0-alpha [09/07/2024]

- Notes:

    1. Included new major version of [tws_administration_service]
    2. Removed old deprecated services from [tws_administration_service]

- Dependencies upgrade:

    1. path_provider_android ([2.2.6] -> [2.2.7])
    2. path_provider_windows ([2.2.1] -> [2.3.0])

        - Major Versions:

            1. tws_administration_service ([1.1.3] -> [2.0.0])
