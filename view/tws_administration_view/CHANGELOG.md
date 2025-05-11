# CHANGELOG

## CURRENT

- Notes:

    1. Refatoring sets and classes implementations from the newest [tws_foundation_client] version.

    2. Added [Human_Resources] page.
      - Added [Contact] article.

    3. Added [Yardlogs] page.
        - Added [Yardlogs] article.
        - Added [TruckInventory] article.

    4. Whispers added:
        - [Trucks] and [ExternalTrucks] added newer table [View], [Update] & [Delete].
        - [Trailers] and [TrailersExternals] added [View], [Create], [Update] $ [Delete].
        - [Drivers] and [ExternalDrivers] added [View], [Create], [Update] & [Delete].
        - [Locations] added [View], [Create], [Update] & [Delete].
        - [Sections] added [View], [Create], [Update] & [Delete].
        - [Accounts] added [View], [Create], [Update] & [Delete].
        - [Yardlogs] added [View], [Update] & [Delete].

    5. Widget newest version migrated from  [tws_guard_view] project. The migrated widgets are the following:
        - [TWSAutocompleteField].
        - [TWSArticleTable].
        - [TWSDatepickerField].
        - [TWSInputText].
        - [TWSSectionDivider].
        - [TWSImageViewer].
        - [TWSPhotoTaker].

    6. Added new TWS widgets:
        - [TWSCascadeSection].
        - [TWSIncrementalList].
        - [TWSListViewer].
        - [TWSOptionSelector].
        - [TWSSelectableList].

    7. Added [Business] articles pages:
        - [Trailers] added with [ExternalTrailers] CRUD capabilities as the same way that [Trucks] article. See Notes #12, #13 and #14.
        - [Drivers] added with [ExternalDrivers] CRUD capabilities as the same way that [Trucks] article. See Notes #12, #13 and #14.
        - [Locations].
        - [Sections].

    8. Added [Security] articles pages:
        - [Account]. 

    9. Removed unnecesary whispers views:
        - Manufacturers
        - Situations

    10. Migrated TWS widget changes from [GuardView] environment: 
        - The [TWSAutocompleteField] future mode, now perform a database search to avoid data overflow.
        - [TWSArticleTableFieldOptions] Width property added.
        - Now each [TWSArticleTable] column width can be changed using the [TWSArticleTableFieldOptions] width property.
        - Added a doble tap summit button preventions on [WisperFrame].

    11. Added exceptions dialogs for all [Business] articles creation forms.
    12. Added [TruckExternal] table to Trucks Article. Now you can switch between Truck view table or External view table in the same article.
    13. Now the [Truck] creation form can set and create [TruckExternal] & [Truck] models
    in the same operation.
    14. Added the update for [ExternalTruck] records in [TruckArticle] selecting [ExternalTruck] table and selecting a record.
    15. added a new [updateFactory] method in [TWSArticleCreatorItemState] to allow switch between sets models.
    16. [Truck] plates creation form now allow creating a [Truck] & [ExternalTruck] with a single plate with any country setting.
    17. Added custom dialogs for [Truck] create and update forms.
    18. [TWSAutocompleteField] Fix: change local lists on rebuild component not update the content.
    19. Plates creation option changed to a [TWSIncrementalList] control.
    20. Fixed some state and view bugs for [TWSAutocompleteField] widget.
    21. Changed [Trucks] article icon.
    22. Added a [SingleChildScrollView] for content in [TWSConfirmationDialog] widget.
    23. Added a [suffixLabel] param to [TWSDatepicker] widget to show secundary text.
    24. Added [TWSCascadeSection] function boolean parameter [onPressed] to return the show button state.
    25. Changed name properties for [TWSAutocompleteField] to fit property name and functionality:
        - [isOptionalLabel] -> [suffixLabel].
        - [suffixLabel] -> [suffixResultLabel].
    26. Added missing information in [Trucks] & [Trailers] view tables.
    27. Now the [TWSAutocompleteField] updates the [localList] data when the parameter changes on rebuild.
    28. Added [color] & [foreColor] parameter in [TWSDisplayFlat].
    29. Added [TWSCascadeSection] in optionals sets objects in update view to all articles in [Business] page.
    30. Added an optional [TimePicker] dialog in [TWSDatetimePicker] to add the time to the date.
    31. Removing text gap in [TWSImageViewer] when is not necesary.
    32. Added the following parameters for [TWSInputText]:
        - [Formatter] Property.
        - [KeyboardType] Property.
    33. Extensions files removed and migrated to foundation environment.
    34. Added search filters to [View] table whispers for all articles in [Business] module.
    35. Added search filters to [View] table whispers for [Accounts] article in [Security] module.
    36. Added exceptions notifications in update forms in all articles for [Business] module.
    37. Added [TWSFilePicker] widget to local files selections.
    38. Added [TWSFilePicker] widget implemtation in [TWSPhotoTaker] widget.
    39. Added exceptions dialogs for [Accounts] article creation form in [Security] module.
    40. Added async gap to [onRemoveRequest] and [closeReinvoke] method in [TWSArticleTable] and return a [Future<bool>] value type to allow async API consume.

    41. TEMPORAL dependencies implementation: file_picker, camera_platform, camera_web.

- Dependencies upgrade:
    1. (-) tws_foundation_client: Deprecated now used tws_foundation_client
    2. (-) csm_view: Deprecated now used csm_view
    3. (+) tws_foundation ([])
    4. (+) csm_view ([])
    5. tws_administration_service ([2.0.0] -> [2.1.0])
    6. web ([0.5.1] -> [1.0.0])
    7. http ([1.2.1] -> [1.2.2])
    8. args ([2.5.0] -> [2.6.0])
    9. go_router ([14.2.7] -> [14.3.0])
    10. loggin ([1.3.0] -> [.1.2.0])
    11. path_provider_android ([2.2.10] -> [2.2.12])
    12. platform ([3.1.5] -> [3.1.6])
    13. typed_data ([1.3.2] -> [1.4.0])
    14. web ([1.0.0] -> [1.1.0])
    15. xdg_directories ([1.0.4] -> [1.1.0])

## 1.0.0-alpha [09/07/2024]

- Notes:

    1. Included new major version of [tws_foundation_client]
    2. Removed old deprecated services from [tws_foundation_client]

- Dependencies upgrade:

    1. path_provider_android ([2.2.6] -> [2.2.7])
    2. path_provider_windows ([2.2.1] -> [2.3.0])

        - Major Versions:

            1. tws_foundation_client ([1.1.3] -> [2.0.0])
