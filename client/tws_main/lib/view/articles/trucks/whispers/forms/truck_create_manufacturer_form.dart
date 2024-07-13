part of '../truck_create_whisper.dart';

class _TruckCreateMaintenanceState extends CSMStateBase{}
final _TruckCreateMaintenanceState _maintenanceformState = _TruckCreateMaintenanceState();

class _TruckCreateManufacturer extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  final TextStyle style;
  const _TruckCreateManufacturer({
    required this.style,
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return CSMDynamicWidget<_TruckCreateMaintenanceState>(
      state: _maintenanceformState,
      designer: (_, _TruckCreateMaintenanceState state){
        final bool isEnable = itemState != null &&  (itemState!.model.manufacturer == 0 || itemState!.model.manufacturerNavigation == null);
        _maintenanceState = state.effect;
        return TWSSection(
          padding: const EdgeInsets.symmetric(vertical: 10),
          title: "Manufacturer*", 
          isOptional: true,
          content: CSMSpacingColumn(
            spacing: 10,
            children: <Widget>[
              Center(
                child: Text(
                  "Create a new manufacturer for this Truck. The 'Select a manufacturer' field must be empty.",
                  style: style,
                )
              ),
              CSMSpacingRow(
                crossAlignment: CrossAxisAlignment.start,
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSInputText(
                      label: 'Brand',
                      maxLength: 15,
                      controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.brand),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Manufacturer? temp = model.manufacturerNavigation;
                        Manufacturer? updateManufacturer = Manufacturer(
                          0, 
                          temp?.model ?? "", 
                          text, 
                          temp?.year ?? _today, 
                          <Truck>[]
                        );
                        itemState!.updateModelRedrawing(
                          model.clone(
                            manufacturerNavigation: updateManufacturer
                          ),
                        );
                      },
                      isEnabled: isEnable,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Model',
                      maxLength: 30,
                      controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.model),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Manufacturer? temp = model.manufacturerNavigation;
                        Manufacturer? updateManufacturer = Manufacturer(
                          0, 
                          text, 
                          temp?.brand ?? "", 
                          temp?.year ?? _today, 
                          <Truck>[]
                        );
                        itemState!.updateModelRedrawing(
                          model.clone(
                            manufacturerNavigation: updateManufacturer
                          ),
                        );
                      },
                      isEnabled: isEnable,
                    ),
                  ),
                  Expanded(
                    child: TWSDatepicker(
                      firstDate: _firstDate,
                      lastDate: _lastlDate,
                      label: 'Year',
                      controller: TextEditingController(text: itemState?.model.manufacturerNavigation?.year.dateOnlyString ?? _today.dateOnlyString),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Manufacturer? temp = model.manufacturerNavigation;
                        Manufacturer? updateManufacturer = Manufacturer(
                          0, 
                          temp?.model ?? "", 
                          temp?.model ?? "", 
                          DateTime.parse(text), 
                          <Truck>[]
                        );
                        itemState!.updateModelRedrawing(
                          model.clone(
                            manufacturerNavigation: updateManufacturer
                          ),
                        );
                      },
                      isEnabled: isEnable,
                    ),
                  ),
                ]
              )
            ]
          )
        );
      }
    );
  }
}