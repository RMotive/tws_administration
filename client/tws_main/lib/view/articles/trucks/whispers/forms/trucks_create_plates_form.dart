part of '../trucks_create_whisper.dart';

class _TruckCreatePlateForm extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool isUSAPlate;
  final bool enable;
  const _TruckCreatePlateForm({
    this.itemState,
    this.enable = true,
    this.isUSAPlate = true
  });

  @override
  Widget build(BuildContext context) {
    final int plateIndex = isUSAPlate? 0 : 1;
    final List<String> statesRepository = isUSAPlate? _usaStateOptions : _mxStateOptions;
    return TWSSection(
      padding: const EdgeInsets.symmetric(vertical: 10),
      title: "${_countryOptions[plateIndex]} Plate", 
      content: CSMSpacingColumn(
        mainSize: MainAxisSize.min,
        spacing: 10,
        children: <Widget>[
          CSMSpacingRow(
            crossAlignment: CrossAxisAlignment.start,
            spacing: 10,
            children: <Widget>[
              Expanded(
                child: TWSInputText(
                  label: 'Identifier',
                  isStrictLength: true,
                  maxLength: 12,
                  controller: TextEditingController(text: itemState?.model.plates[plateIndex].identifier),
                  onChanged: (String text) {
                    if(itemState != null){
                      Truck model = itemState!.model;
                      model.plates[plateIndex] = model.plates[plateIndex].clone(
                        identifier: text
                      );
                      itemState?.updateModelRedrawing(
                        model.clone(
                          plates: model.plates,
                        ),
                      );
                    }
                  },
                  isEnabled: enable,
                ),
              ),
              Expanded(
                child: TWSAutoCompleteField<String>(
                  initialValue: _countryOptions[plateIndex],
                  isEnabled: false,
                  optionsBuilder: (String query) {
                    if(query.isNotEmpty) return _countryOptions.where((String country) => country.toLowerCase().contains(query)).toList();
                    return _countryOptions;
                  } ,
                  displayValue:(String item) => item,
                  label: 'Country',
                  onChanged: (String? value) {
                    if(itemState != null){
                      Truck model = itemState!.model;
                      model.plates[plateIndex] = model.plates[plateIndex].clone(
                        country: value ?? ""
                      );
                      
                      itemState?.updateModelRedrawing(
                        model.clone(
                          plates: model.plates,
                        ),
                      );
                    }
                  },
                )
              ),
              Expanded(
                child: TWSAutoCompleteField<String>(
                  displayValue: (String query) => query,
                  initialValue: itemState?.model.plates[plateIndex].state == "" ? null : itemState?.model.plates[plateIndex].state,
                  optionsBuilder: (String query) {
                    if(query.isNotEmpty) return statesRepository.where((String country) => country.toLowerCase().contains(query)).toList();
                    return statesRepository;
                  },
                  label: 'State',
                  onChanged: (String? text) {
                    Truck model = itemState!.model;
                    
                     model.plates[plateIndex] = model.plates[plateIndex].clone(
                      state: text ?? ""
                    );
                    itemState?.updateModelRedrawing(
                      model.clone(
                        plates: model.plates,
                      ),
                    );
                  },
                  isEnabled: enable
                ),
              ),
          ]),
          CSMSpacingRow(
            spacing: 10,
            children: <Widget>[
              Expanded(
                child: TWSDatepicker(
                  firstDate: _firstDate,
                  lastDate: _lastlDate,
                  label: 'Expiration Date',
                  controller: TextEditingController(text: itemState?.model.plates[plateIndex].expiration.dateOnlyString),
                  onChanged: (String text) {
                    Truck model = itemState!.model;
                     model.plates[plateIndex] = model.plates[plateIndex].clone(
                      expiration: DateTime.parse(text)
                    );
                    itemState?.updateModelRedrawing(
                      model.clone(
                        plates: model.plates,
                      ),
                    );
                  },
                  isEnabled: enable,
                ),
              ),
            ]
          )
        ],
      )
    );
  }
}