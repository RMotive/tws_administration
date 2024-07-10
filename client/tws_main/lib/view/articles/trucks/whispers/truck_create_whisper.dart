import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/constants/twsa_common_displays.dart';
import 'package:tws_main/core/extension/datetime.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';
import 'package:tws_main/view/articles/trucks/trucks_article.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item.dart';
import 'package:tws_main/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item_property.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_agent.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creation_item_state.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creator.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creator_feedback.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_field.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_item_properties.dart';
import 'package:tws_main/view/widgets/tws_datepicker_field.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_adapter.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_field.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';
import 'package:tws_main/view/widgets/tws_section.dart';

part '../options/trucks_whisper_options_adapter.dart';
const List<String> _countryOptions = TWSAMessages.kCountryList;
const List<String> _USAstateOptions = TWSAMessages.kUStateCodes;
class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    final TWSArticleCreatorAgent<Truck> creatorAgent = TWSArticleCreatorAgent<Truck>();
    CSMColorThemeOptions themeSctruct = getTheme<TWSAThemeBase>().primaryControlColor;
    final DateTime firstDate = DateTime(2000);
    final DateTime lastlDate = DateTime(2040);
    final DateTime today = DateTime.now();
    final String defaultUSAstate = _USAstateOptions[4];
    const double fieldHeight = 35;
    TextStyle sectionsLinesStyle =  TextStyle(
      fontWeight: FontWeight.w300,
      fontSize: 12,
      color: themeSctruct.hightlightAlt ?? Colors.white,
      fontStyle: FontStyle.italic
    );

    return WhisperFrame(
      title: 'Create trucks',
      trigger: creatorAgent.create,
      child:  TWSArticleCreator<Truck>(
        agent: creatorAgent,
        factory: () => Truck.def(),
        afterClose: () {
          TrucksArticle.tableAgent.refresh();
        }, 
        modelValidator: (Truck model) => model.evaluate().isEmpty,
        onCreate: (List<Truck> records) async {
          final String currentToken = _sessionStorage.getTokenStrict();
          MainResolver<MigrationTransactionResult<Truck>> resolver = await _trucksService.create(records, currentToken);

          List<TWSArticleCreatorFeedback> feedbacks = <TWSArticleCreatorFeedback>[];
          resolver.resolve(
            decoder: const MigrationTransactionResultDecoder<Truck>(TruckDecoder()),
            onConnectionFailure: () {},
            onException: (Object exception, StackTrace trace) {},
            onFailure: (FailureFrame failure, int status) {},
            onSuccess: (SuccessFrame<MigrationTransactionResult<Truck>> success) {},
          );
          return feedbacks;
        },
        itemDesigner: (Truck actualModel, bool selected, bool valid) {  
          return TWSArticleCreationStackItem(
            selected: selected,
            valid: valid,
            properties: <TwsArticleCreationStackItemProperty>[
              TwsArticleCreationStackItemProperty(
                label: 'VIN',
                minWidth: 150,
                value: actualModel.vin
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Motor',
                minWidth: 150,
                value: actualModel.motor
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Assign Manufacturer',
                minWidth: 150,
                value: actualModel.manufacturer.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Manufacturer Model',
                minWidth: 150,
                value: actualModel.manufacturerNavigation?.model,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Year Model',
                minWidth: 150,
                value: actualModel.manufacturerNavigation?.year.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Insurance Policy',
                minWidth: 150,
                value: actualModel.insuranceNavigation?.policy,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Insurance Expiration',
                minWidth: 150,
                value: actualModel.insuranceNavigation?.expiration.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Insurance Country',
                minWidth: 150,
                value: actualModel.insuranceNavigation?.country,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Anual Maintenance',
                minWidth: 150,
                value: actualModel.maintenanceNavigation?.anual.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Trimestral Maintenance',
                minWidth: 150,
                value: actualModel.maintenanceNavigation?.trimestral.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: 'SCT type',
                minWidth: 150,
                value: actualModel.sctNavigation?.type,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'SCT Number',
                minWidth: 150,
                value: actualModel.sctNavigation?.number,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'SCT Configuration',
                minWidth: 150,
                value: actualModel.sctNavigation?.configuration,
              ),
              TwsArticleCreationStackItemProperty(
                label: ' Assign Situation',
                minWidth: 150,
                value: actualModel.situation.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Situation name',
                minWidth: 150,
                value: actualModel.situationNavigation?.name,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Situation name',
                minWidth: 150,
                value: actualModel.situationNavigation?.description,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Situation name',
                minWidth: 150,
                value: actualModel.situationNavigation?.description,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'USA Plate - State',
                minWidth: 150,
                value: actualModel.plates[0].state.isEmpty ? defaultUSAstate : actualModel.plates[0].state ,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'USA Plate - Country',
                minWidth: 150,
                value: actualModel.plates[0].country,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'USA Plate - Expiration',
                minWidth: 150,
                value: actualModel.plates[0].expiration.dateOnlyString,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'MX Plate - Identifier',
                minWidth: 150,
                value: actualModel.plates[1].identifier,
              ),
               TwsArticleCreationStackItemProperty(
                label: 'MX Plate - State',
                minWidth: 150,
                value: actualModel.plates[1].state,
              ),
               TwsArticleCreationStackItemProperty(
                label: 'MX Plate - Country',
                minWidth: 150,
                value: actualModel.plates[1].country,
              ),
               TwsArticleCreationStackItemProperty(
                label: 'MX Plate - Expiration',
                minWidth: 150,
                value: actualModel.plates[1].expiration.dateOnlyString,
              ),
              
            ], 
          );
        }
        
        , 
        formDesigner: (TWSArticleCreatorItemState<Truck>? itemState) {  
          final bool formDisabled = !(itemState == null);

          return SingleChildScrollView(
            child: Padding(
              padding: const EdgeInsets.all(10),
          child: CSMSpacingColumn(
            mainSize: MainAxisSize.min,
            spacing: 12,
            crossAlignment: CrossAxisAlignment.start,
            children: <Widget>[
                CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSInputText(
                      label: 'VIN',
                      controller: TextEditingController(text: itemState?.model.vin),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        itemState.updateModelRedrawing(
                          model.clone(
                            vin: text,
                          ),
                        );
                      },
                      isEnabled: formDisabled,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Motor',
                      controller: TextEditingController(text: itemState?.model.motor),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        itemState.updateModelRedrawing(
                          model.clone(
                            motor: text,
                          ),
                        );
                      },
                      isEnabled: formDisabled,
                    ),
                  ),
                ],
              ),
              CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSFutureAutoCompleteField<Manufacturer>(
                      label: "Select a Manufacturer",
                      hint: "Assing an existing manufacturer",
                      adapter:  const _ManufacturerViewAdapter(),
                      onChanged: (String input, TWSAutocompleteItemProperties<Manufacturer>? selectedItem) {  
                        Truck model = itemState!.model;
                        itemState.updateModelRedrawing(
                          model.clone(
                            manufacturer: selectedItem?.value.id,
                          ),
                        );
                      }, 
                      optionsBuilder: (int int , Manufacturer manufacturer) {  
                        return TWSAutocompleteItemProperties<Manufacturer>(label: '${manufacturer.brand} - ${manufacturer.model}', value: manufacturer);
                      }
                    ),
                  ),
                  Expanded(
                    child: TWSFutureAutoCompleteField<Situation>(
                      label: "Select a Situation",
                      hint: "Assing an existing Situation status",
                      adapter:  const _SituationsViewAdapter(),
                      onChanged: (String input, TWSAutocompleteItemProperties<Situation>? selectedItem) {  
                        Truck model = itemState!.model;
                        itemState.updateModelRedrawing(
                          model.clone(
                            situation: selectedItem?.value.id,
                          ),
                        );
                      }, 
                      optionsBuilder: (int int , Situation situation) {  
                        return TWSAutocompleteItemProperties<Situation>(label: situation.name, value: situation);
                      }
                    ),
                  ),
                ],
              ),
              TWSSection(
                padding: const EdgeInsets.symmetric(vertical: 10),
                title: "USA Plate", 
                content: CSMSpacingColumn(
                  mainSize: MainAxisSize.min,
                  spacing: 20,
                  children: <Widget>[
                    CSMSpacingRow(
                      spacing: 10,
                      children: <Widget>[
                        Expanded(
                          child: TWSInputText(
                            height: fieldHeight,
                            label: 'Identifier',
                            controller: TextEditingController(text: itemState?.model.plates[0].identifier),
                            onChanged: (String text) {
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[0];
                              Plate generatePlate = Plate(
                                temp.id, 
                                text, 
                                temp.state, 
                                temp.country, 
                                temp.expiration, 
                                0, 
                                null);
                              plates.add(generatePlate);
                              plates.add(itemState.model.plates[1]);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled,
                          ),
                        ),
                        Expanded(
                          child: TWSAutoCompleteField<String>(
                            initialValue: _countryOptions[1],
                            isEnabled: false,
                            optionsBuilder: (String query) {
                              if(query.isNotEmpty) return _countryOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                              return _countryOptions;
                            } ,
                            displayValue:(String item) => item,
                            height: fieldHeight,
                            label: 'Country',
                            // controller: TextEditingController(text: itemState?.model.plates[0].country),
                            onChanged: (String? value) {
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[0];
                              Plate generatePlate = Plate(
                                temp.id, 
                                temp.country, 
                                temp.state, 
                                value ?? "", 
                                temp.expiration, 
                                0, 
                                null);
                              plates.add(generatePlate);
                              plates.add(itemState.model.plates[1]);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                          )
                        ),
                        Expanded(
                          child: TWSAutoCompleteField<String>(
                            height: fieldHeight,
                            displayValue: (String query) => query,
                            initialValue: (itemState?.model.plates[0].state ?? "").isEmpty ? defaultUSAstate: itemState?.model.plates[0].state ,
                            optionsBuilder: (String query) {
                              if(query.isNotEmpty) return _USAstateOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                              return _USAstateOptions;
                            } ,
                            label: 'State',
                            onChanged: (String? text) {
                              print("Notifying -> $text");
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[0];
                              Plate generatePlate = Plate(
                                temp.id, 
                                temp.identifier, 
                                text ?? "", 
                                temp.country, 
                                temp.expiration, 
                                0, 
                                null);
                              plates.add(generatePlate);
                              plates.add(itemState.model.plates[1]);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled
                          ),
                        ),
                    ]),
                    CSMSpacingRow(
                      spacing: 10,
                      children: <Widget>[
                        Expanded(
                          child: TWSDatepicker(
                            firstDate: firstDate,
                            lastDate: lastlDate,
                            height: fieldHeight,
                            label: 'Expiration Date',
                            controller: TextEditingController(text: itemState?.model.plates[0].expiration.dateOnlyString),
                            onChanged: (String text) {
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[0];
                              Plate generatePlate = Plate(
                                temp.id, 
                                temp.identifier, 
                                temp.state, 
                                temp.country, 
                                DateTime.parse(text), 
                                0, 
                                null);
                              plates.add(generatePlate);
                              plates.add(itemState.model.plates[1]);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled,
                          ),
                        ),
                      ])
              
                  ],
                )
              ),
              TWSSection(
                padding: const EdgeInsets.symmetric(vertical: 10),
                title: "MX Plate", 
                content: CSMSpacingColumn(
                  mainSize: MainAxisSize.min,
                  spacing: 20,
                  children: <Widget>[
                    CSMSpacingRow(
                      spacing: 10,
                      children: <Widget>[
                        Expanded(
                          child: TWSInputText(
                            height: 35,
                            label: 'Identifier',
                            controller: TextEditingController(text: itemState?.model.plates[1].identifier),
                            onChanged: (String text) {
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[1];
                              Plate generatePlate = Plate(
                                temp.id, 
                                text, 
                                temp.state, 
                                temp.country, 
                                temp.expiration, 
                                0, 
                                null);
                              plates.add(itemState.model.plates[0]);
                              plates.add(generatePlate);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled,
                          ),
                        ),
                        Expanded(
                          child: TWSInputText(
                            height: fieldHeight,
                            label: 'Country',
                            controller: TextEditingController(text: itemState?.model.plates[1].country),
                            onChanged: (String text) {
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[1];
                              Plate generatePlate = Plate(
                                temp.id, 
                                temp.country, 
                                temp.state, 
                                text, 
                                temp.expiration, 
                                0, 
                                null);
                              plates.add(itemState.model.plates[0]);
                              plates.add(generatePlate);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled,
                          )
                        ),
                        Expanded(
                          child: TWSInputText(
                            height: fieldHeight,
                            label: 'State',
                            controller: TextEditingController(text: itemState?.model.plates[1].state),
                            onChanged: (String text) {
                              Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[1];
                              Plate generatePlate = Plate(
                                temp.id, 
                                temp.identifier, 
                                text, 
                                temp.country, 
                                temp.expiration, 
                                0, 
                                null);
                              plates.add(itemState.model.plates[0]);
                              plates.add(generatePlate);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled,
                          ),
                        ),
                    ]),
                    CSMSpacingRow(
                      spacing: 10,
                      children: <Widget>[
                        Expanded(
                          child: TWSDatepicker(
                            firstDate: firstDate,
                            lastDate: lastlDate,
                            height: 35,
                            label: 'Expiration Date',
                            controller: TextEditingController(text: itemState?.model.plates[1].expiration.dateOnlyString),
                            onChanged: (String text) {
                             Truck model = itemState!.model;
                              List<Plate>? plates =  <Plate>[];
                              final Plate temp = itemState.model.plates[1];
                              Plate generatePlate = Plate(
                                temp.id, 
                                temp.identifier, 
                                temp.state, 
                                temp.country, 
                                DateTime.parse(text), 
                                0, 
                                null);
                              plates.add(itemState.model.plates[0]);
                              plates.add(generatePlate);
                              itemState.updateModelRedrawing(
                                model.clone(
                                  plates: plates,
                                ),
                              );
                            },
                            isEnabled: formDisabled,
                          ),
                        ),
                      ])
              
                  ],
                )
              ),
             Center(
              child: Text(
                "<Optional Sections>",
                style: sectionsLinesStyle.copyWith(fontSize: 18)
              ),
             ),
            TWSSection(
              title: "Manufacturer", 
              content: CSMSpacingColumn(
                spacing: 10,
                children: <Widget>[
                  Center(
                    child: Text(
                      "Create a new manufacturer for this Truck. The 'Select a manufacturer' field must be empty.",
                      style: sectionsLinesStyle,
                    )
                  ),
                  CSMSpacingRow(
                    spacing: 10,
                    children: <Widget>[
                      Expanded(
                        child: TWSInputText(
                          label: 'Brand',
                          controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.brand),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Manufacturer? temp = model.manufacturerNavigation;
                            Manufacturer? updateManufacturer = Manufacturer(
                              0, 
                              temp?.model ?? "", 
                              text, 
                              temp?.year ?? today, 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                manufacturerNavigation: updateManufacturer
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSInputText(
                          label: 'Model',
                          controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.model),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Manufacturer? temp = model.manufacturerNavigation;
                            Manufacturer? updateManufacturer = Manufacturer(
                              0, 
                              text, 
                              temp?.brand ?? "", 
                              temp?.year ?? today, 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                manufacturerNavigation: updateManufacturer
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSDatepicker(
                          firstDate: firstDate,
                          lastDate: lastlDate,
                          label: 'Year',
                          controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.year.dateOnlyString),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Manufacturer? temp = model.manufacturerNavigation;
                            Manufacturer? updateManufacturer = Manufacturer(
                              0, 
                              temp?.model ?? "", 
                              temp?.model ?? "", 
                              DateTime.parse(text), 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                manufacturerNavigation: updateManufacturer
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                    ]
                  )
                ]
              )
            ),
            TWSSection(
              title: "Situation", 
              content: CSMSpacingColumn(
                spacing: 10,
                children: <Widget>[
                  Center(
                    child: Text(
                      "Create a new situation status for this Truck. The 'Select a situation' field must be empty.",
                      style: sectionsLinesStyle,
                    )
                  ),
                  CSMSpacingRow(
                    spacing: 10,
                    children: <Widget>[
                      Expanded(
                        child: TWSInputText(
                          label: 'Name',
                          controller: TextEditingController(text: itemState?.model.situationNavigation?.name),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Situation? temp = model.situationNavigation;
                            Situation? updateSituation = Situation(
                              0, 
                              text, 
                              temp?.description,  
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                situationNavigation: updateSituation
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSInputText(
                          label: 'Description',
                          isOptional: true,
                          controller: TextEditingController(text: itemState?.model.situationNavigation?.description),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Situation? temp = model.situationNavigation;
                            Situation? updateSituation = Situation(
                              0, 
                              temp?.name ?? "", 
                              text,  
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                situationNavigation: updateSituation
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      )
                    ]
                  )
                ]
              )
            ),
            TWSSection(
              title: "Maintenance", 
              content: CSMSpacingColumn(
                spacing: 10,
                children: <Widget>[
                  CSMSpacingRow(
                    spacing: 10,
                    children: <Widget>[
                      Expanded(
                        child: TWSDatepicker(
                          firstDate: firstDate,
                          lastDate: lastlDate,
                          label: 'Anual',
                          controller: TextEditingController(text: itemState?.model.maintenanceNavigation?.anual.dateOnlyString),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Maintenance? temp = model.maintenanceNavigation;
                            Maintenance? updateMaintenance = Maintenance(
                              0, 
                              DateTime.parse(text), 
                              temp?.trimestral ?? today,  
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                maintenanceNavigation: updateMaintenance
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSDatepicker(
                          firstDate: firstDate,
                          lastDate: lastlDate,
                          label: 'Trimestral',
                          controller: TextEditingController(text: itemState?.model.maintenanceNavigation?.trimestral.dateOnlyString),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Maintenance? temp = model.maintenanceNavigation;
                            Maintenance? updateMaintenance = Maintenance(
                              0,
                              temp?.anual ?? today,
                              DateTime.parse(text),   
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                maintenanceNavigation: updateMaintenance
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      )
                    ]
                  )
                ]
              )
            ),
            TWSSection(
              title: "Insurance", 
              content: CSMSpacingColumn(
                spacing: 10,
                children: <Widget>[
                  CSMSpacingRow(
                    spacing: 10,
                    children: <Widget>[
                      Expanded(
                        child: TWSInputText(
                          label: 'Policy',
                          controller: TextEditingController(text: itemState?.model.insuranceNavigation?.policy),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Insurance? temp = model.insuranceNavigation;
                            Insurance? updateInsurance = Insurance(
                              0,
                              text,
                              temp?.expiration ?? today,  
                              temp?.country ?? "", 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                insuranceNavigation: updateInsurance
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSDatepicker(
                          firstDate: firstDate,
                          lastDate: lastlDate,
                          label: 'Expiration',
                          controller: TextEditingController(text: itemState?.model.insuranceNavigation?.expiration.dateOnlyString),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Insurance? temp = model.insuranceNavigation;
                            Insurance? updateInsurance = Insurance(
                              0,
                              temp?.policy ?? "",
                              DateTime.parse(text),  
                              temp?.country ?? "", 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                insuranceNavigation: updateInsurance
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSInputText(
                          label: 'Country',
                          controller: TextEditingController(text: itemState?.model.insuranceNavigation?.country),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            Insurance? temp = model.insuranceNavigation;
                            Insurance? updateInsurance = Insurance(
                              0,
                              temp?.policy ?? "",
                              temp?.expiration ?? today,  
                              text, 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                insuranceNavigation: updateInsurance
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      )
                    ]
                  )
                ]
              )
            ),
            TWSSection(
              title: "SCT", 
              content: CSMSpacingColumn(
                spacing: 10,
                children: <Widget>[
                  CSMSpacingRow(
                    spacing: 10,
                    children: <Widget>[
                      Expanded(
                        child: TWSInputText(
                          label: 'Type',
                          controller: TextEditingController(text: itemState?.model.sctNavigation?.type),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            SCT? temp = model.sctNavigation;
                            SCT? updateSCT = SCT(
                              0,
                              text,
                              temp?.number ?? "",  
                              temp?.configuration ?? "", 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                sctNavigation: updateSCT
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSInputText(
                          label: 'Number',
                          controller: TextEditingController(text: itemState?.model.sctNavigation?.number),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            SCT? temp = model.sctNavigation;
                            SCT? updateSCT = SCT(
                              0,
                              temp?.type ?? "",
                              text,  
                              temp?.configuration ?? "", 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                sctNavigation: updateSCT
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      ),
                      Expanded(
                        child: TWSInputText(
                          label: 'Configuration',
                          controller: TextEditingController(text: itemState?.model.sctNavigation?.configuration),
                          onChanged: (String text) {
                            Truck model = itemState!.model;
                            SCT? temp = model.sctNavigation;
                            SCT? updateSCT = SCT(
                              0,
                              temp?.type ?? "",
                              temp?.number ?? "",  
                              text, 
                              <Truck>[]
                            );
                            itemState.updateModelRedrawing(
                              model.clone(
                                sctNavigation: updateSCT
                              ),
                            );
                          },
                          isEnabled: formDisabled,
                        ),
                      )
                    ]
                  )
                ]
              )
            )
          ],
        ),
      )
    );
  })   
);
}}