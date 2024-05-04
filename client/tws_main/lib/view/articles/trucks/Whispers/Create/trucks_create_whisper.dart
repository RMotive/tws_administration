import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_multi_form/agents/tws_multi_form_agent.dart';
import 'package:tws_main/view/widgets/tws_multi_form/tws_multi_form.dart';

class TrucksCreateWhisper extends CSMPageBase{
  const TrucksCreateWhisper({super.key});

  @override 
  Widget compose(BuildContext ctx, Size window){
    final TWSMultiFormAgent agent = TWSMultiFormAgent();

    return WhisperFrame(
      title: 'Create trucks',
      child: TWSMultiForm<Solution>(
        agent: agent,
        onSubmit: (List<Solution> records) {},
      ),
      trigger: () {
        agent.submit();
      },
    );
  }
}