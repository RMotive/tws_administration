part of '../trucks_create_whisper.dart';

class _TruckForm extends StatelessWidget {
  final TWSArticleCreatorItemState<Object>? itemState;
  final bool formDisabled;
  final TextStyle style;
  const _TruckForm({
    required this.style,
    this.itemState,
    this.formDisabled = true,
  });
  @override
  Widget build(BuildContext context) {
    // Truck item = itemState!.model as Truck;
    return CSMSpacingColumn(
      mainSize: MainAxisSize.min,
      spacing: 12,
      crossAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        _TruckCreateMainForm(
          itemState: itemState,
          enable: formDisabled,
        ),
        _TruckCreatePlatesSection(
          style: style,
          itemState: itemState,
          enable: formDisabled,
        ),
        _TruckCreateManufacturer(
          itemState: itemState,
          enable: formDisabled,
          style: style,
        ),
        _TruckCreateSituation(
          itemState: itemState,
          enable: formDisabled,
          style: style,
        ),
        _TruckCreateMaintenance(
          itemState: itemState,
          enable: formDisabled,
        ),
        _TruckCreateInsurance(
          itemState: itemState,
          enable: formDisabled,
        ),
        _TruckCreateSCT(
          itemState: itemState,
          enable: formDisabled,
        )
      ],
    );
  }
}
