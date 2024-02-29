
import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/foundation/components/cosmos_table.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:tws_main/presentation/components/tws_display_flat.dart';
import 'package:tws_main/presentation/components/tws_section.dart';

part 'current_trips_section/current_trips_section.dart';
part 'current_trips_section/current_trips_table.dart';

part 'current_drivers_section/current_drivers_section.dart';
part 'current_drivers_section/drivers_tracking_table.dart';

const double _maxFloatSectionWidth = 400;
const double _minFocusSectionWidth = 450;
const BoxConstraints _floatSectionConstrains = BoxConstraints(
  maxWidth: _maxFloatSectionWidth,
  minWidth: _maxFloatSectionWidth - 100,
);

class LandingPage extends CosmosPage {
  const LandingPage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {

    return LayoutBuilder(
      builder: (_, BoxConstraints constraints) {
        final double availableWidth = constraints.maxWidth;
        final bool hasPassedThreshold = availableWidth < (_maxFloatSectionWidth + _minFocusSectionWidth);
        final double? sectionsSharedHeight = hasPassedThreshold ? null : constraints.smallest.height;

        final double floatSectionSupposedWidth = availableWidth * .25;
        final double floatSectionWidth = hasPassedThreshold ? double.maxFinite : _floatSectionConstrains.constrainWidth(floatSectionSupposedWidth);
        final double focusSectionWidth = hasPassedThreshold ? double.maxFinite : availableWidth - floatSectionWidth;

        return Wrap(
          children: <Widget>[
            ConstrainedBox(
              constraints: const BoxConstraints(
                minWidth: _minFocusSectionWidth,
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
