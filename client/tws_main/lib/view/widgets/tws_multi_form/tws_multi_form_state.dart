part of "tws_multi_form.dart";

final class _MultiFormState extends CSMStateBase {}

/// [_formKey] GlobalKey for [TWSInputText] Form management.
final GlobalKey<FormState> _formKey = GlobalKey<FormState>();

/// Main Scroll component.
final ScrollController mainScroll = ScrollController();

/// [_firstRun] validates if the component was built 1 time at least.
bool _firstRun = true;

//theme declaration
final TWSAThemeBase _theme = getTheme();
final CSMColorThemeOptions _pageTheme = _theme.pageColor;
final CSMStateThemeOptions _struct = _asrStruct(_theme);

/// [_rowLabelMaxWidth] maxWidth for the labels on table rows.
const double _rowLabelMaxWidth = 150;

/// [_globalState] storage the GlobalKey [CSMStateBase] state.
/// It's values is given on component runtime.
dynamic _globalState;

/// [_selectedItem] Current index for the selected item on the List table.
int _selectedItem = 0;

/// Internal components measures
const double _tdListsMinHeigth = 445;

/// [_maxFloatSectionWidth] Value Details width
//const double _maxFloatSectionWidth = 900;

/// [_kMinFocusSectionWidth] Min Data Table width, pass this value will trigger contrains.maxwidth.
//const double _kMinFocusSectionWidth = 120;

/// [_floatSectionConstrains] Constraints for [floatSectionWidth] when constrains.maxwidth is false.
// const BoxConstraints _floatSectionConstrains = BoxConstraints(
//   maxWidth: _maxFloatSectionWidth,
//   minWidth: _maxFloatSectionWidth - 500,
// );

/// [_headerStyle] Text Style for the header in Table Data section.
final TextStyle _headerStyle = TextStyle(
  fontSize: 14,
  color: _pageTheme.fore,
);

/// [_resumeFieldTitle] Text Style for the title on the [_ResumeField] component
final TextStyle _resumeFieldTitle = TextStyle(fontSize: 14, color: _pageTheme.fore, fontWeight: FontWeight.bold);

/// [_resumeFieldSubtitle] Text Style for the subtitle (content or value caputured by the input components)
/// on the [_ResumeField] component.
final TextStyle _resumeFieldSubtitle = TextStyle(fontSize: 12, color: _pageTheme.fore, fontStyle: FontStyle.italic, fontWeight: FontWeight.w100);

/// Asserts the primary control theme color struct to ensure dependencies.
CSMStateThemeOptions _asrStruct(TWSAThemeBase theme) {
  CSMStateThemeOptions struct = theme.articlesLayoutSelectorButtonState;
  return struct;
}

/// [_tableValues] List that stores the row list data, including the data set on the Value Details Section.
List<List<CollectorData>> _tableValues = <List<CollectorData>>[];

/// [_valueDetails] List that stores the generated widgets based on [inputTemplate].
List<Widget> _valueDetails = <Widget>[];

/// [_retriveType] Method that evaluates the object type given in parameter
///  and generate an [CollectorData] object based on its type.
CollectorData _retriveType(dynamic object) {
  assert(object.runtimeType != CollectorTextOption || object.runtimeType != CollectorSwitchOption, "validateType() - Invalid Object: ${object.runtimeType}");
  if (object.runtimeType == CollectorSwitchOption) {
    return CollectorData(type: object.runtimeType, title: object.label, value: object.defaultvalue);
  }
  return CollectorData(
    type: object.runtimeType,
    title: object.label,
  );

  // if (object.runtimeType == CollectorToggleOption) {
  //   CollectorToggleOption temp = object;
  //   return CollectorData(title: "Toggle test", type: CollectorToggleOption);
  // }

  ///Validate if the current input template is a valid component type
}

/// [_onTapOutside] Method that triggers the validate and rebuild states,
/// used for the OnTapOutside event in [TWSInputText] widget.
void _onTapOutside() {
  _formKey.currentState!.validate();
  _globalState.effect();
}

/// [_addItem] Method that add a new item row to the Table Data Section.
void _addItem() {
  // List of [CollectorData] objects that stores the data to display in [_CollectorResumeField] components.
  List<CollectorData> temp = <CollectorData>[];
  // print("**************************************");

  // Loop that generate the object with its appropiated values.
  for (int cont = 0; cont < inputTemplate.length; cont++) {
    temp.add(_retriveType(inputTemplate[cont]));
    // print("Tipo de objeto agregado en AddItem: ${retriveType(inputTemplate[cont]).type} \n En Index: $cont");
  }
  // print("************************************** \n\n");

  _tableValues.add(temp);

  //Move the current table item selection to the last item added.
  _selectItem(_tableValues.length - 1);
}

/// [_deleteItem] Method that delete the row selection in the Table data section, based the given index parameter.
void _deleteItem(int index) {
  // Validate if the [_selectedItem] index is valid.
  if (_selectedItem >= 0) {
    _tableValues.removeAt(index);
  }
  // Validate if the [_tableValues] is not empty.
  if (_tableValues.isNotEmpty) {
    // Select the last item row in the Table data section.
    _selectItem(_tableValues.length - 1);
  } else {
    // after delete the last row remaining in the Table data section,
    //set the [_selectedItem] to an invalid index value.
    _selectedItem = -1;
  }
}

void _selectItem(int index) {
  /// Data for the selected index in table section.

  final List<CollectorData> rowSelected = _tableValues[index];
  _selectedItem = index;

  for (int cont = 0; cont < _valueDetails.length; cont++) {
    final dynamic tempDetails = _valueDetails[cont];

    /// [rowelement] is a [ResumeField] component for the selected item in [TableData] component.
    final CollectorData rowElement = rowSelected[cont];

    /// generated components for value data section
    final _Collectorbehavior behavior = _Collectorbehavior(textActions: () {
      TWSInputText input = tempDetails;
      input.controller?.value = TextEditingValue(text: rowElement.value ?? "");
      rowElement.value = input.controller?.value.text;
    }, switchActions: () {
      // input.changeValue(rowElement.value);
    });

    behavior.collectorValidationType(tempDetails.runtimeType);
  }

  /// Execute the validation for the current item selection.
  /// This condition is required to update the inputText error state,
  /// due to the errorMessage parameter cannot be implemented properly in this component.
  if (!_firstRun) _formKey.currentState!.validate();
  _firstRun = false;
}

/// [_onChanged] Method to trigger on "onEditingComplete" event in the TWSInputText.
/// This method overwrite the [_ResumeField] on the selected Item based on parameters.
void _onChanged(int inputIndex) {
  // print("Triggering OnEditingText Event.....");
  // print("Parameters given: \n SelectedItem: $selectedItem \n InputIndex: $inputIndex");
  // print("longitud de ValueDetails: ${valueDetails.length}");

  // Individual element to overwrite in row item.
  CollectorData rowElement = _tableValues[_selectedItem][inputIndex];

  /// List with the widgets in Value Details section.
  final dynamic tempDetails = _valueDetails[inputIndex];

  /// Validate the type of the input element
  if (tempDetails.runtimeType == TWSInputText) {
    TWSInputText input = tempDetails;
    rowElement.value = input.controller?.value.text;
    // if(_formKey.currentState!.validate()){
    // If all fields are valid.....
    // }
  }
}

/// [_retriveData] Method, Verify all the stored inputs data using as validator the
/// validator methods given in the component options list parameters.
bool _retriveData() {
  // Success validation variable.
  bool allGood = true;

  //Clear the debug console.
  //Use only in for debuging propurses!
  print('\x1B[2J\x1B[0;0H');

  if (_tableValues.isEmpty) return false;

  /// Iterate each item row in table
  for (int rowcont = 0; rowcont < _tableValues.length; rowcont++) {
    final List<CollectorData> currentRow = _tableValues[rowcont];
    print("📌 Row actual: ${rowcont + 1}");

    /// Iterate each [CollectorData] object in the current row.
    for (int rowFieldCont = 0; rowFieldCont < currentRow.length; rowFieldCont++) {
      final CollectorData currentRowField = currentRow[rowFieldCont];

      /// Current input [inputTemplate] component in Value Details section.
      final dynamic currentInput = inputTemplate[rowFieldCont];

      print("   Evaluando Campo numero ${rowFieldCont + 1} - ${currentRowField.title}.....");
      print("       Contenido: ${currentRowField.value}");

      /// Validate if method validator exist in this object
      if (currentInput.runtimeType == CollectorSwitchOption) {
        print("       Tipo de objeto: ${currentInput.runtimeType}");
      } else if (currentInput.validator != null) {
        /// Validate the input content
        print("       Resultado de Evaluacion: ${currentInput.validator(currentRowField.value ?? "")}");
        //if the valitador method returns an any string content, then set the [allGood]
        //variable as an false, indicating an validation error.
        if (currentInput.validator(currentRowField.value ?? "") != null) {
          print("   🚩 ERROR DETECTADO....");
          allGood = false;
          continue;
        }

        print("   📗 Exitoso");
      }
    }
  }
  // Last validation, to update the state of the current selected item.
  if (!_formKey.currentState!.validate()) {
    print("Fallo en la ultima validacion del form");
    allGood = false;
  }
  // if the validation was succesful, then do this...
  if (allGood) {
    print(" 🎇🎆🎇 All Good.... No hay errores de input..");

    return allGood;
    // Retornar Datos al componente padre......
  } else {
    print("\n🚫🚫🚫 Errro de valicacion, no se concluyo la validacion exitosamente. ");
    // showmessage.....
    return allGood;
  }
}
