part of '../trailers_create_whisper.dart';

class _TrailerStackItem extends StatelessWidget {
  final Trailer actualModel;
  final bool selected;
  final bool valid;
  const _TrailerStackItem({
    required this.actualModel,
    required this.selected,
    required this.valid,
  });

  String displayInsurance(Insurance? insurance) {
    if (insurance != null && (insurance.country.isNotEmpty && insurance.policy.isNotEmpty)) return insurance.policy;
    return "---";
  }

  String displayPlate(Plate? plate) {
    if (plate != null && (plate.country.isNotEmpty && plate.identifier.isNotEmpty)) return "${plate.country} - ${plate.identifier}";
    return "---";
  }

  String displaySCT(SCT? sct) {
    if (sct != null &&
        (sct.configuration.isNotEmpty &&
            sct.number.isNotEmpty &&
            sct.type.isNotEmpty)) return sct.number;
    return "---";
  }

  String displayMaintenance(Maintenance? maintenance, bool isAnual) {
    if (maintenance != null) {
      if (isAnual && maintenance.anual != DateTime(0)) return maintenance.anual.dateOnlyString;
      if (!isAnual && maintenance.trimestral != DateTime(0)) return maintenance.trimestral.dateOnlyString;
    }
    return "---";
  }

  @override
  Widget build(BuildContext context) {
    return TWSArticleCreationStackItem(
      selected: selected,
      valid: valid,
      properties: <TwsArticleCreationStackItemProperty>[
        TwsArticleCreationStackItemProperty(
          label: 'Economic',
          minWidth: 150,
          value: actualModel.trailerCommonNavigation?.economic,
        ),
        TwsArticleCreationStackItemProperty(
          label: 'Type',
          minWidth: 150,
          value: actualModel.trailerCommonNavigation?.trailerTypeNavigation !=
                  null
              ? "${actualModel.trailerCommonNavigation?.trailerTypeNavigation?.trailerClassNavigation?.name} - ${actualModel.trailerCommonNavigation?.trailerTypeNavigation?.size}"
              : "---",
        ),
        TwsArticleCreationStackItemProperty(
          label: 'Model',
          minWidth: 150,
          value: _displayModel(actualModel.vehiculeModelNavigation),
        ),
        TwsArticleCreationStackItemProperty(
          label: "Carrier",
          minWidth: 150,
          value: actualModel.carrierNavigation?.name ?? "---",
        ),
        TwsArticleCreationStackItemProperty(
          label: 'Situation',
          minWidth: 150,
          value: actualModel.trailerCommonNavigation?.situationNavigation?.name ??
              "---",
        ),
        for (int i = 0; i < actualModel.plates.length; i++)
          TwsArticleCreationStackItemProperty(
            label: 'Plate ${i + 1}',
            minWidth: 150,
            value: displayPlate(actualModel.plates[i]),
          ),
        TwsArticleCreationStackItemProperty(
          label: 'Anual Maint.',
          minWidth: 150,
          value: displayMaintenance(actualModel.maintenanceNavigation, true),
        ),
        TwsArticleCreationStackItemProperty(
          label: 'Trimestral Maint.',
          minWidth: 150,
          value: displayMaintenance(actualModel.maintenanceNavigation, false),
        ),
        TwsArticleCreationStackItemProperty(
          label: 'SCT Number',
          minWidth: 150,
          value: displaySCT(actualModel.sctNavigation),
        ),
      ],
    );
  }
}
