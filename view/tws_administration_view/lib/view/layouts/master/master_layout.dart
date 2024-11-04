import 'dart:ui';
import 'package:csm_view/csm_view.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/layouts/master/master_layout_menu/master_layout_menu_state.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part 'master_layout_header/master_layout_header.dart';
part 'master_layout_header/master_layout_user/content_menu/content_menu.dart';
part 'master_layout_header/master_layout_user/content_menu/menu_option.dart';
part 'master_layout_header/master_layout_user/master_user_button.dart';
part 'master_layout_header/master_layout_user/master_user_button_state.dart';
part 'master_layout_large.dart';
part 'master_layout_menu/master_layout_menu.dart';
part 'master_layout_menu/master_layout_menu_button.dart';
part 'master_layout_menu/master_layout_menu_button_options.dart';
part 'master_layout_small.dart';



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
        route: TWSARoutes.overviewPage,
      ),
      _MasterLayoutMenuButtonOptions(
        label: 'Security',
        icon: Icons.security_outlined,
        route: TWSARoutes.securityPage,
      ),
      _MasterLayoutMenuButtonOptions(
        label: 'Business',
        icon: Icons.emoji_transportation,
        route: TWSARoutes.businessPage,
      ),
      _MasterLayoutMenuButtonOptions(
        label: 'Human Resources',
        icon: Icons.people_alt_outlined,
        route: TWSARoutes.humanResourcesPage,
      ),
      _MasterLayoutMenuButtonOptions(
        label: 'Yardlogs',
        icon: Icons.list,
        route: TWSARoutes.yardlogPage,
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
