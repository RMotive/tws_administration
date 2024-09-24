part of '../trucks_create_whisper.dart';

class _TruckCreateSCT extends StatelessWidget {
  final TWSArticleCreatorItemState<Truck>? itemState;
  final bool enable;
  const _TruckCreateSCT({
    this.itemState,
    this.enable = true,
  });

  @override
  Widget build(BuildContext context) {
    return const TWSSection(
      isOptional: true,
      padding: EdgeInsets.symmetric(vertical: 10),
      title: "SCT*", 
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