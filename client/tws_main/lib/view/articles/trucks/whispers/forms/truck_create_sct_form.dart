part of '../truck_create_whisper.dart';

class _TruckCreateSCT extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  const _TruckCreateSCT({
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return TWSSection(
      isOptional: true,
      padding: const EdgeInsets.symmetric(vertical: 10),
      title: "SCT*", 
      content: CSMSpacingColumn(
        spacing: 10,
        children: <Widget>[
          CSMSpacingRow(
            spacing: 10,
            children: <Widget>[
              Expanded(
                child: TWSInputText(
                  label: 'Type',
                  maxLength: 6,
                  controller: TextEditingController(text: itemState?.model.sctNavigation?.type),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    SCT? temp = model.sctNavigation;
                    SCT? updateSCT = SCT(
                      0,
                      text,
                      temp?.number ?? "",  
                      temp?.configuration ?? "", 
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        sctNavigation: updateSCT
                      ),
                    );
                  },
                  isEnabled: enable,
                ),
              ),
              Expanded(
                child: TWSInputText(
                  label: 'Number',
                  maxLength: 25,
                  isStrictLength: true,
                  controller: TextEditingController(text: itemState?.model.sctNavigation?.number),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    SCT? temp = model.sctNavigation;
                    SCT? updateSCT = SCT(
                      0,
                      temp?.type ?? "",
                      text,  
                      temp?.configuration ?? "", 
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        sctNavigation: updateSCT
                      ),
                    );
                  },
                  isEnabled: enable,
                ),
              ),
              Expanded(
                child: TWSInputText(
                  label: 'Configuration',
                  maxLength: 10,
                  controller: TextEditingController(text: itemState?.model.sctNavigation?.configuration),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    SCT? temp = model.sctNavigation;
                    SCT? updateSCT = SCT(
                      0,
                      temp?.type ?? "",
                      temp?.number ?? "",  
                      text, 
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        sctNavigation: updateSCT
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