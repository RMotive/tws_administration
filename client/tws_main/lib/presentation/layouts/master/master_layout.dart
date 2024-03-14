import 'dart:ui';

import 'package:cosmos_foundation/contracts/cosmos_layout.dart';
import 'package:cosmos_foundation/enumerators/cosmos_control_states.dart';
import "package:cosmos_foundation/enumerators/evaluators/cosmos_control_states_evaluator.dart";
import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/foundation/conditionals/responsive_view.dart';
import 'package:cosmos_foundation/foundation/simplifiers/spacing_column.dart';
import 'package:cosmos_foundation/helpers/responsive.dart';
import 'package:cosmos_foundation/helpers/route_driver.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/options/route_options.dart';
import 'package:cosmos_foundation/models/outputs/route_output.dart';
import 'package:cosmos_foundation/models/structs/clamp_ratio_constraints.dart';
import 'package:cosmos_foundation/models/structs/standard_theme_struct.dart';
import 'package:cosmos_foundation/models/structs/states_control_theme_struct.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/k_routes.dart';
import 'package:tws_main/core/theme/theme_base.dart';
import 'package:tws_main/presentation/layouts/master/master_layout_menu/master_layout_menu_state.dart';

part 'master_layout_menu/master_layout_menu.dart';
part 'master_layout_menu/master_layout_menu_button.dart';
part 'master_layout_menu/master_layout_menu_button_options.dart';

part 'master_layout_header/master_layout_header.dart';

part 'master_layout_small.dart';
part 'master_layout_large.dart';

const double _minMenuWidth = 175;
const double _maxMenuWidth = 250;

class MasterLayout extends CosmosLayout {
  final RouteOutput routeOutput;

  const MasterLayout({
    super.key,
    required super.page,
    required this.routeOutput,
  });

  @override
  Widget build(BuildContext context) {
    const List<_MasterLayoutMenuButtonOptions> buttons = <_MasterLayoutMenuButtonOptions>[
      _MasterLayoutMenuButtonOptions(
        label: 'Overview',
        icon: Icons.dashboard_outlined,
        route: KRoutes.overviewPage,
      ),
      _MasterLayoutMenuButtonOptions(
        label: 'Security',
        icon: Icons.security_outlined,
        route: KRoutes.securityPage,
      )
    ];

    return ResponsiveView(
      onLarge: _MasterLayoutLarge(
        buttons: buttons,
        currentRoute: routeOutput.absolutePath,
        page: page,
      ),
      onMedium: _MasterLayoutSmall(
        page: page,
        currentRoute: routeOutput.absolutePath,
        buttons: buttons,
      ),
      onSmall: _MasterLayoutSmall(
        page: page,
        currentRoute: routeOutput.absolutePath,
        buttons: buttons,
      ),
    );
  }
}
