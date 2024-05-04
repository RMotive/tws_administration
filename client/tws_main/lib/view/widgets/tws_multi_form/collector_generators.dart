part of "tws_multi_form.dart";

/// [_buildValueDetails] Build the input components for Value Detail section, based on [CollectorOptions] Class.
/// This method evaluate which component will be generated and added to the [_valueDetails] List.
List<Widget> _buildValueDetails() {
  _valueDetails = <Widget>[];

  //For each value in [inputTemplate] evaluates the object type and generate the appropriate component.
  for (int cont = 0; cont < inputTemplate.length; cont++) {
    final dynamic currentTemplate = inputTemplate[cont];
    _Collectorbehavior behavior = _Collectorbehavior(
      switchActions: () => _valueDetails.add(_generateSwitchButton(currentTemplate, cont)),
      textActions: () => _valueDetails.add(_generateInputText(currentTemplate, cont)),
    );
    behavior.collectorValidationType(currentTemplate.runtimeType);
  }
  return _valueDetails;
}

TWSSwitchButton _generateSwitchButton(CollectorSwitchOption content, int index) {
  return TWSSwitchButton(
      title: content.label,
      value: _tableValues[_selectedItem][index].value,
      onChanged: (bool value) {
        _tableValues[_selectedItem][index].value = value;
        _globalState.effect();
      });
}

/// [_generateInputText] generate a [TWSInputText] based on parameters inputs
TWSInputText _generateInputText(CollectorTextOption content, int index) {
  TextEditingController newController = TextEditingController();
  CollectorTextOption content = inputTemplate[index];

  /// Custom declaration of a [TWSInputText] for the [tableValues] List.
  final TWSInputText actual = TWSInputText(
    onChanged: (String value) => _onChanged(index),
    onTapOutside: (PointerDownEvent p0) => _onTapOutside(),
    controller: newController,
    label: content.label,
    hint: content.hint,
    width: content.width,
    validator: (String? value) {
      //[result] Execute the given validators in [inputTemplate] and used the text values stored in it's controller
      String? result = content.validator(newController.text);
      if (_tableValues.isNotEmpty) {
        if (result != null) {
          //If the validator returns any string error, then overwrite the .error property in the
          //current [tablevalues] List index, to display the error on the table row.
          _tableValues[_selectedItem][index].error = "* $result";
          return result;
        }
        //If none validation error was triggered, then the error values is deleted in case
        //that was an string error stored previously.
        _tableValues[_selectedItem][index].error = null;
      }
      return null;
    },
  );

  return actual;
}

///[_generateLabels] Generate the labels list for each row in Table Data section.
List<_ResumeField> _generateLabels(int rowIndex) {
  //Select the current row based on the [rowIndex] parameter
  final List<CollectorData> currentRow = _tableValues[rowIndex];
  List<_ResumeField> labelsList = <_ResumeField>[];
  _ResumeField temporal;

  /// Generate the [_CollectorResumeField] component based on the stored data in [currentRow] List.
  for (int cont = 0; cont < currentRow.length; cont++) {
    temporal = _ResumeField(
      data: currentRow[cont],
      maxWidth: _rowLabelMaxWidth,
      titleStyle: _resumeFieldTitle,
      subtitleStyle: _resumeFieldSubtitle,
    );
    labelsList.add(temporal);
  }
  return labelsList;
}

/// [inputTemplate] Is a list of TWSInputText that the user provides as a template for the data rows.
List<dynamic> inputTemplate = <dynamic>[
  CollectorTextOption(
      required: true,
      label: "VIN",
      hint: "Insert the VIN number",
      width: 250,
      validator: (String? value) {
        if (value!.isEmpty) return "Is empty";
        if (value.length > 20) {
          return "Text too long";
        }
        return null;
      }),
  CollectorTextOption(
      label: "Maintenance",
      hint: "Insert the Maintenance date",
      width: 250,
      validator: (String? value) {
        if (value!.isEmpty) return "Is empty";
        if (value.length > 20) {
          return "Text too long";
        }
      }),
  CollectorTextOption(
      required: true,
      label: "Plate",
      hint: "Insert the Plate number",
      width: 250,
      validator: (String? value) {
        return null;
      }),
  const CollectorSwitchOption(
    label: "Operative",
  ),
  CollectorTextOption(
      required: true,
      label: "SCT",
      hint: "Insert the SCT number",
      width: 250,
      validator: (String? value) {
        return null;
      })
];
