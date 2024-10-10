part of '../trucks_create_whisper.dart';

class _TruckCreateMainForm extends StatelessWidget {
 final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  const _TruckCreateMainForm({
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return CSMSpacingColumn(
      spacing: 10,
      children: <Widget>[
        CSMSpacingRow(
          spacing: 10,
          children: <Widget>[
            Expanded(
              child: TWSInputText(
                maxLength: 17,
                isStrictLength: true,
                label: 'VIN',
                controller: TextEditingController(text: itemState?.model.vin),
                onChanged: (String text) {
                  Truck model = itemState!.model;
                  itemState!.updateModelRedrawing(
                    model.clone(
                      vin: text,
                    ),
                  );
                },
                isEnabled: enable,
              ),
            ),
            Expanded(
              child: TWSInputText(
                maxLength: 16,
                label: 'Motor',
                isStrictLength: true,
                controller: TextEditingController(text: itemState?.model.motor),
                onChanged: (String text) {
                  Truck model = itemState!.model;
                  itemState!.updateModelRedrawing(
                    model.clone(
                      motor: text,
                    ),
                  );
                },
                isEnabled: enable,
              ),
            ),
          ],
        ),
        const CSMSpacingRow(
          spacing: 10,
          children: <Widget>[
            // Expanded(
            //   child: TWSFutureAutoCompleteField<Manufacturer>(
            //     label: "Select a Manufacturer",
            //     hint: "Assing an existing manufacturer",
            //     isOptional: true,
            //     initialValue: itemState?.model.manufacturerNavigation ,
            //     displayValue: (Manufacturer set) => "${set.brand} ${set.model}" ,
            //     isEnabled: enable,
            //     adapter:  const _ManufacturerViewAdapter(),
            //     onChanged: (Manufacturer? selectedItem) {  
            //       Truck model = itemState!.model;
            //       itemState!.updateModelRedrawing(
            //         model.clone(
            //           manufacturer: selectedItem?.id ?? 0,
            //           manufacturerNavigation: selectedItem
            //         )
            //       );
            //       _maintenanceState();
            //     }, 

            //   ),
            // ),
            
          ],
        ),
      ]);
  }
}