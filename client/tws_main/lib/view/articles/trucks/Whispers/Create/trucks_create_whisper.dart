import 'package:cosmos_foundation/router/router_module.dart';
import 'package:cosmos_foundation/widgets/widgets_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/view/frames/collector/collector_frame.dart';
import 'package:tws_main/view/frames/collector/table/collector_data.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';

part 'trucks_create_whisper_state.dart';

typedef _State = _TrucksCreateWhisperState;

class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    return CSMDynamicWidget<_State>(
      state: _State(),
      designer: (BuildContext ctx,CSMStateBase state) {
        return CollectorFrameTable(
              state: state,
              onSummit: (List<List<CollectorData>>? result, bool success) {
                print("OBTUVE RESULTADO $success......");
                print(result);
              },
              // constraints: constraints,
            );
      },
    );
  }

}