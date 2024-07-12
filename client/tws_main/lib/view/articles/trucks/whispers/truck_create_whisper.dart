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
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart';
import 'package:tws_main/view/widgets/tws_datepicker_field.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_adapter.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_field.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';
import 'package:tws_main/view/widgets/tws_section.dart';

part '../options/trucks_whisper_options_adapter.dart';

const List<String> _countryOptions = TWSAMessages.kCountryList;
const List<String> _usaStateOptions = TWSAMessages.kUStateCodes;
const List<String> _mxStateOptions = TWSAMessages.kMXStateCodes;
final DateTime _firstDate = DateTime(2000);
final DateTime _lastlDate = DateTime(2040);
final DateTime _today = DateTime.now();
final String _defaultUSAstate = _usaStateOptions[4];
final String _defaultMXstate =_mxStateOptions[1];
const double _fieldHeight = 35;

class _TruckCreateWhisteState extends CSMStateBase{ }
final _TruckCreateWhisteState _formsState = _TruckCreateWhisteState();

class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});
  /// 
  List<Widget> _buildMainPropertiesForm(bool disable, TWSArticleCreatorItemState<Truck>? itemState, _TruckCreateWhisteState state){
      return <Widget>[
        CSMSpacingRow(
          spacing: 10,
          children: <Widget>[
            Expanded(
              child: TWSInputText(
                maxLength: 17,
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
                isEnabled: disable,
              ),
            ),
            Expanded(
              child: TWSInputText(
                maxLength: 16,
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
                isEnabled: disable,
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
                initialValue: itemState?.model.manufacturerNavigation ,
                displayValue: (Manufacturer set) => "${set.brand} ${set.model}" ,
                isEnabled: disable,
                adapter:  const _ManufacturerViewAdapter(),
                onChanged: (Manufacturer? selectedItem) {  
                  print("catch..");
                  Truck model = itemState!.model;
                  Truck newTruck = Truck(
                  model.id, 
                  model.vin, 
                  selectedItem?.id ?? 0, 
                  model.motor, 
                  model.sct, 
                  model.insurance, 
                  model.situation, 
                  model.insurance, 
                  selectedItem, 
                  model.sctNavigation, 
                  model.maintenanceNavigation, 
                  model.situationNavigation, 
                  model.insuranceNavigation, 
                  model.plates
                  );
                  itemState.updateModel(newTruck);
                  state.effect();
                }, 

              ),
            ),
            Expanded(
              child: TWSFutureAutoCompleteField<Situation>(
                label: "Select a Situation",
                hint: "Assing an existing Situation status",
                initialValue: itemState?.model.situationNavigation,
                displayValue: (Situation set) => set.name,
                adapter:  const _SituationsViewAdapter(),
                isEnabled: disable,
                onChanged: (Situation? selectedItem) {
                  print("catch..");
                  Truck model = itemState!.model;
                  Truck newTruck = Truck(
                    model.id, 
                    model.vin, 
                    model.manufacturer, 
                    model.motor, 
                    model.sct, 
                    model.maintenance, 
                    selectedItem?.id ?? 0, 
                    model.insurance, 
                    model.manufacturerNavigation, 
                    model.sctNavigation, 
                    model.maintenanceNavigation, 
                    selectedItem, 
                    model.insuranceNavigation, 
                    model.plates
                  );
                  itemState.updateModel(newTruck);
                  state.effect();
                }, 
              ),
            ),
          ],
        ),
      ];
    }

    List<Widget> buildUSAPlateForm(bool disable, TWSArticleCreatorItemState<Truck>? itemState){
      return <Widget>[
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
                      label: 'Identifier',
                      maxLength: 12,
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
                      isEnabled: disable,
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
                      height: _fieldHeight,
                      label: 'Country',
                      onChanged: (String? value) {
                        Truck model = itemState!.model;
                        List<Plate>? plates =  <Plate>[];
                        final Plate temp = itemState.model.plates[0];
                        Plate generatePlate = Plate(
                          temp.id, 
                          temp.identifier, 
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
                      height: _fieldHeight,
                      displayValue: (String query) => query,
                      initialValue: (itemState?.model.plates[0].state ?? "").isEmpty ? _defaultUSAstate: itemState!.model.plates[0].state ,
                      optionsBuilder: (String query) {
                        if(query.isNotEmpty) return _usaStateOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                        return _usaStateOptions;
                      } ,
                      label: 'State',
                      onChanged: (String? text) {
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
                      isEnabled: disable
                    ),
                  ),
              ]),
              CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSDatepicker(
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
                      height: _fieldHeight,
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
                      isEnabled: disable,
                    ),
                  ),
                ])
        
            ],
          )
        ),
      ];
    }

    List<Widget> buildMXPlateForm(bool disable, TWSArticleCreatorItemState<Truck>? itemState){
      return <Widget>[
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
                      maxLength: 12,
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
                          null
                        );
                        plates.add(itemState.model.plates[0]);
                        plates.add(generatePlate);
                        itemState.updateModelRedrawing(
                          model.clone(
                            plates: plates,
                          ),
                        );
                      },
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSAutoCompleteField<String>(
                      initialValue: _countryOptions[0],
                      isEnabled: false,
                      optionsBuilder: (String query) {
                        if(query.isNotEmpty) return _countryOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                        return _countryOptions;
                      } ,
                      displayValue:(String item) => item,
                      height: _fieldHeight,
                      label: 'Country',
                      onChanged: (String? value) {
                        Truck model = itemState!.model;
                        List<Plate>? plates =  <Plate>[];
                        final Plate temp = itemState.model.plates[1];
                        Plate generatePlate = Plate(
                          temp.id, 
                          temp.identifier, 
                          temp.state, 
                          value ?? "", 
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
                    )
                  ),
                  Expanded(
                    child: TWSAutoCompleteField<String>(
                      height: _fieldHeight,
                      displayValue: (String query) => query,
                      initialValue: (itemState?.model.plates[1].state ?? "").isEmpty ? _defaultMXstate: itemState!.model.plates[1].state ,
                      optionsBuilder: (String query) {
                        if(query.isNotEmpty) return _mxStateOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                        return _usaStateOptions;
                      } ,
                      label: 'State',
                      onChanged: (String? text) {
                        Truck model = itemState!.model;
                        List<Plate>? plates =  <Plate>[];
                        final Plate temp = itemState.model.plates[1];
                        Plate generatePlate = Plate(
                          temp.id, 
                          temp.identifier, 
                          text ?? "", 
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
                      isEnabled: disable
                    ),
                  ),
              ]),
              CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSDatepicker(
                      height: _fieldHeight,
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
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
                      isEnabled: disable,
                    ),
                  ),
                ]
              )
            ],
          )
        ),
      ];
    }

    List<Widget> buildManfucaturerform(bool disable, TWSArticleCreatorItemState<Truck>? itemState, TextStyle style){
      return <Widget>[
        TWSSection(
          title: "Manufacturer", 
          content: CSMSpacingColumn(
            spacing: 10,
            children: <Widget>[
              Center(
                child: Text(
                  "Create a new manufacturer for this Truck. The 'Select a manufacturer' field must be empty.",
                  style: style,
                )
              ),
              CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSInputText(
                      label: 'Brand',
                      maxLength: 15,
                      controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.brand),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Manufacturer? temp = model.manufacturerNavigation;
                        Manufacturer? updateManufacturer = Manufacturer(
                          0, 
                          temp?.model ?? "", 
                          text, 
                          temp?.year ?? _today, 
                          <Truck>[]
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            manufacturerNavigation: updateManufacturer
                          ),
                        );
                      },
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Model',
                      maxLength: 30,
                      controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.model),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Manufacturer? temp = model.manufacturerNavigation;
                        Manufacturer? updateManufacturer = Manufacturer(
                          0, 
                          text, 
                          temp?.brand ?? "", 
                          temp?.year ?? _today, 
                          <Truck>[]
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            manufacturerNavigation: updateManufacturer
                          ),
                        );
                      },
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSDatepicker(
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
                      label: 'Year',
                      controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.year.dateOnlyString ?? _today.dateOnlyString),
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
                      isEnabled: disable,
                    ),
                  ),
                ]
              )
            ]
          )
        ),
      ];
    }

    List<Widget> buildSituationform(bool disable, TWSArticleCreatorItemState<Truck>? itemState, TextStyle style){
      return <Widget>[
        TWSSection(
          title: "Situation", 
          content: CSMSpacingColumn(
            spacing: 10,
            children: <Widget>[
              Center(
                child: Text(
                  "Create a new situation status for this Truck. The 'Select a situation' field must be empty.",
                  style: style,
                )
              ),
              CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSInputText(
                      maxLength: 25,
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
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Description',
                      maxLength: 100,
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
                      isEnabled: disable,
                    ),
                  )
                ]
              )
            ]
          )
        ),
      ];
    }

    List<Widget> buildMaintenanceform(bool disable, TWSArticleCreatorItemState<Truck>? itemState){
      return <Widget>[
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
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
                      label: 'Anual',
                      controller: TextEditingController(text: itemState?.model.maintenanceNavigation?.anual.dateOnlyString),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Maintenance? temp = model.maintenanceNavigation;
                        Maintenance? updateMaintenance = Maintenance(
                          0, 
                          DateTime.parse(text), 
                          temp?.trimestral ?? _today,  
                          <Truck>[]
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            maintenanceNavigation: updateMaintenance
                          ),
                        );
                      },
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSDatepicker(
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
                      label: 'Trimestral',
                      controller: TextEditingController(text: itemState?.model.maintenanceNavigation?.trimestral.dateOnlyString),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Maintenance? temp = model.maintenanceNavigation;
                        Maintenance? updateMaintenance = Maintenance(
                          0,
                          temp?.anual ?? _today,
                          DateTime.parse(text),   
                          <Truck>[]
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            maintenanceNavigation: updateMaintenance
                          ),
                        );
                      },
                      isEnabled: disable,
                    ),
                  )
                ]
              )
            ]
          )
        ),
      ];
    }

    List<Widget> buildInsuranceform(bool disable, TWSArticleCreatorItemState<Truck>? itemState){
      return <Widget>[
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
                      maxLength: 20,
                      label: 'Policy',
                      controller: TextEditingController(text: itemState?.model.insuranceNavigation?.policy),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Insurance? temp = model.insuranceNavigation;
                        Insurance? updateInsurance = Insurance(
                          0,
                          text,
                          temp?.expiration ?? _today,  
                          temp?.country ?? "", 
                          <Truck>[]
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            insuranceNavigation: updateInsurance
                          ),
                        );
                      },
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSDatepicker(
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
                      label: 'Expiration',
                      controller: TextEditingController(text: itemState?.model.insuranceNavigation?.expiration.dateOnlyString ?? _today.dateOnlyString),
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
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSAutoCompleteField<String>(
                      optionsBuilder: (String query) {
                        if(query.isNotEmpty) return _countryOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                        return _countryOptions;
                      } ,
                      displayValue:(String item) => item,
                      label: 'Country',
                      onChanged: (String? value) {
                       Truck model = itemState!.model;
                        Insurance? temp = model.insuranceNavigation;
                        Insurance? updateInsurance = Insurance(
                          0,
                          temp?.policy ?? "",
                          temp?.expiration ?? _today,  
                          value ?? "", 
                          <Truck>[]
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            insuranceNavigation: updateInsurance
                          ),
                        );
                      },
                    )
                  )
                ]
              )
            ]
          )
        ),
      ];
    }

    List<Widget> buildSCTform(bool disable, TWSArticleCreatorItemState<Truck>? itemState){
      return <Widget>[
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
                      maxLength: 6,
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
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Number',
                      maxLength: 25,
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
                      isEnabled: disable,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Configuration',
                      maxLength: 10,
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
                      isEnabled: disable,
                    ),
                  )
                ]
              )
            ]
          )
        )
      ];
    }
    
    String displayModel(Manufacturer? manufacturer){
      String data = "---";
      if(manufacturer != null && (manufacturer.brand.isNotEmpty  && manufacturer.model.isNotEmpty)){
        data = "${manufacturer.brand} ${manufacturer.model} ${manufacturer.year.year}";
      }
      return data;
    }
    
    String displayInsurance(Insurance?  insurance){
      String data = "---";
      if(insurance != null && (insurance.country.isNotEmpty && insurance.policy.isNotEmpty)){
        data = insurance.policy;
      }
      return data;
    }

    String displaySCT(SCT?  sct){
      String data = "---";
      if(sct != null && (sct.configuration.isNotEmpty && sct.number.isNotEmpty && sct.type.isNotEmpty)){
        data = sct.number;
      }
      return data;
    }

    String displayPlate(Plate?  plate){
      String data = "---";
      if(plate != null && (plate.country.isNotEmpty && plate.identifier.isNotEmpty && plate.state.isNotEmpty)){
        data = plate.identifier;
      }
      return data;
    }
  @override 
  Widget compose(BuildContext ctx, Size window){
    final TWSArticleCreatorAgent<Truck> creatorAgent = TWSArticleCreatorAgent<Truck>();
    CSMColorThemeOptions themeSctruct = getTheme<TWSAThemeBase>().primaryControlColor;
    TextStyle sectionsLinesStyle =  TextStyle(
      fontWeight: FontWeight.w300,
      fontSize: 12,
      color: themeSctruct.hightlightAlt ?? Colors.white,
      fontStyle: FontStyle.italic
    );

    

    return WhisperFrame(
      title: 'Create trucks',
      trigger: creatorAgent.create,
      child:  CSMDynamicWidget<_TruckCreateWhisteState>(
        state: _formsState,
        designer: (_, _TruckCreateWhisteState state) {
          return TWSArticleCreator<Truck>(
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
                  label: 'Model',
                  minWidth: 150,
                  value: displayModel(actualModel.manufacturerNavigation),
                ),
                TwsArticleCreationStackItemProperty(
                  label: 'Situation',
                  minWidth: 150,
                  value: actualModel.situationNavigation?.name,
                ),
                TwsArticleCreationStackItemProperty(
                  label: 'USA Plate',
                  minWidth: 150,
                  value: displayPlate(actualModel.plates[0]),
                ),
                TwsArticleCreationStackItemProperty(
                  label: 'MX Plate',
                  minWidth: 150,
                  value: displayPlate(actualModel.plates[1]),
                ),   
                TwsArticleCreationStackItemProperty(
                  label: 'Insurance Policy',
                  minWidth: 150,
                  value: displayInsurance(actualModel.insuranceNavigation),
                ),
                TwsArticleCreationStackItemProperty(
                  label: 'Anual Maint.',
                  minWidth: 150,
                  value: actualModel.maintenanceNavigation?.anual.dateOnlyString,
                ),
                TwsArticleCreationStackItemProperty(
                  label: 'Trimestral Maint.',
                  minWidth: 150,
                  value: actualModel.maintenanceNavigation?.trimestral.dateOnlyString,
                ),
                TwsArticleCreationStackItemProperty(
                  label: 'SCT Number',
                  minWidth: 150,
                  value: displaySCT(actualModel.sctNavigation),
                ),
              ], 
            );
          },
          formDesigner: (TWSArticleCreatorItemState<Truck>? itemState) {  
            final bool formDisabled = !(itemState == null);
            final bool disableManufacturer = formDisabled && itemState.model.manufacturer == 0;
            final bool disableSituations = formDisabled && (itemState.model.situation == 0 || itemState.model.situation == null);
            final ScrollController scrollController = ScrollController();
            return SingleChildScrollView(
              controller: scrollController,
              child: Padding(
                padding: const EdgeInsets.all(5),
            child: CSMSpacingColumn(
              mainSize: MainAxisSize.min,
              spacing: 12,
              crossAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                ..._buildMainPropertiesForm(formDisabled, itemState, state),
                ...buildUSAPlateForm(formDisabled, itemState),
                ...buildMXPlateForm(formDisabled, itemState),
                Center(
                  child: Text(
                    "<Optional Sections>",
                    style: sectionsLinesStyle.copyWith(fontSize: 18)
                  ),
                ),
                ...buildManfucaturerform(disableManufacturer, itemState, sectionsLinesStyle),
                ...buildSituationform(disableSituations, itemState, sectionsLinesStyle),
                ...buildMaintenanceform(formDisabled, itemState),
                ...buildInsuranceform(formDisabled, itemState),
                ...buildSCTform(formDisabled, itemState)
            ],
                    ),
                  )
                );
          });
        },
         
      )   
);
}}