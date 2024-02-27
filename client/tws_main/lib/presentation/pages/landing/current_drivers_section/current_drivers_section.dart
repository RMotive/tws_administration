part of '../landing_page.dart';

class _CurrentDriversSection extends StatelessWidget {
  final double width;

  const _CurrentDriversSection({
    required this.width,
  });

  @override
  Widget build(BuildContext context) {
    return ConstrainedBox(
      constraints: const BoxConstraints(
        minWidth: _minSectionsWidht,
      ),
      child: SizedBox(
        width: width,
        child: const Column(
          children: <Widget>[
            TWSSection(
              title: 'Working Drivers',
              content: Column(),
            ),
            TWSSection(
              title: 'Idling Drivers',
              content: Column(),
            )
          ],
        ),
      ),
    );
  }
}
