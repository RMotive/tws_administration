import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_agent.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_item_properties.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_adapter.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_field.dart';

part '../options/trucks_whisper_options_adapter.dart';
class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    print('creando agente...');
    final TWSArticleCreatorAgent<Truck> creatorAgent = TWSArticleCreatorAgent<Truck>();

    return WhisperFrame(
      title: 'Create trucks',
      trigger: creatorAgent.create,
      child:  Row(
        children: <Widget>[
          TWSFutureAutoCompleteField<Manufacturer>(
            width: 200,
            adapter:  const _ManufacturerViewAdapter(),
            onChanged: (String input, TWSAutocompleteItemProperties<Manufacturer>? selectedItem) {  
              
            }, optionsBuilder: (int int , Manufacturer manufacturer) {  
              return TWSAutocompleteItemProperties<Manufacturer>(label: '${manufacturer.brand} - ${manufacturer.model}', value: manufacturer);
          
            }
          ),
          TWSFutureAutoCompleteField<Situation>(
            width: 100,
            adapter:  const _SituationsViewAdapter(),
            onChanged: (String input, TWSAutocompleteItemProperties<Situation>? selectedItem) {  
              
            }, optionsBuilder: (int int , Situation situation) {  
              return TWSAutocompleteItemProperties<Situation>(label: situation.name, value: situation);
          
            }
          )
        ],
      )
     
    );
  }
}