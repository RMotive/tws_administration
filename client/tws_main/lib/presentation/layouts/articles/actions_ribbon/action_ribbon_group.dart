part of '../articles_layout.dart';

class _ActionRibbonGroup extends StatelessWidget {
  final String? name;

  const _ActionRibbonGroup([
    this.name,
  ]);

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: <Widget>[
        const Expanded(
          child: SpacingRow(
            spacing: 8,
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: <Widget>[],
          ),
        ),
        Visibility(
          visible: name != null,
          child: Text(
            name ?? '',
            style: const TextStyle(
              fontSize: 12,
            ),
          ),
        ),
      ],
    );
  }
}
