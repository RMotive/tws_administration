part of '../articles_layout.dart';

class _ActionsRibbon extends StatelessWidget {
  const _ActionsRibbon();

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        return DecoratedBox(
          decoration: const BoxDecoration(
            border: Border.fromBorderSide(
              BorderSide(
                width: 1,
                color: Colors.blueGrey,
              ),
            ),
          ),
          child: ConstrainedBox(
            constraints: BoxConstraints(
              minHeight: constrains.maxHeight,
              minWidth: 400,
            ),
            child: const Padding(
              padding: EdgeInsets.symmetric(
                horizontal: 8,
                vertical: 4,
              ),
              child: SpacingRow(
                spacing: 8,
                children: <Widget>[
                  _ActionRibbonGroup(),
                ],
              ),
            ),
          ),
        );
      },
    );
  }
}
