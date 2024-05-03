part of "../collector_frame.dart";

///[_ValueDetails] Class that shows the [_ValueDetails] Section.
///This section contains the input type specified by the main component [inputTemplate] parameter.
///Build the input template and overwrite the data of the selected item in [_DataTable] section.
class _ValueDetails extends StatelessWidget {
  final double heigth;
  const _ValueDetails({
    required this.heigth
  });

  @override
  Widget build(BuildContext context) {
    return TWSSection(
      title: "Value Details",
      content: Container(
        width: double.maxFinite,
        padding: const EdgeInsets.symmetric(horizontal: 5, vertical: 20),
        constraints: BoxConstraints(minHeight: heigth),
        decoration: BoxDecoration(
          border: Border.fromBorderSide(
            BorderSide(
              color: _pageTheme.highlight,
              width: 2,
            ),
          ),
        ),
        child: Column(
          children: <Widget>[
            Visibility(
              visible: _selectedItem < 0,
              child: const Padding(
                padding: EdgeInsets.only(
                  top: 20,
                ),
                child: TWSDisplayFlat(
                  width: 400,
                  verticalPadding: 10,
                  display: 'Item Not selected',
                ),
              ),
            ),
            Visibility(
              visible: _selectedItem >= 0,
              maintainState: true,
              child: Form(
                key: _formKey,
                child: Wrap(
                    runSpacing: 20,
                    spacing: 20,
                    alignment: WrapAlignment.start,
                    children: _valueDetails),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
