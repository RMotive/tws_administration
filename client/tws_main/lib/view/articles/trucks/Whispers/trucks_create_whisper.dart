import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_field.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_item_properties.dart';

class TrucksCreateWhisper extends CSMPageBase{
  /// Testing options
  static const List<String> _kOptions = <String>[
    'aardvark',
    'bobcat',
    'chameleon',
  ];
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    
    return WhisperFrame(
      title: 'Create trucks',
      trigger: () {
      },
      child:  Row(
        children: <Widget>[
          TWSAutoCompleteField(
            width: 200,
            height: 40,
            listLength: _kOptions.length,
            onChanged: (String input, TWSAutocompleteItemProperties? selectedItem) {
              
            },
            optionsBuilder: (int index) {  
              return TWSAutocompleteItemProperties(
                showValue: 
                _kOptions[index],
                returnValue: _kOptions[index]
              );
            }   
          ),
        ],
      )
     
    );
  }
}