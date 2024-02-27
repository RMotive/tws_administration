part of '../landing_page.dart';

class _CurrentTripsSection extends StatelessWidget {
  final double width;

  const _CurrentTripsSection({
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
        child: const TWSSection(
          title: 'Current trips',
          content: Column(),
        ),
      ),
    );
  }
}
