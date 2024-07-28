part of '../trucks_create_whisper.dart';

class _TruckCreateInsurance extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  const _TruckCreateInsurance({
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return TWSSection(
      isOptional: true,
      padding: const EdgeInsets.symmetric(vertical: 10),
      title: "Insurance*", 
      content: CSMSpacingColumn(
        spacing: 10,
        children: <Widget>[
          CSMSpacingRow(
            crossAlignment: CrossAxisAlignment.start,
            spacing: 10,
            children: <Widget>[
              Expanded(
                child: TWSInputText(
                  maxLength: 20,
                  label: 'Policy',
                  isStrictLength: true,
                  controller: TextEditingController(text: itemState?.model.insuranceNavigation?.policy),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    Insurance? temp = model.insuranceNavigation;
                    Insurance? updateInsurance = Insurance(
                      0,
                      text,
                      temp?.expiration ?? _today,  
                      temp?.country ?? "", 
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        insuranceNavigation: updateInsurance
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
                  label: 'Expiration',
                  controller: TextEditingController(text: itemState?.model.insuranceNavigation?.expiration.dateOnlyString ?? _today.dateOnlyString),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                    Insurance? temp = model.insuranceNavigation;
                    Insurance? updateInsurance = Insurance(
                      0,
                      temp?.policy ?? "",
                      DateTime.parse(text),  
                      temp?.country ?? "", 
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        insuranceNavigation: updateInsurance
                      ),
                    );
                  },
                  isEnabled: enable,
                ),
              ),
              Expanded(
                child: TWSAutoCompleteField<String>(
                  initialValue: itemState?.model.insuranceNavigation?.country,
                  optionsBuilder: (String query) {
                    if(query.isNotEmpty) return _countryOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                    return _countryOptions;
                  } ,
                  displayValue:(String item) => item,
                  isOptional: true,
                  label: 'Country',
                  isEnabled: enable,
                  onChanged: (String? value) {
                    Truck model = itemState!.model;
                    Insurance? temp = model.insuranceNavigation;
                    Insurance? updateInsurance = Insurance(
                      0,
                      temp?.policy ?? "",
                      temp?.expiration ?? _today,  
                      value ?? "", 
                      <Truck>[]
                    );
                    itemState!.updateModelRedrawing(
                      model.clone(
                        insuranceNavigation: updateInsurance
                      ),
                    );
                  },
                )
              )
            ]
          )
        ]
      )
    );
  }
}