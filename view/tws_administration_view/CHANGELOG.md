# CHANGELOG

## CURRENT

- Notes:

    1. Refatoring sets and classes implementations from the newest [tws_foundation_client] version.
    2. Widget newest version migrated from [tws_guard_view] project. The migrated widgets are the following:
        * [TWSAutocompleteField]
        * [TWSArticleTable]
        * [TWSDatepickerField]
        * [TWSInputText]
        * [TWSSectionDivider]
    3. Removed unnecesary whispers views:
        * Manufacturers
        * Situations
    4. [Truck] view table and table viewer implementation.
    5. Added new [TWSCascadeSection] component.
    6. Added new [TWSIncrementalList] component.
    7. Added exceptions dialogs for Trucks creation form.
    8. Now the [Truck] creation form can set and create [TruckExternal] & [Truck] models 
    in the same operation.
    9. added a new [updateFactory] method in [TWSArticleCreatorItemState] to allow switch between sets models.
    10. [Truck] plates creation form now allow creating a [Truck] with a single plate with any country setting.
    11. [Truck] update implementation.
    12. Added custom dialogs for [Truck] create and update forms.
    13. [TWSAutocompleteField] Fix: change local lists on rebuild component not update the content.

- Dependencies upgrade:

    1. tws_administration_service ([2.0.0] -> [2.1.0])
    2. web ([0.5.1] -> [1.0.0])
    3. http ([1.2.1] -> [1.2.2])
    4. args ([2.5.0] -> [2.6.0]) 
    4. go_router ([14.2.7] -> [14.3.0]) 
    4. loggin ([1.3.0] -> [.1.2.0]) 
    4. path_provider_android ([2.2.10] -> [2.2.12]) 
    4. platform ([3.1.5] -> [3.1.6]) 
    4. typed_data ([1.3.2] -> [1.4.0]) 
    4. web ([1.0.0] -> [1.1.0]) 
    4. xdg_directories ([1.0.4] -> [1.1.0]) 


## 1.0.0-alpha [09/07/2024]

- Notes:

    1. Included new major version of [tws_administration_service]
    2. Removed old deprecated services from [tws_administration_service]

- Dependencies upgrade:

    1. path_provider_android ([2.2.6] -> [2.2.7])
    2. path_provider_windows ([2.2.1] -> [2.3.0])

        - Major Versions:

            1. tws_administration_service ([1.1.3] -> [2.0.0])
