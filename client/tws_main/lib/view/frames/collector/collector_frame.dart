
import 'package:cosmos_foundation/theme/csm_theme_manager.dart';
import 'package:cosmos_foundation/theme/models/options/csm_color_theme_options.dart';
import 'package:cosmos_foundation/theme/models/options/csm_state_theme_options.dart';
import 'package:cosmos_foundation/widgets/widgets_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/frames/collector/options/collector_options.dart';
import 'package:tws_main/view/frames/collector/options/collector_text_option.dart';
import 'package:tws_main/view/frames/collector/options/collector_switch_option.dart';
import 'package:tws_main/view/frames/collector/table/collector_data.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_button_chip.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';
import 'package:tws_main/view/widgets/tws_section.dart';
import 'package:tws_main/view/widgets/tws_switch_button.dart';

part "fragments/resume_field.dart";
part "fragments/table_header.dart";
part "collector_state.dart";
part "collector_generators.dart";
part "fragments/data_table.dart";
part "fragments/value_details.dart";

//!Testing class
class _Collectorbehavior {
  final Function() textActions;
  final Function() switchActions;

  _Collectorbehavior({required this.textActions, required this.switchActions});

  void collectorValidationType(Type type) {
     switch (type){
      case (const (TWSInputText) || const (CollectorTextOption)):
        textActions();
      break;
      case (const (TWSSwitchButton) || const (CollectorSwitchOption)):
        switchActions();
      break;
      default:
        print("Error on collectorValidationType. Error in type: $type");
      break;
     }
     
  }
}

/// This [CollectorFrameTable] Widget component build a [WhisperFrame]. This component is divided into two sections:
/// The firts section is [_DataTable], Allows adding an undefined quantity of rows items, in a List builder.
/// This items stores the current data setted for the item,this data can be set or modified in the [_ValueDetails] section.
/// The [_DataTable] section also contains a [_CollectorHeader] component, 
/// that allows add or delete the current item selected.
///
/// The second section is _[_ValueDetails]. 
/// This section shows a list of inputs that set the data for the item selected in [_DataTable].
/// The list of valid Input Class is: .....
/// This list of inputs is configurable via parameters.
/// 
/// On press the [WhisperFrame] acept button, all row data will be validated 
/// with its own validation method specified on [inputTemplate] List.
/// If all the validation was succesful, then the component return a [CollectorData] Object list.
class CollectorFrameTable extends StatelessWidget {
  final CSMStateBase state;
  /// If valitations not pass, then the success parameter will be false.
  final void Function(List<List<CollectorData>>? result,  bool success) onSummit;
  const CollectorFrameTable({
    super.key,
    required this.state,
    required this.onSummit
  });

  @override
  Widget build(BuildContext context) {
    _globalState = state;
    if (_firstRun) {
      _addItem();
      _buildValueDetails();
    }

    return LayoutBuilder(
        builder: (BuildContext context, BoxConstraints constraints) {
      // Actualy the max height measure
      final double maxHeight = constraints.minHeight;
      final double scrollHeigthLimit = maxHeight - 165;
      final double tableDataHeigth = maxHeight - 320;
      final double vdInternalHeight = scrollHeigthLimit - 80;
      // Expands the Data table has it is posible, based on [_maxFloatSectionWidth]
      final double floatSectionSupposedWidth = constraints.maxWidth * 0.55;
      // Validate if components width has an overflow.
      final bool hasPassedThreshold = constraints.maxWidth <
          (_maxFloatSectionWidth + _kMinFocusSectionWidth);
      // Value Details section width
      final double floatSectionWidth = hasPassedThreshold
          ? constraints.maxWidth
          : _floatSectionConstrains.constrainWidth(floatSectionSupposedWidth);
      // Data Table Section width
      final double focusSectionWidth = hasPassedThreshold
          ? constraints.maxWidth
          : constraints.maxWidth - floatSectionWidth - 50;

      return WhisperFrame(
        title: "Trucks",
        trigger: () => onSummit(_tableValues, _retriveData()),
        child: ConstrainedBox(
          constraints: BoxConstraints(maxHeight: scrollHeigthLimit),
          child: SingleChildScrollView(
            child: Wrap(
              children: <Widget>[
                //Table Data -> Responsive Sizedbox
                SizedBox(
                  width: focusSectionWidth,
                  child: _DataTable(
                    itemName: "Truck",
                    listHeight: tableDataHeigth)
                ),
                //Value Details -> Responsive Sizedbox
                SizedBox(
                  width: floatSectionWidth,
                  child: _ValueDetails(heigth: vdInternalHeight)
                )
              ],
            ),
          ),
        ),
      );
    });
  }
}
