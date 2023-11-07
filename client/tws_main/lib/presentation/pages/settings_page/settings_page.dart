import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/presentation/widgets/tws_theme_toogler.dart';

class SettingsPage extends CosmosPage {
  const SettingsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Padding(
      padding: EdgeInsets.all(32.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: <Widget>[
              TwsThemeToogler(
                tooltip: true,
              ),
            ],
          ),
        ],
      ),
    );
  }
}
