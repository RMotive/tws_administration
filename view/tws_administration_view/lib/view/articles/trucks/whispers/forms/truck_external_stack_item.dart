part of '../trucks_create_whisper.dart';

class _TruckExternalStackItem extends StatelessWidget {
  final TruckExternal actualModel;
  final bool selected;
  final bool valid;
  const _TruckExternalStackItem({
    required this.actualModel,
    required this.selected,
    required this.valid,
  });

  String displayInsurance(Insurance? insurance) {
    String data = "---";
    if (insurance != null && (insurance.country.isNotEmpty && insurance.policy.isNotEmpty)) {
      data = insurance.policy;
    }
    return data;
  }

  @override
  Widget build(BuildContext context) {
    return TWSArticleCreationStackItem(
      selected: selected,
      valid: valid,
      properties: <TwsArticleCreationStackItemProperty>[
        TwsArticleCreationStackItemProperty(
          label: 'VIN',
          minWidth: 150,
          value: actualModel.vin ?? '---',
        ),
        TwsArticleCreationStackItemProperty(
          label: 'Economic',
          minWidth: 150,
          value: actualModel.truckCommonNavigation?.economic ?? '---',
        ),
        TwsArticleCreationStackItemProperty(
          label: 'Carrier',
          minWidth: 150,
          value: actualModel.carrier,
        ),
        TwsArticleCreationStackItemProperty(
          label: 'MX Plate',
          minWidth: 150,
          value: actualModel.mxPlate ?? '---',
        ),
        TwsArticleCreationStackItemProperty(
          label: 'USA Plate',
          minWidth: 150,
          value: actualModel.usaPlate ?? '---',
        ),
      ],
    );
  }
}
