part of '../trucks_create_whisper.dart';

class _TruckExternalForm extends StatelessWidget {
  final TWSArticleCreatorItemState<Object>? itemState;
  final bool formDisabled;
  final TextStyle style;
  final TruckExternal item;
  const _TruckExternalForm({
    required this.style,
    required this.item,
    this.itemState,
    this.formDisabled = true,
  });
  @override
  Widget build(BuildContext context) {
    return CSMSpacingColumn(
      mainSize: MainAxisSize.min,
      spacing: 12,
      crossAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        CSMSpacingRow(
          spacing: 10,
          children: <Widget>[
            Expanded(
              child: TWSInputText(
                maxLength: 17,
                label: 'VIN',
                isStrictLength: true,
                controller: TextEditingController(text: item.vin),
                onChanged: (String text) {
                  TruckExternal model = itemState!.model as TruckExternal;
                  itemState!.updateModelRedrawing(
                    model.clone(
                      vin: text,
                    ),
                  );
                },
                isEnabled: formDisabled,
              ),
            ),
            Expanded(
              child: TWSInputText(
                maxLength: 16,
                label: 'Economic',
                isStrictLength: false,
                controller: TextEditingController(
                    text: item.truckCommonNavigation?.economic),
                onChanged: (String text) {
                  TruckExternal model = itemState!.model as TruckExternal;
                  itemState!.updateModelRedrawing(
                    model.clone(
                      truckCommonNavigation:
                          model.truckCommonNavigation?.clone(economic: text) ??
                              TruckCommon.a().clone(economic: text),
                    ),
                  );
                },
                isEnabled: formDisabled,
              ),
            ),
          ],
        ),
        CSMSpacingRow(
          spacing: 10,
          children: <Widget>[
            Expanded(
              child: TWSInputText(
                maxLength: 12,
                label: 'MX Plate',
                isStrictLength: false,
                controller: TextEditingController(text: item.mxPlate),
                onChanged: (String text) {
                  TruckExternal model = itemState!.model as TruckExternal;
                  itemState!.updateModelRedrawing(
                    model.clone(
                      mxPlate: text,
                    ),
                  );
                },
                isEnabled: formDisabled,
              ),
            ),
            Expanded(
              child: TWSInputText(
                maxLength: 12,
                label: 'USA Plate',
                isStrictLength: false,
                controller: TextEditingController(text: item.usaPlate),
                onChanged: (String text) {
                  TruckExternal model = itemState!.model as TruckExternal;
                  itemState!.updateModelRedrawing(
                    model.clone(
                      usaPlate: text,
                    ),
                  );
                },
                isEnabled: formDisabled,
              ),
            ),
          ],
        ),
        CSMSpacingRow(spacing: 10, children: <Widget>[
          Expanded(
            child: TWSInputText(
              maxLength: 100,
              label: 'Carrier',
              isStrictLength: false,
              controller: TextEditingController(text: item.carrier),
              onChanged: (String text) {
                TruckExternal model = itemState!.model as TruckExternal;
                itemState!.updateModelRedrawing(
                  model.clone(
                    carrier: text,
                  ),
                );
              },
              isEnabled: formDisabled,
            ),
          ),
        ]),
      ],
    );
  }
}
