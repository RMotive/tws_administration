part of '../overview_page.dart';

/// Theme Struct:
///   - required[primaryControlColorStruct]
class _CurrentTripsTable extends StatelessWidget {
  const _CurrentTripsTable();

  @override
  Widget build(BuildContext context) {
    return CSMTable<dynamic>(
      headers: const <String>[
        'Customer',
        'Origin',
        'Destination',
        'Driver',
        'Unit',
      ],
      buildHeaderCell: (String header) {
        return ConstrainedBox(
          constraints: const BoxConstraints(
            minWidth: 150,
          ),
          child: Text(
            header,
          ),
        );
      },
      onEmpty: () {
        return const Padding(
          padding: EdgeInsets.symmetric(
            vertical: 50,
            horizontal: 60,
          ),
          child: TWSDisplayFlat(
            verticalPadding: 18,
            display: 'No records to display',
          ),
        );
      },
    );
  }
}
