import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:tws_main/presentation/components/tws_section.dart';

part './current_trips_section/current_trips_section.dart';

part './current_drivers_section/current_drivers_section.dart';

const double _minSectionsWidht = 400;

class LandingPage extends CosmosPage {
  const LandingPage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return LayoutBuilder(
      builder: (_, BoxConstraints constraints) {
        const double maxSupportef = (_minSectionsWidht * 2);
        final double availableWidth = constraints.maxWidth;
        final bool hasPassedThreshold = availableWidth < maxSupportef;
        final double widthPerSection = hasPassedThreshold ? availableWidth : availableWidth * .5;

        return Wrap(
          children: <Widget>[
            _CurrentTripsSection(
              width: widthPerSection,
            ),
            _CurrentDriversSection(
              width: widthPerSection,
            ),
          ],
        );
      },
    );
  }
}
