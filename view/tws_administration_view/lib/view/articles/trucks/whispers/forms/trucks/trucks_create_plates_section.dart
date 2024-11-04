part of '../../trucks_create_whisper.dart';


class _TruckCreatePlatesSection extends StatelessWidget {
  final TWSArticleCreatorItemState<Object>? itemState;
  final bool enable;
  final TextStyle style;
  const _TruckCreatePlatesSection({
    required this.style,
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return TWSSection(
      title: "Add Plates",
      padding: const EdgeInsets.symmetric(vertical: 20),
      content: TWSIncrementalList<Plate>(
        title: "Plates",
        modelBuilder: Plate.a,
        recordLimit: 2,
        recordList: (itemState!.model as Truck).plates, 
        onAdd: (Plate plate) {
          Truck model = itemState!.model as Truck;
          model.plates.add(plate);
          itemState!.updateModelRedrawing(model);
        },
        onRemove: () {
          Truck model = itemState!.model as Truck;
          model.plates.removeLast();
          itemState!.updateModelRedrawing(model);
        },
        recordBuilder:(Plate record, int index) {
          return _TruckCreatePlateForm(
            index: index,
            plate: record,
            identifierOnChange:(String text) {
              Truck model = itemState!.model as Truck;
              model.plates[index].identifier = text;
              itemState!.updateModelRedrawing(model);
            },
            countryOnChange:(String? text) {
              Truck model = itemState!.model as Truck;
              model.plates[index].country = text ?? "";
              itemState!.updateModelRedrawing(model);
            }, 
            stateOnChange:(String? text) {
              Truck model = itemState!.model as Truck;
              model.plates[index].state = text;
              itemState!.updateModelRedrawing(model);
            }, 
            expirationOnChange:(String text) {
              Truck model = itemState!.model as Truck;
              model.plates[index].expiration = DateTime.tryParse(text);
              itemState!.updateModelRedrawing(model);
            },
          );
        },
      ),
    );
  }
}