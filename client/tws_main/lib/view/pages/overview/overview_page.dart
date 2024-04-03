import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/foundation/components/cosmos_table.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_section.dart';

part 'current_trips_section/current_trips_section.dart';
part 'current_trips_section/current_trips_table.dart';

part 'current_drivers_section/current_drivers_section.dart';
part 'current_drivers_section/drivers_tracking_table.dart';

const double _kLargeViewMinTablesHeight = 450;
const double _maxFloatSectionWidth = 400;
const double _kMinFocusSectionWidth = 450;
const BoxConstraints _floatSectionConstrains = BoxConstraints(
  maxWidth: _maxFloatSectionWidth,
  minWidth: _maxFloatSectionWidth - 100,
);

class OverviewPage extends CosmosPage {
  const OverviewPage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constraints) {
        final double availableWidth = constraints.maxWidth;
        final bool hasPassedThreshold = availableWidth < (_maxFloatSectionWidth + _kMinFocusSectionWidth);
        double? sectionsSharedHeight = hasPassedThreshold ? null : constraints.smallest.height;
        if ((sectionsSharedHeight ?? _kLargeViewMinTablesHeight) < _kLargeViewMinTablesHeight) {
          sectionsSharedHeight = _kLargeViewMinTablesHeight;
        }

        final double floatSectionSupposedWidth = availableWidth * .25;
        final double floatSectionWidth = hasPassedThreshold ? constraints.maxWidth : _floatSectionConstrains.constrainWidth(floatSectionSupposedWidth);
        final double focusSectionWidth = hasPassedThreshold ? constraints.maxWidth : availableWidth - floatSectionWidth;
        final double minFocusSectionWidth = hasPassedThreshold ? constraints.maxWidth : _kMinFocusSectionWidth;

        return Wrap(
          children: <Widget>[
            ConstrainedBox(
              constraints: BoxConstraints(
                minWidth: minFocusSectionWidth,
              ),
              child: SizedBox(
                height: sectionsSharedHeight,
                width: focusSectionWidth,
                child: const _CurrentTripsSection(),
              ),
            ),
            SizedBox(
              height: sectionsSharedHeight,
              width: floatSectionWidth,
              child: const _CurrentDriversSection(),
            ),
          ],
        );
      },
    );
  }
}
