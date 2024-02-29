part of '../landing_page.dart';

class _CurrentTripsSection extends StatelessWidget {
  const _CurrentTripsSection();

  @override
  Widget build(BuildContext context) {
    return const TWSSection(
      title: 'Current trips',
      content: _CurrentTripsTable(),
    );
  }
}
