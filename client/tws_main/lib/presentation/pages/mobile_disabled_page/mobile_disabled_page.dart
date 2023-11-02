import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

class MobileDisabledPage extends CosmosPage {
  const MobileDisabledPage({super.key});

  @override
  Widget build(BuildContext context) {
    final ThemeBase theme = getTheme();

    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        // --> Work in progress Icon
        Icon(
          Icons.miscellaneous_services_outlined,
          color: theme.primaryColor.textColor,
          size: 62,
        ),
        // --> Work in progress message
        const Padding(
          padding: EdgeInsets.only(
            top: 16,
          ),
          child: Text(
            'Work in progress for Small Device Screens',
          ),
        ),
      ],
    );
  }
}
