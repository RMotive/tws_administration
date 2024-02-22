
import 'package:cosmos_foundation/contracts/cosmos_layout.dart';
import 'package:cosmos_foundation/enumerators/cosmos_control_states.dart';
import 'package:cosmos_foundation/enumerators/evaluators/cosmos_control_states_evaluator.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/theme_base.dart';

part 'master_layout_menu.dart';
part 'master_layout_header.dart';
part 'master_layout_menu_button.dart';

class MasterLayout extends CosmosLayout {
  const MasterLayout({
    super.key,
    required super.page,
  });

  @override
  Widget build(BuildContext context) {
    final ThemeBase theme = getTheme<ThemeBase>();

    return ColoredBox(
      color: theme.pageColorStruct.mainColor,
      child: Row(
        children: <Widget>[
          const _MasterLayoutMenu(),
          Expanded(
            child: Column(
              children: <Widget>[
                const _MsaterLayoutHeader(),
                Expanded(child: page),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
