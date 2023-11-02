import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:flutter/material.dart';

/// UI Page for Business Dashboard functionallity.
/// This Page is the first one that is introduced when the user authenticates itself.
///
/// Mostly used to show a quick resume of all the business data highlights.
class BusinessDashboardPage extends CosmosPage {
  const BusinessDashboardPage({super.key});

  @override
  Widget build(BuildContext context) {
    return DecoratedBox(
      decoration: BoxDecoration(
        border: Border.fromBorderSide(
          BorderSide(
            color: Colors.teal.shade900,
            width: 2,
          ),
        ),
      ),
      child: const SizedBox(),
    );
  }
}
