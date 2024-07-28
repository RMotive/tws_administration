part of '../trucks_create_whisper.dart';

class _TruckCreateSituationState extends CSMStateBase{}
final _TruckCreateSituationState _situationformState = _TruckCreateSituationState();

class _TruckCreateSituation extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  final TextStyle style;
  const _TruckCreateSituation({
    required this.style,
    this.itemState,
    this.enable = true,
  });


  @override
  Widget build(BuildContext context) {
    return CSMDynamicWidget<_TruckCreateSituationState>(
      state: _situationformState, 
      designer: (_,_TruckCreateSituationState state){
        _situationState = state.effect;

        final bool isEnable = itemState != null &&  (itemState!.model.situation == 0 || itemState!.model.situation == null);
        return TWSSection(
          isOptional: true,
          padding: const EdgeInsets.symmetric(vertical: 10),
          title: "Situation*", 
          content: CSMSpacingColumn(
            spacing: 10,
            children: <Widget>[
              Center(
                child: Text(
                  "Create a new situation status for this Truck. The 'Select a situation' field must be empty.",
                  style: style,
                )
              ),
              CSMSpacingRow(
                spacing: 10,
                children: <Widget>[
                  Expanded(
                    child: TWSInputText(
                      maxLength: 25,
                      label: 'Name',
                      controller: TextEditingController(text: itemState?.model.situationNavigation?.name),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Situation? temp = model.situationNavigation;
                        Situation? updateSituation = Situation(
                          0, 
                          text, 
                          temp?.description,  
                          <Truck>[]
                        );
                        itemState!.updateModelRedrawing(
                          model.clone(
                            situationNavigation: updateSituation
                          ),
                        );
                      },
                      isEnabled: isEnable,
                    ),
                  ),
                  Expanded(
                    child: TWSInputText(
                      label: 'Description',
                      maxLength: 100,
                      suffixLabel: " (Optional)",
                      isOptional: true,
                      controller: TextEditingController(text: itemState?.model.situationNavigation?.description),
                      onChanged: (String text) {
                        Truck model = itemState!.model;
                        Situation? temp = model.situationNavigation;
                        Situation? updateSituation = Situation(
                          0, 
                          temp?.name ?? "", 
                          text,  
                          <Truck>[]
                        );
                        itemState!.updateModelRedrawing(
                          model.clone(
                            situationNavigation: updateSituation
                          ),
                        );
                      },
                      isEnabled: isEnable,
                    ),
                  )
                ]
              )
            ]
          )
        );
      }
    );
  }
}