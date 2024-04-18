import 'package:cosmos_foundation/router/router_module.dart';
import 'package:cosmos_foundation/widgets/widgets_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

part 'trucks_create_whisper_state.dart';

typedef _State = _TrucksCreateWhisperState;

class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    return CSMDynamicWidget<_State>(
      state: _State(), 
      designer:(BuildContext ctx, CSMStateBase state){
        return const WhisperFrame(
          title: 'Truck creation',
          child: CSMForm(
            options: CSMFormOptions(
              name: 'truck-creation',
            ),
            child: SizedBox(
              width: double.maxFinite,
              child: Padding(
                padding: EdgeInsets.symmetric(
                  vertical: 16,
                  horizontal: 32,
                ),
                child: Wrap(
                  children: <Widget>[
                    /// --> Input for [Feature Name]
                    TWSInputText(
                      label: 'VIN',
                      hint: 'Identification name for the Truck',
                      width: 325,
                    ),
                    TWSInputText(
                      label: 'PLATE',
                      hint: 'Identification name for the Truck',
                      width: 325,
                    ),
                    TWSInputText(
                      label: 'SCT',
                      hint: 'Identification name for the Truck',
                      width: 325,
                    ),
                    TWSInputText(
                      label: 'Maintenance',
                      hint: 'Identification name for the Truck',
                      width: 325,
                    ),
                    TWSInputText(
                      label: 'Brand',
                      hint: 'Identification name for the Truck',
                      width: 325,
                    ),
                  ],
                ),
              ),
            ),
          ),
        );

      });
  }

}