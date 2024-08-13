part of '../trucks_create_whisper.dart';

class _TruckCreateMaintenance extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  const _TruckCreateMaintenance({
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return TWSSection(
      isOptional: true,
      padding: const EdgeInsets.symmetric(vertical: 10),
      title: "Maintenance*", 
      content: CSMSpacingColumn(
        spacing: 10,
        children: <Widget>[
          CSMSpacingRow(
            spacing: 10,
            children: <Widget>[
              Expanded(
                child: TWSDatepicker(
                  firstDate: _firstDate,
                  lastDate: _lastlDate,
                  label: 'Anual',
                  controller: TextEditingController(text: itemState?.model.maintenanceNavigation?.anual.dateOnlyString),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    Maintenance? temp = model.maintenanceNavigation;
                    Maintenance? updateMaintenance = Maintenance(
                      0, 
                      DateTime.parse(text), 
                      temp?.trimestral ?? _today,  
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        maintenanceNavigation: updateMaintenance
                      ),
                    );
                  },
                  isEnabled: enable,
                ),
              ),
              Expanded(
                child: TWSDatepicker(
                  firstDate: _firstDate,
                  lastDate: _lastlDate,
                  label: 'Trimestral',
                  controller: TextEditingController(text: itemState?.model.maintenanceNavigation?.trimestral.dateOnlyString),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    Maintenance? temp = model.maintenanceNavigation;
                    Maintenance? updateMaintenance = Maintenance(
                      0,
                      temp?.anual ?? _today,
                      DateTime.parse(text),   
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        maintenanceNavigation: updateMaintenance
                      ),
                    );
                  },
                  isEnabled: enable,
                ),
              )
            ]
          )
        ]
      )
    );
  }
}