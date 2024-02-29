part of '../landing_page.dart';

class _DriversTrackingTable extends StatelessWidget {
  final List<dynamic> samples;

  const _DriversTrackingTable({
    required this.samples,
  });

  @override
  Widget build(BuildContext context) {
    return CosmosTable<dynamic>(
      samples: samples,
      buildHeaderCell: (String header) {
        return Text(
          header,
        );
      },
      headers: const <String>[
        'Driver',
        'Truck',
      ],
      onEmpty: () {
        return const Padding(
          padding: EdgeInsets.symmetric(
            vertical: 40,
            horizontal: 20,
          ),
          child: TWSDisplayFlat(
            verticalPadding: 10,
            display: 'No records to display',
          ),
        );
      },
    );
  }
}
