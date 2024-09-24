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
    return const TWSSection(
      isOptional: true,
      padding: EdgeInsets.symmetric(vertical: 10),
      title: "Maintenance*", 
      content: CSMSpacingColumn(
        spacing: 10,
        children: <Widget>[
          CSMSpacingRow(
            spacing: 10,
            children: <Widget>[
             
              
              
            ]
          )
        ]
      )
    );
  }
}