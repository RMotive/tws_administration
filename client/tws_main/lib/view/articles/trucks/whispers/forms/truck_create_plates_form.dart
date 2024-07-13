part of '../truck_create_whisper.dart';

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
    final String defaultState = isUSAPlate? _usaStateOptions[4] : _mxStateOptions[1];
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
                    Truck model = itemState!.model;
                    List<Plate>? plates =  <Plate>[];
                    final Plate temp = itemState!.model.plates[plateIndex];
                    Plate generatePlate = Plate(
                      temp.id, 
                      text, 
                      temp.state, 
                      temp.country, 
                      temp.expiration, 
                      0, 
                      null
                    );
                    if(isUSAPlate){
                      plates.add(generatePlate);
                      plates.add(itemState!.model.plates[1]);
                    }else{
                      plates.add(itemState!.model.plates[0]);
                      plates.add(generatePlate);
                    }
                    
                    itemState?.updateModelRedrawing(
                      model.clone(
                        plates: plates,
                      ),
                    );
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
                    Truck model = itemState!.model;
                    List<Plate>? plates =  <Plate>[];
                    final Plate temp = itemState!.model.plates[plateIndex];
                    Plate generatePlate = Plate(
                      temp.id, 
                      temp.identifier, 
                      temp.state, 
                      value ?? "", 
                      temp.expiration, 
                      0, 
                      null
                    );
                    if(isUSAPlate){
                      plates.add(generatePlate);
                      plates.add(itemState!.model.plates[1]);
                    }else{
                      plates.add(itemState!.model.plates[0]);
                      plates.add(generatePlate);
                    }
                    itemState!.updateModelRedrawing(
                      model.clone(
                        plates: plates,
                      ),
                    );
                  },
                )
              ),
              Expanded(
                child: TWSAutoCompleteField<String>(
                  displayValue: (String query) => query,
                  initialValue: (itemState?.model.plates[plateIndex].state ?? "").isEmpty ? defaultState: itemState!.model.plates[plateIndex].state ,
                  optionsBuilder: (String query) {
                    if(query.isNotEmpty) return statesRepository.where((String country) => country.toLowerCase().contains(query)).toList();
                    return statesRepository;
                  } ,
                  label: 'State',
                  onChanged: (String? text) {
                    Truck model = itemState!.model;
                    List<Plate>? plates =  <Plate>[];
                    final Plate temp = itemState!.model.plates[plateIndex];
                    Plate generatePlate = Plate(
                      temp.id, 
                      temp.identifier, 
                      text ?? "", 
                      temp.country, 
                      temp.expiration, 
                      0, 
                      null
                    );
                    if(isUSAPlate){
                      plates.add(generatePlate);
                      plates.add(itemState!.model.plates[1]);
                    }else{
                      plates.add(itemState!.model.plates[0]);
                      plates.add(generatePlate);
                    }
                    itemState!.updateModelRedrawing(
                      model.clone(
                        plates: plates,
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
                    List<Plate>? plates =  <Plate>[];
                    final Plate temp = itemState!.model.plates[plateIndex];
                    Plate generatePlate = Plate(
                      temp.id, 
                      temp.identifier, 
                      temp.state, 
                      temp.country, 
                      DateTime.parse(text), 
                      0, 
                      null
                    );
                    if(isUSAPlate){
                      plates.add(generatePlate);
                      plates.add(itemState!.model.plates[1]);
                    }else{
                      plates.add(itemState!.model.plates[0]);
                      plates.add(generatePlate);
                    }
                    itemState!.updateModelRedrawing(
                      model.clone(
                        plates: plates,
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