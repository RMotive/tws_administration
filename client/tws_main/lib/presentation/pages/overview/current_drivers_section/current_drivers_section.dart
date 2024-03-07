part of '../overview_page.dart';

class _CurrentDriversSection extends StatelessWidget {
  const _CurrentDriversSection();

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constrains) {
        double? tableSize;
        if (constrains.hasBoundedHeight) {
          tableSize = constrains.maxHeight * .5;
        }

        return Column(
          children: <Widget>[
            SizedBox(
              height: tableSize,
              child: const TWSSection(
                title: 'Working Drivers',
                content: _DriversTrackingTable(
                  samples: <dynamic>[],
                ),
              ),
            ),
            SizedBox(
              height: tableSize,
              child: const TWSSection(
                title: 'Idling Drivers',
                content: _DriversTrackingTable(
                  samples: <dynamic>[],
                ),
              ),
            )
          ],
        );
      },
    );
  }
}
