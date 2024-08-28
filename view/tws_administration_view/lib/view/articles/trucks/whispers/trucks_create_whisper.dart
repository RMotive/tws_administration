
import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_administration_view/core/constants/twsa_common_displays.dart';
import 'package:tws_administration_view/core/extension/datetime.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/data/services/sources.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/articles/trucks/trucks_article.dart';
import 'package:tws_administration_view/view/frames/whisper/whisper_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item_property.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_agent.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_creation_item_state.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_creator.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_creator_feedback.dart';
import 'package:tws_administration_view/view/widgets/tws_autocomplete_field.dart';
import 'package:tws_administration_view/view/widgets/tws_datepicker_field.dart';
import 'package:tws_administration_view/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_field.dart';
import 'package:tws_administration_view/view/widgets/tws_input_text.dart';
import 'package:tws_administration_view/view/widgets/tws_section.dart';

part '../options/trucks_whisper_options_adapter.dart';
part 'forms/trucks_create_insurance_form.dart';
part 'forms/trucks_create_main_form.dart';
part 'forms/trucks_create_maintenance_form.dart';
part 'forms/trucks_create_manufacturer_form.dart';
part 'forms/trucks_create_plates_form.dart';
part 'forms/trucks_create_sct_form.dart';
part 'forms/trucks_create_situation_form.dart';

const List<String> _countryOptions = TWSAMessages.kCountryList;
const List<String> _usaStateOptions = TWSAMessages.kUStateCodes;
const List<String> _mxStateOptions = TWSAMessages.kMXStateCodes;
final DateTime _firstDate = DateTime(2000);
final DateTime _lastlDate = DateTime(2040);
final DateTime _today = DateTime.now();
late void Function() _maintenanceState;
late void Function() _situationState;

class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

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
      child: TWSArticleCreator<Truck>(
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
                _TruckCreateMainForm(itemState: itemState, enable: formDisabled),
                _TruckCreatePlateForm(itemState: itemState, enable: formDisabled),
                _TruckCreatePlateForm(itemState: itemState, enable: formDisabled, isUSAPlate: false),
                _TruckCreateManufacturer(itemState: itemState, enable: formDisabled, style: sectionsLinesStyle),
                _TruckCreateSituation(itemState: itemState, enable: formDisabled, style: sectionsLinesStyle),
                _TruckCreateMaintenance(itemState: itemState, enable: formDisabled),
                _TruckCreateInsurance(itemState: itemState, enable: formDisabled),
                _TruckCreateSCT(itemState: itemState, enable: formDisabled)
              ],
            ),
          )
        );
      }
    )
  );
}}