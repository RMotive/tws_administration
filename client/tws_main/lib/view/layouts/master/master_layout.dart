import 'dart:ui';
import 'package:cosmos_foundation/common/common_module.dart';
import 'package:cosmos_foundation/common/tools/csm_responsive.dart';
import 'package:cosmos_foundation/router/router_module.dart';
import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:cosmos_foundation/widgets/csm_responsive_view.dart';
import 'package:cosmos_foundation/widgets/csm_spacing_column.dart';
import 'package:cosmos_foundation/widgets/enums/csm_states.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/router/twsa_k_routes.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/layouts/master/master_layout_menu/master_layout_menu_state.dart';

part 'master_layout_menu/master_layout_menu.dart';
part 'master_layout_menu/master_layout_menu_button.dart';
part 'master_layout_menu/master_layout_menu_button_options.dart';

part 'master_layout_header/master_layout_header.dart';

part 'master_layout_small.dart';
part 'master_layout_large.dart';

const double _minMenuWidth = 175;
const double _maxMenuWidth = 250;

class MasterLayout extends CSMLayoutBase {
  final CSMRouterOutput rOutput;

  const MasterLayout({
    super.key,
    required super.page,
    required this.rOutput,
  });

  @override
  Widget build(BuildContext context) {
    const List<_MasterLayoutMenuButtonOptions> buttons = <_MasterLayoutMenuButtonOptions>[
      _MasterLayoutMenuButtonOptions(
        label: 'Overview',
        icon: Icons.dashboard_outlined,
        route: TWSAKRoutes.overviewPage,
      ),
      _MasterLayoutMenuButtonOptions(
        label: 'Security',
        icon: Icons.security_outlined,
        route: TWSAKRoutes.securityPage,
      )
    ];

    return CSMResponsiveView(
      onLarge: _MasterLayoutLarge(
        buttons: buttons,
        currentRoute: rOutput.absolutePath,
        page: page,
      ),
      onMedium: _MasterLayoutSmall(
        page: page,
        currentRoute: rOutput.absolutePath,
        buttons: buttons,
      ),
      onSmall: _MasterLayoutSmall(
        page: page,
        currentRoute: rOutput.absolutePath,
        buttons: buttons,
      ),
    );
  }
}
